﻿using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc;

public readonly record struct CodeComment(string Text, XElement Element)
{
    public static CodeComment Empty(string text) => new CodeComment(text, new XElement("root"));
}

public static class Extensions
{
    public static void AddRange<T>(this ImmutableHashSet<T>.Builder builder, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            builder.Add(item);
        }
    }

    public static IReadOnlyCollection<BlockSyntax> FindInitializerMethodBodies(this TypeDeclarationSyntax cls, params string[] methodNames)
    {
        List<BlockSyntax> bodies = new();

        foreach (MemberDeclarationSyntax member in cls.Members)
        {
            switch (member)
            {
                // always include constructor
                case ConstructorDeclarationSyntax { Body: { } body }:
                    bodies.Add(body);
                    break;
                case MethodDeclarationSyntax { Body: { } body, Identifier.Text: var name } when methodNames.Contains(name):
                    bodies.Add(body);
                    break;
            }
        }

        return bodies.ToImmutableArray();
    }

    public static int FindIndex<T>(this IReadOnlyList<T> list, Predicate<T> predicate)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (predicate(list[i]))
            {
                return i;
            }
        }

        return -1;
    }

    public static string GetIdentifierString(this IdentifierNameSyntax identifier) => identifier.Identifier.Text;
    public static string GetIdentifierString(this SimpleNameSyntax name) => name.Identifier.Text;
    public static T? OfType<T>(this BinaryExpressionSyntax binaryExpression) where T : ExpressionSyntax => binaryExpression.Left as T ?? binaryExpression.Right as T;

    public static string EvaluateAsCSharpExpression(this string expr)
    {
        while (true)
        {
            int nameofIndex = expr.IndexOf("nameof(", StringComparison.Ordinal);
            if (nameofIndex < 0) { break; }

            int indexOfClosingParenthesis = expr.IndexOf(")", nameofIndex, StringComparison.Ordinal);
            if (indexOfClosingParenthesis < 0) { break; }

            string name = expr[nameofIndex..indexOfClosingParenthesis];
            name = name[(name.IndexOf("(", StringComparison.Ordinal)+1)..];

            int indexOfLastDot = name.LastIndexOf(".", StringComparison.Ordinal);
            if (indexOfLastDot > 0) { name = name[(indexOfLastDot+1)..]; }

            expr = expr[..nameofIndex]+$"\"{name}\""+expr[(indexOfClosingParenthesis+1)..];
        }
        
        return CSharpScript.EvaluateAsync<string>(expr).Result;
    }

    public static string GuessCaseFromMemberName(this string xmlIdentifier, string fieldName)
    {
        string guyWithAllTheCaps =
            xmlIdentifier.Count(char.IsUpper) > fieldName.Count(char.IsUpper)
                ? xmlIdentifier
                : fieldName;

        if (xmlIdentifier.Equals(guyWithAllTheCaps, StringComparison.OrdinalIgnoreCase))
        {
            return guyWithAllTheCaps;
        }

        var words = new List<string>();
        int wordStart = 0; string pendingWord = "";
        for (int i = 0; i < guyWithAllTheCaps.Length; i++)
        {
            if (char.IsUpper(guyWithAllTheCaps[i]) && wordStart != i)
            {
                words.Add(pendingWord);
                wordStart = i;
                pendingWord = "";
            }

            pendingWord += guyWithAllTheCaps[i];
        }
        if (!string.IsNullOrEmpty(pendingWord)) { words.Add(pendingWord); }

        string result = "";

        foreach (var word in words)
        {
            string concat = result+word;
            if (xmlIdentifier.StartsWith(concat, StringComparison.OrdinalIgnoreCase))
            {
                result = concat;
            }
        }

        if (xmlIdentifier.Length > result.Length)
        {
            if (xmlIdentifier.Length > result.Length+1)
            {
                result += char.ToUpper(xmlIdentifier[result.Length])
                          + xmlIdentifier[(result.Length+1)..];
            }
            else
            {
                result += xmlIdentifier[result.Length];
            }
        }

        return result;
    }
    
    public static XElement? ElementOfName(this XElement element, string name, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        => element.Elements().FirstOrDefault(e => e.Name.LocalName.Equals(name, comparison));

    public static TypeDeclarationSyntax? GetDeclaringType(this SyntaxNode node)
    {
        SyntaxNode? parentNode = node;
        do
        {
            parentNode = parentNode?.Parent;
        }
        while (parentNode is not TypeDeclarationSyntax);
        return parentNode as TypeDeclarationSyntax;
    }
    
    public static CodeComment FindCommentAttachedToMember(this SyntaxNode member)
    {
        char[] trimChars =
        {
            // Whitespace
            ' ',
            '\t',
            '\n',
            '\r',

            // Comment syntax
            '/',
            '*'
        };

        string triviaListToText(IEnumerable<SyntaxTrivia> triviaList, out XElement xml)
        {
            var txtUntreatedNewlines = string.Join("\n",
                triviaList
                    .Select(t => t.ToString())
                    .SelectMany(t => t.Split("\n"))
                    .Select(t => t.Trim(trimChars)))
                .Replace("\r", "");
            var txt = "";
            for (int i = 0; i < txtUntreatedNewlines.Length; i++)
            {
                txt += txtUntreatedNewlines[i] != '\n'
                       || (i > 0 && txtUntreatedNewlines[i-1] == '\n')
                       || (i < (txtUntreatedNewlines.Length-1) && txtUntreatedNewlines[i+1] == '\n')
                       ? txtUntreatedNewlines[i]
                       : " ";
            }
            txt = txt.Trim();
            
            xml = XElement.Parse($"<root>{txt}</root>");

            txt = xml.ElementOfName("summary") is { } summary
                ? summary.ElementInnerText().Trim(trimChars)
                : txt;

            return txt;
        }
        
        if (member.Parent is null) { return new CodeComment(Text: "", Element: new XElement("root")); }
        var allSiblingNodes = member.Parent.DescendantNodes();
        var allSiblingTrivia = member.Parent.DescendantTrivia()
            // Apparently this can sometimes include
            // the parent's leading trivia, which we
            // don't want, so let's filter it out here
            .Where(t => t.SpanStart > member.Parent.SpanStart);
        var allSiblings =
            allSiblingNodes.Select(n => (Span: n.Span, Sibling: (object) n))
            .Concat(allSiblingTrivia.Select(t => (Span: t.Span, Sibling: (object) t)))
            .OrderBy(t => t.Span.Start)
            .ToArray();

        var inputSpanMemberTuple = (member.Span, (object) member);
        var indexOfInputMember = allSiblings.FindIndex(t => t == inputSpanMemberTuple);

        // Find trivia that comes after the input node and is on the same line
        var triviaAfter = allSiblings[(indexOfInputMember+1)..]
            .TakeWhile(t => t.Sibling is SyntaxTrivia && !t.Sibling.ToString()!.Contains("\n"))
            .Select(t => (SyntaxTrivia)t.Sibling)
            .ToArray();

        var triviaAfterTxt = triviaListToText(triviaAfter, out var triviaAfterXml);
        if (!string.IsNullOrWhiteSpace(triviaAfterTxt)) { return new CodeComment(triviaAfterTxt, triviaAfterXml); }

        // Find trivia that comes before the input node and after the previous node        
        var triviaBefore = allSiblings[..indexOfInputMember]
            .Reverse()
            .TakeWhile(t => t.Sibling is SyntaxTrivia)
            .Reverse()
            .Select(t => (SyntaxTrivia)t.Sibling)
            .ToArray();
        var triviaBeforeTxt = triviaListToText(triviaBefore, out var triviaBeforeXml);
        if (!string.IsNullOrWhiteSpace(triviaBeforeTxt)) { return new CodeComment(triviaBeforeTxt, triviaBeforeXml); }
        
        // Couldn't find attached trivia in siblings, look for it in parents
        while (member is { Parent: not null })
        {
            var leadingTrivia = triviaListToText(member.GetLeadingTrivia(), out var xml);

            if (!string.IsNullOrEmpty(leadingTrivia)) { return new CodeComment(leadingTrivia, xml); }

            member = member.Parent;

            if (member is TypeDeclarationSyntax) { break; }
        }

        // Failed to find any trivia at all
        return new CodeComment("", new XElement("root"));
    }
    
    public static IEnumerable<SerializableProperty> GetSerializableProperties(this TypeDeclarationSyntax @class)
    {
        foreach (var member in @class.Members)
        {
            if (member is not PropertyDeclarationSyntax property) { continue; }
            var serializeAttr = property.AttributeLists
                .SelectMany(l => l.Attributes)
                .FirstOrDefault(a => a.Name.ToString() == "Serialize");
            if (serializeAttr is null) { continue; }

            string cleanupDefaultValue(string v)
                => v.EndsWith("f") ? v[..^1] : v;
            
            string cleanupDescription(string desc)
                => desc.EvaluateAsCSharpExpression();

            string getArgument(string argName)
            {
                var arg = serializeAttr.ArgumentList!.Arguments.FirstOrDefault(arg
                    => arg.NameColon?.Name.Identifier.Text == argName);
                if (arg is null)
                {
                    switch (argName)
                    {
                        case "defaultValue":
                            arg = serializeAttr.ArgumentList!.Arguments[0];
                            break;
                        case "description":
                            arg = serializeAttr.ArgumentList!.Arguments.Count >= 3 ? serializeAttr.ArgumentList.Arguments[2] : null;
                            break;
                    }
                }

                return arg?.NameColon is not { Name.Identifier.Text: { } name } || name == argName
                    ? arg?.Expression.ToString() ?? ""
                    : "";
            }
            
            yield return new SerializableProperty(
                Name: property.Identifier.Text,
                Type: property.Type.ToString(),
                DefaultValue: cleanupDefaultValue(getArgument("defaultValue")),
                Description: cleanupDescription(getArgument("description")));
        }
    }

    public static string ElementInnerText(this XElement el)
    {
        StringBuilder str = new StringBuilder();
        foreach (XNode node in el.DescendantNodes())
        {
            if (node.NodeType == XmlNodeType.Text)
            {
                str.Append(node);
            }
            else if (node is XElement element && element.Name.LocalName.Equals("see", StringComparison.OrdinalIgnoreCase))
            {
                str.Append(element.GetAttributeValue("cref"));
            }
        }
        return str.ToString();
    }

    public static string Unescape(this string str)
        => str
            .Replace("&lt;", "<")
            .Replace("&gt;", ">")
            .Replace("&amp;", "&");

    public static string? GetAttributeValue(this XElement elem, string attrName)
        => elem.Attributes().FirstOrDefault(a => a.Name.LocalName.Equals(attrName, StringComparison.OrdinalIgnoreCase))
            ?.Value;

    public static bool EqCaseInsensitive(this string? str, string? other)
        => string.Equals(str, other, StringComparison.OrdinalIgnoreCase);
    
    public static bool ContainsCaseInsensitive(this string? str, string? other)
        => other is not null && (str?.Contains(other, StringComparison.OrdinalIgnoreCase) ?? false);
}