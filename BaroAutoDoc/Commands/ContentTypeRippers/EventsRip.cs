
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
                }
            )
            .Submit();

        builder.Build(string.Empty);
    }
}