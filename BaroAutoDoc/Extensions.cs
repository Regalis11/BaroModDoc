using System.Text;
using System.Xml;
using System.Xml.Linq;

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
    
    public static string ElementInnerText(this XElement el)
    {
        StringBuilder str = new StringBuilder();
        foreach (XNode element in el.DescendantNodes().Where(x => x.NodeType == XmlNodeType.Text))
        {
            str.Append(element.ToString());
        }
        return str.ToString();
    }
}