﻿using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.SyntaxWalkers;

class ItemComponentRipper : FolderSyntaxWalker
{
    public readonly Dictionary<string, ClassDeclarationSyntax> Types = new();

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        var baseList = node.BaseList?.Types.Select(t => t.ToString()) ?? Enumerable.Empty<string>();
        string typeName = node.Identifier.Text;
        if (typeName == "ItemComponent" || baseList.Any(Types.ContainsKey))
        {
            Types.TryAdd(typeName, node);
        }
    }
}
