# IHarmonyProvider

```cs
public interface IHarmonyProvider : IPluginService
```

Provides a HarmonyX instance that can be used by plugins TODO: more verbose documentation, examples? links to the harmony doc?

## Members

| Member                        | Description                                                                                                                        |
|-------------------------------|------------------------------------------------------------------------------------------------------------------------------------|
| [`GetHarmony()`](#getharmony) | Gets a Harmony instance that can used to patch code, the instance is automatically created with an unique id and properly disposed |
| [`PatchAll()`](#patchall)     | Searches the entire plugin assembly for Harmony annotations and uses them to create patches                                        |

## Methods

### GetHarmony

```cs
Harmony GetHarmony()
```

Gets a Harmony instance that can used to patch code, the instance is automatically created with an unique id and properly disposed

### PatchAll

```cs
void PatchAll()
```

Searches the entire plugin assembly for Harmony annotations and uses them to create patches
