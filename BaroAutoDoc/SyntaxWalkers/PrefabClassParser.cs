using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.SyntaxWalkers;

// FIXME allow this to override sub element types
public enum DocAttributeType
{
    DefaultValue,
    Description,
    Type
}

// TODO what other info do we want to extract? possible errors for example by parsing AddWaring/ThrowError?
public readonly record struct SupportedSubElement(string XMLName, ImmutableArray<SubElementField> AffectedField);

public readonly record struct SubElementField(string Name, string Type);

public readonly record struct DeclaredField(string Name, string Type, string Description, string? OverriddenDefaultValue);

public readonly record struct CorrelatedField(string Global, string Local);

public readonly record struct XMLAssignedField(DeclaredField Field, string XMLIdentifier, string DefaultValue)
{
    public string GetDefaultValue() => Field.OverriddenDefaultValue ?? DefaultValue;
}

public readonly record struct ParsedComment(ImmutableDictionary<DocAttributeType, string> Overrides,
                                            ImmutableArray<XMLAssignedField> ExtraFields,
                                            ImmutableArray<SupportedSubElement> ExtraSubElements);

public record struct ClassParsingOptions(string[] InitializerMethodNames);

internal sealed class PrefabClassParser
{
    public ImmutableArray<SupportedSubElement> SupportedSubElements = ImmutableArray<SupportedSubElement>.Empty;

    public ImmutableArray<XMLAssignedField> XMLAssignedFields = ImmutableArray<XMLAssignedField>.Empty;

    public ImmutableArray<SerializableProperty> SerializableProperties = ImmutableArray<SerializableProperty>.Empty;

    private readonly ClassParsingOptions options;
    private ImmutableArray<DeclaredField> declaredFields = ImmutableArray<DeclaredField>.Empty;

    public ImmutableArray<CodeComment> Comments = ImmutableArray<CodeComment>.Empty;

    public PrefabClassParser(ClassParsingOptions options)
    {
        this.options = options;
    }

    public void ParseClass(ClassDeclarationSyntax cls)
    {
        declaredFields = declaredFields.Union(GetDeclaredFields(cls)).ToImmutableArray();

        CodeComment comment = cls.FindCommentAttachedToMember();

        Comments = Comments.Add(comment);

        SerializableProperties = SerializableProperties.Union(cls.GetSerializableProperties()).ToImmutableArray();

        var initializers = cls.FindInitializerMethodBodies(options.InitializerMethodNames);

        // TODO figure out a way to concat this
        SupportedSubElements = initializers
                               .SelectMany(static syntax => FindSubElementsFrom(syntax))
                               .ToImmutableArray();

        foreach (BlockSyntax block in initializers)
        {
            XMLAssignedFields = XMLAssignedFields.Union(FindXMLAssignedFields(block)).ToImmutableArray();
        }

        var parsedComment = ParseComment(comment);

        foreach (XMLAssignedField extraField in parsedComment.ExtraFields)
        {
            XMLAssignedFields = XMLAssignedFields.Add(extraField);
        }

        foreach (SupportedSubElement extraElement in parsedComment.ExtraSubElements)
        {
            SupportedSubElements = SupportedSubElements.Add(extraElement);
        }
    }

    private static ParsedComment ParseComment(CodeComment comment)
    {
        XElement xml = comment.Element;

        XElement? docs = xml.ElementOfName("doc", StringComparison.OrdinalIgnoreCase);
        if (docs is null)
        {
            return new ParsedComment(ImmutableDictionary<DocAttributeType, string>.Empty,
                ImmutableArray<XMLAssignedField>.Empty,
                ImmutableArray<SupportedSubElement>.Empty);
        }

        var overrides = ImmutableDictionary.CreateBuilder<DocAttributeType, string>();
        var extraFields = ImmutableArray.CreateBuilder<XMLAssignedField>();
        var extraSubElements = ImmutableArray.CreateBuilder<SupportedSubElement>();

        foreach (XElement element in docs.Elements())
        {
            switch (element.Name.ToString().ToLower())
            {
                case "override":
                    overrides[Enum.Parse<DocAttributeType>(GetAttribute("Type"))] = element.Value.Trim('\n');
                    break;
                case "field":
                    string fieldType = GetAttribute("Type"),
                           fieldIdentifier = GetAttribute("Identifier"),
                           fieldDefault = GetAttribute("DefaultValue"),
                           fieldDesc = GetBody().Trim('\n');

                    DeclaredField declaredField = new DeclaredField(string.Empty, fieldType, fieldDesc, null);
                    extraFields.Add(new XMLAssignedField(declaredField, fieldIdentifier, fieldDefault));
                    break;
                case "subelement":
                    string subIdentifier = GetAttribute("Identifier"),
                           subType = GetAttribute("Type");
                    extraSubElements.Add(new SupportedSubElement(subIdentifier, ImmutableArray.Create(new SubElementField(string.Empty, subType))));
                    break;
            }

            string GetAttribute(string name) => element.Attributes().FirstOrDefault(x => string.Equals(x.Name.ToString(), name, StringComparison.OrdinalIgnoreCase))?.Value ?? throw new Exception("Missing attribute");
            string GetBody() => element.Value ?? throw new Exception("Missing body");
        }

        return new ParsedComment(overrides.ToImmutable(), extraFields.ToImmutable(), extraSubElements.ToImmutable());
    }

    private ImmutableArray<XMLAssignedField> FindXMLAssignedFields(BlockSyntax blockSyntax)
    {
        var correlatedFields = GetAssignmentsToGlobalVariable(blockSyntax, declaredFields, GetLocalVariables(blockSyntax));

        string elementName = "";
        if (blockSyntax.Parent is BaseMethodDeclarationSyntax methodSyntax)
        {
            var elementParameter =
                methodSyntax.ParameterList.Parameters
                            .FirstOrDefault(p => (p.Type?.ToString() ?? "").Contains("XElement"));
            elementName = elementParameter?.Identifier.ValueText ?? "";
        }

        var result = ImmutableArray.CreateBuilder<XMLAssignedField>();

        result.AddRange(FindXMLFieldsInStatement(elementName, blockSyntax, correlatedFields));

        foreach (StatementSyntax syntax in blockSyntax.Statements)
        {
            // TODO we probably want to extract the condition here?
            if (syntax is not IfStatementSyntax { Statement: BlockSyntax ifBlock }) { continue; }

            result.AddRange(FindXMLFieldsInStatement(elementName, ifBlock, correlatedFields));
        }

        return result.ToImmutable();
    }

    private ImmutableArray<XMLAssignedField> FindXMLFieldsInStatement(string elementName, BlockSyntax blockSyntax, IReadOnlyCollection<CorrelatedField> correlatedFields)
    {
        var result = ImmutableArray.CreateBuilder<XMLAssignedField>();

        foreach (StatementSyntax statement in blockSyntax.Statements)
        {
            // good lord in heavens above
            if (statement is not ExpressionStatementSyntax
                {
                    Expression: AssignmentExpressionSyntax
                    {
                        Right: InvocationExpressionSyntax
                        {
                            Expression: MemberAccessExpressionSyntax
                            {
                                Expression: var methodOwner,
                                Name: IdentifierNameSyntax rightIdentifier
                            },
                            ArgumentList.Arguments: var assignmentArgumentList
                        },
                        Left: IdentifierNameSyntax leftIdentifier
                    }
                }) { continue; }

            string assignmentMethodName = rightIdentifier.GetIdentifierString(),
                   assignedVariableName = leftIdentifier.GetIdentifierString();

            // probably a better way to do this
            if (!assignmentMethodName.StartsWith("GetAttribute", StringComparison.OrdinalIgnoreCase)) { continue; }

            if (!string.IsNullOrEmpty(elementName) && !methodOwner.ToString().Equals(elementName))
            {
                continue;
            }

            foreach (DeclaredField declaredField in declaredFields)
            {
                DeclaredField field = declaredField;

                foreach (CorrelatedField correctedField in correlatedFields)
                {
                    if (correctedField.Local != declaredField.Name) { continue; }

                    field = field with { Name = correctedField.Global };
                }

                if (field.Name != assignedVariableName) { continue; }

                string xmlIdentifier = assignmentArgumentList[0].ToString().EvaluateAsCSharpExpression();

                xmlIdentifier = xmlIdentifier.GuessCaseFromMemberName(field.Name);

                result.Add(new XMLAssignedField(field, xmlIdentifier, ParseDefaultValueExpression(assignmentArgumentList[1].Expression)));
                break;
            }
        }

        return result.ToImmutable();
    }

    private static string ParseDefaultValueExpression(ExpressionSyntax expressionSyntax)
    {
        const string defaultValue = "See description";

        // FIXME link code to xml identifiers, as in "Same as X" needs to point to the XML identifier that assigns field X
        switch (expressionSyntax)
        {
            // true, 1.0f, "", null
            case LiteralExpressionSyntax literalExpression:
            {
                return literalExpression.Token.Value switch
                {
                    // TODO most of the case the default value being null isn't actually the case, how do we handle that?
                    null => "-",
                    string literalValue => $"\"{literalValue}\"",
                    _ => literalExpression.Token.ValueText
                };
            }
            // -1
            case PrefixUnaryExpressionSyntax prefixUnaryExpression:
            {
                return prefixUnaryExpression.ToString();
            }
            // MaxStrength
            case IdentifierNameSyntax identifierName:
            {
                return $"Same as {identifierName.GetIdentifierString()}";
            }
            // Value * 0.Xf
            case BinaryExpressionSyntax
            {
                OperatorToken.ValueText: "*"
            } binaryExpression:
            {
                var value = binaryExpression.OfType<LiteralExpressionSyntax>();
                var identifier = binaryExpression.OfType<IdentifierNameSyntax>();

                if (value?.Token.Value is not float floatValue) { return defaultValue; } // TODO error handling

                float percentage = floatValue * 100;

                return $"{percentage}% of {identifier?.Identifier.ValueText}";
            }
            // X && Y
            case BinaryExpressionSyntax
            {
                OperatorToken.ValueText: "&&"
            }:
            {
                // TODO I genuinely don't think it's worth parsing this and instead just manually writing description for it
                // This is what you'd have to parse: !IsBuff && AfflictionType != "geneticmaterialbuff" && AfflictionType != "geneticmaterialdebuff"
                return defaultValue;
            }
            // Identifier.Empty, String.Empty
            case MemberAccessExpressionSyntax memberAccess:
            {
                var accesses = FindMemberAccessses(memberAccess).ToImmutableArray();

                if (accesses.Length is 2 && accesses[0] is "Empty" && accesses[1] is ("String" or "Identifier"))
                {
                    return "\"\"";
                }

                // enums
                return accesses[0];
            }
            // Math.Max(0, Value)
            case InvocationExpressionSyntax
            {
                Expression: MemberAccessExpressionSyntax
                {
                    Name: IdentifierNameSyntax { Identifier.ValueText: "Max" },
                    Expression: IdentifierNameSyntax { Identifier.ValueText: "Math" or "MathF" }
                },
                ArgumentList.Arguments: { Count: 2 } argumentList
            }:
            {
                static string ParseArgument(ArgumentSyntax argument) =>
                    argument.Expression switch
                    {
                        LiteralExpressionSyntax literalExpression => literalExpression.Token.ValueText,
                        IdentifierNameSyntax identifierName => identifierName.GetIdentifierString(),
                        _ => string.Empty // unparsable
                    };

                string firstArgument = ParseArgument(argumentList[0]),
                       secondArgument = ParseArgument(argumentList[1]);

                if (string.IsNullOrWhiteSpace(firstArgument) || string.IsNullOrWhiteSpace(secondArgument))
                {
                    return defaultValue;
                }

                // TODO how do we communicate this?
                return $"max( {firstArgument} , {secondArgument} )";
            }
            // Array.Empty<T>()
            case InvocationExpressionSyntax
            {
                Expression: MemberAccessExpressionSyntax
                {
                    Name: GenericNameSyntax { Identifier.ValueText: "Empty" },
                    Expression: IdentifierNameSyntax { Identifier.ValueText: "Array" or "ImmutableArray" or "ImmutableHashSet" }
                }
            }:
            {
                return "[]";
            }
        }

        return defaultValue;
    }

    private static ImmutableArray<DeclaredField> GetLocalVariables(BlockSyntax block)
    {
        var result = ImmutableArray.CreateBuilder<DeclaredField>();

        foreach (var localDeclaration in block.Statements.OfType<LocalDeclarationStatementSyntax>())
        {
            foreach (VariableDeclaratorSyntax variableName in localDeclaration.Declaration.Variables)
            {
                result.Add(new DeclaredField(
                    Name: variableName.Identifier.ToString(),
                    Type: localDeclaration.Declaration.Type.ToString(),
                    Description: "",
                    OverriddenDefaultValue: null));
            }
        }

        return result.ToImmutable();
    }

    private static ImmutableArray<CorrelatedField> GetAssignmentsToGlobalVariable(BlockSyntax block, ImmutableArray<DeclaredField> globalVariables, ImmutableArray<DeclaredField> localVariables)
    {
        var result = ImmutableArray.CreateBuilder<CorrelatedField>();

        foreach (var assignment in block.Statements.OfType<ExpressionStatementSyntax>())
        {
            if (assignment.Expression is not AssignmentExpressionSyntax
                {
                    Left: IdentifierNameSyntax leftIdentifier,
                    Right: var right
                }) { continue; }

            if (globalVariables.All(field => field.Name != leftIdentifier.GetIdentifierString())) { continue; }

            switch (right)
            {
                case InvocationExpressionSyntax
                {
                    Expression: MemberAccessExpressionSyntax { Expression: IdentifierNameSyntax rightIdentifier }
                } when localVariables.Any(field => field.Name == rightIdentifier.GetIdentifierString()):
                {
                    result.Add(new CorrelatedField(leftIdentifier.GetIdentifierString(), rightIdentifier.GetIdentifierString()));
                    break;
                }
            }
        }

        return result.ToImmutable();
    }

    private static ImmutableArray<DeclaredField> GetDeclaredFields(ClassDeclarationSyntax cls)
    {
        var result = ImmutableArray.CreateBuilder<DeclaredField>();

        foreach (var field in cls.Members.OfType<FieldDeclarationSyntax>())
        {
            foreach (var variable in field.Declaration.Variables)
            {
                var comment = field.FindCommentAttachedToMember();

                var overrides = ParseComment(comment).Overrides;

                result.Add(new DeclaredField(
                    Name: variable.Identifier.ToString(),
                    Type: GetValueOrDefault(overrides, DocAttributeType.Type, field.Declaration.Type.ToString()),
                    Description: GetValueOrDefault(overrides, DocAttributeType.Description, comment.Text),
                    OverriddenDefaultValue: GetValueOrDefault(overrides, DocAttributeType.DefaultValue, null)));
            }
        }

        foreach (var property in cls.Members.OfType<PropertyDeclarationSyntax>())
        {
            var comment = property.FindCommentAttachedToMember();
            var overrides = ParseComment(comment).Overrides;

            result.Add(new DeclaredField(
                Name: property.Identifier.ToString(),
                Type: GetValueOrDefault(overrides, DocAttributeType.Type, property.Type.ToString()),
                Description: GetValueOrDefault(overrides, DocAttributeType.Description, comment.Text),
                OverriddenDefaultValue: GetValueOrDefault(overrides, DocAttributeType.DefaultValue, null)));
        }

        return result.ToImmutable();

        [return:NotNullIfNotNull("def")]
        static string? GetValueOrDefault(ImmutableDictionary<DocAttributeType, string> dict, DocAttributeType key, string? def) => dict.TryGetValue(key, out var value) ? value : def;
    }

    /// <summary>
    /// Finds foreach/switch/if/else statements in the given code block and compiles them into a list of of possible
    /// sub elements that the content type accepts in XML
    /// </summary>
    /// <param name="blockSyntax"></param>
    private static ImmutableArray<SupportedSubElement> FindSubElementsFrom(BlockSyntax blockSyntax)
    {
        List<SupportedSubElement> elements = new();
        foreach (StatementSyntax statement in blockSyntax.Statements)
        {
            switch (statement)
            {
                case ForEachStatementSyntax { Statement: BlockSyntax foreachBlock }:
                    elements.AddRange(FindSubElementsFromBlock(foreachBlock));
                    break;
            }
        }

        return elements.ToImmutableArray();
    }

    private static IReadOnlyCollection<SupportedSubElement> FindSubElementsFromBlock(BlockSyntax block)
    {
        List<SupportedSubElement> elements = new();
        foreach (StatementSyntax syntax in block.Statements)
        {
            switch (syntax)
            {
                case SwitchStatementSyntax switchStatement when IsExpressionRelatedToXML(switchStatement.Expression):
                    elements.AddRange(FindSubElementsFromSwitch(switchStatement));
                    break;
                case IfStatementSyntax:
                    // FIXME implement if statements
                    break;
            }
        }

        return elements;
    }

    private static bool IsExpressionRelatedToXML(ExpressionSyntax expression)
    {
        ImmutableArray<string> calls = FindMemberAccessses(expression).ToImmutableArray();

        // TODO do we care about extracting this?
        bool isCaseSensitive = !calls.Any(static call => call.Equals("ToLower", StringComparison.OrdinalIgnoreCase) ||
                                                         call.Equals("ToLowerInvariant", StringComparison.OrdinalIgnoreCase));

        // TODO is hardcoding this the best way to do this?
        return calls.Last().Equals("subElement", StringComparison.OrdinalIgnoreCase);
    }

    private static IEnumerable<string> FindMemberAccessses(ExpressionSyntax expression)
    {
        // this is a mess
        switch (expression)
        {
            case InvocationExpressionSyntax
            {
                Expression: var subExpression,
            }:
            {
                foreach (string memberAccesss in FindMemberAccessses(subExpression))
                {
                    yield return memberAccesss;
                }

                yield break;
            }
            case IdentifierNameSyntax identifierNameSyntax:
            {
                yield return identifierNameSyntax.GetIdentifierString();
                yield break;
            }
            case MemberAccessExpressionSyntax
            {
                Name: var name,
                Expression: var nestedExpression
            }:
            {
                yield return name.GetIdentifierString();

                foreach (string memberAccesss in FindMemberAccessses(nestedExpression))
                {
                    yield return memberAccesss;
                }

                break;
            }
        }
    }

    private static IEnumerable<SupportedSubElement> FindSubElementsFromSwitch(SwitchStatementSyntax switchStatement) =>
        from switchSection in switchStatement.Sections
        from caseLabel in switchSection.Labels.OfType<CaseSwitchLabelSyntax>()
        select new SupportedSubElement(caseLabel.Value.ToString().EvaluateAsCSharpExpression(), ParseStatements(switchSection.Statements).ToImmutableArray());

    private static IEnumerable<SubElementField> ParseStatements(SyntaxList<StatementSyntax> syntaxes)
    {
        foreach (StatementSyntax syntax in syntaxes)
        {
            switch (syntax)
            {
                case ExpressionStatementSyntax { Expression: AssignmentExpressionSyntax assignment }:
                    yield return new SubElementField(assignment.Left.ToString(), GetTypeFromAssignment(assignment.Right));
                    break;
                case BreakStatementSyntax:
                    break;
                case ExpressionStatementSyntax
                {
                    Expression: InvocationExpressionSyntax
                    {
                        Expression: MemberAccessExpressionSyntax
                        {
                            Name.Identifier.Text: "Add" or "TryAdd"
                        } memberAccess,
                        ArgumentList.Arguments: var arguments
                    }
                }:
                    yield return new SubElementField(memberAccess.Expression.ToString(), GetTypeFromArguments(arguments));
                    break;
            }
        }

        static string GetTypeFromAssignment(ExpressionSyntax syntax) =>
            syntax switch
            {
                ObjectCreationExpressionSyntax { Type: var type } => type.ToString(),
                InvocationExpressionSyntax
                {
                    Expression: MemberAccessExpressionSyntax
                    {
                        Name.Identifier.Text: "Load",
                        Expression: IdentifierNameSyntax nameSyntax
                    }
                } => nameSyntax.GetIdentifierString(),
                _ => "???"
            };

        static string GetTypeFromArguments(IReadOnlyCollection<ArgumentSyntax> arguments)
        {
            foreach (ExpressionSyntax syntax in arguments.Select(static a => a.Expression))
            {
                return GetTypeFromAssignment(syntax);
            }

            return "???";
        }
    }
}