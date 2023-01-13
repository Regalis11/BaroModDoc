using System.Collections.Immutable;
using System.Text.RegularExpressions;
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

        // TODO we probably want to reuse this for other content types
        foreach (var (key, cls) in contentTypeFinder.AfflictionPrefabs.Union(contentTypeFinder.AfflictionTypes))
        {
            Console.WriteLine($"{key}:");

            PrefabClassParser parser = new PrefabClassParser(new ClassParsingOptions
            {
                InitializerMethodNames = new[] { "LoadEffects" },
            });

            parser.ParseClass(cls);

            foreach (XMLAssignedField field in parser.XMLAssignedFields)
            {
                Console.WriteLine($"    {field.Field.Name}: \"{field.XMLIdentifier}\" ({field.Field.Type})");
            }

            foreach (SupportedSubElement affectedElement in parser.SupportedSubElements)
            {
                // TODO we need to consider that a lot of lists are created in the constructor and then assigned into the global variable
                // one such case is the descriptions in affliction
                if (affectedElement.AffectedField.Length is 0) { continue; }
                Console.WriteLine($"    <{affectedElement.XMLName}> {string.Join(',', affectedElement.AffectedField)}");
            }
        }
    }
}