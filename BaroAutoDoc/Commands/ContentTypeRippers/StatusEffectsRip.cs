using BaroAutoDoc.SyntaxWalkers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Reflection;
using static BaroAutoDoc.Page;

namespace BaroAutoDoc.Commands.ContentTypeSpecific;

sealed class StatusEffectsRip : Command
{
    private EnumDeclarationSyntax? actionTypes;

    public void Invoke()
    {

        Directory.SetCurrentDirectory(GlobalConfig.RepoPath);
        const string srcPathFmt = "Barotrauma/Barotrauma{0}/{0}Source/StatusEffects/";
        string[] srcPathParams = { "Shared", "Client" };

        var contentTypeFinder = new StatusEffectRipper();
        foreach (string p in srcPathParams)
        {
            string srcPath = string.Format(srcPathFmt, p);
            contentTypeFinder.VisitAllInDirectory(srcPath);
        }

        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText("Barotrauma/BarotraumaShared/SharedSource/Enums.cs"));

        var root = syntaxTree.GetRoot();        
        foreach (var childNode in root.ChildNodes())
        {
            if (childNode is NamespaceDeclarationSyntax)
            {
                foreach (var grandChildNode in childNode.ChildNodes())
                {
                    if (grandChildNode is EnumDeclarationSyntax enumSyntax && enumSyntax.Identifier.Text == "ActionType")
                    {
                        actionTypes = enumSyntax;
                        break;
                    }
                }
            }
        }

        Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!);

        foreach (var (key, cls) in contentTypeFinder.StatusEffectTypes)
        {
            ParsedType parser = ParsedType.CreateParser(cls, new ClassParsingOptions
            {
                InitializerMethodNames = new[] { "InitProjSpecific" },
            });

            parser.ParseType(cls);

            Page page = new()
            {
                Title = key
            };

            string introductionText = File.ReadAllText("ManualDocs/StatusEffectIntroduction.md");
            if (actionTypes != null && BaseRip.ConstructEnumTable(actionTypes, out var actionTypesTable))
            {
                introductionText = introductionText.Replace("[TODO: list ActionTypes]", string.Join('\n', actionTypesTable.Value.Select(s => s.ToMarkdown())));
            }
            if (parser.Enums.ContainsKey("TargetType"))
            {
                var targetTypes = new Dictionary<string, ImmutableArray<(string, string)>>
                {
                    { "TargetType", parser.Enums["TargetType"] }
                };
                parser.Enums.Remove("TargetType");
                if (BaseRip.ConstructEnumTable(targetTypes, out ImmutableArray<Page.Section>? enumTable))
                {
                    introductionText = introductionText.Replace("[TODO: list TargetTypes]", string.Join('\n', enumTable.Value.Select(s => s.ToMarkdown())));
                }
            }
            var introduction = new InlineMarkdown(introductionText);
            var elementTable = new InlineMarkdown(File.ReadAllText("ManualDocs/StatusEffectElementTable.md"));

            page.Subsections.Add(CreateSection(key, parser, includeComments: false, elementTable, introduction));

            foreach (ClassDeclarationSyntax syntax in cls.Members.OfType<ClassDeclarationSyntax>())
            {
                ParsedType subParser = ParsedType.CreateParser(syntax, new ClassParsingOptions());
                subParser.ParseType(syntax);

                page.Subsections.Add(CreateSection(syntax.Identifier.ValueText, subParser, includeComments: true, elementTable: null, preamble: null));
            }

            File.WriteAllText($"{key}.md", page.ToMarkdown());
        }

        static Page.Section CreateSection(string name, ParsedType parser, bool includeComments, BodyComponent? elementTable, BodyComponent? preamble)
        {
            Page.Section mainSection = new()
            {
                Title = name
            };

            if (includeComments)
            {
                foreach (CodeComment comment in parser.Comments)
                {
                    if (string.IsNullOrWhiteSpace(comment.Text)) { continue; }
                    mainSection.Body.Components.Add(new Page.RawText(comment.Text));
                }
            }
            if (preamble != null)
            {
                mainSection.Body.Components.Add(preamble);
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
                attributesTable.BodyRows.Add(new Page.Table.Row(property.Name, property.Type, property.DefaultValue, property.Description));
            }

            foreach (XMLAssignedField field in parser.XMLAssignedFields)
            {
                attributesTable.BodyRows.Add(new Page.Table.Row(field.XMLIdentifier, field.Field.Type, field.DefaultValue, field.Field.Description));
            }

            if (attributesTable.BodyRows.Any())
            {
                attributesSection.Body.Components.Add(attributesTable);
                mainSection.Subsections.Add(attributesSection);
            }

            Page.Section subElementSection = new()
            {
                Title = "Elements"
            };

            if (elementTable == null)
            {
                Page.Table subElementTable = new()
                {
                    HeadRow = new Page.Table.Row("Element", "Type", "Description")
                };
                foreach (SupportedSubElement affectedElement in parser.SupportedSubElements)
                {
                    if (affectedElement.AffectedField.Length is 0) { continue; }

                    // TODO we probably need to generate a list of all these elements and then link to them
                    // for example sprite, sound, effect
                    DeclaredField field = affectedElement.AffectedField.First();
                    subElementTable.BodyRows.Add(new Page.Table.Row(
                        affectedElement.XMLName,
                        field.Type,
                        field.Description));
                }
                if (subElementTable.BodyRows.Any())
                {
                    subElementSection.Body.Components.Add(subElementTable);
                    mainSection.Subsections.Add(subElementSection);
                }
            }
            else
            {
                subElementSection.Body.Components.Add(elementTable);
                mainSection.Subsections.Add(subElementSection);
            }

            AddEnumTable();

            void AddEnumTable()
            {
                if (BaseRip.ConstructEnumTable(parser.Enums, out ImmutableArray<Page.Section>? enums))
                {
                    mainSection.Subsections.AddRange(enums);
                }
            }

            return mainSection;
        }
    }
}

