using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace BaroAutoDoc.PluginDocs;

/// <summary>
/// The XML documentation comment (<c>///</c>) attached to a declaration, parsed into XML.
/// </summary>
internal sealed class DocComment
{
    public static readonly DocComment Empty = new(new XElement("root"));

    public XElement Root { get; }

    private DocComment(XElement root)
    {
        Root = root;
    }

    public bool IsEmpty => !Root.HasElements && string.IsNullOrWhiteSpace(Root.Value);

    public XElement? Summary => Root.Element("summary");
    public XElement? Remarks => Root.Element("remarks");
    public XElement? Returns => Root.Element("returns");
    public XElement? Value => Root.Element("value");
    public IEnumerable<XElement> Examples => Root.Elements("example");
    public IEnumerable<XElement> Params => Root.Elements("param");
    public IEnumerable<XElement> TypeParams => Root.Elements("typeparam");
    public IEnumerable<XElement> Exceptions => Root.Elements("exception");

    public static DocComment From(SyntaxNode node)
    {
        StringBuilder sb = new();
        foreach (SyntaxTrivia trivia in node.GetLeadingTrivia())
        {
            if (!trivia.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia)) { continue; }

            foreach (string rawLine in trivia.ToFullString().Split('\n'))
            {
                string line = rawLine.TrimStart();
                if (!line.StartsWith("///", StringComparison.Ordinal)) { continue; }

                line = line.Substring(3);
                if (line.StartsWith(' ')) { line = line.Substring(1); }
                sb.Append(line.TrimEnd('\r')).Append('\n');
            }
        }

        if (sb.Length == 0) { return Empty; }

        try
        {
            return new DocComment(XElement.Parse($"<root>{sb}</root>"));
        }
        catch (Exception e)
        {
            Console.WriteLine($"WARNING: Failed to parse documentation comment ({e.Message}):\n{sb}");
            return Empty;
        }
    }

    /// <summary>
    /// Finds the <c>&lt;param&gt;</c> or <c>&lt;typeparam&gt;</c> element with the given name.
    /// </summary>
    public static XElement? FindNamed(IEnumerable<XElement> elements, string name)
        => elements.FirstOrDefault(e => string.Equals((string?)e.Attribute("name"), name, StringComparison.Ordinal));
}

/// <summary>
/// Converts the contents of documentation comment elements into markdown.
/// </summary>
internal sealed class DocRenderer
{
    private readonly Func<string, string> crefResolver;

    /// <param name="crefResolver">Converts the value of a <c>cref</c> attribute into inline markdown.</param>
    public DocRenderer(Func<string, string> crefResolver)
    {
        this.crefResolver = crefResolver;
    }

    public static string EscapeText(string text)
        => text.Replace("<", "&lt;").Replace(">", "&gt;");

    public static string Cell(string text)
        => text.Replace("|", "\\|");

    /// <summary>
    /// Renders the element's content as a single inline run of markdown, any code blocks are flattened to inline code.
    /// </summary>
    public string Inline(XElement? element)
        => element is null ? string.Empty : Inline(element.Nodes());

    public string Inline(IEnumerable<XNode> nodes)
    {
        StringBuilder sb = new();
        foreach (XNode node in nodes)
        {
            switch (node)
            {
                case XText text:
                    sb.Append(EscapeText(Regex.Replace(text.Value, @"\s+", " ")));
                    break;
                case XElement element:
                    sb.Append(InlineElement(element));
                    break;
            }
        }

        string result = Regex.Replace(sb.ToString(), @" {2,}", " ");
        result = Regex.Replace(result, @" ?\n ?", "\n");
        return result.Trim();
    }

    private string InlineElement(XElement element)
    {
        switch (element.Name.LocalName)
        {
            case "see":
            case "seealso":
                if (element.Attribute("cref") is { } cref) { return crefResolver(cref.Value); }
                if (element.Attribute("langword") is { } langword) { return $"`{langword.Value}`"; }
                if (element.Attribute("href") is { } href)
                {
                    string label = Inline(element);
                    return $"[{(label.Length > 0 ? label : href.Value)}]({href.Value})";
                }
                return Inline(element);
            case "paramref":
            case "typeparamref":
                return $"`{element.Attribute("name")?.Value}`";
            case "c":
                return $"`{element.Value.Trim()}`";
            case "code":
                return $"`{element.Value.Trim()}`";
            case "br":
                return "<br/>";
            case "para":
                return $"\n\n{Inline(element)}\n\n";
            case "a":
                return $"[{Inline(element)}]({element.Attribute("href")?.Value})";
            case "list":
                return "\n\n" + string.Join("\n", element.Elements("item").Select(item =>
                {
                    XElement? term = item.Element("term");
                    XElement? description = item.Element("description");
                    string text = term is not null && description is not null
                        ? $"{Inline(term)}: {Inline(description)}"
                        : Inline(description ?? term ?? item);
                    return $"- {text}";
                })) + "\n\n";
            default:
                return Inline(element);
        }
    }

    /// <summary>
    /// Renders the element's content as markdown blocks, paragraphs of text separated from <c>&lt;code&gt;</c> blocks.
    /// </summary>
    public string Blocks(XElement? container)
    {
        if (container is null) { return string.Empty; }

        List<string> parts = new();
        List<XNode> buffer = new();

        void Flush()
        {
            if (buffer.Count == 0) { return; }

            string text = Inline(buffer);
            buffer.Clear();
            if (text.Length > 0) { parts.Add(text); }
        }

        foreach (XNode node in container.Nodes())
        {
            if (node is XElement { Name.LocalName: "code" } code)
            {
                Flush();
                parts.Add(CodeBlock(code.Value, (string?)code.Attribute("lang")));
            }
            else
            {
                buffer.Add(node);
            }
        }

        Flush();
        return string.Join("\n\n", parts);
    }

    /// <summary>
    /// Renders an <c>&lt;example&gt;</c> element. Some examples contain code that is not wrapped in a
    /// <c>&lt;code&gt;</c> element, those are rendered as a single code block.
    /// </summary>
    public string Example(XElement example)
        => example.Elements("code").Any()
            ? Blocks(example)
            : CodeBlock(example.Value, null);

    public static string CodeBlock(string value, string? lang)
    {
        string[] lines = value.Replace("\r", "").Split('\n');

        int start = 0;
        int end = lines.Length;
        while (start < end && string.IsNullOrWhiteSpace(lines[start])) { start++; }
        while (end > start && string.IsNullOrWhiteSpace(lines[end - 1])) { end--; }

        string[] used = lines[start..end];
        int indent = used
            .Where(static l => !string.IsNullOrWhiteSpace(l))
            .Select(static l => l.Length - l.TrimStart().Length)
            .DefaultIfEmpty(0)
            .Min();

        string text = string.Join("\n", used.Select(l => l.Length >= indent ? l.Substring(indent).TrimEnd() : l.Trim()));
        lang ??= text.TrimStart().StartsWith('<') ? "xml" : "csharp";
        return $"```{lang}\n{text}\n```";
    }
}
