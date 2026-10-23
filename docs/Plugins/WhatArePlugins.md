# What are plugins?

Most of Barotrauma can be modified with XML alone: items, monsters, events and so on are all defined in [content packages](../Intro/ContentPackages.md) and need no code. A **plugin** is for the things XML can't do. It is a compiled C# assembly (a `.dll`) shipped inside a content package, which the game loads and runs alongside the rest of the mod.

Plugins can, for example, add new [item components](CreatingItemComponents.md) with their own behavior, react to things happening in the game, register custom commands and settings, or send their own network messages. Content packages containing a plugin are often called *code mods*.

If you want to start writing one, see [Getting started with plugins](GettingStarted.md).

## The dangers of plugins

**A plugin is a program that runs on your computer with the same permissions as the game itself.** Unlike XML mods, which can only change what the game's own systems allow, plugin code is not restricted or sandboxed. Installing a mod with custom code has the same risks as running an unknown program from the internet. A malicious plugin could:

- access or delete your files,
- steal personal information,
- install malware.

### What protects you

The game does not prevent a plugin from doing harm. It only makes sure you are asked first:

- The first time you enable a package with a plugin, the game shows a **warning** listing the risks, and you have to confirm before the mod is enabled. The same warning is shown when you join a server that uses code mods you haven't enabled yet.
- The game remembers the plugin assemblies you have approved, by file and by hash. If an assembly is **new** or has been **modified** since you approved it (for example after a Workshop update), the warning is shown again and a warning icon appears next to the mod.
- The warning window has buttons to open the folder containing the plugin and to decompile it, so you can check what it does before you accept.

### Advice for players

- Only enable code mods from authors you trust. Check the author, the mod's page and its reviews before subscribing.
- Be especially careful with mods that update often. An update replaces the code you approved earlier and it's a good idea to check again for anything suspicious.

### Advice for plugin authors

- Be open about what your plugin does and publish the source code, so players can verify it. [Also take note that publishing obfuscated code to the workshop is prohibited.](https://steamcommunity.com/workshop/discussions/18446744073709551615/581683835681502773/?appid=602960)