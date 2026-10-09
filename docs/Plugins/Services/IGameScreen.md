# IGameScreen

```cs
public interface IGameScreen : IPluginService
```

Provides a service to add events that trigger on resolution change.

## Members

| Member                                                                  | Description                                                               |
|-------------------------------------------------------------------------|---------------------------------------------------------------------------|
| [`RegisterResolutionChangeEvent()`](#registerresolutionchangeevent)     | Registers an action to be executed when the screen resolution is changed. |
| [`DeregisterResolutionChangeEvent()`](#deregisterresolutionchangeevent) | Deregisters an event by identifier.                                       |

## Methods

### RegisterResolutionChangeEvent

```cs
void RegisterResolutionChangeEvent(Identifier identifier, Action onResolutionChanged)
```

Registers an action to be executed when the screen resolution is changed.

| Parameter             | Type         | Description                                                                                                        |
|-----------------------|--------------|--------------------------------------------------------------------------------------------------------------------|
| `identifier`          | `Identifier` | Unique identifier of this resolution change event. If the identifier already exists, the old one will be replaced. |
| `onResolutionChanged` | `Action`     | An action to be executed when the screen resolution is changed.                                                    |

**Example**

```csharp
public class MyInterface : IDisposable
{
    private static readonly Identifier eventIdentifier = nameof(MyInterface).ToIdentifier();
    public MyInterface(RectTransform parent)
    {
        // build the UI...
        GameScreen.RegisterResolutionChangeEvent(eventIdentifier, OnResolutionChanged);
    }

    private void OnResolutionChanged()
    {
        // reconstruct the UI...
    }

    public void Dispose()
        => GameScreen.DeregisterResolutionChangeEvent(eventIdentifier);
}
```

### DeregisterResolutionChangeEvent

```cs
void DeregisterResolutionChangeEvent(Identifier identifier)
```

Deregisters an event by identifier.

| Parameter    | Type         | Description                            |
|--------------|--------------|----------------------------------------|
| `identifier` | `Identifier` | Identifier of the event to be removed. |

**Remarks**

The service provider will automatically clear all resolution change events when the plugin is unloaded. However, it is still good practice to deregister them manually to avoid the action from hindering garbage collection, for example, while in the main menu.
