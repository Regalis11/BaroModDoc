# IAIObjectiveRegistrar

```cs
public interface IAIObjectiveRegistrar : IPluginService
```

Allows plugins to configure what AIObjective is created for an order identifier.

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
