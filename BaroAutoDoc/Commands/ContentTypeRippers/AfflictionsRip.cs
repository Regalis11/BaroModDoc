using System.Reflection;
using BaroAutoDoc.SyntaxWalkers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.Commands.ContentTypeSpecific;

class AfflictionsRip : Command
{

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
            PrefabClassParser parser = new PrefabClassParser(new ClassParsingOptions
            {
                InitializerMethodNames = new[] { "LoadEffects" },
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
                Title = name,
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
                HeadRow = new Page.Table.Row("Element", "Type")
            };

            foreach (SupportedSubElement affectedElement in parser.SupportedSubElements)
            {
                if (affectedElement.AffectedField.Length is 0) { continue; }

                // TODO we probably need to generate a list of all these elements and then link to them
                // for example sprite, sound, effect
                subElementTable.BodyRows.Add(new Page.Table.Row(affectedElement.XMLName, affectedElement.AffectedField.First().Type));
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