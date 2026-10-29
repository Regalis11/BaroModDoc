# Plugin unloading

Plugins can be unloaded while the game is running (disabling a mod, changing the mod list or updating a mod doesn't require restarting Barotrauma). This page explains what the game does when a plugin is unloaded, why a plugin sometimes doesn't unload completely, and how to write a plugin that unloads cleanly.

## What the game does

When a plugin is unloaded, the game does the following:

1. calls `Dispose()` on the plugin's [`IBarotraumaPlugin`](Services/IBarotraumaPlugin.md),
2. clears everything the plugin registered through the [plugin services](Services/Services.md) (hooks, item components, extra fields, and so on),
3. disposes the services that hold on to things for the plugin, which includes undoing the [Harmony](Services/IHarmonyProvider.md) patches made through the service, and
4. asks .NET to unload the plugin's assembly load context.

All of this is tracked per plugin so that none of it has to be cleaned up by hand. That is the reason to use the services instead of reaching into the game's internals, since anything registered through a service is removed by the game for you.

## Plugins don't always unload

Plugins are loaded into a collectible assembly load context. Calling `Unload()` on a collectible context doesn't remove the assembly by itself. It only marks it as unloadable, and .NET actually frees it later, once the garbage collector finds that **nothing is referencing it any more**.

If something in the game, or in the plugin itself, still holds a reference to the plugin, the assembly stays in memory and the plugin is stuck half-unloaded. For example, if the game kept a list of objects and a plugin added an instance of its own class to that list, the game would then have an object whose type belongs to an assembly that is supposed to be gone, so the runtime keeps the assembly loaded. This is why all the things a plugin registers are kept together and cleared when it is unloaded.

## How you can tell an unload was successful

After a plugin is unloaded the game waits briefly, then forces garbage collections a few times and checks whether the assembly is gone. if the console prints a green message, "Plugin was successfully unloaded after N seconds", with the name of the content package, then the unload was successful. if The game prints a warning: *the plugin could not be fully unloaded and is still active in memory after cleanup attempts*, it means immediate unloading has failed.

## Writing a plugin that unloads cleanly

The unloading problems come from references that outlive the plugin. The plugin services deal with most of them for you, so the rest is up to the plugin's own code:

- **Clean up in `Dispose()`.** Undo whatever `Init()` and the rest of the plugin did that the services don't track.
- **Unsubscribe from events.** If you subscribe to an event on something that outlives the plugin, such as a static event or an event on a game object, the event's delegate keeps your assembly alive. Unsubscribe in `Dispose()`.
- **Don't put your objects in the game's collections.** Anything you add to a list, dictionary or cache owned by the game, or by a library, must be removed again.
- **Be careful with statics.** Static fields that are filled with game objects, or that other code can reach, are a common way to leak. Clear them in `Dispose()`.
- **Use the services.** Register hooks, item components and so on through the [plugin services](Services/Services.md) rather than by patching or by storing your own references into the game.
- **Use the Harmony service.** Patches created via [`IHarmonyProvider`](Services/IHarmonyProvider.md) are removed automatically when the plugin unloads. If you create your own `Harmony` instance, you must call `UnpatchSelf()` on it yourself.
- **Store per-object data with extra fields.** Instead of keeping your own dictionary keyed by game objects, use the extra fields the game keeps per plugin. They are cleared with the plugin.
- **Test unloading.** Enable and disable your mod a few times while testing and check that the console shows the success message each time. The warning is much easier to track down while you still remember what you changed.

## When a plugin won't unload

If the not-unloaded warning shows up for your mod:

1. Check that your own code isn't the cause, using the list above. Event subscriptions and entries left in game collections are the most common causes.
2. Try disabling the mod, waiting a few seconds, and checking the console again. Some plugins do unload slightly later.
3. If you suspect the game is keeping a reference to something the plugin registered, that is a bug in the game and worth reporting, including the console output.
4. Restarting the game always unloads the plugin. It's the safe fallback for players too.
