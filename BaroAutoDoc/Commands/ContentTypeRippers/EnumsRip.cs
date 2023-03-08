using BaroAutoDoc.SyntaxWalkers;

namespace BaroAutoDoc.Commands.ContentTypeSpecific;

sealed class EnumsRip : Command
{
    public void Invoke()
    {
        SyntaxRipperBuilder builder = new();

        Directory.SetCurrentDirectory(GlobalConfig.RepoPath);

        builder
            .Prepare("Enums")
            .AddFile("Barotrauma/BarotraumaShared/SharedSource/Enums.cs")
            .Map
            (
                new FileMap("StatTypes")
                {
                    "StatTypes",
                    "AbilityFlags"
                }
            )
            .Submit();

        builder.Build(string.Empty);
    }
}