using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.SyntaxWalkers;

class StatusEffectRipper : FolderSyntaxWalker
{
    public readonly Dictionary<string, ClassDeclarationSyntax> StatusEffectTypes = new();

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        var baseList = node.BaseList?.Types.Select(static t => t.ToString()).ToImmutableArray() ?? ImmutableArray<string>.Empty;
        string typeName = node.Identifier.Text;
        if (typeName == "StatusEffect" || baseList.Contains("StatusEffect"))
        {
            StatusEffectTypes.TryAdd(typeName, node);
        }
    }
}