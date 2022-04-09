using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace BaroAutoDoc.SyntaxWalkers;

public class FolderSyntaxWalker : CSharpSyntaxWalker
{
    public string CurrentFile = "";
    
    public void VisitAllInDirectory(string dir)
    {
        if (!Directory.Exists(dir)) { return; }
        foreach (string f in Directory.GetFiles(dir, "*.cs", SearchOption.AllDirectories))
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(f));
            CurrentFile = f.Replace('\\', '/');
            Visit(syntaxTree.GetRoot());
        }
    }
}
