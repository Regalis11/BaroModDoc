using System.Collections.Immutable;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using BaroAutoDoc.SyntaxWalkers;

namespace BaroAutoDoc.Commands.ContentTypeSpecific;

sealed class AfflictionsRip : Command
{
    public void Invoke()
    {
        SyntaxRipperBuilder builder = new();

        Directory.SetCurrentDirectory(GlobalConfig.RepoPath);

        builder
            .Prepare("Prefabs") // TODO not required idk why I added it
            .AddDirectory("Barotrauma/Barotrauma{0}/{0}Source/Characters/Health/Afflictions/", "*.cs", fmt: new[] { "Shared", "Client", "Server" })
            .AddFile("Barotrauma/BarotraumaShared/SharedSource/Map/Explosion.cs") // for testing
            .WithOptions(new ClassParsingOptions(new[] { "LoadEffects" }))
            .Map
            (
                new FileMap("AfflictionPrefab")
                {
                    "AfflictionPrefab",
                    "AfflictionPrefabHusk",
                    "Description",
                    "TargetType",
                    "Effect",
                    "AppliedAbilityFlag",
                    "AppliedStatValue",
                    "PeriodicEffect"
                },
                new FileMap("Affliction")
                {
                    "Affliction",
                    "AfflictionHusk",
                    "AfflictionPsychosis",
                    "AfflictionSpaceHerpes"
                },
                new FileMap("Explosion") // for testing
                {
                    "Explosion"
                }
            )
            .Submit();

        builder.Build(string.Empty);
    }
}