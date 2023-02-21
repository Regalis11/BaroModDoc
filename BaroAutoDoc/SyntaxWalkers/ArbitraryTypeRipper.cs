using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.SyntaxWalkers;

sealed class ArbitraryTypeRipper : FolderSyntaxWalker
{
    private readonly string typeToFind;

    private readonly List<BaseTypeDeclarationSyntax> declarations;
    public IReadOnlyList<BaseTypeDeclarationSyntax> Declarations => declarations;

    public ArbitraryTypeRipper(string typeToFind)
    {
        this.typeToFind = typeToFind;
        this.declarations = new();
    }

    private void VisitTypeDeclaration(BaseTypeDeclarationSyntax node)
    {
        if (!node.Identifier.Text.Equals(typeToFind)) { return; }
        declarations.Add(node);
    }

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        => VisitTypeDeclaration(node);

    public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
        => VisitTypeDeclaration(node);

    public override void VisitRecordDeclaration(RecordDeclarationSyntax node)
        => VisitTypeDeclaration(node);

    public override void VisitStructDeclaration(StructDeclarationSyntax node)
        => VisitTypeDeclaration(node);

    public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        => VisitTypeDeclaration(node);
}
