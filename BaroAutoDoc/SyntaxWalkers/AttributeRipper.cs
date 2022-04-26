using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.SyntaxWalkers;

public class AttributeRipper : FolderSyntaxWalker
{
    public string TypeToLookFor { get; private set; }
    public ContentType ContentType { get; private set; }
    
    public AttributeRipper(ContentType contentType)
    {
        ContentType = contentType;

        int numSharedStart(string a, string b)
        {
            for (int i=0;i<Math.Min(a.Length, b.Length);i++)
            {
                if (a[i] == b[i]) { continue; }
                return i;
            }
            return Math.Min(a.Length, b.Length);
        }
        
        TypeToLookFor = contentType.ConstructedTypes
            .OrderByDescending(t => numSharedStart(t, contentType.Name))
            .ThenBy(t => t).FirstOrDefault() ?? "";
        if (string.IsNullOrEmpty(TypeToLookFor))
        {
            Console.WriteLine($"No constructed types from {ContentType.Name}");
        }
        else
        {
            Console.WriteLine($"Extracting attributes for {ContentType.Name}: Starting with {TypeToLookFor}");
        }
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

        var properties = node.DescendantNodes().OfType<PropertyDeclarationSyntax>();
        var serializableProperties = properties
            .Where(p => p.AttributeLists
                .SelectMany(l => l.Attributes)
                .Any(a => a.Name is IdentifierNameSyntax {Identifier.ValueText: "Serialize"}))
            .ToArray();
        TypeToLookFor = node.BaseList?.Types.FirstOrDefault()?.Type.ToString() ?? "";

        ContentType = ContentType with
        {
            XmlAttributes = ContentType.XmlAttributes.Union(
                serializableProperties.Select(p
                    => new ContentType.XmlAttribute(p.Type.ToString(), p.Identifier.ValueText)))
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