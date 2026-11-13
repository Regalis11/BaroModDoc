# Publicized assemblies

By default, your plugin is built against the game's normal assemblies, so it can only use the game's `public` members. The plugin template can instead build it against a "publicized" copy of the game's assemblies, where the non-public members (`private`, `protected` and `internal`) are made public, so code that uses them compiles.

Only do this if the [plugin services](Services/Services.md) and [Harmony](Services/IHarmonyProvider.md) can't do what you need. Non-public members aren't meant to be used by mods, so they can change or disappear in any game update without warning, and code that uses them breaks more often than code that sticks to the public API.

To enable this, set `UsePublicizedAssemblies` to `true` in `BuildData.props`:

```xml
<UsePublicizedAssemblies>true</UsePublicizedAssemblies>
```

This makes the project reference the `Barotrauma.ReferenceAssemblies.Vanilla.Publicized` package instead of `Barotrauma.ReferenceAssemblies.Vanilla`.

## Allowing the access at runtime

Besides enabling that flag, you also need to tell .NET that your plugin is allowed to access these non-public members in the game assemblies. To do that, add an assembly-wide `IgnoresAccessChecksTo` attribute for each of the game's three assemblies, in any file of your **SharedProject**, for example above the namespace in `Plugin.cs`:

```cs
using System.Runtime.CompilerServices;

[assembly: IgnoresAccessChecksTo("BarotraumaCore")]
[assembly: IgnoresAccessChecksTo("Barotrauma")]
[assembly: IgnoresAccessChecksTo("DedicatedServer")]
```
