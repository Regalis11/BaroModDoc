# Creating custom item components

Item components are the building blocks of items: a `Door`, a `Holdable` or a `Reactor` are all item components. Plugins can define new ones in C# and use them in item XML just like the built-in ones. This page walks through creating one, based on the [Examples plugin](https://github.com/FakeFishGames/ExamplesBaroPlugin). If you haven't set up a plugin project yet, see [Getting started with plugins](GettingStarted.md).

## 1. Write the component

Create a class that inherits from `ItemComponent` and has a constructor taking `(Item item, ContentXElement element)`:

```cs
using Barotrauma;
using Barotrauma.Items.Components;

namespace Examples;

public class ExampleItemComponent : ItemComponent
{
    [Serialize(1000f, IsPropertySaveable.No, "How much torque is applied to the object every frame")]
    public float SpinForce { get; private set; }

    public ExampleItemComponent(Item item, ContentXElement element) : base(item, element)
    {
        IsActive = true;
    }

    public override void Update(float deltaTime, Camera cam)
    {
        if (item.body != null)
        {
            item.body.ApplyTorque(SpinForce * deltaTime);
        }
    }
}
```

A few things to note:

- **The constructor** must have exactly this signature, because the game creates components by reflection when it loads an item.
- **`IsActive`** controls whether `Update` is called. It is `false` by default, so set it to `true` in the constructor (or later, when the component should start doing work) if you need per-frame logic.
- **`item`** is the item that owns the component. For example, `item.body` is the item's physics body, which only exists if the item has a `<Body>` element in its XML.

### Properties and XML attributes

Properties marked with the `[Serialize]` attribute can be set from the item's XML:

```cs
[Serialize(defaultValue, IsPropertySaveable.No, "Description")]
public float MyProperty { get; private set; }
```

| Parameter | Description |
|-----------|-------------|
| `defaultValue` | The value used when the attribute is not present in the XML. Also determines the property's type. |
| `IsPropertySaveable` | `Yes` if the value should be written to save files and submarine files, `No` if it only comes from the XML prefab. |
| `description` | A description of the property. |

The XML attribute name matches the property name and is not case-sensitive, so the property above is set with `SpinForce="10"` or `spinforce="10"`.

### Overriding behavior

`ItemComponent` has a number of virtual methods you can override to react to what happens to the item. Some of the most commonly used ones:

| Method | Called when |
|--------|-------------|
| `Update(float deltaTime, Camera cam)` | Every frame, while `IsActive` is true. |
| `Use(float deltaTime, Character character)` | A character uses the item (for example by left-clicking with it equipped). |
| `SecondaryUse(float deltaTime, Character character)` | A character uses the item with the secondary action. |
| `Select(Character character)` / `Pick(Character picker)` | A character selects (interacts with) or picks up the item. |
| `Equip(Character character)` / `Unequip(Character character)` | The item is equipped or unequipped. |
| `ReceiveSignal(Signal signal, Connection connection)` | The item receives a signal through one of its connections. |
| `OnItemLoaded()` / `OnMapLoaded()` | The item or the whole map has finished loading. |
| `RemoveComponentSpecific()` | The component is being removed. Use it to clean up anything you created. |

For example, if your component creates a physics body, remove it when the component is removed:

```cs
protected override void RemoveComponentSpecific()
{
    if (Pusher != null)
    {
        Pusher.Remove();
    }
}
```

Many components only need a couple of these. See the [item component documentation](../ItemComponents/ItemComponent.md) for the attributes shared by all components.

## 2. Register the component

The game only recognizes component types that have been registered. In your plugin's `Init`, get the `IItemComponentRegistrar` service and register the class:

```cs
public partial class Plugin : IBarotraumaPlugin
{
    public static readonly IItemComponentRegistrar ItemComponentRegistrar =
        PluginServiceProvider.GetService<IItemComponentRegistrar>();

    public void Init()
    {
        ItemComponentRegistrar.RegisterItemComponent<ExampleItemComponent>();
    }

    // ...
}
```

Registered components are automatically unregistered when the plugin is unloaded.

### Registration methods

`IItemComponentRegistrar` provides these methods (see the [full reference](Services/IItemComponentRegistrar.md)):

| Method | Description |
|--------|-------------|
| `RegisterItemComponent<T>()` | Registers a single component type. |
| `RegisterItemComponents(params Type[] types)` | Registers several component types. Throws an `ArgumentException` if one of the types doesn't inherit `ItemComponent`. |
| `RegisterAllItemComponents()` | Finds every non-abstract class in your plugin that inherits `ItemComponent` and registers it. |
| `AddItemComponentToItem(Identifier identifierOrTag, ContentXElement element)` | Adds a component to an item that already exists (for example a vanilla item), without editing its XML. `element` is the XML element describing the component. |

## 3. Use it in an item

In the item's XML, use the class name as the element name and set the properties as attributes:

```xml
<Items>
  <Item name="Example Item" identifier="exampleitem">
    <Sprite texture="Content/Items/OxygenGenerator/oxygengenerator.png" depth="0.3" sourcerect="848,16,143,143"/>
    <Body width="100" height="100" density="20" />
    <ExampleItemComponent SpinForce="10100.0" />
  </Item>
</Items>
```

Make sure this file is listed in your plugin's `ContentPackageBuilder/filelist.xml` as an `Item` file, as described in [content packages](../Intro/ContentPackages.md):

```xml
<Item file="%ModDir%/Content/ExampleItems.xml" />
```

Start the game with your plugin enabled and spawn the item (for example with the `spawnitem exampleitem` console command) to try it out.
