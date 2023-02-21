using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.SyntaxWalkers;

sealed class ArbitraryTypeRipper : FolderSyntaxWalker
{
    private readonly string typeToFind;

    private readonly List<ClassDeclarationSyntax> declarations;
    public IReadOnlyList<ClassDeclarationSyntax> Declarations => declarations;

    public ArbitraryTypeRipper(string typeToFind)
    {
        this.typeToFind = typeToFind;
        this.declarations = new();
    }

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        if (!node.Identifier.Text.Equals(typeToFind)) { return; }
        declarations.Add(node);
    }
}
