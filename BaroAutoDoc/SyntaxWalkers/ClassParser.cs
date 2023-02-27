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

public enum DocAttributeTarget
{
    Field,
    SubElement
}

// TODO what other info do we want to extract? possible errors for example by parsing AddWaring/ThrowError?
public readonly record struct SupportedSubElement(string XMLName, ImmutableArray<DeclaredField> AffectedField);

public readonly record struct DeclaredField(string Name, string Type, string Description, string? OverriddenDefaultValue);

public readonly record struct CorrelatedField(string Global, string Local);

public readonly record struct XMLAssignedField(DeclaredField Field, string XMLIdentifier, string DefaultValue)
{
    public string GetDefaultValue() => Field.OverriddenDefaultValue ?? DefaultValue;
}

public readonly record struct ExtraDeclarations(ImmutableArray<XMLAssignedField> ExtraFields,
                                                ImmutableArray<SupportedSubElement> ExtraSubElements,
                                                ImmutableArray<ExtraType> ExtraTypes)
{
    public static readonly ExtraDeclarations Empty = new(ImmutableArray<XMLAssignedField>.Empty,
        ImmutableArray<SupportedSubElement>.Empty,
        ImmutableArray<ExtraType>.Empty);
}

public readonly record struct ParsedComment(ImmutableDictionary<DocAttributeType, string> Overrides,
                                            ImmutableDictionary<DocAttributeTarget, ImmutableArray<string>> Ignores,
                                            ExtraDeclarations ExtraDeclarations);

public record struct ClassParsingOptions(string[]? InitializerMethodNames);

public class ParsedType
{
    public readonly List<SupportedSubElement> SupportedSubElements = new();

    public readonly List<XMLAssignedField> XMLAssignedFields = new();

    public readonly List<SerializableProperty> SerializableProperties = new();

    public readonly List<CodeComment> Comments = new();

    public readonly List<string> BaseClasses = new();

    public readonly Dictionary<string, ParsedType> SubClasses = new();

    public readonly Dictionary<string, ImmutableArray<(string Value, string Description)>> Enums = new();

    public readonly string Name;

    protected readonly List<DeclaredField> declaredFields = new();

    protected readonly ClassParsingOptions options;

    protected ParsedType(ClassParsingOptions options, string name)
    {
        Name = name;
        this.options = options;
    }

    public virtual void ParseType(TypeDeclarationSyntax type) { }

    public static ParsedType CreateParser(TypeDeclarationSyntax type, ClassParsingOptions options)
    {
        string name = type.Identifier.ValueText;
        return type switch
        {
            RecordDeclarationSyntax => new RecordParser(options, name),
            _ => new ClassParser(options, name)
        };
    }
}

public sealed class ExtraType : ParsedType
{
    public ExtraType(string identifier, CodeComment description) : base(new ClassParsingOptions(Array.Empty<string>()), identifier)
    {
        Comments.Add(description);
    }
}

internal sealed class RecordParser : ParsedType
{
    public RecordParser(ClassParsingOptions options, string name) : base(options, name) { }

    public override void ParseType(TypeDeclarationSyntax type)
    {
        RecordDeclarationSyntax record = (RecordDeclarationSyntax)type;

        CodeComment comment = type.FindCommentAttachedToMember();
        Comments.Add(comment);

        var localFields = GetDeclaredFields(record, comment.Element).ToImmutableArray();
        declaredFields.AddRange(localFields);

        var initializers = type.Members.OfType<ConstructorDeclarationSyntax>();

        foreach (var initializer in initializers)
        {
            if (initializer.Initializer?.ArgumentList.Arguments is not { } baseArgs) { continue; }

            for (int i = 0; i < baseArgs.Count; i++)
            {
                ArgumentSyntax arg = baseArgs[i];
                DeclaredField field = localFields[i];

                if (arg.Expression is not InvocationExpressionSyntax invocation) { continue; }

                // TODO validate methodOwner
                var (identifier, methodOwner, defaultValue, wasFound) = ClassParser.FindXMLAssignmentFromExpression(invocation);

                if (!wasFound) { continue; }

                XMLAssignedFields.Add(new XMLAssignedField(field, identifier, defaultValue));
            }
        }
    }

    private static IEnumerable<DeclaredField> GetDeclaredFields(RecordDeclarationSyntax record, XElement comment)
    {
        foreach (DeclaredField declaredField in ClassParser.GetDeclaredFields(record))
        {
            yield return declaredField;
        }

        if (record.ParameterList is not { } parameterList) { yield break; }

        foreach (var param in parameterList.Parameters)
        {
            string name = param.Identifier.ValueText;
            string type = param.Type!.ToString(); // why can type be null?
            string description = string.Empty;

            foreach (XElement element in comment.Elements())
            {
                if (!element.Name.ToString().Equals("param", StringComparison.OrdinalIgnoreCase)) { continue; }

                if (element.Attribute("name")?.Value != name) { continue; }

                description = element.Value.Trim('\n');
                break;
            }

            yield return new DeclaredField(name, type, description, null);
        }
    }
}

internal sealed class ClassParser : ParsedType
{
    public ClassParser(ClassParsingOptions options, string name) : base(options, name) { }

    public override void ParseType(TypeDeclarationSyntax cls)
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

        foreach (TypeDeclarationSyntax syntax in cls.Members.OfType<TypeDeclarationSyntax>())
        {
            ParsedType subParser = CreateParser(syntax, options);
            subParser.ParseType(syntax);

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

        foreach (var subElement in initializers.SelectMany(syntax => FindSubElementsFrom(syntax)))
        {
            if (parsedComment.Ignores[DocAttributeTarget.SubElement].Any(s => s.Equals(subElement.XMLName, StringComparison.OrdinalIgnoreCase))) { continue; }

            SupportedSubElements.Add(subElement);
        }


        foreach (BlockSyntax block in initializers)
        {
            foreach (var field in FindXMLAssignedFields(block))
            {
                if (parsedComment.Ignores[DocAttributeTarget.Field].Any(s => s.Equals(field.XMLIdentifier, StringComparison.OrdinalIgnoreCase))) { continue; }

                XMLAssignedFields.Add(field);
            }
        }

        foreach (XMLAssignedField extraField in parsedComment.ExtraDeclarations.ExtraFields)
        {
            XMLAssignedFields.Add(extraField);
        }

        foreach (SupportedSubElement extraElement in parsedComment.ExtraDeclarations.ExtraSubElements)
        {
            SupportedSubElements.Add(extraElement);
        }

        foreach (ExtraType extraType in parsedComment.ExtraDeclarations.ExtraTypes)
        {
            SubClasses.Add(extraType.Name, extraType);
        }
    }

    public static ParsedComment ParseComment(CodeComment comment)
    {
        XElement xml = comment.Element;

        XElement? docs = xml.ElementOfName("doc", StringComparison.OrdinalIgnoreCase);

        var ignores = new Dictionary<DocAttributeTarget, List<string>>
        {
            [DocAttributeTarget.SubElement] = new List<string>(),
            [DocAttributeTarget.Field] = new List<string>()
        };

        if (docs is null)
        {
            return new ParsedComment(ImmutableDictionary<DocAttributeType, string>.Empty,
                ToImmutableDictionary(ignores),
                ExtraDeclarations.Empty);
        }

        static ExtraDeclarations ParseExtraDeclarations(XElement element)
        {
            var extraFields = ImmutableArray.CreateBuilder<XMLAssignedField>();
            var extraSubElements = ImmutableArray.CreateBuilder<SupportedSubElement>();
            var extraTypes = ImmutableArray.CreateBuilder<ExtraType>();

            foreach (XElement subElement in element.Elements())
            {
                switch (subElement.Name.ToString().ToLower())
                {
                    case "field":
                        string fieldType = GetAttribute(subElement, "Type"),
                               fieldIdentifier = GetAttribute(subElement, "Identifier"),
                               fieldDefault = GetAttribute(subElement, "DefaultValue"),
                               fieldDesc = GetBody(subElement);

                        DeclaredField declaredField = new DeclaredField(string.Empty, fieldType, fieldDesc, null);
                        extraFields.Add(new XMLAssignedField(declaredField, fieldIdentifier, fieldDefault));
                        break;
                    case "subelement":
                        string subIdentifier = GetAttribute(subElement, "Identifier"),
                               subType = GetAttribute(subElement, "Type"),
                               subDesc = GetBody(subElement);
                        extraSubElements.Add(new SupportedSubElement(subIdentifier, ImmutableArray.Create(new DeclaredField(string.Empty, subType, subDesc, null))));
                        break;
                    case "type":
                        ExtraDeclarations extraDeclarations = ParseExtraDeclarations(subElement);

                        CodeComment comment = CodeComment.Empty(string.Empty);
                        XElement? summary = subElement.ElementOfName("summary", StringComparison.OrdinalIgnoreCase);

                        if (summary is not null)
                        {
                            comment = new CodeComment(GetBody(summary), summary);
                        }


                        ExtraType extraType = new ExtraType(GetAttribute(subElement, "identifier"), comment);
                        extraType.XMLAssignedFields.AddRange(extraDeclarations.ExtraFields);
                        extraType.SupportedSubElements.AddRange(extraDeclarations.ExtraSubElements);
                        extraTypes.Add(extraType);
                        break;
                }
            }

            return new ExtraDeclarations(extraFields.ToImmutable(), extraSubElements.ToImmutable(), extraTypes.ToImmutable());
        }

        var overrides = ImmutableDictionary.CreateBuilder<DocAttributeType, string>();

        foreach (XElement element in docs.Elements())
        {
            switch (element.Name.ToString().ToLower())
            {
                case "override":
                    overrides[Enum.Parse<DocAttributeType>(GetAttribute(element, "type"))] = GetBody(element);
                    break;
                case "ignore":
                    ignores[Enum.Parse<DocAttributeTarget>(GetAttribute(element, "type"))].Add(GetAttribute(element, "identifier"));
                    break;
            }
        }

        return new ParsedComment(overrides.ToImmutable(), ToImmutableDictionary(ignores), ParseExtraDeclarations(docs));

        static string GetAttribute(XElement element, string name) => element.Attributes().FirstOrDefault(x => string.Equals(x.Name.ToString(), name, StringComparison.OrdinalIgnoreCase))?.Value ?? throw new Exception("Missing attribute");
        static string GetBody(XElement element) => element.Value.Trim('\n') ?? throw new Exception("Missing body");
        static ImmutableDictionary<DocAttributeTarget, ImmutableArray<string>> ToImmutableDictionary(Dictionary<DocAttributeTarget, List<string>> dictionary) => dictionary.ToImmutableDictionary(static x => x.Key, static x => x.Value.ToImmutableArray());
    }

    private ImmutableArray<XMLAssignedField> FindXMLAssignedFields(BlockSyntax blockSyntax)
    {
        var correlatedFields = GetAssignmentsToGlobalVariable(blockSyntax, declaredFields, GetLocalVariables(blockSyntax));

        string elementName = "";
        if (blockSyntax.Parent is BaseMethodDeclarationSyntax methodSyntax)
        {
            var elementParameter =
                methodSyntax.ParameterList.Parameters
                            .FirstOrDefault(static p => (p.Type?.ToString() ?? "").Contains("XElement"));
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

    public static (string XMLIdentifier, string MethodOwner, string DefaultValue, bool WasFound) FindXMLAssignmentFromExpression(InvocationExpressionSyntax expression)
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
                builder.Append(',').Append(ExpressionToString(args[i]));
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

    public static ImmutableArray<DeclaredField> GetDeclaredFields(TypeDeclarationSyntax cls)
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

                    if (assignmentName.Contains("."))
                    {
                        Console.WriteLine($"WARNING: Could not parse the assignment {assignment}. Assignment to the field of some other object than the one we're parsing?");
                        continue;
                    }

                    var relatedField = FindRelatedFieldDeclaration(assignmentName);
                    if (string.IsNullOrWhiteSpace(assignmentType)) { assignmentType = relatedField.Type.ExtractContainedTypeFromContainerType(); }
                    yield return relatedField with
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
                    var relatedField2 = FindRelatedFieldDeclaration(memberAccessName);
                    if (string.IsNullOrWhiteSpace(memberType)) { memberType = relatedField2.Type.ExtractContainedTypeFromContainerType(); }
                    yield return relatedField2 with
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