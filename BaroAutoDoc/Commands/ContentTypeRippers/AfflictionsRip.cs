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
                    new TypeSection("AfflictionPrefab"),
                    new TypeSection("AfflictionPrefabHusk"),
                    new TypeSection("Description"),
                    new TypeSection("TargetType"),
                    new TypeSection("Effect"),
                    new TypeSection("AppliedAbilityFlag"),
                    new TypeSection("AppliedStatValue"),
                    new TypeSection("PeriodicEffect")
                },
                new FileMap("Affliction")
                {
                    new TypeSection("Affliction"),
                    new TypeSection("AfflictionHusk"),
                    new TypeSection("AfflictionPsychosis"),
                    new TypeSection("AfflictionSpaceHerpes")
                }
            )
            .Submit();

        builder.Build(string.Empty);
    }
}