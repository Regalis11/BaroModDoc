# IStatusEffectService

```cs
public interface IStatusEffectService : IPluginService
```

Allows plugins to register custom status effect actions.

## Remarks

Naming conflicts are not deterministic. Avoid using common names for your status effect actions. When possible, use unique prefixes to reduce the risk of colliding with action names from other plugins.

## Example

You can create new status effect actions by implementing the interface [`IStatusEffectAction`](IStatusEffectService.md#istatuseffectaction):

```csharp
public class FooAction : IStatusEffectAction
{
    private readonly string _message;

    public FooAction(StatusEffect statusEffect, XElement element)
    {
        _message = element.GetAttributeString("message", "");
    }

    public void Apply(StatusEffectParams effectParams)
    {
        if (!_message.IsNullOrEmpty())
        {
            DebugConsole.NewMessage(_message);
        }
    }
}
```

And to register the type, you can use the built-in factory implementation [`StatusEffectActionFactory`](#statuseffectactionfactory):

```csharp
StatusEffectService.RegisterAction(new StatusEffectActionFactory("Foo", (StatusEffect statusEffect, XElement element) => new FooAction(statusEffect, element)));
```

Now you can use your new custom action in status effects:

```xml
<StatusEffect ...>
  <Foo message="Test" />
</StatusEffect>
```

## Members

| Member                                | Description                                                                                 |
|---------------------------------------|---------------------------------------------------------------------------------------------|
| [`RegisterAction()`](#registeraction) | Registers a new status effect action factory that is used to create a status effect action. |

## Methods

### RegisterAction

```cs
void RegisterAction(IStatusEffectActionFactory actionFactory)
```

Registers a new status effect action factory that is used to create a status effect action.

## Related types

### StatusEffectParams

```cs
public readonly record struct StatusEffectParams(
    float DeltaTime,
    Entity Entity,
    IReadOnlyList<ISerializableEntity> Targets,
    Vector2? WorldPosition)
```

### IStatusEffectAction

```cs
public interface IStatusEffectAction
```

#### Apply

```cs
void Apply(StatusEffectParams effectParams)
```

Called when a status effect with this action activates.

### IStatusEffectActionFactory

```cs
public interface IStatusEffectActionFactory
```

#### SupportsElement

```cs
bool SupportsElement(XElement element)
```

#### Create

```cs
IStatusEffectAction Create(StatusEffect statusEffect, XElement element)
```

### StatusEffectActionFactory

```cs
public class StatusEffectActionFactory : IStatusEffectActionFactory
```

#### ElementName

```cs
string ElementName { get; private set; }
```

#### StatusEffectActionFactory

```cs
StatusEffectActionFactory(string elementName, Func<StatusEffect, XElement, IStatusEffectAction> createFunc)
```

#### SupportsElement

```cs
bool SupportsElement(XElement element)
```

#### Create

```cs
IStatusEffectAction Create(StatusEffect statusEffect, XElement element)
```
