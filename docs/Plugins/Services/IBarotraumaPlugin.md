# IBarotraumaPlugin

```cs
public interface IBarotraumaPlugin : IDisposable
```

The main entry point for a Barotrauma plugin. The interface implements `IDisposable` to allow for cleanup when the plugin is unloaded.

## Example

```csharp
internal sealed class MyBarotraumaPlugin : IBarotraumaPlugin
{
    public static IDebugConsole DebugConsole = PluginServiceProvider.GetService<IDebugConsole>();

    public void Init()
    {
        DebugConsole.NewMessage("My plugin was enabled!");
        // Call services here.
    }

    public void Dispose()
    {
        DebugConsole.NewMessage("My plugin was disabled!");
        // The plugin was disabled, clean up resources if needed.
    }
}
```

## Members

| Member                                  | Description                                                                                                                       |
|-----------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------|
| [`Init()`](#init)                       | Used to initialize the plugin. This is where you would call plugin services to register events, hooks and whatever else you need. |
| [`OnContentLoaded()`](#oncontentloaded) | Called after all the content of the content package has been loaded.                                                              |

## Methods

### Init

```cs
void Init()
```

Used to initialize the plugin. This is where you would call plugin services to register events, hooks and whatever else you need.

### OnContentLoaded

```cs
void OnContentLoaded()
```

Called after all the content of the content package has been loaded.
