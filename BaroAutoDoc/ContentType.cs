using System.Collections.Immutable;

namespace BaroAutoDoc;

public record ContentType(
    string Name,
    ImmutableArray<string> AltNames,
    bool RequiredByCorePackage,
    bool IsSubmarineType,
    string? MatchSingular,
    string? MatchPlural);