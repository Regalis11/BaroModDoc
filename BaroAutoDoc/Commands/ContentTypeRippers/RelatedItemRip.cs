
using BaroAutoDoc;
using BaroAutoDoc.Commands;
using BaroAutoDoc.SyntaxWalkers;

sealed class RelatedItemRip : Command
{
    public void Invoke()
    {
        SyntaxRipperBuilder builder = new();

        Directory.SetCurrentDirectory(GlobalConfig.RepoPath);

        builder
            .Prepare("RelatedItem")
            .AddFile("Barotrauma/BarotraumaShared/SharedSource/Items/RelatedItem.cs")
            .WithOptions(new ClassParsingOptions(new[] { "Load" }))
            .Map
            (
                new FileMap("RelatedItem")
                {
                    new TypeSection("RelatedItem")
                }
            )
            .Submit();

        builder.Build(String.Empty);
    }
}