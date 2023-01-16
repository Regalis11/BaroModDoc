using System.Reflection;
using BaroAutoDoc.SyntaxWalkers;

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

            Page.Section attributesSection = new();
            page.Subsections.Add(attributesSection);
            attributesSection.Title = "Attributes";

            Page.Table attributesTable = new()
            {
                HeadRow = new Page.Table.Row("Attribute", "Type", "Default value", "Description")
            };

            foreach (XMLAssignedField field in parser.XMLAssignedFields)
            {
                attributesTable.BodyRows.Add(new Page.Table.Row(field.XMLIdentifier, field.Field.Type, "TODO", field.Field.Description));
            }

            if (attributesTable.BodyRows.Any())
            {
                attributesSection.Body.Components.Add(attributesTable);
            }

            Page.Section subElementSection = new();
            page.Subsections.Add(subElementSection);
            subElementSection.Title = "Sub Elements";

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
            }

            File.WriteAllText($"{key}.md", page.ToMarkdown());
        }
    }
}