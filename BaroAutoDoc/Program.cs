using System.Reflection;
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

Directory.SetCurrentDirectory(repoPath);

const string srcPathFmt = "Barotrauma/Barotrauma{0}/{0}Source";
const string contentFilePathFmt = $"{srcPathFmt}/ContentManagement/ContentFile";
string[] srcPathParams = {"Shared"};

var contentTypeFinder = new ContentTypeFinder();
foreach (string p in srcPathParams)
{
    string contentFilePath = string.Format(contentFilePathFmt, p);
    if (!Directory.Exists(contentFilePath)) { continue; }
    foreach (string f in Directory.GetFiles(contentFilePath))
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(f));
        contentTypeFinder.CurrentFile = f.Replace('\\', '/');
        contentTypeFinder.Visit(syntaxTree.GetRoot());
    }
}

var subElementRipper = new SubElementRipper(contentTypeFinder.ContentTypes);

foreach (string p in srcPathParams)
{
    string srcPath = string.Format(srcPathFmt, p);
    if (!Directory.Exists(srcPath)) { continue; }
    foreach (string f in Directory.GetFiles(srcPath, "*.cs", SearchOption.AllDirectories))
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(f));
        subElementRipper.CurrentFile = f.Replace('\\', '/');
        subElementRipper.Visit(syntaxTree.GetRoot());
    }
}

var contentTypes = subElementRipper.ContentTypes;

Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!);
Directory.CreateDirectory("markdown/ContentTypes");

foreach (var contentType in contentTypes)
{
    string? matchSingular = contentType.MatchSingular;
    string? matchPlural = contentType.MatchPlural;

    if (contentType.Name.EndsWith("s"))
    {
        matchPlural ??= contentType.Name;
    }
    else
    {
        matchSingular ??= contentType.Name;
    }
    
    string transform(string? s, Func<string, string> t)
        => s is null ? "AUTODOC_FAIL" : t(s);
    matchSingular ??= transform(matchPlural, s => s[..^1]);
    matchPlural ??= transform(matchSingular, s => $"{s}s");

    var xmlWriterSettings = new XmlWriterSettings
    {
        Indent = true,
        OmitXmlDeclaration = true,
        NewLineOnAttributes = true
    };

    string[] shorthands = {"Client", "Server", "Shared"};

    string pickShorthand(string file)
        => shorthands.First(s => file.Contains($"Barotrauma{s}"));

    string fileLink(string file)
        => $"[{pickShorthand(file)}:{Path.GetFileName(file)}]"
           + $"(https://github.com/Regalis11/Barotrauma/blob/master/{file})";

    string markdown = $"# {contentType.Name}\n\n"
                      + $"<sup>Relevant files: {string.Join(" ", contentType.RelevantFiles.Select(fileLink))}</sup>\n\n";
    
    if (string.IsNullOrWhiteSpace(contentType.MatchSingular) && string.IsNullOrWhiteSpace(contentType.MatchPlural))
    {
        markdown += "**WARNING:** This file likely generated completely incorrectly!\n\n";
    }

    markdown += $"- **Required by core package:** {(contentType.RequiredByCorePackage ? "Yes" : "No")}\n";
    if (contentType.AltNames is { Length: >0 } altNames)
    {
        markdown += $"- **Alternate names:** {string.Join(", ", altNames)}\n";
    }
    markdown += "\n";

    markdown += "## Attributes\n\n";

    markdown += string.Join("\n", contentType.XmlAttributes.Select(a => a.ToBulletPoint()));
    
    markdown += "\n\n";

    if (contentType.IsSubmarineType)
    {
        markdown += "This content type is created in the submarine editor.";
    }
    else
    {
        XElement singularExample(int index = 0)
        {
            IEnumerable<XAttribute> attributes = new[]
            {
                new XAttribute("identifier", $"my{matchSingular}{(index > 0 ? index : "")}")
            };
            return new XElement(matchSingular, attributes);
        }
        XElement pluralExample = new XElement(matchPlural,
            singularExample(1),
            singularExample(2));
        XElement overrideExample = new XElement("override",
            singularExample(1),
            singularExample(2));

        
        string xmlToMarkdown(XElement element)
        {
            var xmlStringBuilder = new StringBuilder();
            using (var xmlWriter = XmlWriter.Create(xmlStringBuilder, xmlWriterSettings))
            {
                element.Save(xmlWriter);
            }
            return $"```xml\n{xmlStringBuilder}\n```\n\n";
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
