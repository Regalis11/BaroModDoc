using System.Collections.Immutable;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using BaroAutoDoc.SyntaxWalkers;

namespace BaroAutoDoc.Commands.ContentTypeSpecific;

sealed class AfflictionsRip : Command
{
    //private readonly record struct AfflictionSection(Page.Section Section, ImmutableArray<(string, string, string)> ElementTable, ParsedType Parser);

    public void Invoke()
    {
        SyntaxRipperBuilder builder = new();

        /*
        const string srcPathFmt = "Barotrauma/Barotrauma{0}/{0}Source/Characters/Health/Afflictions/";
        string[] srcPathParams = { "Shared", "Client" };*/

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
            Page page = new()
            {
                Title = file
            };

            var (classes, enums) = lists;

            foreach (var parser in classes)
            {
                var section = CreateSection(parser.Name, parser);
                page.Subsections.Add(section);
            }

            foreach (var parser in enums)
            {
                var section = CreateEnumSection(parser);
                if (section is null) { continue; }
                page.Subsections.Add(section);
            }

            File.WriteAllText($"{file}.md", page.ToMarkdown());
        }

        /*Dictionary<string, ParsedType> parsedClasses = new();
        foreach (var (key, cls) in contentTypeFinder.AfflictionPrefabs)
        {
            if (parsedClasses.TryGetValue(key, out ParsedType? parser))
            {
                parser.ParseType(cls);
                continue;
            }

            parser = ParsedType.CreateParser(cls, new ClassParsingOptions
            {
                InitializerMethodNames = new[] { "LoadEffects" }
            });
            parser.ParseType(cls);
            parsedClasses.Add(key, parser);
        }

        Dictionary<string, ParsedType> parsedAfflictionTypes = new();
        foreach (var (key, cls) in contentTypeFinder.AfflictionTypes)
        {
            var parser = ParsedType.CreateParser(cls, new ClassParsingOptions());
            parser.ParseType(cls);
            parsedAfflictionTypes.Add(key, parser);
        }

        foreach (var (key, parser) in parsedClasses)
        {
            Dictionary<string, AfflictionSection> finalSections = new();

            Page page = new()
            {
                Title = key
            };

            if (key == "AfflictionPrefab")
            {
                if (ConstructAfflictionTypeTable(parsedAfflictionTypes, out var result))
                {
                    page.Subsections.Add(result);
                }
            }

            finalSections.Add(key, CreateSection(key, parser));

            WriteSubClasses(parser);

            void WriteSubClasses(ParsedType subParser)
            {
                foreach (var (subName, subSubParser) in subParser.SubClasses)
                {
                    finalSections.Add(subName, CreateSection(subName, subSubParser));
                    WriteSubClasses(subSubParser);
                }
            }

            foreach (var (identifier, section) in finalSections)
            {
                if (ConstructSubElementTable(finalSections, section.ElementTable, out Page.Section? table))
                {
                    section.Section.Subsections.Add(table);
                }

                if (key != identifier || parser.BaseClasses.Count is 0)
                {
                    page.Subsections.Add(section.Section);
                    AddEnumTable();
                    continue;
                }

                foreach (string type in parser.BaseClasses)
                {
                    if (!contentTypeFinder.AfflictionPrefabs.Keys.Any(k => string.Equals(k, type, StringComparison.OrdinalIgnoreCase))) { continue; }

                    section.Section.Body.Components.Add(new Page.RawText("This prefab also supports the attributes defined in: "));
                    section.Section.Body.Components.Add(new Page.Hyperlink($"{type}.md#{type.ToLower()}", type));
                }

                page.Subsections.Add(section.Section);
                AddEnumTable();

                void AddEnumTable()
                {
                    if (ConstructEnumTable(section.Parser.Enums, out ImmutableArray<Page.Section>? enums))
                    {
                        section.Section.Subsections.AddRange(enums);
                    }
                }
            }

            File.WriteAllText($"{key}.md", page.ToMarkdown());
        }

        static bool ConstructSubElementTable(Dictionary<string, AfflictionSection> sections, ImmutableArray<(string, string, string)> elementTable, [NotNullWhen(true)] out Page.Section? result)
        {
            if (!elementTable.Any())
            {
                result = null;
                return false;
            }

            Page.Section section = new()
            {
                Title = "Elements"
            };

            Page.Table table = new()
            {
                HeadRow = new Page.Table.Row("Element", "Type", "Description")
            };

            foreach (var (element, type, description) in elementTable)
            {
                if (string.IsNullOrWhiteSpace(type))
                {
                    Console.WriteLine($"WARNING: Element {element} has no type!");
                    continue;
                }

                string fmtType =
                    new Page.Hyperlink(
                        Url: sections.ContainsKey(type)
                            ? $"#{type.ToLower()}"
                            : $"{type}.md",
                        Text: type,
                        AltText: description)
                    .ToMarkdown();

                table.BodyRows.Add(new Page.Table.Row(element, fmtType, description));
            }

            if (table.BodyRows.Count is 0)
            {
                result = null;
                return false;
            }

            section.Body.Components.Add(table);

            result = section;
            return true;
        }


        static bool ConstructAfflictionTypeTable(Dictionary<string, ParsedType> parsedAfflictionTypes, [NotNullWhen(true)] out Page.Section? result)
        {
            if (!parsedAfflictionTypes.Any())
            {
                result = null;
                return false;
            }

            Page.Section section = new()
            {
                Title = "Types"
            };

            Page.Table table = new()
            {
                HeadRow = new Page.Table.Row("Element", "Description")
            };

            foreach (var (subName, subSubParser) in parsedAfflictionTypes)
            {
                table.BodyRows.Add(new Page.Table.Row(subName, string.Join('\n', subSubParser.Comments.Select(c => c.Text))));
            }

            if (table.BodyRows.Count is 0)
            {
                result = null;
                return false;
            }

            section.Body.Components.Add(table);

            result = section;
            return true;
        }

        static bool ConstructEnumTable(Dictionary<string, ImmutableArray<(string, string)>> enums, [NotNullWhen(true)] out ImmutableArray<Page.Section>? result)
        {
            if (!enums.Any())
            {
                result = null;
                return false;
            }

            var builder = ImmutableArray.CreateBuilder<Page.Section>();
            foreach (var (type, values) in enums)
            {
                Page.Section section = new()
                {
                    Title = type
                };

                Page.Table table = new()
                {
                    HeadRow = new Page.Table.Row("Value", "Description")
                };

                foreach (var (value, description) in values)
                {
                    table.BodyRows.Add(new Page.Table.Row(value, description));
                }

                section.Body.Components.Add(table);

                builder.Add(section);
            }

            result = builder.ToImmutable();
            return true;
        } */

        static Page.Section CreateSection(string name, ParsedType parser)
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

                /*string fmtType =
                    new Page.Hyperlink(
                            Url: sections.ContainsKey(field.Type)
                                ? $"#{type.ToLower()}"
                                : $"{type}.md",
                            Text: type,
                            AltText: description)
                        .ToMarkdown();*/

                table.BodyRows.Add(new Page.Table.Row(element.XMLName, field.Type, field.Description));
            }

            if (table.BodyRows.Any())
            {
                elementSection.Body.Components.Add(table);
                mainSection.Subsections.Add(elementSection);
            }

            /*foreach (var @enum in parser.Enums)
            {
                var enumSections = CreateEnumSection(@enum);
                if (enumSections is null) { continue; }
                mainSection.Subsections.Add(enumSections);
            }*/


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