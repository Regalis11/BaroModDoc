using System.Reflection;
using BaroAutoDoc.PluginDocs;

namespace BaroAutoDoc.Commands;

/// <summary>
/// Generates documentation for the plugin services (and the plugin entry point and hooks) from the
/// documentation comments in the game's code.
/// </summary>
sealed class PluginServicesRip : Command
{
    private const string PluginsPath = "Barotrauma/BarotraumaShared/SharedSource/Plugins";

    private static readonly PageSpec[] ExtraPages =
    {
        new(
            "IBarotraumaPlugin.md",
            new[] { $"{PluginsPath}/IBarotraumaPlugin.cs" }),
        new(
            "Hooks.md",
            new[] { $"{PluginsPath}/Services/HookDelegates.cs", $"{PluginsPath}/Services/OutpostFactionCandidate.cs" },
            Title: "Hooks",
            Introduction: "Hooks are callbacks that get called at specific points in the game. " +
                          "Register one with [ISimpleHookService](ISimpleHookService.md)'s `RegisterHook<T>()`, where `T` is one of the delegate types listed here.")
    };

    /// <param name="outputDir">Where to write the markdown files. Defaults to "markdown/PluginServices" next to the executable.</param>
    public void Invoke(string outputDir = "")
    {
        if (string.IsNullOrEmpty(outputDir))
        {
            outputDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "markdown", "PluginServices");
        }
        outputDir = Path.GetFullPath(outputDir);

        string interfaceDir = Path.Combine(GlobalConfig.RepoPath, PluginsPath, "Services", "Interface");
        if (!Directory.Exists(interfaceDir))
        {
            throw new DirectoryNotFoundException($"Could not find {interfaceDir}, is the repository path in the config correct?");
        }

        List<PageSpec> specs = new();
        specs.AddRange(ExtraPages.Take(1));
        foreach (string file in Directory.GetFiles(interfaceDir, "*.cs").OrderBy(static f => f, StringComparer.OrdinalIgnoreCase))
        {
            // The file name is only used to find the source, the page is named after the service defined in it.
            specs.Add(new PageSpec(Path.GetFileNameWithoutExtension(file) + ".md", new[] { Path.GetRelativePath(GlobalConfig.RepoPath, file) }));
        }
        specs.AddRange(ExtraPages.Skip(1));

        List<PluginPage> pages = new();
        foreach (PageSpec spec in specs)
        {
            PluginPage page = new() { Spec = spec };
            foreach (string file in spec.SourceFiles)
            {
                page.Types.AddRange(SourceModel.ParseFile(Path.Combine(GlobalConfig.RepoPath, file)));
            }

            if (page.Types.Count == 0)
            {
                Console.WriteLine($"WARNING: No public types found for {spec.FileName}, skipping.");
                continue;
            }

            if (spec.Title is null)
            {
                page.Main = page.Types.FirstOrDefault(static t => t.IsPluginService) ?? page.Types[0];
                // Name the page after the type it documents.
                page.Spec = spec with { FileName = page.Main.Name + ".md" };
            }

            pages.Add(page);
        }

        Directory.CreateDirectory(outputDir);

        PluginPageWriter writer = new(pages);
        foreach (PluginPage page in pages)
        {
            File.WriteAllText(Path.Combine(outputDir, page.Spec.FileName), writer.WritePage(page));
        }

        File.WriteAllText(Path.Combine(outputDir, "Services.md"), writer.WriteIndex(pages));

        Console.WriteLine($"Wrote {pages.Count + 1} pages to {outputDir}");
    }
}
