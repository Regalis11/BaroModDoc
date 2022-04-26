using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BaroAutoDoc.SyntaxWalkers;

public class ContentTypeFinder : FolderSyntaxWalker
{
    private readonly List<ContentType> contentTypes = new();
    public IReadOnlyList<ContentType> ContentTypes => contentTypes;
    
    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        bool isAbstract = node.Modifiers.Any(m => m.IsKind(SyntaxKind.AbstractKeyword));
        if (isAbstract) { return; }

        if (node.BaseList is null) { return; }

        var baseList = node.BaseList.Types.Select(t => t.ToString()).ToHashSet();

        bool derivesFromGenericPrefabFile = baseList.Any(b => b.Contains("GenericPrefabFile"));
        bool derivesFromBaseSubFile = baseList.Contains("BaseSubFile");
        bool derivesFromContentFile =
            derivesFromGenericPrefabFile
            || derivesFromBaseSubFile
            || baseList.Any(b => b.EndsWith("File"));
        
        if (!derivesFromContentFile) { return; }

        string name = node.Identifier.ValueText[..^4];

        var attributes = node.AttributeLists
            .SelectMany(l => l.Attributes)
            .ToArray();

        bool attrHasName(AttributeSyntax a, string nm)
            => a.Name is IdentifierNameSyntax {Identifier.ValueText: var v} && v == nm;
        bool requiredByCorePackage = attributes.Any(
                a => attrHasName(a, "RequiredByCorePackage"));

        List<string> altNames = new();
        var altNamesAttr = attributes.FirstOrDefault(a => attrHasName(a, "AlternativeContentTypeNames"));
        if (altNamesAttr?.ArgumentList?.Arguments is { } argList)
        {
            altNames.AddRange(
                argList
                    .Select(arg => arg.Expression is LiteralExpressionSyntax l
                        ? l.Token.ValueText : ""));
            altNames.RemoveAll(string.IsNullOrWhiteSpace);
        }
        
        string? matchesSingular = null;
        string? matchesPlural = null;

        HashSet<string> constructedTypes = new HashSet<string>();

        foreach (var member in node.Members)
        {
            if (member is not MethodDeclarationSyntax method) { continue; }
            string methodName = method.Identifier.ValueText;

            void extractLiteral(string nameMatch, ref string? literal)
            {
                if (methodName != nameMatch) { return; }
                var l = method.DescendantNodes()
                    .FirstOrDefault(n => n is LiteralExpressionSyntax) as LiteralExpressionSyntax;
                literal = l?.Token.ValueText ?? literal;
            }
            extractLiteral("MatchesSingular", ref matchesSingular);
            extractLiteral("MatchesPlural", ref matchesPlural);

            var objectCreationExpressions = method.DescendantNodes().OfType<ObjectCreationExpressionSyntax>();
            constructedTypes.UnionWith(objectCreationExpressions
                .Select(e => e.Type)
                .OfType<IdentifierNameSyntax>()
                .Select(id => id.Identifier.ValueText));
        }
        
        contentTypes.Add(new ContentType(
            name,
            altNames.ToImmutableArray(),
            requiredByCorePackage,
            derivesFromBaseSubFile,
            matchesSingular,
            matchesPlural,
            constructedTypes.ToImmutableHashSet(),
            RelevantFiles: new[] { CurrentFile }.ToImmutableArray(),
            XmlSubElements: ImmutableArray<string>.Empty,
            XmlAttributes: ImmutableArray<ContentType.XmlAttribute>.Empty));
        
        base.VisitClassDeclaration(node);
    }
}