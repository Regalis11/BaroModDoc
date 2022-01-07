using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc;

public class SubElementRipper : CSharpSyntaxWalker
{
    private readonly ImmutableHashSet<string> typesToLookFor;
    private readonly ContentType[] contentTypes;
    public IReadOnlyList<ContentType> ContentTypes => contentTypes;

    public SubElementRipper(IReadOnlyList<ContentType> contentTypes)
    {
        this.contentTypes = contentTypes.ToArray();
        this.typesToLookFor = contentTypes
            .Where(t => t.ConstructedTypes.Count == 1)
            .SelectMany(t => t.ConstructedTypes)
            .Distinct()
            .ToImmutableHashSet();
    }

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        string className = node.Identifier.ValueText;
        if (!typesToLookFor.Contains(className)) { return; }

        var constructors = node.DescendantNodes().OfType<ConstructorDeclarationSyntax>().ToArray();
        if (constructors.Length != 1) { return; }

        var properties = node.DescendantNodes().OfType<PropertyDeclarationSyntax>();
        var serializableProperties = properties
            .Where(p => p.AttributeLists
                .SelectMany(l => l.Attributes)
                .Any(a => a.Name is IdentifierNameSyntax {Identifier.ValueText: "Serialize"}))
            .ToArray();

        var ctIndex = contentTypes.FindIndex(t => t.ConstructedTypes.Contains(className));

        contentTypes[ctIndex] = contentTypes[ctIndex] with
        {
            XmlAttributes = contentTypes[ctIndex].XmlAttributes.AddRange(
                serializableProperties.Select(p
                    => new ContentType.XmlAttribute(p.Type.ToString(), p.Identifier.ValueText))),
            RelevantFiles = contentTypes[ctIndex].RelevantFiles.Add(CurrentFile)
        };
        
        base.VisitClassDeclaration(node);
    }

    public string CurrentFile = "";
}