using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace BaroAutoDoc.Commands;

public class SteamToMd : Command
{
    public void Invoke(string url, string fileName)
    {
        HttpClient httpClient = new();
        var response = httpClient.Send(new HttpRequestMessage(HttpMethod.Get,
            new Uri(url)));
        using var stream = response.Content.ReadAsStream();

        using var reader = new StreamReader(stream);
        string pageStr = reader.ReadToEnd();
        pageStr = pageStr[pageStr.IndexOf("<div class=\"guide subSections\"")..];
        pageStr = pageStr[..pageStr.IndexOf("<div id=\"-1\"")];
        pageStr = pageStr.Replace("<br>", "<br/>");
        pageStr = Regex.Replace(pageStr, "<img(.+?)>", "<img$1/>");
        
        XElement xElem = XElement.Parse(pageStr, LoadOptions.None);
        foreach (var text in xElem.DescendantNodes().OfType<XText>())
        {
            text.Value = text.Value.Trim();
            if (string.IsNullOrEmpty(text.Value)) { text.Remove(); }
        }

        var titles = xElem.DescendantsAndSelf().Where(e => e.Attribute("class")?.Value == "subSectionTitle").ToArray();
        var descriptions = xElem.DescendantsAndSelf().Where(e => e.Attribute("class")?.Value == "subSectionDesc").ToArray();

        (string Title, XElement Description)[] sectionsXml = titles.Zip(descriptions,
            (t, d) =>
                (t.Nodes().OfType<XText>().First().Value, d))
            .ToArray();

        void parseNode(XNode node, List<Page.BodyComponent> components)
        {
            switch (node)
            {
                case XText txt:
                    components.Add(new Page.RawText(txt.Value+" "));
                    break;
                case XElement elem:
                    switch (elem.Name.LocalName)
                    {
                        case "div":
                            if (elem.Attribute("class")?.Value == "bb_code")
                            {
                                components.Add(new Page.NewLine());
                                components.Add(new Page.CodeBlock(Lang: "", Value: elem.ElementInnerText().Unescape()));
                                components.Add(new Page.NewLine());
                            }
                            break;
                        case "br":
                            components.Add(new Page.NewLine());
                            break;
                        case "b":
                            components.Add(new Page.InlineMarkdown($"**{elem.ElementInnerText()}**"));
                            break;
                        case "i":
                            components.Add(new Page.InlineMarkdown($"*{elem.ElementInnerText()}*"));
                            break;
                        case "a":
                            var nestedImg = elem.Element("img");
                            if (nestedImg != null)
                            {
                                components.Add(new Page.Image(Url: nestedImg.Attribute("src").Value));
                            }
                            else
                            {
                                components.Add(new Page.Hyperlink(Url: elem.Attribute("href").Value, Text: elem.ElementInnerText()));
                            }
                            break;
                        case "ul":
                            var list = new Page.BulletList(); components.Add(list);
                            foreach (var listItem in elem.Elements("li"))
                            {
                                var itemComponents = new List<Page.BodyComponent>();
                                foreach (var listItemNode in listItem.Nodes())
                                {
                                    parseNode(listItemNode, itemComponents);
                                }
                                list.Items.Add(new Page.InlineMarkdown(itemComponents.ToArray()));
                            }
                            break;
                    }
                    components.Add(new Page.RawText(" "));
                    break;
            }
        }
        
        Page markdown = new Page();
        foreach (var (title, description) in sectionsXml)
        {
            var section = new Page.Section();
            section.Title = title;
            markdown.Subsections.Add(section);
            foreach (var node in description.Nodes())
            {
                parseNode(node, section.Body.Components);
            }
        }
        
        Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!);
        
        File.WriteAllLines(fileName, markdown.ToMarkdown()
            .Split("\n")
            .Select(s => s.Trim()));
    }
}
