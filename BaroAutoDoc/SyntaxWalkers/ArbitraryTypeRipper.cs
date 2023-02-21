using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.SyntaxWalkers;

sealed class ArbitraryTypeRipper : FolderSyntaxWalker
{
    private readonly Predicate<BaseTypeDeclarationSyntax> predicate;

    private readonly List<BaseTypeDeclarationSyntax> types = new();
    public IReadOnlyList<BaseTypeDeclarationSyntax> Types => types;

    public ArbitraryTypeRipper(Predicate<BaseTypeDeclarationSyntax> predicate)
    {
        this.predicate = predicate;
    }

    public ArbitraryTypeRipper(string typeToFind) : this(syntax => syntax.Identifier.Text == typeToFind) { }

    private void VisitTypeDeclaration(BaseTypeDeclarationSyntax node)
    {
        if (!predicate(node)) { return; }
        types.Add(node);
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
