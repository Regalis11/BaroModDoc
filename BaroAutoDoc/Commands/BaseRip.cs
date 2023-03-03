using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using BaroAutoDoc.SyntaxWalkers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.Commands;

sealed class BaseRip : Command
{
    public void Invoke()
    {
        Directory.SetCurrentDirectory(GlobalConfig.RepoPath);

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
                var contentType = contentTypes[i];
                
                int numSharedStart(string a, string b)
                {
                    for (int i=0;i<Math.Min(a.Length, b.Length);i++)
                    {
                        if (a[i] == b[i]) { continue; }
                        return i;
                    }
                    return Math.Min(a.Length, b.Length);
                }
        
                var typeToLookFor = contentType.ConstructedTypes
                    .OrderByDescending(t => numSharedStart(t, contentType.Name))
                    .ThenBy(t => t).FirstOrDefault() ?? "";
                if (string.IsNullOrEmpty(typeToLookFor))
                {
                    Console.WriteLine($"No constructed types from {contentType.Name}");
                }
                else
                {
                    Console.WriteLine($"Extracting attributes for {contentType.Name}: Starting with {typeToLookFor}");
                }
                
                var attrRipper = new ContentTypeAttributeRipper(contentType, typeToLookFor);
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
                    var childElementsSection = new Page.Section
                    {
                        Title = "Child elements"
                    };
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
                    var attributesSection = new Page.Section
                    {
                        Title = "Attributes"
                    };
                    markdown.Subsections.Add(attributesSection);

                    Page.Table attributesTable = new()
                    {
                        HeadRow = new Page.Table.Row("Attribute", "Type", "Default value", "Description")
                    };

                    attributesSection.Body.Components.Add(attributesTable);
                    foreach (var attr in contentType.XmlAttributes)
                    {
                        attributesTable.BodyRows.Add(new Page.Table.Row(attr.Name, attr.Type, attr.DefaultValue, attr.Description));
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

    public static bool ConstructEnumTable(EnumDeclarationSyntax syntax, [NotNullWhen(true)] out ImmutableArray<Page.Section>? result)
    {
        List<ParsedEnum> enums = new();

        List<EnumValue> enumMembers = new();
        foreach (var enumMember in syntax.Members)
        {
            enumMembers.Add(new EnumValue(enumMember.Identifier.ValueText, enumMember.FindCommentAttachedToMember().Text));
        }

        enums.Add(new ParsedEnum(syntax.Identifier.ValueText, enumMembers.ToImmutableArray()));

        return ConstructEnumTable(enums, out result);
    }


    public static bool ConstructEnumTable(IReadOnlyCollection<ParsedEnum> enums, [NotNullWhen(true)] out ImmutableArray<Page.Section>? result)
    {
        if (!enums.Any())
        {
            result = null;
            return false;
        }

        var builder = ImmutableArray.CreateBuilder<Page.Section>();
        foreach (var (type, values) in enums)
        {
            Page.Section section = new()
            {
                Title = type
            };

            Page.Table table = new()
            {
                HeadRow = new Page.Table.Row("Value", "Description")
            };

            foreach (var (value, description) in values)
            {
                table.BodyRows.Add(new Page.Table.Row(value, description));
            }

            section.Body.Components.Add(table);

            builder.Add(section);
        }

        result = builder.ToImmutable();
        return true;
    }
}
