using System.Reflection;
using BaroAutoDoc.SyntaxWalkers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.Commands.ContentTypeSpecific;

class StatusEffectsRip : Command
{
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

        Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!);

        foreach (var (key, cls) in contentTypeFinder.StatusEffectTypes)
        {
            PrefabClassParser parser = new PrefabClassParser(new ClassParsingOptions
            {
                InitializerMethodNames = new[] { "InitProjSpecific" },
            });

            parser.ParseClass(cls);

            Page page = new()
            {
                Title = key
            };

            page.Subsections.Add(CreateSection(key, parser));

            foreach (ClassDeclarationSyntax syntax in cls.Members.OfType<ClassDeclarationSyntax>())
            {
                PrefabClassParser subParser = new PrefabClassParser(new ClassParsingOptions());
                subParser.ParseClass(syntax);

                page.Subsections.Add(CreateSection(syntax.Identifier.ValueText, subParser));
            }

            File.WriteAllText($"{key}.md", page.ToMarkdown());
        }

        static Page.Section CreateSection(string name, PrefabClassParser parser)
        {
            Page.Section mainSection = new()
            {
                Title = name
            };

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

            Page.Table subElementTable = new()
            {
                HeadRow = new Page.Table.Row("Element")
            };

            foreach (SupportedSubElement affectedElement in parser.SupportedSubElements)
            {
                if (affectedElement.AffectedField.Length is 0 ||
                    affectedElement.AffectedField.Contains("DebugConsole")) { continue; }

                // TODO we probably need to generate a list of all these elements and then link to them
                // for example sprite, sound, effect
                subElementTable.BodyRows.Add(new Page.Table.Row(affectedElement.XMLName));
            }

            if (subElementTable.BodyRows.Any())
            {
                subElementSection.Body.Components.Add(subElementTable);
                mainSection.Subsections.Add(subElementSection);
            }

            return mainSection;
        }
    }
}

