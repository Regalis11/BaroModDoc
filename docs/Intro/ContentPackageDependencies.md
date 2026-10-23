# Content package dependencies

A [content package](ContentPackages.md) can declare that it depends on other content packages. Dependencies tell the game three things:

- which other mods your mod needs (or can make use of), so the game can refuse to enable it, or warn the player, when they are missing,
- where in the mod list your mod needs to be placed relative to them,
- which other mods' files your mod can refer to, and which parts of your mod should only be loaded when a certain mod is also enabled.

You should declare a dependency whenever your mod [overrides](Overrides.md) or extends content from another mod or DLC, or refers to its files.

## Declaring a dependency

Dependencies are `Dependency` elements in the content package's file list, next to the elements that list the content files:

```xml
<contentpackage name="Alien Wrench Expansion" modversion="1.0.0">
    <Dependency identifier="alienwrench"
                names="Alien Wrench"
                ids="1234567890"
                isoptional="false"
                order="Above" />
    <Item file="%ModDir%/expansion.xml" />
</contentpackage>
```

| Attribute | Description |
|-----------|-------------|
| `identifier` | A name you choose for the dependency, which is only used inside your own package. It's how [file groups](#loading-files-conditionally) and [`%ModDir:...%` paths](#referring-to-files-in-a-dependency) refer to the dependency. |
| `names` | Comma-separated names of the content package. |
| `ids` | Comma-separated IDs of the content package. For Steam Workshop mods this is the Workshop item ID, the number at the end of the mod's Workshop URL. |
| `steamappids` | Comma-separated Steam app IDs. Use this when the dependency is a DLC. |
| `isoptional` | If true, the game doesn't complain when the dependency is missing. Defaults to false. |
| `order` | Where your package wants to be in the mod list relative to the dependency: `None` (the default), `Above` or `Below`. See [Load order](#load-order). |

### How a dependency is matched

A dependency is considered present when one of the enabled packages matches **any** of the names, IDs or app IDs you listed. A package matches by its name (or one of its alternative names), its Workshop ID, or, for a DLC, its Steam app ID.

You can list several, which is useful when a mod has been republished or forked under a different name or ID, and either copy should satisfy the dependency: `names="Alien Wrench,Alien Wrench Fork"`.

Only packages that are enabled count. A mod that is installed but disabled doesn't satisfy a dependency.

Core packages can't have dependencies. The game reports an error if a core package contains one.

## Missing dependencies

If a **required** dependency (one that isn't optional) isn't enabled, the game refuses to enable your package and tells players to enable the missing mod, so players are not left with a broken mod.

An **optional** dependency never causes an error. It's useful together with [file groups](#loading-files-conditionally) when your mod works on its own, and some extra content is only loaded when the other mod is also enabled.

## Load order

Mods in the mod list are loaded from the bottom of the list to the top, so a mod **higher** in the list loads later and has a higher priority. This matters for [overrides](Overrides.md), since an override only works when the mod that overrides the content is above the mod that defines it.

The `order` attribute lets your package state where it needs to be relative to the dependency:

| `order` | Meaning |
|---------|---------|
| `None` | The order doesn't matter. |
| `Above` | Your package must be placed **above** the dependency in the list. It loads after the dependency, so it can override and extend its content. This is what you want for most mods that build on another mod. |
| `Below` | Your package must be placed **below** the dependency. It loads before the dependency, so the dependency's content takes priority over yours. |

The game can apply these constraints for you. When the **auto order mod list** option in the installed mods tab is on (it is by default), the game rearranges the enabled mods to satisfy every `order` requirement whenever the mod list is applied.

If the constraints can't all be satisfied, for example two mods that both require being above the other, the game can't rearrange the list and prints an error about a cyclic dependency in the console. If auto ordering is turned off, a mod that is in the wrong place is highlighted in yellow in the mod list.

Optional dependencies take part in ordering too. The order is only enforced when the dependency is present.

## Referring to files in a dependency

In file paths, `%ModDir%` is the folder of the content package the file is in. `%ModDir:name%` is the folder of another content package, which lets you use a texture or other file from a dependency instead of copying it into your own mod:

```xml
<Sprite texture="%ModDir:alienwrench%/Textures/wrench.png" sourcerect="0,0,64,64" />
```

The name can be the `identifier` of one of your dependencies, which is the recommended way because it keeps working if the mod is renamed or forked. It can also be the name or Workshop ID of a content package. If the game can't find the package, an error is thrown when the path is used, so only use this for packages that you have declared as required dependencies, or when you know the package is enabled.

## Loading files conditionally

By default every file listed in the file list is loaded. A `FileGroup` loads a set of files only when a condition about your dependencies is met, which is how an optional dependency can add extra content:

```xml
<contentpackage name="Alien Wrench Expansion" modversion="1.0.0">
    <Dependency identifier="alienwrench" names="Alien Wrench" isoptional="true" order="Above" />
    <Item file="%ModDir%/expansion.xml" />

    <FileGroup>
        <Conditional dependencies="alienwrench" operator="and" />
        <Item file="%ModDir%/alienwrench_patch.xml" />
    </FileGroup>
</contentpackage>
```

Files that are outside any `FileGroup` are always loaded. The files in this group are only loaded when the dependency with the identifier `alienwrench` is present.

A `Conditional` has these attributes:

| Attribute | Description |
|-----------|-------------|
| `dependencies` | Comma-separated list of dependency identifiers, **not** package names. A dependency counts when it is present. To require that a dependency is *not* present, put `ne` and a space in front of its identifier: `dependencies="ne alienwrench"`. |
| `operator` | `Or` (the default): the condition is met when **any** of the listed dependencies matches. `And`: it's met only when **all** of them match. |

If a group has several `Conditional` elements, all of them must be met.

This can be used both ways, for example by having one group for when a mod is present and another, with `ne`, for the same content when it isn't:

```xml
<FileGroup>
    <Conditional dependencies="alienwrench" />
    <Item file="%ModDir%/with_alienwrench.xml" />
</FileGroup>
<FileGroup>
    <Conditional dependencies="ne alienwrench" />
    <Item file="%ModDir%/without_alienwrench.xml" />
</FileGroup>
```

For a conditional file group to work properly, the dependency has to be declared with a matching `identifier`, and it should usually be `isoptional="true"`, otherwise the package can't be enabled without it and the other group would never be loaded.

## Tips

- Use `order="Above"` for mods that override or patch another mod's content.
- Prefer the Workshop `ids` over `names` when the dependency is on the Workshop, since names can be changed by the author. Using both is fine.
- Mark a dependency optional if your mod still makes sense without it, and use a [file group](#loading-files-conditionally) for the parts that need it.