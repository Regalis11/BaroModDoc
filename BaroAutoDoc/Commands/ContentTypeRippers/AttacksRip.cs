
using BaroAutoDoc;
using BaroAutoDoc.Commands;
using BaroAutoDoc.SyntaxWalkers;

sealed class AttacksRip : Command
{
    public void Invoke()
    {
        SyntaxRipperBuilder builder = new();

        Directory.SetCurrentDirectory(GlobalConfig.RepoPath);

        builder
            .Prepare("Attack")
            .AddFile("Barotrauma/Barotrauma{0}/{0}Source/Characters/Attack.cs", fmt: new []{ "Client", "Shared" })
            .AddFile("Barotrauma/Barotrauma{0}/{0}Source/Map/Explosion.cs", fmt: new []{ "Client", "Shared" })
            .WithOptions(new ClassParsingOptions(new[] { "InitProjSpecific" }))
            .Map
            (
                new FileMap("Attack")
                {
                    new("Attack")
                },
                new FileMap("Explosion")
                {
                    new TypeSection("Explosion").AddInheritDisclaimer(("Attack", "/BaroModDoc/Misc/Attack.md"))
                }
            )
            .Submit();

        builder.Build(string.Empty);
    }
}