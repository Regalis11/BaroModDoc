using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using BaroAutoDoc.SyntaxWalkers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.Commands;

public class ItemRip : Command
{
    private record TreeNode(ClassDeclarationSyntax Class)
    {
        public record Attribute(string Name, string Type, string DefaultValue, string Description);
        
        public readonly List<TreeNode> Children = new();
        public readonly HashSet<TreeNode> InteractsWith = new();
        public string Name => Class.Identifier.Text;
        public string ParentName => Class.BaseList!.Types.First().ToString();

        public IEnumerable<Attribute> Attributes
        {
            get
            {
                foreach (var member in Class.Members)
                {
                    if (member is not PropertyDeclarationSyntax property) { continue; }
                    var serializeAttr = property.AttributeLists
                        .SelectMany(l => l.Attributes)
                        .FirstOrDefault(a => a.Name.ToString() == "Serialize");
                    if (serializeAttr is null) { continue; }

                    string cleanupDefaultValue(string v)
                        => v.EndsWith("f") ? v[..^1] : v;
                    
                    string cleanupDescription(string desc)
                        => CSharpScript.EvaluateAsync<string>(desc).Result;

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
                    
                    yield return new Attribute(
                        Name: property.Identifier.Text,
                        Type: property.Type.ToString(),
                        DefaultValue: cleanupDefaultValue(getArgument("defaultValue")),
                        Description: cleanupDescription(getArgument("description")));
                }
            }
        }
        
        public bool IsAbstract => Class.Modifiers.Any(m => m.IsKind(SyntaxKind.AbstractKeyword));
        
        public TreeNode? Parent = null;
    }
    
    public void Invoke(string repoPath = "C:/Users/juanj/Desktop/Repos/Barotrauma-development")
    {
        Directory.SetCurrentDirectory(repoPath);

        const string srcPathFmt = "Barotrauma/Barotrauma{0}/{0}Source/Items";
        string[] srcPathParams = {"Shared"};
        
        var itemComponentRipper = new ItemComponentRipper();
        int prevTypeCount;
        do
        {
            prevTypeCount = itemComponentRipper.Types.Count;
            foreach (string p in srcPathParams)
            {
                string srcPath = string.Format(srcPathFmt, p);
                itemComponentRipper.VisitAllInDirectory(srcPath);
            }
        } while (prevTypeCount != itemComponentRipper.Types.Count);

        //Construct a tree out of the found classes
        Dictionary<string, TreeNode> nodes = new();
        foreach (var type in itemComponentRipper.Types.Values)
        {
            var baseList = type.BaseList;
            string parentName = baseList!.Types.First().ToString();
            TreeNode newNode = new TreeNode(type);
            if (nodes.TryGetValue(parentName, out var parentNode))
            {
                newNode.Parent = parentNode;
                parentNode.Children.Add(newNode);
            }
            foreach (var childNode in nodes.Values.Where(n => n.ParentName == newNode.Name))
            {
                childNode.Parent = newNode;
                newNode.Children.Add(childNode);
            }
            nodes.Add(newNode.Name, newNode);
        }

        //Find references from one class to another
        var referenceFinder
            = new Regex(@"item\.GetComponent[s]?<(.+?)>", RegexOptions.CultureInvariant | RegexOptions.Compiled);
        var qualityRefFinder
            = new Regex(@"item\.GetQualityModifier\((.+?)\)", RegexOptions.CultureInvariant | RegexOptions.Compiled);
        foreach (var node in nodes.Values)
        {
            var code = node.Class.ToString();
            var matches = referenceFinder.Matches(code);
            foreach (Match match in matches)
            {
                //Console.WriteLine($"{node.Name} -> {match.Groups[0].Value}");
                node.InteractsWith.Add(nodes[match.Groups[1].Value]);
            }

            var qualityMatches = qualityRefFinder.Matches(code);
            if (qualityMatches.Any())
            {
                nodes["Quality"].InteractsWith.Add(node);
            }
        }

        IEnumerable<TreeNode> allInteractsWith(TreeNode n)
            => new[] { nodes["ConnectionPanel"] }
                .Union(n.InteractsWith)
                .Union(n.Parent != null ? allInteractsWith(n.Parent) : Enumerable.Empty<TreeNode>());
        IEnumerable<TreeNode> allInteractsWithAndSelf(TreeNode n)
            => new[] { n }.Union(allInteractsWith(n));

        //Get all of the item XML definitions
        const string gameRoot = "Barotrauma/BarotraumaShared";
        const string contentPackagePath = $"{gameRoot}/Content/ContentPackages/Vanilla.xml";
        XDocument contentPackageFileList = XDocument.Load(contentPackagePath);
        var itemFiles = contentPackageFileList.Root!.Elements("Item").ToArray();
        List<XElement> itemElements = new();
        foreach (var itemFileElem in itemFiles)
        {
            XDocument itemFile = XDocument.Load(Path.Combine(gameRoot, itemFileElem.Attribute("file")!.Value));
            itemElements.AddRange(itemFile.Root!.Elements()
                .Where(e => e.GetAttributeValue("category") is not { } category
                            || (!category.ContainsCaseInsensitive("legacy") /* skip legacy items */
                                && !category.ContainsCaseInsensitive("hidden") /* skip hidden items */))
                .Where(e => !bool.TryParse(e.GetAttributeValue("HideInMenus"), out bool hiddenInMenus) || !hiddenInMenus));
        }

        itemElements = itemElements
            .OrderBy(e => e.ToString().Length)
            .ThenBy(e => e.GetAttributeValue("identifier")?.Length ?? 10000)
            .ThenBy(e => e.GetAttributeValue("identifier") ?? "")
            .ToList();

        //Rip out some example of each of the item components, preferring
        //to include as many of the referenced itemcomponents as possible
        Dictionary<TreeNode, XElement> examples = new();
        //HashSet<XElement> usedExamples = new();
        foreach (var node in nodes.Values)
        {
            XElement[] potentialExamples = itemElements
                .Where(e => e.Elements().Any(ic => string.Equals(ic.Name.LocalName, node.Name, StringComparison.OrdinalIgnoreCase)))
                .ToArray();

            int countMatching(XElement example)
                => example.Elements().Count(ic => allInteractsWithAndSelf(node).Any(iw
                    => string.Equals(ic.Name.LocalName, iw.Name, StringComparison.OrdinalIgnoreCase)));

            potentialExamples = potentialExamples
                //prefer examples where the identifier contains the class name
                .OrderByDescending(e => e.GetAttributeValue("identifier").ContainsCaseInsensitive(node.Name))
                //prefer non-wrecked examples
                .ThenBy(e => e.GetAttributeValue("identifier").ContainsCaseInsensitive("wreck"))
                //prefer non-thalamus examples
                .ThenBy(e => e.GetAttributeValue("identifier").ContainsCaseInsensitive("thalamus"))
                //prefer non-alien examples
                .ThenBy(e => e.GetAttributeValue("category").ContainsCaseInsensitive("alien"))
                //prefer examples with the most referenced itemcomponents
                .ThenByDescending(countMatching)
                .ToArray();

            XElement? result = potentialExamples.FirstOrDefault();
            if (result != null)
            {
                //usedExamples.Add(result);
                //Console.WriteLine($"{node.Name} -> {result.Attribute("identifier")!.Value}");
                examples.Add(node, result);
            }
        }
        
        //Trim the extracted examples by removing any elements that do not illustrate the components
        Dictionary<TreeNode, XElement> trimmedExamples = new();
        foreach (var (node, example) in examples)
        {
            XElement trimmed = new XElement(example.Name);
            trimmed.Add(example.Attributes()
                .Where(a => a.Name != "name" && a.Name != "description")
                .Select(a => (object)a).ToArray());
            bool markOmission = false;
            foreach (var ic in example.Elements()
                         .OrderByDescending(e => e.Name.LocalName == node.Name))
            {
                if (allInteractsWithAndSelf(node).Any(n => string.Equals(n.Name, ic.Name.LocalName, StringComparison.OrdinalIgnoreCase)))
                {
                    trimmed.Add(new XElement(ic));
                }
                else
                {
                    markOmission = true;
                }
            }
            if (markOmission) { trimmed.Add(new XElement("__OMISSION__")); }
            trimmedExamples.Add(node, trimmed);
        }
        
        //Convert the trimmed examples to Markdown and write the pages
        Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!);
        Directory.CreateDirectory("ItemComponents");
        foreach (var node in nodes.Values)
        {
            if (node.IsAbstract) { continue; }
            
            Page page = new();
            page.Title = node.Name;

            Page.Section attributesSection = new(); page.Subsections.Add(attributesSection);
            attributesSection.Title = "Attributes";

            Page.Table attributesTable = new()
            {
                HeadRow = new Page.Table.Row("Attribute", "Type", "Default value", "Description")
            };

            IEnumerable<TreeNode.Attribute> getAttributes(TreeNode n)
                => n.Attributes.Concat(n.Parent is { IsAbstract: true }
                    ? getAttributes(n.Parent)
                    : Enumerable.Empty<TreeNode.Attribute>());
            
            foreach (var attr in getAttributes(node))
            {
                attributesTable.BodyRows.Add(new Page.Table.Row(attr.Name, attr.Type, attr.DefaultValue, attr.Description));
            }

            if (attributesTable.BodyRows.Any())
            {
                attributesSection.Body.Components.Add(attributesTable);
            }
            else
            {
                Console.WriteLine($"{node.Name} has no attributes table");
            }

            attributesSection.Body.Components.Add(new Page.NewLine());
            if (node.Parent != null)
            {
                attributesSection.Body.Components.Add(new Page.RawText($"This component {(attributesTable.BodyRows.Any() ? "also " : "")}supports the attributes defined in: "));
                var p = node.Parent;
                while (true)
                {
                    if (!p.IsAbstract) { attributesSection.Body.Components.Add(new Page.Hyperlink($"{p.Name}.md", p.Name)); }
                    if (p.Parent is null) { break; }
                    if (!p.IsAbstract) { attributesSection.Body.Components.Add(new Page.RawText(", ")); }
                    p = p.Parent;
                }
            }

            if (trimmedExamples.TryGetValue(node, out var example))
            {
                Page.Section exampleSection = new(); page.Subsections.Add(exampleSection);
                exampleSection.Title = "Example";
                exampleSection.Body.Components.Add(Page.CodeBlock.FromXElement(example)
                    .PostProcess(s => s.Replace("<__OMISSION__ />", "[...]")));
                File.WriteAllText(Path.Combine("ItemComponents", $"{node.Name}.md"), page.ToMarkdown());
            }
        }
        
        //Write a list of non-abstract components first in inheritance order, then alphabetical
        Page listPage = new();
        Page.BulletList list = new(); listPage.Body.Components.Add(list);

        static void addToList(Page.BulletList list, TreeNode node)
        {
            if (!node.IsAbstract)
            {
                list.Items.Add(new Page.Hyperlink($"ItemComponents/{node.Name}.md", node.Name));
            }

            foreach (var child in node.Children.OrderBy(c => c.Name))
            {
                addToList(list, child);
            }
        }
        addToList(list, nodes["ItemComponent"]);
        File.WriteAllText("itemComponentList.md", listPage.ToMarkdown());
    }
}
