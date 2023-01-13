using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.SyntaxWalkers;

// TODO what other info do we want to extract? possible errors for example by parsing AddWaring/ThrowError?
public readonly record struct SupportedSubElement(string XMLName, ImmutableArray<string> AffectedField);

public readonly record struct DeclaredField(string Name, string Type);

public readonly record struct CorrectedField(string Global, string Local);

public readonly record struct XMLAssignedField(DeclaredField Field, string XMLIdentifier);

public record struct ClassParsingOptions(string[] InitializerMethodNames);

internal sealed class PrefabClassParser
{
    private ImmutableHashSet<DeclaredField> declaredFields = ImmutableHashSet<DeclaredField>.Empty;
    public ImmutableHashSet<SupportedSubElement> SupportedSubElements = ImmutableHashSet<SupportedSubElement>.Empty;

    public ImmutableHashSet<XMLAssignedField> XMLAssignedFields = ImmutableHashSet<XMLAssignedField>.Empty; 

    private readonly ClassParsingOptions options;

    public PrefabClassParser(ClassParsingOptions options)
    {
        this.options = options;
    }

    public void ParseClass(ClassDeclarationSyntax cls)
    {
        declaredFields = declaredFields.Union(GetDeclaredFields(cls));

        var initializers = cls.FindInitializerMethodBodies(options.InitializerMethodNames);

        // TODO figure out a way to concat this
        SupportedSubElements = initializers
                               .SelectMany(static syntax => FindSubElementsFrom(syntax))
                               .ToImmutableHashSet();

        foreach (BlockSyntax block in initializers)
        {
            var localVariables = GetLocalVariables(block);

            ImmutableHashSet<CorrectedField> correlatedFields = GetAssignmentsToGlobalVariable(block, declaredFields, localVariables);

            XMLAssignedFields = XMLAssignedFields.Union(FindXMLAssignedFields(declaredFields, correlatedFields, block));
        }
    }

    private static ImmutableHashSet<XMLAssignedField> FindXMLAssignedFields(ImmutableHashSet<DeclaredField> fields, ImmutableHashSet<CorrectedField> correlatedFields, BlockSyntax blockSyntax)
    {
        var result = ImmutableHashSet.CreateBuilder<XMLAssignedField>();

        foreach (StatementSyntax statement in blockSyntax.Statements)
        {
            // FIXME this is genuinely unreadable (not like this form of code is readable anyway) but I can do better
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

            foreach (DeclaredField declaredField in fields)
            {
                DeclaredField field = declaredField;
                // this is backwards??
                foreach (CorrectedField correctedField in correlatedFields)
                {
                    if (correctedField.Global != declaredField.Name) { continue; }
                    field = field with { Name = correctedField.Global };
                }

                if (field.Name != assignedVariableName) { continue; }

                string xmlIdentifier = assignmentArgumentList[0].ToString().EvaluateAsCSharpExpression();

                result.Add(new XMLAssignedField(field, xmlIdentifier));
                break;
            }
        }

        return result.ToImmutable();
    }

    private static ImmutableHashSet<DeclaredField> GetLocalVariables(BlockSyntax block)
    {
        var result = ImmutableHashSet.CreateBuilder<DeclaredField>();

        foreach (var localDeclaration in block.Statements.OfType<LocalDeclarationStatementSyntax>())
        {
            foreach (VariableDeclaratorSyntax variableName in localDeclaration.Declaration.Variables)
            {
                result.Add(new DeclaredField(variableName.Identifier.ToString(), localDeclaration.Declaration.Type.ToString()));
            }
        }

        return result.ToImmutable();
    }

    private static ImmutableHashSet<CorrectedField> GetAssignmentsToGlobalVariable(BlockSyntax block, ImmutableHashSet<DeclaredField> globalVariables, ImmutableHashSet<DeclaredField> localVariables)
    {
        var result = ImmutableHashSet.CreateBuilder<CorrectedField>();

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
                    result.Add(new CorrectedField(leftIdentifier.GetIdentifierString(), rightIdentifier.GetIdentifierString()));
                    break;
                }
            }
        }

        return result.ToImmutable();
    }

    private static ImmutableHashSet<DeclaredField> GetDeclaredFields(ClassDeclarationSyntax cls)
    {
        var result = ImmutableHashSet.CreateBuilder<DeclaredField>();

        foreach (var field in cls.Members.OfType<FieldDeclarationSyntax>())
        {
            foreach (var variable in field.Declaration.Variables)
            {
                result.Add(new DeclaredField(variable.Identifier.ToString(), field.Declaration.Type.ToString()));
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
                case SwitchStatementSyntax switchStatement:
                    elements.AddRange(FindSubElementsFromSwitch(switchStatement));
                    break;
                case IfStatementSyntax:
                    // FIXME implement if statements
                    break;
            }
        }

        return elements;
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
                default:
                    Console.WriteLine(syntax.ToString());
                    break;
            }
        }
    }
}