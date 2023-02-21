using System.Reflection;
using BaroAutoDoc.SyntaxWalkers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.Commands;

sealed class ConditionalRip : Command
{
    public void Invoke()
    {
        Directory.SetCurrentDirectory(GlobalConfig.RepoPath);

        const string srcPathFmt = "Barotrauma/Barotrauma{0}/{0}Source";
        var typeRipper = new ArbitraryTypeRipper("PropertyConditional");
        typeRipper.VisitAllInDirectory(string.Format(srcPathFmt, "Shared"));

        var declaration = typeRipper.Declarations.Single();

        EnumDeclarationSyntax findEnum(string enumName)
            => declaration.DescendantNodes()
                .OfType<EnumDeclarationSyntax>()
                .First(eds => eds.Identifier.Text.Equals(enumName));

        Page.Table createEnumTable(EnumDeclarationSyntax enumSyntax)
        {
            Page.Table table = new()
            {
                HeadRow = new Page.Table.Row("Value", "Description")
            };

            foreach (var member in enumSyntax.Members)
            {
                var comment = member.FindCommentAttachedToMember();
                table.BodyRows.Add(new Page.Table.Row(member.Identifier.Text,
                    comment.Element.ElementOfName("NoAutoDoc") == null
                        ? comment.Text
                        : "TODO: write manual comment"));
            }

            return table;
        }

        var conditionTypeEnum = findEnum("ConditionType");
        var comparisonOperatorEnum = findEnum("ComparisonOperatorType");
        var logicalOperatorEnum = findEnum("LogicalOperatorType");

        var page = new Page(); page.Title = "Conditionals";
        var intro = page.Body;
        intro.Components.Add(new Page.InlineMarkdown("***TODO***"));

        var typesSection = new Page.Section(); page.Subsections.Add(typesSection);
        typesSection.Title = "Categories";
        var typesTable = createEnumTable(conditionTypeEnum);
        typesSection.Body.Components.Add(typesTable);

        var comparisonOperatorSection = new Page.Section(); page.Subsections.Add(comparisonOperatorSection);
        comparisonOperatorSection.Title = "Comparison operators";
        comparisonOperatorSection.Body.Components.Add(new Page.InlineMarkdown("Comparison operators determine how the value being checked against is matched with the value given in XML."));
        comparisonOperatorSection.Body.Components.Add(createEnumTable(comparisonOperatorEnum));
        
        var logicalOperatorSection = new Page.Section(); page.Subsections.Add(logicalOperatorSection);
        logicalOperatorSection.Title = "Logical operators";
        logicalOperatorSection.Body.Components.Add(new Page.InlineMarkdown("Logical operators determine how multiple are combined."));
        logicalOperatorSection.Body.Components.Add(createEnumTable(logicalOperatorEnum));
        
        Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!);

        File.WriteAllText("Conditional.md", page.ToMarkdown());
    }
}
