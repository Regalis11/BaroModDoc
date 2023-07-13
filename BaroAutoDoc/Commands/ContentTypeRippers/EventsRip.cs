
using BaroAutoDoc;
using BaroAutoDoc.Commands;
using BaroAutoDoc.SyntaxWalkers;

sealed class EventsRip : Command
{
    public void Invoke()
    {
        SyntaxRipperBuilder builder = new();

        Directory.SetCurrentDirectory(GlobalConfig.RepoPath);

        builder
            .Prepare("RandomEvents")
            .AddFile("Barotrauma/Barotrauma{0}/{0}Source/Events/EventSet.cs", fmt: new [] { "Shared" })
            .AddFile("Barotrauma/Barotrauma{0}/{0}Source/Events/EventPrefab.cs", fmt: new [] { "Client", "Shared" })
            .AddFile("Barotrauma/Barotrauma{0}/{0}Source/Events/MonsterEvent.cs", fmt: new[] { "Shared" })
            .WithOptions(new ClassParsingOptions(new[] { "InitProjSpecific" }))
            .Map
            (
                new FileMap("RandomEvents")
                {
                    new("EventSet")
                },
                new FileMap("RandomEvents")
                {
                    new ("EventPrefab")
                },
                new FileMap("RandomEvents")
                {
                    new ("MonsterEvent")
                }
            )
            .Submit();

        builder.Build(string.Empty);
    }
}