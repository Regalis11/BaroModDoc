using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace BaroAutoDoc.PluginDocs;

/// <summary>
/// Describes a generated page: the source files it is generated from and, for pages that have no single
/// main type (such as the list of hooks), a title and an introduction.
/// </summary>
internal sealed record PageSpec(string FileName, string[] SourceFiles, string? Title = null, string? Introduction = null);

internal sealed class PluginPage
{
    public PageSpec Spec = null!;
    public List<TypeDoc> Types = new();
    public TypeDoc? Main;
    public string Title => Spec.Title ?? Main?.Name ?? Path.GetFileNameWithoutExtension(Spec.FileName);
}

/// <summary>
/// Writes <see cref="PluginPage"/>s as markdown.
/// </summary>
internal sealed class PluginPageWriter
{
    private const int MaxSignatureLength = 110;

    private readonly DocRenderer renderer;
    private readonly Dictionary<string, PluginPage> pageOfType;
    private PluginPage? currentPage;

    public PluginPageWriter(IReadOnlyList<PluginPage> pages)
    {
        pageOfType = new Dictionary<string, PluginPage>();
        foreach (PluginPage page in pages)
        {
            foreach (TypeDoc type in page.Types)
            {
                pageOfType.TryAdd(type.Name, page);
            }
        }

        renderer = new DocRenderer(ResolveCref);
    }

    public string WritePage(PluginPage page)
    {
        currentPage = page;
        PageContext context = new(page);
        StringBuilder md = new();

        Heading(md, context, 1, page.Title);

        if (page.Main is { } main)
        {
            WriteTypeBody(md, context, main, 1, isMainType: true);
        }
        else if (page.Spec.Introduction is { } introduction)
        {
            Paragraph(md, introduction);
        }

        List<TypeDoc> others = page.Types.Where(t => t != page.Main).ToList();
        if (page.Main is null && others.Count > 3)
        {
            Page.Table overview = new() { HeadRow = new Page.Table.Row("Name", "Description") };
            foreach (TypeDoc type in others)
            {
                overview.BodyRows.Add(new Page.Table.Row(
                    $"[`{type.Name}`](#{Slug(type.Name)})",
                    DocRenderer.Cell(FirstParagraph(renderer.Blocks(type.Doc.Summary)))));
            }

            md.Append(overview.ToMarkdown().Trim('\n')).Append("\n\n");
        }

        if (others.Count > 0)
        {
            int level = 2;
            if (page.Main is not null)
            {
                Heading(md, context, 2, "Related types");
                level = 3;
            }

            foreach (TypeDoc type in others)
            {
                Heading(md, context, level, type.Name);
                WriteTypeBody(md, context, type, level, isMainType: false);
            }
        }

        return Regex.Replace(md.ToString().TrimEnd(), @"\n{3,}", "\n\n") + "\n";
    }

    public string WriteIndex(IReadOnlyList<PluginPage> pages)
    {
        StringBuilder md = new();
        md.Append("# Plugin services\n\n");
        Paragraph(md, "Plugin services are the interfaces plugins use to interact with the game. " +
                      "Get a service in your plugin with `PluginServiceProvider.GetService<T>()`, for example:");
        Paragraph(md, "```cs\npublic static readonly IItemComponentRegistrar ItemComponentRegistrar =\n    PluginServiceProvider.GetService<IItemComponentRegistrar>();\n```");
        Paragraph(md, "This documentation is generated from the documentation comments in the game's code.");

        Page.Table table = new() { HeadRow = new Page.Table.Row("Service", "Description") };
        foreach (PluginPage page in pages)
        {
            string description = page.Main is { } main
                ? FirstParagraph(renderer.Blocks(main.Doc.Summary))
                : page.Spec.Introduction ?? "";
            table.BodyRows.Add(new Page.Table.Row($"[{page.Title}]({page.Spec.FileName})", DocRenderer.Cell(description)));
        }

        md.Append(table.ToMarkdown()).Append('\n');
        return md.ToString();
    }

    private sealed class PageContext
    {
        public readonly PluginPage Page;
        private readonly Dictionary<string, int> idCounts = new();

        public PageContext(PluginPage page)
        {
            Page = page;
        }

        /// <summary>
        /// Registers a heading and returns the id the markdown renderer will generate for it.
        /// </summary>
        public string RegisterHeading(string text)
        {
            string id = Slug(text);
            idCounts.TryGetValue(id, out int count);
            idCounts[id] = count + 1;
            return count == 0 ? id : $"{id}-{count}";
        }
    }

    private static string Slug(string text)
    {
        string id = Regex.Replace(text, "^[^a-zA-Z]+", "");
        id = Regex.Replace(id, "[^a-zA-Z0-9 -]", "").Replace(' ', '-').ToLowerInvariant();
        return id.Length == 0 ? "section" : id;
    }

    private static string Heading(StringBuilder md, PageContext context, int level, string text)
    {
        string id = context.RegisterHeading(text);
        md.Append(new string('#', level)).Append(' ').Append(text).Append("\n\n");
        return id;
    }

    private static void Paragraph(StringBuilder md, string text)
    {
        if (string.IsNullOrWhiteSpace(text)) { return; }

        md.Append(text.Trim('\n')).Append("\n\n");
    }

    private static string FirstParagraph(string text)
        => text.Split("\n\n")[0];

    private static string AvailabilityNote(Availability availability)
        => availability switch
        {
            PluginDocs.Availability.ClientOnly => "Only available on the client.",
            PluginDocs.Availability.ServerOnly => "Only available on the server.",
            _ => ""
        };

    private void WriteTypeBody(StringBuilder md, PageContext context, TypeDoc type, int level, bool isMainType)
    {
        Paragraph(md, $"```cs\n{Wrap(type.Declaration)}\n```");
        if (AvailabilityNote(type.Availability) is { Length: > 0 } availability) { Paragraph(md, $"*{availability}*"); }

        Paragraph(md, renderer.Blocks(type.Doc.Summary));

        WriteSection(md, context, level + 1, isMainType, "Remarks", renderer.Blocks(type.Doc.Remarks));

        foreach (XElement example in type.Doc.Examples)
        {
            WriteSection(md, context, level + 1, isMainType, "Example", renderer.Example(example));
        }

        if (type.Keyword == "delegate" && type.DelegateParameters is { Parameters.Count: > 0 } parameters)
        {
            WriteParameters(md, type.Doc, parameters.Parameters.Select(static p => (p.Identifier.Text, p.Type?.ToString() ?? "")));
            WriteReturns(md, type.Doc);
        }

        if (type.EnumValues.Count > 0)
        {
            Page.Table table = new() { HeadRow = new Page.Table.Row("Value", "Description") };
            foreach (EnumValueDoc value in type.EnumValues)
            {
                string name = value.Value is null ? value.Name : $"{value.Name} = {value.Value}";
                table.BodyRows.Add(new Page.Table.Row(DocRenderer.Cell($"`{name}`"), DocRenderer.Cell(renderer.Inline(value.Doc.Summary))));
            }

            md.Append(table.ToMarkdown()).Append('\n');
        }

        if (type.Members.Count == 0) { return; }

        // Members of the main type get a heading per kind of member and an overview table, members of other types are listed directly.
        List<(MemberKind Kind, string Title)> groups = new()
        {
            (MemberKind.Constructor, "Constructors"),
            (MemberKind.Property, "Properties"),
            (MemberKind.Indexer, "Indexers"),
            (MemberKind.Field, "Fields"),
            (MemberKind.Method, "Methods"),
            (MemberKind.Event, "Events")
        };

        if (isMainType)
        {
            Heading(md, context, level + 1, "Members");
            const string overviewPlaceholder = "@@OVERVIEW@@";
            md.Append(overviewPlaceholder).Append("\n\n");

            Page.Table overview = new() { HeadRow = new Page.Table.Row("Member", "Description") };
            foreach ((MemberKind kind, string title) in groups)
            {
                List<MemberDoc> members = type.Members.Where(m => m.Kind == kind).ToList();
                if (members.Count == 0) { continue; }

                Heading(md, context, level + 1, title);
                foreach (MemberDoc member in members)
                {
                    string id = Heading(md, context, level + 2, member.Name);
                    WriteMember(md, type, member);
                    overview.BodyRows.Add(new Page.Table.Row(
                        $"[`{DocRenderer.Cell(DisplayName(member))}`](#{id})",
                        DocRenderer.Cell(FirstParagraph(renderer.Blocks(member.Doc.Summary)))));
                }
            }

            md.Replace(overviewPlaceholder, overview.ToMarkdown().Trim('\n'));
        }
        else
        {
            foreach (MemberDoc member in type.Members)
            {
                Heading(md, context, level + 1, member.Name);
                WriteMember(md, type, member);
            }
        }
    }

    private void WriteSection(StringBuilder md, PageContext context, int level, bool asHeading, string title, string content)
    {
        if (string.IsNullOrWhiteSpace(content)) { return; }

        if (asHeading)
        {
            Heading(md, context, level, title);
        }
        else
        {
            Paragraph(md, $"**{title}**");
        }

        Paragraph(md, content);
    }

    private void WriteMember(StringBuilder md, TypeDoc type, MemberDoc member)
    {
        Paragraph(md, $"```cs\n{Wrap(member.Signature)}\n```");
        if (AvailabilityNote(member.Availability) is { Length: > 0 } availability) { Paragraph(md, $"*{availability}*"); }

        DocComment doc = member.Doc;
        if (doc.IsEmpty)
        {
            Console.WriteLine($"WARNING: {type.Name}.{member.Name} has no documentation comment.");
        }

        Paragraph(md, renderer.Blocks(doc.Summary));

        if (doc.TypeParams.Any())
        {
            Page.Table table = new() { HeadRow = new Page.Table.Row("Type parameter", "Description") };
            foreach (XElement typeParam in doc.TypeParams)
            {
                table.BodyRows.Add(new Page.Table.Row($"`{(string?)typeParam.Attribute("name")}`", DocRenderer.Cell(renderer.Inline(typeParam))));
            }

            md.Append(table.ToMarkdown()).Append('\n');
        }

        ParameterListSyntaxView? parameters = ParametersOf(member);
        if (parameters is not null)
        {
            WriteParameters(md, doc, parameters.Value.Parameters);
        }

        WriteReturns(md, doc);

        if (doc.Value is { } value)
        {
            Paragraph(md, $"**Value:** {renderer.Inline(value)}");
        }

        if (doc.Exceptions.Any())
        {
            Page.Table table = new() { HeadRow = new Page.Table.Row("Exception", "Condition") };
            foreach (XElement exception in doc.Exceptions)
            {
                string cref = (string?)exception.Attribute("cref") ?? "";
                table.BodyRows.Add(new Page.Table.Row(DocRenderer.Cell(ResolveCref(cref)), DocRenderer.Cell(renderer.Inline(exception))));
            }

            md.Append(table.ToMarkdown()).Append('\n');
        }

        if (doc.Remarks is not null)
        {
            Paragraph(md, "**Remarks**");
            Paragraph(md, renderer.Blocks(doc.Remarks));
        }

        foreach (XElement example in doc.Examples)
        {
            Paragraph(md, "**Example**");
            Paragraph(md, renderer.Example(example));
        }
    }

    private void WriteParameters(StringBuilder md, DocComment doc, IEnumerable<(string Name, string Type)> parameters)
    {
        List<(string Name, string Type)> list = parameters.ToList();
        if (list.Count == 0 || !list.Any(p => DocComment.FindNamed(doc.Params, p.Name) is not null)) { return; }

        Page.Table table = new() { HeadRow = new Page.Table.Row("Parameter", "Type", "Description") };
        foreach ((string name, string type) in list)
        {
            XElement? description = DocComment.FindNamed(doc.Params, name);
            table.BodyRows.Add(new Page.Table.Row($"`{name}`", DocRenderer.Cell($"`{type}`"), DocRenderer.Cell(renderer.Inline(description))));
        }

        md.Append(table.ToMarkdown()).Append('\n');
    }

    private void WriteReturns(StringBuilder md, DocComment doc)
    {
        if (doc.Returns is { } returns)
        {
            Paragraph(md, $"**Returns:** {renderer.Inline(returns)}");
        }
    }

    private readonly record struct ParameterListSyntaxView(IReadOnlyList<(string Name, string Type)> Parameters);

    private static ParameterListSyntaxView? ParametersOf(MemberDoc member)
    {
        Microsoft.CodeAnalysis.CSharp.Syntax.BaseParameterListSyntax? list = member.Syntax switch
        {
            Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax m => m.ParameterList,
            Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorDeclarationSyntax c => c.ParameterList,
            Microsoft.CodeAnalysis.CSharp.Syntax.IndexerDeclarationSyntax i => i.ParameterList,
            _ => null
        };

        if (list is null || list.Parameters.Count == 0) { return null; }

        return new ParameterListSyntaxView(list.Parameters.Select(static p => (p.Identifier.Text, p.Type?.ToString() ?? "")).ToList());
    }

    private static string DisplayName(MemberDoc member)
    {
        Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterListSyntax? typeParameters =
            (member.Syntax as Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax)?.TypeParameterList;
        return member.Kind switch
        {
            MemberKind.Method => $"{member.Name}{typeParameters}()",
            MemberKind.Constructor => $"{member.Name}()",
            _ => member.Name
        };
    }

    /// <summary>
    /// Puts each parameter of a long signature on its own line.
    /// </summary>
    private static string Wrap(string signature)
    {
        if (signature.Length <= MaxSignatureLength) { return signature; }

        int open = signature.IndexOf('(');
        if (open < 0) { return signature; }

        int depth = 0;
        int close = -1;
        List<string> parameters = new();
        StringBuilder current = new();
        for (int i = open + 1; i < signature.Length; i++)
        {
            char c = signature[i];
            if (c is '(' or '<' or '[' or '{') { depth++; }
            else if (c is ')' && depth == 0) { close = i; break; }
            else if (c is ')' or '>' or ']' or '}') { depth--; }

            if (c == ',' && depth == 0)
            {
                parameters.Add(current.ToString().Trim());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }

        if (close < 0) { return signature; }

        parameters.Add(current.ToString().Trim());
        return signature.Substring(0, open + 1)
               + "\n    " + string.Join(",\n    ", parameters)
               + signature.Substring(close);
    }

    /// <summary>
    /// Turns the value of a <c>cref</c> attribute into inline code, linked to the type's page when it is documented here.
    /// </summary>
    private string ResolveCref(string cref)
    {
        string name = cref;
        if (name.Length > 2 && name[1] == ':') { name = name.Substring(2); }

        int paren = name.IndexOf('(');
        if (paren >= 0) { name = name.Substring(0, paren); }

        name = name.Replace('{', '<').Replace('}', '>');
        string[] segments = name.Split('.');

        if (segments.Length == 1 && currentPage is not null)
        {
            string memberName = segments[0].Split('<')[0];
            if (currentPage.Types.Any(t => t.Members.Any(m => m.Name == memberName)))
            {
                return $"[`{segments[0]}`](#{Slug(memberName)})";
            }
        }

        // Keep Type.Member when the type is documented here, otherwise just the last segment.
        string display = segments[^1];
        string? typeName = null;
        if (segments.Length >= 2 && pageOfType.ContainsKey(segments[^2]))
        {
            typeName = segments[^2];
            display = $"{segments[^2]}.{segments[^1]}";
        }
        else if (pageOfType.ContainsKey(segments[^1]))
        {
            typeName = segments[^1];
        }

        string code = $"`{display}`";
        if (typeName is null) { return code; }

        PluginPage page = pageOfType[typeName];
        string anchor = page.Main?.Name == typeName ? "" : $"#{Slug(typeName)}";
        if (segments.Length >= 2 && display.Contains('.'))
        {
            anchor = $"#{Slug(segments[^1].Split('<')[0])}";
        }

        return $"[{code}]({page.Spec.FileName}{anchor})";
    }
}
