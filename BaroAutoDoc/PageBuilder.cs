#nullable enable

using System.Collections.Immutable;
using System.Text;
using System.Xml.Linq;
using BaroAutoDoc.SyntaxWalkers;

namespace BaroAutoDoc;

internal sealed class PageBuilder
{
    private readonly Page page;

    public PageBuilder(string title, List<(TypeOrEnum Type, SectionExtras Extras)> types)
    {
        var typesPresentOnPage = types.Select(static e => e.Type.Name).ToImmutableHashSet();

        page = new Page
        {
            Title = title
        };

        foreach (var either in types)
        {
            if (either.Type.GetType(out ParsedType? parser))
            {
                var section = CreateSection(parser.Name, parser, typesPresentOnPage, either.Extras);
                page.Subsections.Add(section);
            }
            else if (either.Type.GetEnum(out ParsedEnum enumParser))
            {
                var section = CreateEnumSection(enumParser, either.Extras);
                if (section is null) { continue; }

                page.Subsections.Add(section);
            }
        }
    }

    public void WriteToFile(string file)
    {
        File.WriteAllText(file, page.ToMarkdown());
    }

    private static void AddSupportedTypes(Page.Section mainSection, SectionExtras extras)
    {
        if (extras.AlsoSupportedTypes is null) { return; }

        List<Page.Hyperlink> links = new();
        foreach (var (typeName, filePath) in extras.AlsoSupportedTypes)
        {
            links.Add(new Page.Hyperlink(
                Url: filePath,
                Text: typeName,
                AltText: string.Empty));
        }

        mainSection.Body.Components.Add(new Page.NewLine());
        mainSection.Body.Components.Add(new Page.RawText("This type also supports the attributes defined in: "));

        foreach (var link in links)
        {
            mainSection.Body.Components.Add(link);
        }
    }

    private static void AddBodyText(Page.Section mainSection, IReadOnlyList<CodeComment> comments, SectionExtras extras)
    {
        string? bodyText = extras.BodyTextOverrideFile;

        if (bodyText != null)
        {
            var markdown = new Page.InlineMarkdown(File.ReadAllText(bodyText));
            mainSection.Body.Components.Add(markdown);
        }
        else
        {
            foreach (Page.BodyComponent description in CreateDescription(comments))
            {
                mainSection.Body.Components.Add(description);
            }
        }
    }

    private static Page.Section CreateSection(string name, ParsedType parser, IReadOnlyCollection<string> typesPresentOnPage, SectionExtras extras)
    {
        Page.Section mainSection = new()
        {
            Title = name
        };

        AddBodyText(mainSection, parser.Comments, extras);

        AddSupportedTypes(mainSection, extras);

        Page.Section attributesSection = new()
        {
            Title = "Attributes"
        };

        Page.Table attributesTable = new()
        {
            HeadRow = new Page.Table.Row("Attribute", "Type", "Default value", "Description")
        };

        foreach (SerializableProperty property in parser.SerializableProperties)
        {
            string defaultValue = DefaultValue.MakeMorePresentable(property.DefaultValue, property.Type);

            string type = ProcessTypeString(property.Type, property.Description);

            attributesTable.BodyRows.Add(new Page.Table.Row(property.Name, type, defaultValue, property.Description));
        }

        foreach (XMLAssignedField field in parser.XMLAssignedFields)
        {
            string type = ProcessTypeString(field.Field.Type, field.Field.Description);
            attributesTable.BodyRows.Add(new Page.Table.Row(field.XMLIdentifier, type, field.GetDefaultValue(), field.Field.Description));
        }

        if (attributesTable.BodyRows.Any())
        {
            attributesSection.Body.Components.Add(attributesTable);
            mainSection.Subsections.Add(attributesSection);
        }

        Page.Section elementSection = new()
        {
            Title = "Elements"
        };

        Page.Table table = new()
        {
            HeadRow = new Page.Table.Row("Element", "Type", "Description")
        };

        foreach (SupportedSubElement element in parser.SupportedSubElements)
        {
            if (element.AffectedField.Length is 0) { continue; }

            DeclaredField field = element.AffectedField.First();

            if (string.IsNullOrWhiteSpace(field.Type))
            {
                Console.WriteLine($"WARNING: Element {element} has no type!");
                continue;
            }

            string fmtType = field.Type;

            if (typesPresentOnPage.Contains(field.Type))
            {
                fmtType = new Page.Hyperlink(
                    Url: $"#{field.Type.ToLower()}",
                    Text: field.Type,
                    AltText: string.Empty).ToMarkdown();
            }
            else if (ContentLocations.TryGetType(field.Type, out string? path))
            {
                fmtType = new Page.Hyperlink(
                    Url: path,
                    Text: field.Type,
                    AltText: string.Empty).ToMarkdown();
            }

            table.BodyRows.Add(new Page.Table.Row(element.XMLName, fmtType, field.Description));
        }

        if (table.BodyRows.Any())
        {
            elementSection.Body.Components.Add(table);
            mainSection.Subsections.Add(elementSection);
        }

        return mainSection;

        string ProcessTypeString(string type, string description)
        {
            string fmtType =
                typesPresentOnPage.Contains(type)
                    ? new Page.Hyperlink(
                            Url: $"#{type.ToLower()}",
                            Text: type,
                            AltText: description)
                        .ToMarkdown()
                    : type;

            return fmtType;
        }
    }

    private static Page.Section? CreateEnumSection(ParsedEnum e, SectionExtras extras)
    {
        Page.Section enumSection = new()
        {
            Title = e.Name
        };

        AddBodyText(enumSection, new[] { e.Comment }, extras);

        AddSupportedTypes(enumSection, extras);

        Page.Table enumTable = new()
        {
            HeadRow = new Page.Table.Row("Value", "Description")
        };

        foreach (var (value, description) in e.Values)
        {
            enumTable.BodyRows.Add(new Page.Table.Row(value, description));
        }

        if (!enumTable.BodyRows.Any()) { return null; }

        enumSection.Body.Components.Add(enumTable);
        return enumSection;
    }

    private static IReadOnlyList<Page.BodyComponent> CreateDescription(IReadOnlyList<CodeComment> comments)
    {
        var builder = ImmutableList.CreateBuilder<Page.BodyComponent>();
        foreach (CodeComment s in comments)
        {
            if (string.IsNullOrWhiteSpace(s.Text)) { continue; }

            builder.Add(new Page.RawText(s.Text));
            builder.Add(new Page.NewLine());
            foreach (var element in s.Element.Elements())
            {
                if (element.Name != "example") { continue; }

                if (element.Element("code") is not { } codeElement) { continue; }

                builder.Add(new Page.CodeBlock(codeElement.Attribute("lang")?.Value ?? "xml", ConstructXMLString(codeElement)));

                static string ConstructXMLString(XElement element)
                {
                    var nodes = element.Nodes().ToImmutableArray();

                    switch (nodes.Length)
                    {
                        case 0:
                            return element.Value;
                        case 1:
                            return nodes[0].ToString();
                    }

                    StringBuilder sb = new StringBuilder(nodes[0].ToString());

                    for (int i = 1; i < nodes.Length; i++)
                    {
                        sb.Append('\n').Append(nodes[i]);
                    }

                    return sb.ToString();
                }
            }
        }

        return builder.ToImmutable();
    }
}