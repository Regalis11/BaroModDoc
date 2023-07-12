using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.SyntaxWalkers;

class AfflictionRipper : FolderSyntaxWalker
{
    public readonly Dictionary<string, ClassDeclarationSyntax> AfflictionTypes = new(),
                                                               AfflictionPrefabs = new();

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        var baseList = node.BaseList?.Types.Select(static t => t.ToString()).ToImmutableArray() ?? ImmutableArray<string>.Empty;
        string typeName = node.Identifier.Text;
        if (typeName == "Affliction" || baseList.Contains("Affliction"))
        {
            AfflictionTypes.TryAdd(typeName, node);
        }
        else if (typeName == "AfflictionPrefab" || baseList.Contains("AfflictionPrefab"))
        {
            AfflictionPrefabs.TryAdd(typeName, node);
        }
    }
}