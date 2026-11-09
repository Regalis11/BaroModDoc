# IHarmonyProvider

```cs
public interface IHarmonyProvider : IPluginService
```

Provides a [HarmonyX](https://github.com/BepInEx/HarmonyX) instance that plugins can use to patch game methods at runtime, i.e. run their own code before (prefix), after (postfix) or instead of existing methods.

## Remarks

Each plugin gets its own Harmony instance. When the plugin is unloaded, every patch made through that instance is removed automatically (`UnpatchSelf`), so you do not need to unpatch manually. <br/><br/> Prefer the plugin services (hooks, registrars) when they cover what you need, and use Harmony when they don't. Patches are applied to compiled code, so they may break when the patched method changes in a game update, and methods that get inlined by the JIT cannot be reliably patched. <br/><br/> The methods are instance methods. The examples assume an instance has been retrieved and stored, e.g.:

```csharp
public static readonly IHarmonyProvider HarmonyProvider = PluginServiceProvider.GetService<IHarmonyProvider>();
```

Further reading:

- [Harmony documentation](https://harmony.pardeike.net/articles/intro.html): patching concepts, prefixes, postfixes, transpilers and injected parameters.
- [HarmonyX wiki](https://github.com/BepInEx/HarmonyX/wiki): differences and additions in HarmonyX compared to Harmony.

## Example

Patch with attributes and apply all patches in the plugin assembly with [`PatchAll`](#patchall):

```csharp
[HarmonyPatch(typeof(Character), nameof(Character.Kill))]
public static class CharacterKillPatch
{
    // Runs after Character.Kill. "__instance" is the character being killed.
    public static void Postfix(Character __instance, CauseOfDeathType causeOfDeath)
    {
        DebugConsole.NewMessage($"{__instance.Name} died ({causeOfDeath})");
    }
}
```

Plugin init:

```csharp
HarmonyProvider.PatchAll();
```

## Members

| Member                        | Description                                                                                                                                                               |
|-------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [`GetHarmony()`](#getharmony) | Gets the plugin's Harmony instance, creating it on the first call. Subsequent calls return the same instance.                                                             |
| [`PatchAll()`](#patchall)     | Searches the plugin assembly for classes annotated with `HarmonyPatch` attributes and applies them using the plugin's Harmony instance (see [`GetHarmony`](#getharmony)). |

## Methods

### GetHarmony

```cs
Harmony GetHarmony()
```

Gets the plugin's Harmony instance, creating it on the first call. Subsequent calls return the same instance.

**Remarks**

The instance is created with a unique id (the plugin assembly's full name), and all patches made through it are removed automatically when the plugin is unloaded.

**Example**

Patch a method manually, without attributes:

```csharp
Harmony harmony = HarmonyProvider.GetHarmony();
harmony.Patch(
    original: AccessTools.Method(typeof(Character), nameof(Character.Kill)),
    postfix: new HarmonyMethod(typeof(MyPatches), nameof(MyPatches.KillPostfix)));
```

```csharp
public static class MyPatches
{
    public static void KillPostfix(Character __instance)
    {
        /* Do something */
    }
}
```

### PatchAll

```cs
void PatchAll()
```

Searches the plugin assembly for classes annotated with `HarmonyPatch` attributes and applies them using the plugin's Harmony instance (see [`GetHarmony`](#getharmony)).

**Remarks**

Equivalent to `GetHarmony().PatchAll(pluginAssembly)`
