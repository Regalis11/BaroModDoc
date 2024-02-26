using BaroAutoDoc.SyntaxWalkers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;

namespace BaroAutoDoc.Commands;

sealed class ScriptedEventRip : Command
{
    private record TreeNode(List<ClassDeclarationSyntax> Classes)
    {
        public readonly List<TreeNode> Children = new();
        public readonly HashSet<TreeNode> InteractsWith = new();
        public string Name => Classes.First().Identifier.Text;
        public string ParentName => Classes.First().BaseList!.Types.First().ToString();

        public IEnumerable<SerializableProperty> Attributes
            => Classes.SelectMany(c => c.GetSerializableProperties()).DistinctBy(p => p.Name);
        
        public bool IsAbstract => Classes.First().Modifiers.Any(m => m.IsKind(SyntaxKind.AbstractKeyword));
        
        public TreeNode? Parent = null;
    }
    
    public void Invoke()
    {
        Directory.SetCurrentDirectory(GlobalConfig.RepoPath);

        const string srcPathFmt = "Barotrauma/Barotrauma{0}/{0}Source/Events/EventActions";
        string[] srcPathParams = { "Shared", "Client", "Server" };
        EventActionRipper[] eventActionRippers = new EventActionRipper[srcPathParams.Length];
        for (int i = 0; i < srcPathParams.Length; i++)
        {
            var EventActionRipper = eventActionRippers[i] = new EventActionRipper();
            string srcPath = string.Format(srcPathFmt, srcPathParams[i]);
            int prevTypeCount;
            do
            {
                prevTypeCount = EventActionRipper.Types.Count;
                EventActionRipper.VisitAllInDirectory(srcPath);
            } while (prevTypeCount != EventActionRipper.Types.Count);
        }

        //Construct a tree out of the found classes
        Dictionary<string, TreeNode> nodes = new();
        foreach (var kvp in eventActionRippers[0].Types)
        {
            string typeName = kvp.Key;
            ClassDeclarationSyntax type = kvp.Value;
            var baseList = type.BaseList;
            string parentName = baseList?.Types.First().ToString() ?? "EventAction";

            List<ClassDeclarationSyntax> typeList = new() { type };
            typeList.AddRange(eventActionRippers.SelectMany(i => i.Types.Where(t => t.Key == typeName).Select(t => t.Value)));

            TreeNode newNode = new(typeList);
            if (nodes.TryGetValue(parentName, out var parentNode))
            {
                newNode.Parent = parentNode;
                parentNode.Children.Add(newNode);
            }
            foreach (var childNode in nodes.Values.Where(n => !n.IsAbstract && n.ParentName == newNode.Name))
            {
                childNode.Parent = newNode;
                newNode.Children.Add(childNode);
            }
            nodes.Add(newNode.Name, newNode);
        }

        //Convert the trimmed examples to Markdown and write the pages
        Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)!);

        string introductionText = File.ReadAllText("ManualDocs/ScriptedEventIntroduction.md");

        Directory.CreateDirectory("EventActions");
        foreach (var node in nodes.Values)
        {
            if (node.IsAbstract) { continue; }

            Page page = new()
            {
                Title = node.Name
            };

            var comment = node.Classes.First().FindCommentAttachedToMember();
            if (!string.IsNullOrEmpty(comment.Text))
            {
                page.Body.Components.Add(new Page.NewLine());
                page.Body.Components.Add(new Page.RawText(comment.Text));
            }

            Page.Section attributesSection = new(); page.Subsections.Add(attributesSection);
            attributesSection.Title = "Attributes";

            Page.Table attributesTable = new()
            {
                HeadRow = new Page.Table.Row("Attribute", "Type", "Default value", "Description")
            };

            IEnumerable<SerializableProperty> getAttributes(TreeNode n)
                => n.Attributes.Concat(n.Parent is { IsAbstract: true }
                    ? getAttributes(n.Parent)
                    : Enumerable.Empty<SerializableProperty>());
            
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
            if (node.Parent != null && !node.Parent.IsAbstract)
            {
                attributesSection.Body.Components.Add(new Page.RawText($"This action {(attributesTable.BodyRows.Any() ? "also " : "")}supports the attributes defined in: "));
                var p = node.Parent;
                while (true)
                {
                    if (!p.IsAbstract) { attributesSection.Body.Components.Add(new Page.Hyperlink($"{p.Name}.md", p.Name)); }
                    if (p.Parent is null) { break; }
                    if (!p.IsAbstract) { attributesSection.Body.Components.Add(new Page.RawText(", ")); }
                    p = p.Parent;
                }
            }

            File.WriteAllText(Path.Combine("EventActions", $"{node.Name}.md"), page.ToMarkdown());
        }
        
        //Write a list of non-abstract components first in inheritance order, then alphabetical
        Page listPage = new();
        Page.BulletList list = new(); listPage.Body.Components.Add(list);

        static void addToList(Page.BulletList list, TreeNode node)
        {
            if (!node.IsAbstract)
            {
                list.Items.Add(new Page.Hyperlink($"EventActions/{node.Name}.md", node.Name));
            }

            foreach (var child in node.Children.OrderBy(c => c.Name))
            {
                addToList(list, child);
            }
        }
        addToList(list, nodes["EventAction"]);
        File.WriteAllText("EventActionList.md", listPage.ToMarkdown());

        introductionText = introductionText.Replace("[TODO: list EventActions]", listPage.ToMarkdown());
        File.WriteAllText($"ScripedEvents.md", introductionText);
    }
}
