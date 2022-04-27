using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using BaroAutoDoc.SyntaxWalkers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace BaroAutoDoc.Commands;

public class BaseRip : Command
{
    public void Invoke(string repoPath = "C:/Users/juanj/Desktop/Repos/Barotrauma-development")
    {
        Directory.SetCurrentDirectory(repoPath);

        const string srcPathFmt = "Barotrauma/Barotrauma{0}/{0}Source";
        const string contentFilePathFmt = $"{srcPathFmt}/ContentManagement/ContentFile";
        string[] srcPathParams = {"Shared"};

        var contentTypeFinder = new ContentTypeFinder();
        foreach (string p in srcPathParams)
        {
            string contentFilePath = string.Format(contentFilePathFmt, p);
            contentTypeFinder.VisitAllInDirectory(contentFilePath);
        }

        ContentType[] contentTypes = contentTypeFinder.ContentTypes.ToArray();

        foreach (string p in srcPathParams)
        {
            string srcPath = string.Format(srcPathFmt, p);
            for (int i = 0; i < contentTypes.Length; i++)
            {
                var attrRipper = new AttributeRipper(contentTypes[i]);
                string typeToLookFor;
                do
                {
                    typeToLookFor = attrRipper.TypeToLookFor;
                    attrRipper.VisitAllInDirectory(srcPath);
                } while (typeToLookFor != attrRipper.TypeToLookFor);
                contentTypes[i] = attrRipper.ContentType;
            }
        }

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

            string[] shorthands = { "Client", "Server", "Shared" };

            string pickShorthand(string file)
                => shorthands.First(s => file.Contains($"Barotrauma{s}"));

            Page markdown = new Page();
            markdown.Title = contentType.Name;
            var relevantFilesSup = new Page.Superscript();
            relevantFilesSup.Children.Add(new Page.RawText("Relevant files:"));
            foreach (var file in contentType.RelevantFiles.Distinct())
            {
                relevantFilesSup.Children.Add(new Page.Hyperlink(
                    Url: $"https://github.com/Regalis11/Barotrauma/blob/master/{file}",
                    Text: $"[{pickShorthand(file)}:{Path.GetFileName(file)}]"));
            }

            markdown.Body.Components.Add(relevantFilesSup);
            markdown.Body.AddNewLine();

            markdown.Body.Components.Add(new Page.NewLine());
            markdown.Body.Components.Add(new Page.InlineMarkdown("*This page was generated automatically.*\n\n"));

            markdown.Body.Components.Add(new Page.InlineMarkdown(
                $"- **Required by core package:** {(contentType.RequiredByCorePackage ? "Yes" : "No")}\n"));
            if (contentType.AltNames is { Length: > 0 } altNames)
            {
                markdown.Body.Components.Add(
                    new Page.InlineMarkdown($"- **Alternate names:** {string.Join(", ", altNames)}\n"));
            }

            markdown.Body.AddNewLine();

            if (contentType.IsSubmarineType)
            {
                markdown.Body.Components.Add(
                    new Page.InlineMarkdown("This content type is created in the submarine editor."));
            }
            else
            {
                if (contentType.XmlSubElements.Any())
                {
                    var childElementsSection = new Page.Section();
                    childElementsSection.Title = "Child elements";
                    markdown.Subsections.Add(childElementsSection);

                    var elemList = new Page.BulletList();
                    childElementsSection.Body.Components.Add(elemList);
                    foreach (var elem in contentType.XmlSubElements)
                    {
                        elemList.Items.Add(new Page.InlineMarkdown($"`{elem}`"));
                    }
                }
                if (contentType.XmlAttributes.Any())
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
                }
            }

            File.WriteAllText($"markdown/ContentTypes/{contentType.Name}.md", markdown.ToMarkdown());
        }

        File.WriteAllText("markdown/ContentTypes.md",
            string.Join("\n", contentTypeFinder.ContentTypes.Select(t
                => $"- [{t.Name}](ContentTypes/{t.Name}.md)")));
    }
}
