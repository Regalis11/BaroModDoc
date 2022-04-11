using BaroAutoDoc.SyntaxWalkers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.Commands;

public class ItemRip : Command
{
    private record TreeNode(
        ClassDeclarationSyntax Class)
    {
        public readonly List<TreeNode> Children = new();
        public string Name => Class.Identifier.Text;
        public string ParentName => Class.BaseList!.Types.First().ToString();
        
        public TreeNode? Parent = null;
    }
    
    public override void Invoke(string[] args)
    {
        string repoPath = args.Length > 0 ? args[0] : "C:/Users/juanj/Desktop/Repos/Barotrauma-development";
        
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

        Dictionary<string, TreeNode> nodes = new();
        foreach (var type in itemComponentRipper.Types.Values)
        {
            var baseList = type.BaseList;
            string parentName = baseList.Types.First().ToString();
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

        var treeTop = nodes["ItemComponent"];

        void print(TreeNode node, int indent)
        {
            Console.WriteLine("{0}\\_{1}",
                string.Concat(Enumerable.Repeat("| ", indent)), node.Name);
            for (int i=0;i<node.Children.Count;i++)
            {
                var child = node.Children[i];
                print(child, indent+1);
            }
        }
        print(treeTop, 0);
    }
}
