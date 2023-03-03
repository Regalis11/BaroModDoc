using System.Collections.Immutable;

namespace BaroAutoDoc;

record ContentType(
    string Name,
    ImmutableArray<string> AltNames,
    bool RequiredByCorePackage,
    bool IsSubmarineType,
    string? MatchSingular,
    string? MatchPlural,
    ImmutableHashSet<string> ConstructedTypes,
    ImmutableArray<string> RelevantFiles,
    ImmutableArray<string> XmlSubElements,
    ImmutableArray<ContentType.XmlAttribute> XmlAttributes)
{
    public record XmlAttribute(string Type, string Name, string DefaultValue, string Description)
    {
        public Page.InlineMarkdown ToBulletPoint()
            => new($"`{Name}` : `{Type}`");
    }
}