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
                var autoDocEntryName = comment.Element.ElementOfName("AutoDocEntryName") is { } entryNameElem
                    ? entryNameElem.GetAttributeValue("value")
                    : member.Identifier.Text;
                // TODO: fix Page.Table to be able to handle code blocks in cells
                /*var example = comment.Element.ElementOfName("example") is { } exampleElem
                    ? Page.CodeBlock.FromXElement(exampleElem).ToMarkdown()
                    : "";*/
                table.BodyRows.Add(new Page.Table.Row(autoDocEntryName, comment.Text));
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
        typesSection.Title = "Attributes";
        var typesTable = createEnumTable(conditionTypeEnum);
        typesSection.Body.Components.Add(typesTable);

        var comparisonOperatorSection = new Page.Section(); page.Subsections.Add(comparisonOperatorSection);
        comparisonOperatorSection.Title = "Comparison operators";
        comparisonOperatorSection.Body.Components.Add(new Page.InlineMarkdown("Comparison operators determine how the value being checked against is matched with the value given in XML."));
        var comparisonOperatorTable = createEnumTable(comparisonOperatorEnum);
        comparisonOperatorTable.HeadRow!.Values[0] = "Operator";
        comparisonOperatorSection.Body.Components.Add(comparisonOperatorTable);

        var comparisonOperatorPrefixSwitch = declaration.DescendantNodes().OfType<MethodDeclarationSyntax>()
            .First(m => m.Identifier.Text == "GetComparisonOperatorType")
            .DescendantNodes().OfType<SwitchStatementSyntax>().First();
        foreach (var member in comparisonOperatorEnum.Members)
        {
            var matchingSection = comparisonOperatorPrefixSwitch.Sections
                .FirstOrDefault(s => s.Statements.Any(stmt => stmt.ToString().Contains(member.Identifier.Text)));
            if (matchingSection is null) { continue; }
            var labels = matchingSection.Labels
                .OfType<CaseSwitchLabelSyntax>()
                .Select(c => c.Value.ToString())
                .ToArray();
            comparisonOperatorTable.BodyRows.First(r => r.Values[0] == member.Identifier.Text).Values[0] = string.Join(", ", labels);
        }
        comparisonOperatorTable.BodyRows.RemoveAll(r => string.IsNullOrWhiteSpace(r.Values[0]));

        var logicalOperatorSection = new Page.Section(); page.Subsections.Add(logicalOperatorSection);
        logicalOperatorSection.Title = "Logical operators";
        logicalOperatorSection.Body.Components.Add(new Page.InlineMarkdown("Logical operators determine how multiple conditionals are combined."));
        logicalOperatorSection.Body.Components.Add(createEnumTable(logicalOperatorEnum));
        
        Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!);

        File.WriteAllText("Conditional.md", page.ToMarkdown());
    }
}
