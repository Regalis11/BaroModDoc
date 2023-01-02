using System.Diagnostics;
using System.Xml.Linq;

namespace BaroAutoDoc.Commands;

public class XmlNames : Command
{
    public void Invoke(
        string contentType,
        int startDepth,
        int maxDepth = -1,
        string repoPath = "C:/Users/juanj/Desktop/Repos/Barotrauma-development")
    {
        Directory.SetCurrentDirectory(repoPath);
        
        if (maxDepth < 0) { maxDepth = startDepth; }

        const string gameRoot = "Barotrauma/BarotraumaShared";
        const string contentPackagePath = $"{gameRoot}/Content/ContentPackages/Vanilla.xml";
        var contentPackageFileList = XDocument.Load(contentPackagePath)?.Root
            ?? throw new Exception("Failed to load Vanilla.xml");
        var files = contentPackageFileList.Elements()
            .Where(e => e.Name.LocalName.Equals(contentType, StringComparison.OrdinalIgnoreCase))
            .Select(e => e.Attribute("file")!.Value)
            .ToArray();

        HashSet<string>[] elementsByDepth
            = Enumerable.Range(0, maxDepth - startDepth + 1).Select(_ => new HashSet<string>()).ToArray();
        foreach (var filePath in files)
        {
            var file = XDocument.Load(Path.Combine(gameRoot, filePath));
            XElement[] elems = { file?.Root ?? throw new Exception($"Failed to load {filePath}") };
            for (int i = 0; i < startDepth; i++)
            {
                elems = elems.SelectMany(e => e.Elements()).ToArray();
            }

            for (int i = startDepth; i <= maxDepth; i++)
            {
                elementsByDepth[i-startDepth] =
                    elementsByDepth[i-startDepth].Concat(elems.Select(e => e.Name.LocalName))
                        .DistinctBy(s => s.GetHashCode(StringComparison.OrdinalIgnoreCase))
                        .ToHashSet();
                elems = elems.SelectMany(e => e.Elements()).ToArray();
            }
        }

        for (int i = 0; i < elementsByDepth.Length; i++)
        {
            foreach (var elemName in elementsByDepth[i])
            {
                Console.WriteLine($"{new string('\t', i)}- {elemName}");
            }
        }
    }
}
