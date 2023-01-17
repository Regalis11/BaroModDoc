using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.SyntaxWalkers;

// TODO what other info do we want to extract? possible errors for example by parsing AddWaring/ThrowError?
public readonly record struct SupportedSubElement(string XMLName, ImmutableArray<string> AffectedField);

public readonly record struct DeclaredField(string Name, string Type, string Description);

public readonly record struct CorrelatedField(string Global, string Local);

public readonly record struct XMLAssignedField(DeclaredField Field, string XMLIdentifier, string DefaultValue);

public record struct ClassParsingOptions(string[] InitializerMethodNames);

internal sealed class PrefabClassParser
{
    public ImmutableArray<SupportedSubElement> SupportedSubElements = ImmutableArray<SupportedSubElement>.Empty;

    public ImmutableArray<XMLAssignedField> XMLAssignedFields = ImmutableArray<XMLAssignedField>.Empty;

    private readonly ClassParsingOptions options;
    private ImmutableArray<DeclaredField> declaredFields = ImmutableArray<DeclaredField>.Empty;

    public PrefabClassParser(ClassParsingOptions options)
    {
        this.options = options;
    }

    public void ParseClass(ClassDeclarationSyntax cls)
    {
        declaredFields = declaredFields.Union(GetDeclaredFields(cls)).ToImmutableArray();

        var initializers = cls.FindInitializerMethodBodies(options.InitializerMethodNames);

        // TODO figure out a way to concat this
        SupportedSubElements = initializers
                               .SelectMany(static syntax => FindSubElementsFrom(syntax))
                               .ToImmutableArray();

        foreach (BlockSyntax block in initializers)
        {
            XMLAssignedFields = XMLAssignedFields.Union(FindXMLAssignedFields(block)).ToImmutableArray();
        }
    }

    private ImmutableArray<XMLAssignedField> FindXMLAssignedFields(BlockSyntax blockSyntax)
    {
        var correlatedFields = GetAssignmentsToGlobalVariable(blockSyntax, declaredFields, GetLocalVariables(blockSyntax));

        var result = ImmutableArray.CreateBuilder<XMLAssignedField>();

        result.AddRange(FindXMLFieldsInStatement(blockSyntax, correlatedFields));

        foreach (StatementSyntax syntax in blockSyntax.Statements)
        {
            // TODO we probably want to extract the condition here?
            if (syntax is not IfStatementSyntax { Statement: BlockSyntax ifBlock }) { continue; }

            result.AddRange(FindXMLFieldsInStatement(ifBlock, correlatedFields));
        }

        return result.ToImmutable();
    }

    private ImmutableArray<XMLAssignedField> FindXMLFieldsInStatement(BlockSyntax blockSyntax, IReadOnlyCollection<CorrelatedField> correlatedFields)
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
                            Expression: MemberAccessExpressionSyntax { Name: IdentifierNameSyntax rightIdentifier },
                            ArgumentList.Arguments: var assignmentArgumentList
                        },
                        Left: IdentifierNameSyntax leftIdentifier
                    }
                }) { continue; }

            string assignmentMethodName = rightIdentifier.GetIdentifierString(),
                   assignedVariableName = leftIdentifier.GetIdentifierString();

            // probably a better way to do this
            if (!assignmentMethodName.StartsWith("GetAttribute", StringComparison.OrdinalIgnoreCase)) { continue; }

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

                if (value?.Token.Value is not float floatValue) { return "TODO"; } // TODO error handling

                float percentage = floatValue * 100;

                return $"{percentage}% of {identifier?.Identifier.ValueText}";
            }
            case MemberAccessExpressionSyntax memberAccess:
            {
                var accesses = FindMemberAccessses(memberAccess).ToImmutableArray();

                if (accesses.Length is 2 && accesses[0] is ("String" or "Identifier") && accesses[1] is "Empty")
                {
                    return @"""";
                }

                return "TODO";
            }
        }

        return "TODO";
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
                    Description: ""));
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

                result.Add(new DeclaredField(
                    Name: variable.Identifier.ToString(),
                    Type: field.Declaration.Type.ToString(),
                    Description: comment));
            }
        }

        return result.ToImmutable();
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

    private static IEnumerable<string> ParseStatements(SyntaxList<StatementSyntax> syntaxes)
    {
        foreach (StatementSyntax syntax in syntaxes)
        {
            switch (syntax)
            {
                case ExpressionStatementSyntax { Expression: AssignmentExpressionSyntax assignment }:
                    yield return assignment.Left.ToString();
                    break;
                case BreakStatementSyntax:
                    break;
                case ExpressionStatementSyntax { Expression: InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax memberAccess } }:
                    yield return memberAccess.Expression.ToString();
                    break;
            }
        }
    }
}