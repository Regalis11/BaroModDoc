# IItemComponentRegistrar

```cs
public interface IItemComponentRegistrar : IPluginService
```

Provides methods for registering custom implementations of `ItemComponent` that can be added to items.

## Members

| Member                                                      | Description                                                             |
|-------------------------------------------------------------|-------------------------------------------------------------------------|
| [`RegisterItemComponent<T>()`](#registeritemcomponent)      | Registers a custom item component to be recognized by the game.         |
| [`RegisterItemComponents()`](#registeritemcomponents)       | Registers multiple custom item components to be recognized by the game. |
| [`RegisterAllItemComponents()`](#registerallitemcomponents) | Registers all types in the calling plugin that inherit `ItemComponent`. |
| [`AddItemComponentToItem()`](#additemcomponenttoitem)       | Adds a custom item component to an already existing item.               |

## Methods

### RegisterItemComponent

```cs
void RegisterItemComponent<T>() where T : ItemComponent
```

Registers a custom item component to be recognized by the game.

| Type parameter | Description                                    |
|----------------|------------------------------------------------|
| `T`            | Implementation of `ItemComponent` to register. |

**Example**

MyItemComponent.cs:

```csharp
public class MyItemComponent(Item item, ContentXElement element) : ItemComponent(item, element)
{
    [Serialize(0, IsPropertySaveable.Yes, description: "My custom property")]
    public int MyProperty { get; set; }
}
```

Plugin init:

```csharp
IItemComponentRegistrar.RegisterItemComponent<MyItemComponent>();
```

### RegisterItemComponents

```cs
void RegisterItemComponents(params Type[] types)
```

Registers multiple custom item components to be recognized by the game.

| Parameter | Type     | Description                                           |
|-----------|----------|-------------------------------------------------------|
| `types`   | `Type[]` | list of types inheriting `ItemComponent` to register. |

| Exception           | Condition                                   |
|---------------------|---------------------------------------------|
| `ArgumentException` | if a type does not inherit `ItemComponent`. |

**Example**

MyItemComponent.cs:

```csharp
public class MyItemComponent(Item item, ContentXElement element) : ItemComponent(item, element)
{
    [Serialize("", IsPropertySaveable.Yes, description: "My custom property")]
    public int MyProperty { get; set; }
}

public class AnotherItemComponent(Item item, ContentXElement element) : ItemComponent(item, element)
{
    [Serialize("", IsPropertySaveable.Yes, description: "Another custom property")]
    public string AnotherProperty { get; set; }
}
```

Plugin init:

```csharp
IItemComponentRegistrar.RegisterItemComponents(typeof(MyItemComponent), typeof(AnotherItemComponent));
```

### RegisterAllItemComponents

```cs
void RegisterAllItemComponents()
```

Registers all types in the calling plugin that inherit `ItemComponent`.

**Remarks**

The method will find all types in the calling assembly that inherit `ItemComponent` via reflection and register them using [`RegisterItemComponents`](#registeritemcomponents).

**Example**

Plugin init:

```csharp
IItemComponentRegistrar.RegisterAllItemComponents();
```

### AddItemComponentToItem

```cs
void AddItemComponentToItem(Identifier identifierOrTag, ContentXElement element)
```

Adds a custom item component to an already existing item.

| Parameter         | Type              | Description                                       |
|-------------------|-------------------|---------------------------------------------------|
| `identifierOrTag` | `Identifier`      | Identifier of the item to add the component to.   |
| `element`         | `ContentXElement` | The initial prefab element of the item component. |

**Example**

Plugin init:

```csharp
IItemComponentRegistrar.AddItemComponentToItem("planter".ToIdentifier(), prefabElement));
```
