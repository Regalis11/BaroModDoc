using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.SyntaxWalkers;

class ContentTypeAttributeRipper : FolderSyntaxWalker
{
    public string TypeToLookFor { get; private set; }
    public ContentType ContentType { get; private set; }
    
    public ContentTypeAttributeRipper(ContentType contentType, string typeToLookFor)
    {
        ContentType = contentType;
        TypeToLookFor = typeToLookFor;
    }

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        string className = node.Identifier.ValueText;
        if (TypeToLookFor != className) { return; }
        if (TypeToLookFor.StartsWith("Prefab")) { return; }

        var switchStatements = node.DescendantNodes().OfType<ConstructorDeclarationSyntax>()
            .SelectMany(c => c.Body?.DescendantNodes().OfType<SwitchStatementSyntax>() ?? Array.Empty<SwitchStatementSyntax>())
            .ToArray();
        var subElemSwitches = switchStatements.Where(s => s.Expression.ToString().Contains("subElement.Name")).ToArray();

        var serializableProperties = node.GetSerializableProperties().ToArray();
        TypeToLookFor = node.BaseList?.Types.FirstOrDefault()?.Type.ToString() ?? "";

        ContentType = ContentType with
        {
            XmlAttributes = ContentType.XmlAttributes.Union(
                serializableProperties.Select(p
                    => new ContentType.XmlAttribute(p.Type, p.Name)))
                .ToImmutableArray(),
            XmlSubElements = ContentType.XmlSubElements.Union(
                subElemSwitches.SelectMany(s => s.Sections.Select(
                    sec => sec.Labels.OfType<CaseSwitchLabelSyntax>().FirstOrDefault())
                    .OfType<CaseSwitchLabelSyntax>()
                    .Select(cs => cs.Value.ToString().Replace("\"", ""))))
                .ToImmutableArray(),
            RelevantFiles = ContentType.RelevantFiles.Add(CurrentFile)
        };
        
        base.VisitClassDeclaration(node);
    }
}