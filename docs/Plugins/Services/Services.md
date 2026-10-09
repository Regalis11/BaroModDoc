# Plugin services

Plugin services are the interfaces plugins use to interact with the game. Get a service in your plugin with `PluginServiceProvider.GetService<T>()`, for example:

```cs
public static readonly IItemComponentRegistrar ItemComponentRegistrar =
    PluginServiceProvider.GetService<IItemComponentRegistrar>();
```

This documentation is generated from the documentation comments in the game's code.


| Service                                                               | Description                                                                                                                                                                                                  |
|-----------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [IBarotraumaPlugin](IBarotraumaPlugin.md)                             | The main entry point for a Barotrauma plugin. The interface implements `IDisposable` to allow for cleanup when the plugin is unloaded.                                                                       |
| [IAIObjectiveRegistrar](IAIObjectiveRegistrar.md)                     | Allows plugins to configure what AIObjective is created for an order identifier.                                                                                                                             |
| [IContentFileRegistrar](IContentFileRegistrar.md)                     | Interface that allows registering custom XML content in your mod.                                                                                                                                            |
| [IContentPackageRetrievalService](IContentPackageRetrievalService.md) | Provides a way for plugins to get their own Content Package and check for dependencies.                                                                                                                      |
| [IDebugConsole](IDebugConsole.md)                                     | Provides an interface to `DebugConsole` for plugins. Allows registering commands and writing messages to F3 debug console.                                                                                   |
| [IEventActionRegistrar](IEventActionRegistrar.md)                     | Provides methods for registering custom event actions to be loaded in `ScriptedEvent`.                                                                                                                       |
| [IGameNetwork](IGameNetwork.md)                                       | Provides a service to allow client and server side network communication in multiplayer.                                                                                                                     |
| [IGameScreen](IGameScreen.md)                                         | Provides a service to add events that trigger on resolution change.                                                                                                                                          |
| [IHarmonyProvider](IHarmonyProvider.md)                               | Provides a HarmonyX instance that can be used by plugins TODO: more verbose documentation, examples? links to the harmony doc?                                                                               |
| [IItemComponentRegistrar](IItemComponentRegistrar.md)                 | Provides methods for registering custom implementations of `ItemComponent` that can be added to items.                                                                                                       |
| [ISettingsService](ISettingsService.md)                               | Allows plugins to create their own settings that can be saved to disk, edited in game and networked.                                                                                                         |
| [ISimpleHookService](ISimpleHookService.md)                           | Allows plugins to register callback functions that get called at specific points in the game.                                                                                                                |
| [IStatusEffectService](IStatusEffectService.md)                       | Allows plugins to register custom status effect actions.                                                                                                                                                     |
| [Hooks](Hooks.md)                                                     | Hooks are callbacks that get called at specific points in the game. Register one with [ISimpleHookService](ISimpleHookService.md)'s `RegisterHook<T>()`, where `T` is one of the delegate types listed here. |

