using System.Collections.Immutable;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using BaroAutoDoc.SyntaxWalkers;

namespace BaroAutoDoc.Commands.ContentTypeSpecific;

sealed class AfflictionsRip : Command
{
    public void Invoke()
    {
        SyntaxRipperBuilder builder = new();

        Directory.SetCurrentDirectory(GlobalConfig.RepoPath);

        builder
            .Prepare("Prefabs") // TODO not required idk why I added it
            .AddDirectory("Barotrauma/Barotrauma{0}/{0}Source/Characters/Health/Afflictions/", "*.cs", fmt: new[] { "Shared", "Client", "Server" })
            .AddFile("Barotrauma/BarotraumaShared/SharedSource/Map/Explosion.cs") // for testing
            .WithOptions(new ClassParsingOptions(new []{ "LoadEffects" }))
            .Map
            (
                new FileMap("AfflictionPrefab")
                {
                    "AfflictionPrefab",
                    "AfflictionPrefabHusk",
                    "Description",
                    "TargetType",
                    "Effect",
                    "AppliedStatValue",
                    "PeriodicEffect"
                },
                new FileMap("Affliction")
                {
                    "Affliction",
                    "AfflictionHusk",
                    "AfflictionPsychosis",
                    "AfflictionSpaceHerpes"
                },
                new FileMap("Explosion") // for testing
                {
                    "Explosion"
                }
            )
            .Submit();

        Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!);

        var files = builder.Build();

        foreach (var (file, lists) in files)
        {
            var typesPresentOnPage = lists.Select(static e => e.Name).ToImmutableHashSet();

            Page page = new()
            {
                Title = file
            };

            foreach (var either in lists)
            {
                if (either.GetType(out ParsedType? parser))
                {
                    var section = CreateSection(parser.Name, parser, typesPresentOnPage);
                    page.Subsections.Add(section);
                }
                else if (either.GetEnum(out ParsedEnum enumParser))
                {
                    var section = CreateEnumSection(enumParser);
                    if (section is null) { continue; }
                    page.Subsections.Add(section);
                }
            }

            File.WriteAllText($"{file}.md", page.ToMarkdown());
        }

        static Page.Section CreateSection(string name, ParsedType parser, IReadOnlyCollection<string> typesPresentOnPage)
        {
            Page.Section mainSection = new()
            {
                Title = name
            };

            foreach (CodeComment s in parser.Comments)
            {
                if (string.IsNullOrWhiteSpace(s.Text)) { continue; }

                mainSection.Body.Components.Add(new Page.RawText(s.Text));
                mainSection.Body.Components.Add(new Page.NewLine());
                foreach (var element in s.Element.Elements())
                {
                    if (element.Name != "example") { continue; }

                    if (element.Element("code") is not { } codeElement) { continue; }

                    mainSection.Body.Components.Add(new Page.CodeBlock(codeElement.Attribute("lang")?.Value ?? "xml", ConstructXMLString(codeElement)));

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

                attributesTable.BodyRows.Add(new Page.Table.Row(property.Name, property.Type, defaultValue, property.Description));
            }

            foreach (XMLAssignedField field in parser.XMLAssignedFields)
            {
                attributesTable.BodyRows.Add(new Page.Table.Row(field.XMLIdentifier, field.Field.Type, field.GetDefaultValue(), field.Field.Description));
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

                string fmtType =
                    new Page.Hyperlink(
                            Url: typesPresentOnPage.Contains(field.Type)
                                ? $"#{field.Type.ToLower()}"
                                : $"{field.Type}.md",
                            Text: field.Type,
                            AltText: field.Description)
                        .ToMarkdown();

                table.BodyRows.Add(new Page.Table.Row(element.XMLName, fmtType, field.Description));
            }

            if (table.BodyRows.Any())
            {
                elementSection.Body.Components.Add(table);
                mainSection.Subsections.Add(elementSection);
            }

            return mainSection;
        }

        static Page.Section? CreateEnumSection(ParsedEnum e)
        {
            Page.Section enumSection = new()
            {
                Title = e.Name
            };

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
    }
}