#nullable enable

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace BaroAutoDoc;

internal static class ContentLocations
{
    private static readonly string filePath = File.ReadAllText("ManualDocs/ContentLocations.xml");
    private static readonly ImmutableDictionary<string, string> contentLocations;

    static ContentLocations()
    {
        XDocument doc = XDocument.Parse(filePath);

        if (doc.Root is null)
        {
            throw new Exception("ContentLocations.xml is empty.");
        }

        Dictionary<string,string> dict = new();
        foreach (XElement element in doc.Root.Elements())
        {
            string name = element.GetAttributeValue("name")!,
                   path = element.GetAttributeValue("path")!;
            dict.Add(name, path);
        }

        contentLocations = dict.ToImmutableDictionary();
    }

    public static bool TryGetType(string name, [NotNullWhen(true)] out string? value) => contentLocations.TryGetValue(name, out value);
}