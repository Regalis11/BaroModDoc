using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text;
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
public readonly record struct SupportedSubElement(string XMLName, ImmutableArray<DeclaredField> AffectedField);

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
    public readonly List<SupportedSubElement> SupportedSubElements = new();

    public readonly List<XMLAssignedField> XMLAssignedFields = new();

    public readonly List<SerializableProperty> SerializableProperties = new();

    public readonly List<CodeComment> Comments = new();

    public readonly List<string> BaseClasses = new();

    public readonly Dictionary<string, PrefabClassParser> SubClasses = new();

    public readonly Dictionary<string, ImmutableArray<(string Value, string Description)>> Enums = new();

    private readonly List<DeclaredField> declaredFields = new();

    private readonly ClassParsingOptions options;

    public PrefabClassParser(ClassParsingOptions options)
    {
        this.options = options;
    }

    public void ParseClass(ClassDeclarationSyntax cls)
    {
        declaredFields.AddRange(GetDeclaredFields(cls));

        CodeComment comment = cls.FindCommentAttachedToMember();
        ParsedComment parsedComment = ParseComment(comment);
        Comments.Add(comment);

        if (cls.BaseList is { } baseList)
        {
            foreach (BaseTypeSyntax type in baseList.Types)
            {
                string typeName = type.Type.ToString();
                BaseClasses.Add(typeName);
            }
        }

        foreach (ClassDeclarationSyntax syntax in cls.Members.OfType<ClassDeclarationSyntax>())
        {
            PrefabClassParser subParser = new PrefabClassParser(options);
            subParser.ParseClass(syntax);

            string identifier = syntax.Identifier.ValueText;
            SubClasses.Add(identifier, subParser);
        }

        foreach (EnumDeclarationSyntax syntax in cls.Members.OfType<EnumDeclarationSyntax>())
        {
            List<(string, string)> enumMembers = new();
            foreach (var enumMember in syntax.Members)
            {
                enumMembers.Add((enumMember.Identifier.ValueText, enumMember.FindCommentAttachedToMember().Text));
            }

            Enums.Add(syntax.Identifier.ValueText, enumMembers.ToImmutableArray());
        }

        SerializableProperties.AddRange(cls.GetSerializableProperties());

        var initializers = cls.FindInitializerMethodBodies(options.InitializerMethodNames ?? Array.Empty<string>());

        SupportedSubElements.AddRange(initializers.SelectMany(syntax => FindSubElementsFrom(syntax)));

        foreach (BlockSyntax block in initializers)
        {
            XMLAssignedFields.AddRange(FindXMLAssignedFields(block));
        }

        foreach (XMLAssignedField extraField in parsedComment.ExtraFields)
        {
            XMLAssignedFields.Add(extraField);
        }

        foreach (SupportedSubElement extraElement in parsedComment.ExtraSubElements)
        {
            SupportedSubElements.Add(extraElement);
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
                           subType = GetAttribute("Type"),
                           subDesc = GetBody().Trim('\n');
                    extraSubElements.Add(new SupportedSubElement(subIdentifier, ImmutableArray.Create(new DeclaredField(string.Empty, subType, subDesc, null))));
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
                        Left: IdentifierNameSyntax leftIdentifier,
                        Right: InvocationExpressionSyntax right
                    }
                }) { continue; }

            string assignedVariableName = leftIdentifier.GetIdentifierString();

            var (xmlIdentifier, methodOwner, defaultValue, wasFound) = FindXMLAssignmentFromExpression(right);

            if (!wasFound) { continue; }

            if (!string.IsNullOrEmpty(elementName) && !methodOwner.Equals(elementName))
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

                result.Add(new XMLAssignedField(field, xmlIdentifier.GuessCaseFromMemberName(field.Name), defaultValue));
                break;
            }
        }

        return result.ToImmutable();
    }

    private static (string XMLIdentifier, string MethodOwner, string DefaultValue, bool WasFound) FindXMLAssignmentFromExpression(InvocationExpressionSyntax expression)
    {
        if (expression is not
            {
                Expression: MemberAccessExpressionSyntax
                {
                    Expression: var methodOwner,
                    Name: IdentifierNameSyntax rightIdentifier
                },
                ArgumentList.Arguments: var assignmentArgumentList
            }) { return default; }

        string assignmentMethodName = rightIdentifier.GetIdentifierString(),
               methodOwnerName = methodOwner.ToString();

        var argumentList = assignmentArgumentList.Select(static x => x.Expression).ToImmutableArray();

        var (identifier, defaultValue, wasFound) = ParseGetAttributeSyntax(assignmentMethodName, argumentList);

        if (!wasFound)
        {
            if (methodOwnerName is not ("MathHelper" or "Math" or "MathF" or "XMLExtensions"))
            {
                return default;
            }

            foreach (InvocationExpressionSyntax syntax in argumentList.OfType<InvocationExpressionSyntax>())
            {
                var found = FindXMLAssignmentFromExpression(syntax);
                if (found != default) { return found; }
            }
        }

        return (identifier, methodOwner.ToString(), defaultValue, true);
    }

    private static (string Identifier, string DefaultValue, bool WasFound) ParseGetAttributeSyntax(string assignmentMethodName, IReadOnlyList<ExpressionSyntax> args)
    {
        if (!assignmentMethodName.StartsWith("GetAttribute", StringComparison.OrdinalIgnoreCase)) { return default; }

        if (assignmentMethodName.Equals("GetAttributeFloat", StringComparison.OrdinalIgnoreCase) && args.Count >= 3)
        {
            // combine all args after the first one into a string separated by a comma
            var builder = new StringBuilder(ExpressionToString(args[1]));
            for (int i = 2; i < args.Count; i++)
            {
                builder.Append(",").Append(ExpressionToString(args[i]));
            }

            return (builder.ToString(), ParseDefaultValueExpression(args[0]), true);
        }

        if (args.Count is 1 or 0)
        {
            Console.WriteLine("WARNING: odd GetAttribute call");
            return default;
        }

        string xmlIdentifier = ExpressionToString(args[0]),
               defaultValue = ParseDefaultValueExpression(args[1]);

        return (xmlIdentifier, defaultValue, true);

        static string ExpressionToString(ExpressionSyntax syntax) => syntax.ToString().EvaluateAsCSharpExpression();
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

    private static ImmutableArray<CorrelatedField> GetAssignmentsToGlobalVariable(BlockSyntax block, IReadOnlyCollection<DeclaredField> globalVariables, ImmutableArray<DeclaredField> localVariables)
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

        [return: NotNullIfNotNull("def")]
        static string? GetValueOrDefault(ImmutableDictionary<DocAttributeType, string> dict, DocAttributeType key, string? def) => dict.TryGetValue(key, out var value) ? value : def;
    }

    /// <summary>
    /// Finds foreach/switch/if/else statements in the given code block and compiles them into a list of of possible
    /// sub elements that the content type accepts in XML
    /// </summary>
    /// <param name="blockSyntax"></param>
    private ImmutableArray<SupportedSubElement> FindSubElementsFrom(BlockSyntax blockSyntax)
    {
        var correlatedFields = GetAssignmentsToGlobalVariable(blockSyntax, declaredFields, GetLocalVariables(blockSyntax));

        List<SupportedSubElement> elements = new();
        foreach (StatementSyntax statement in blockSyntax.Statements)
        {
            switch (statement)
            {
                case ForEachStatementSyntax { Statement: BlockSyntax foreachBlock }:
                    elements.AddRange(FindSubElementsFromBlock(foreachBlock, correlatedFields));
                    break;
            }
        }

        return elements.ToImmutableArray();
    }

    private IReadOnlyCollection<SupportedSubElement> FindSubElementsFromBlock(BlockSyntax block, IReadOnlyCollection<CorrelatedField> correlatedFields)
    {
        List<SupportedSubElement> elements = new();
        foreach (StatementSyntax syntax in block.Statements)
        {
            switch (syntax)
            {
                case SwitchStatementSyntax switchStatement when IsExpressionRelatedToXML(switchStatement.Expression):
                    elements.AddRange(FindSubElementsFromSwitch(switchStatement, correlatedFields));
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

    private IEnumerable<SupportedSubElement> FindSubElementsFromSwitch(SwitchStatementSyntax switchStatement, IReadOnlyCollection<CorrelatedField> correlatedFields)
    {
        foreach (var switchSection in switchStatement.Sections)
        {
            foreach (var caseLabel in switchSection.Labels.OfType<CaseSwitchLabelSyntax>())
            {
                string xmlName = caseLabel.Value.ToString().EvaluateAsCSharpExpression();
                var fields = ParseStatements(switchSection.Statements, correlatedFields).ToImmutableArray();

                yield return new SupportedSubElement(xmlName, fields);
            }
        }
    }

    private IEnumerable<DeclaredField> ParseStatements(SyntaxList<StatementSyntax> syntaxes, IReadOnlyCollection<CorrelatedField> correlatedFields)
    {
        foreach (StatementSyntax syntax in syntaxes)
        {
            switch (syntax)
            {
                case ExpressionStatementSyntax { Expression: AssignmentExpressionSyntax assignment }:
                    string assignmentName = assignment.Left.ToString();
                    string assignmentType = GetTypeFromAssignment(assignment.Right);

                    yield return FindRelatedFieldDeclaration(assignmentName) with
                    {
                        Type = assignmentType
                    };
                    break;
                case BreakStatementSyntax:
                    break;
                case ExpressionStatementSyntax
                {
                    Expression: InvocationExpressionSyntax
                    {
                        Expression: MemberAccessExpressionSyntax
                        {
                            Name.Identifier.Text: ("Add" or "TryAdd")
                        } memberAccess,
                        ArgumentList.Arguments: var arguments
                    }
                }:
                    string memberAccessName = memberAccess.Expression.ToString();
                    string memberType = GetTypeFromArguments(arguments);
                    yield return FindRelatedFieldDeclaration(memberAccessName) with
                    {
                        Type = memberType
                    };
                    break;
            }

            DeclaredField FindRelatedFieldDeclaration(string name)
            {
                foreach (CorrelatedField field in correlatedFields)
                {
                    if (field.Local != name) { continue; }

                    name = field.Global;
                    break;
                }

                foreach (DeclaredField declaredField in declaredFields)
                {
                    if (declaredField.Name != name) { continue; }

                    return declaredField;
                }

                throw new InvalidOperationException($"Could not find a field declaration for {name}");
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
                _ => string.Empty
            };

        static string GetTypeFromArguments(IReadOnlyCollection<ArgumentSyntax> arguments)
        {
            foreach (ExpressionSyntax syntax in arguments.Select(static a => a.Expression))
            {
                return GetTypeFromAssignment(syntax);
            }

            return string.Empty;
        }
    }
}