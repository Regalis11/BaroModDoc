using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.SyntaxWalkers;

// TODO what other info do we want to extract? possible errors for example by parsing AddWaring/ThrowError?
public record struct SupportedSubElement(string XMLName, ImmutableArray<string> AffectedField);

internal static class SubElementFinder
{
    /// <summary>
    /// Finds foreach/switch/if/else statements in the given code block and compiles them into a list of of possible
    /// sub elements that the content type accepts in XML
    /// </summary>
    /// <param name="blockSyntax"></param>
    public static ImmutableArray<SupportedSubElement> FindSubElementsFrom(BlockSyntax blockSyntax)
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
        select new SupportedSubElement(caseLabel.Value.ToString(), ParseStatements(switchSection.Statements).ToImmutableArray());

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
                case ExpressionStatementSyntax { Expression: InvocationExpressionSyntax { Expression: MemberAccessExpressionSyntax memberAccess} }:
                    yield return memberAccess.Expression.ToString();
                    break;
                default:
                    Console.WriteLine(syntax.ToString());
                    break;
            }
        }
    }
}