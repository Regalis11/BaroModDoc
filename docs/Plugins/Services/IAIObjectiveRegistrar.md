# IAIObjectiveRegistrar

```cs
public interface IAIObjectiveRegistrar : IPluginService
```

Allows plugins to configure what AIObjective is created for an order identifier.

## Remarks

Factories are queried before the built-in objectives, so returning an objective for a vanilla order identifier overrides the vanilla behavior.

## Example

Create a factory by implementing [`IOrderObjectiveFactory`](IAIObjectiveRegistrar.md#iorderobjectivefactory). Return an objective for the orders you want to handle, and null for everything else:

```csharp
public class FooObjectiveFactory : IOrderObjectiveFactory
{
    public AIObjective? CreateObjectiveFromOrder(AIObjectiveManager objectiveManager, Order order, Character character, float priorityModifier)
    {
        if (order.Identifier == "foo")
        {
            return new AIObjectiveFoo(character, objectiveManager, priorityModifier);
        }
        return null;
    }
}
```

The objective itself is a regular `AIObjective`:

```csharp
public class AIObjectiveFoo : AIObjective
{
    public override Identifier Identifier { get; set; } = "foo".ToIdentifier();

    public AIObjectiveFoo(Character character, AIObjectiveManager objectiveManager, float priorityModifier)
        : base(character, objectiveManager, priorityModifier) { }

    protected override void Act(float deltaTime)
    {
        // Make the bot do something
    }

    protected override bool CheckObjectiveState() => false;
}
```

Then register the factory, e.g. when the plugin is initialized:

```csharp
AIObjectiveRegistrar.RegisterObjectiveFactory(new FooObjectiveFactory());
```

Now any order with the identifier "foo" will make the bot pursue `AIObjectiveFoo`.

## Members

| Member                                                    | Description |
|-----------------------------------------------------------|-------------|
| [`RegisterObjectiveFactory()`](#registerobjectivefactory) |             |

## Methods

### RegisterObjectiveFactory

```cs
void RegisterObjectiveFactory(IOrderObjectiveFactory factory)
```

## Related types

### IOrderObjectiveFactory

```cs
public interface IOrderObjectiveFactory
```

#### CreateObjectiveFromOrder

```cs
AIObjective? CreateObjectiveFromOrder(
    AIObjectiveManager objectiveManager,
    Order order,
    Character character,
    float priorityModifier)
```

Creates a new AIObjective based on the passed order

**Remarks**

If the return value is null, the result is ignored
