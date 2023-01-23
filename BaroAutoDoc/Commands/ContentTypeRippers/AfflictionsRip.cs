using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using BaroAutoDoc.SyntaxWalkers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.Commands.ContentTypeSpecific;

class AfflictionsRip : Command
{
    private readonly record struct AfflictionSection(Page.Section Section, Dictionary<string, string> ElementTable);

    public void Invoke()
    {
        Directory.SetCurrentDirectory(GlobalConfig.RepoPath);
        const string srcPathFmt = "Barotrauma/Barotrauma{0}/{0}Source/Characters/Health/Afflictions/";
        string[] srcPathParams = { "Shared", "Client" };

        var contentTypeFinder = new AfflictionRipper();
        foreach (string p in srcPathParams)
        {
            string srcPath = string.Format(srcPathFmt, p);
            contentTypeFinder.VisitAllInDirectory(srcPath);
        }

        Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!);

        foreach (var (key, cls) in contentTypeFinder.AfflictionPrefabs)
        {
            Dictionary<string, AfflictionSection> finalSections = new();
            PrefabClassParser parser = new PrefabClassParser(new ClassParsingOptions
            {
                InitializerMethodNames = new[] { "LoadEffects" }
            });

            parser.ParseClass(cls);

            Page page = new()
            {
                Title = key
            };

            finalSections.Add(key, CreateSection(key, parser));

            foreach (ClassDeclarationSyntax syntax in cls.Members.OfType<ClassDeclarationSyntax>())
            {
                PrefabClassParser subParser = new PrefabClassParser(new ClassParsingOptions());
                subParser.ParseClass(syntax);

                string identifier = syntax.Identifier.ValueText;
                finalSections.Add(identifier, CreateSection(identifier, subParser));
            }

            foreach (AfflictionSection section in finalSections.Values)
            {
                if (ConstructTable(finalSections, section.ElementTable, out Page.Section? table))
                {
                    section.Section.Subsections.Add(table);
                }
                page.Subsections.Add(section.Section);
            }

            File.WriteAllText($"{key}.md", page.ToMarkdown());
        }

        static bool ConstructTable(Dictionary<string, AfflictionSection> sections, Dictionary<string, string> elementTable, [NotNullWhen(true)] out Page.Section? result)
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
                HeadRow = new Page.Table.Row("Element", "Type")
            };

            foreach (var (element, type) in elementTable)
            {
                string fmtType = type;
                if (sections.ContainsKey(type))
                {
                    fmtType = $"[{type}](#{type.ToLower()})";
                }
                else
                {
                    fmtType = $"[{type}]({type}.md)";
                }
                table.BodyRows.Add(new Page.Table.Row(element, fmtType));
            }

            section.Body.Components.Add(table);

            result = section;
            return true;
        }

        static AfflictionSection CreateSection(string name, PrefabClassParser parser)
        {
            Page.Section mainSection = new()
            {
                Title = name
            };

            foreach (CodeComment s in parser.Comments)
            {
                if (string.IsNullOrWhiteSpace(s.Text)) { continue; }
                mainSection.Body.Components.Add(new Page.RawText(s.Text));
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

            Dictionary<string, string> elementTable = new();

            foreach (SupportedSubElement affectedElement in parser.SupportedSubElements)
            {
                if (affectedElement.AffectedField.Length is 0) { continue; }

                // TODO we probably need to generate a list of all these elements and then link to them
                // for example sprite, sound, effect
                elementTable.Add(affectedElement.XMLName, affectedElement.AffectedField.First().Type);
            }

            return new AfflictionSection(mainSection, elementTable);
        }
    }
}