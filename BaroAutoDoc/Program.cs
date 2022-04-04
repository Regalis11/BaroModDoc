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
    return;
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
    
    string transformStringIfNotNull(string? s, Func<string, string> t)
        => s is null ? "AUTODOC_FAIL" : t(s);
    matchSingular ??= transformStringIfNotNull(matchPlural, s => s[..^1]);
    matchPlural ??= transformStringIfNotNull(matchSingular, s => $"{s}s");

    var xmlWriterSettings = new XmlWriterSettings
    {
        Indent = true,
        OmitXmlDeclaration = true,
        NewLineOnAttributes = true
    };

    string[] shorthands = {"Client", "Server", "Shared"};

    string pickShorthand(string file)
        => shorthands.First(s => file.Contains($"Barotrauma{s}"));

    Page markdown = new Page();
    markdown.Title = contentType.Name;
    var relevantFilesSup = new Page.Superscript();
    relevantFilesSup.Children.Add(new Page.RawText("Relevant files:"));
    foreach (var file in contentType.RelevantFiles)
    {
        relevantFilesSup.Children.Add(new Page.Hyperlink(
            Url: $"https://github.com/Regalis11/Barotrauma/blob/master/{file}",
            Text: $"[{pickShorthand(file)}:{Path.GetFileName(file)}]"));
    }
    markdown.Body.Components.Add(relevantFilesSup);
    markdown.Body.AddNewLine();

    /*if (string.IsNullOrWhiteSpace(contentType.MatchSingular) && string.IsNullOrWhiteSpace(contentType.MatchPlural))
    {
        markdown.Body.Components.Add(new Page.InlineMarkdown("**WARNING:** This file likely generated completely incorrectly!\n\n"));
    }*/

    markdown.Body.Components.Add(new Page.InlineMarkdown($"- **Required by core package:** {(contentType.RequiredByCorePackage ? "Yes" : "No")}\n"));
    if (contentType.AltNames is { Length: >0 } altNames)
    {
        markdown.Body.Components.Add(new Page.InlineMarkdown($"- **Alternate names:** {string.Join(", ", altNames)}\n"));
    }

    markdown.Body.AddNewLine();

    if (contentType.IsSubmarineType)
    {
        markdown.Body.Components.Add(new Page.InlineMarkdown("This content type is created in the submarine editor."));
    }
    else
    {
        var attributesSection = new Page.Section();
        attributesSection.Title = "Attributes";
        markdown.Subsections.Add(attributesSection);

        var attrList = new Page.BulletList();
        attributesSection.Body.Components.Add(attrList);
        foreach (var attr in contentType.XmlAttributes)
        {
            attrList.Items.Add(attr.ToBulletPoint());
        }
        attributesSection.Body.AddNewLine();

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

        
        Page.CodeBlock xmlToMarkdown(XElement element)
        {
            var xmlStringBuilder = new StringBuilder();
            using (var xmlWriter = XmlWriter.Create(xmlStringBuilder, xmlWriterSettings))
            {
                element.Save(xmlWriter);
            }
            return new Page.CodeBlock("xml", xmlStringBuilder.ToString());
        }

        /*
        var examplesSection = new Page.Section();
        examplesSection.Title = "Examples";
        markdown.Subsections.Add(examplesSection);

        var example1 = new Page.Section();
        example1.Title = $"Example 1 - single {matchSingular}";
        examplesSection.Subsections.Add(example1);
        example1.Body.Components.Add(xmlToMarkdown(singularExample()));
        
        var example2 = new Page.Section();
        example2.Title = $"Example 2 - multiple {matchPlural}";
        examplesSection.Subsections.Add(example2);
        example2.Body.Components.Add(xmlToMarkdown(pluralExample));
        
        var example3 = new Page.Section();
        example3.Title = $"Example 3 - overriding existing {matchPlural}";
        examplesSection.Subsections.Add(example3);
        example3.Body.Components.Add(xmlToMarkdown(overrideExample));
        */
    }
    
    File.WriteAllText($"markdown/ContentTypes/{contentType.Name}.md", markdown.ToMarkdown());
}

File.WriteAllText("markdown/index.md",
    "# Barotrauma modding guide\n\n"
    + "## Content types\n\n"
    + string.Join("\n", contentTypeFinder.ContentTypes.Select(t
        => $"- [{t.Name}](ContentTypes/{t.Name}.md)")));
