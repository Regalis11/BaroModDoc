using System.Text;
using System.Xml;
using System.Xml.Linq;
using BaroAutoDoc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

string repoPath;
#if DEBUG
repoPath = "C:/Users/juanj/Desktop/Repos/Barotrauma-development";
#else
if (args.Length <= 0)
{
    Console.WriteLine($"Usage: BaroAutoDoc [Barotrauma repository directory]");
}
repoPath = args[0];
#endif
if (!repoPath.EndsWith('/')) { repoPath+="/"; }

string srcPathFmt = $"{repoPath}Barotrauma/Barotrauma{{0}}/{{0}}Source/ContentManagement/ContentFile";
string[] srcPathParams = {"Shared"};

var contentTypeFinder = new ContentTypeFinder();
foreach (string p in srcPathParams)
{
    string srcPath = string.Format(srcPathFmt, p);
    if (!Directory.Exists(srcPath)) { continue; }
    foreach (string f in Directory.GetFiles(srcPath))
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(f));
        contentTypeFinder.Visit(syntaxTree.GetRoot());
    }
}

Directory.CreateDirectory("markdown/ContentTypes");

foreach (var contentType in contentTypeFinder.ContentTypes)
{
    string? matchSingular = contentType.MatchSingular;
    string? matchPlural = contentType.MatchPlural;

    if (contentType.Name.EndsWith("s") && string.IsNullOrWhiteSpace(matchPlural))
    {
        matchPlural = contentType.Name;
    }
    else if (string.IsNullOrWhiteSpace(matchSingular))
    {
        matchSingular = contentType.Name;
    }
    
    string transform(string? s, Func<string, string> t)
        => s is null ? "AUTODOC_FAIL" : t(s);
    matchSingular ??= transform(matchPlural, s => s[..^1]);
    matchPlural ??= transform(matchSingular, s => $"{s}s");
    XElement singularExample(int index=0)
        => new XElement(matchSingular,
            new XAttribute("identifier", $"my{matchSingular}{(index > 0 ? index : "")}"),
            new XAttribute("TODO", "add remaining attributes"));
    XElement pluralExample = new XElement(matchPlural,
        singularExample(1),
        singularExample(2));
    XElement overrideExample = new XElement("override",
        singularExample(1),
        singularExample(2));

    var xmlWriterSettings = new XmlWriterSettings
    {
        Indent = true,
        OmitXmlDeclaration = true,
        NewLineOnAttributes = true
    };

    string xmlToMarkdown(XElement element)
    {
        var xmlStringBuilder = new StringBuilder();
        using (var xmlWriter = XmlWriter.Create(xmlStringBuilder, xmlWriterSettings))
        {
            element.Save(xmlWriter);
        }
        return $"```xml\n{xmlStringBuilder}\n```\n\n";
    }

    string markdown = $"# {contentType.Name}\n\n"
                      + $"- **Required by core package:** {(contentType.RequiredByCorePackage ? "Yes" : "No")}\n";
    if (contentType.AltNames is { Length: >0 } altNames)
    {
        markdown += $"- **Alternate names:** {string.Join(", ", altNames)}\n";
    }
    markdown += "\n";

    if (contentType.IsSubmarineType)
    {
        markdown += "This content type is created in the submarine editor.";
    }
    else
    {
        if (string.IsNullOrWhiteSpace(contentType.MatchSingular) && string.IsNullOrWhiteSpace(contentType.MatchSingular))
        {
            markdown += "**WARNING:** This file likely generated completely incorrectly!\n\n";
        }

        markdown += "## Examples\n\n"
                    + $"### Example 1 - single {matchSingular}\n\n"
                    + xmlToMarkdown(singularExample())
                    + $"### Example 2 - multiple {matchPlural}\n\n"
                    + xmlToMarkdown(pluralExample)
                    + $"### Example 3 - overriding existing {matchPlural}\n\n"
                    + xmlToMarkdown(overrideExample);
    }
    
    File.WriteAllText($"markdown/ContentTypes/{contentType.Name}.md", markdown);
}

File.WriteAllText("markdown/index.md",
    "# Barotrauma modding guide\n\n"
    + "## Content types\n\n"
    + string.Join("\n", contentTypeFinder.ContentTypes.Select(t
        => $"- [{t.Name}](ContentTypes/{t.Name}.md)")));
