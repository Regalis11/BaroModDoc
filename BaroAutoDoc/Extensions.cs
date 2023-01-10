using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc;

public static class Extensions
{
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
    
    public static IEnumerable<SerializableProperty> GetSerializableProperties(this ClassDeclarationSyntax @class)
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
        foreach (XNode element in el.DescendantNodes().Where(x => x.NodeType == XmlNodeType.Text))
        {
            str.Append(element.ToString());
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