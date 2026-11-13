# Creating custom item components

Item components are the building blocks of items: a `Door`, a `Holdable` or a `Reactor` are all item components. Plugins can define new ones in C# and use them in item XML just like the built-in ones. If you haven't set up a plugin project yet, see [Getting started with plugins](GettingStarted.md).

## Creating a new item component class

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

- **`IsActive`** controls whether `Update` is called. It is `false` by default, so set it to `true` in the constructor (or later, when the component should start doing work) if you need per-frame logic.
- **`item`** is the item that owns the component. For example, `item.body` is the item's physics body, which only exists if the item has a `<Body>` element in its XML.

### Properties and XML attributes

Properties marked with the `[Serialize]` attribute can be set from the item's XML:

```cs
[Serialize(defaultValue, IsPropertySaveable.No, "Description")]
public float MyProperty { get; private set; }
```

`defaultValue` - The value used when the attribute is not present in the XML. Also determines the property's type.
`IsPropertySaveable` - `Yes` if the value should be written to save files and submarine files, `No` if it only comes from the XML prefab.
`description` - A description of the property.

The XML attribute name matches the property name, so the property above is set with `MyProperty="10"` in the item component XML.

### Overriding behavior

`ItemComponent` has a number of virtual methods you can override to react to what happens to the item.

For example, `RemoveComponentSpecific` is called when the item is removed. Use it to undo anything the component set up outside itself, such as adding itself to a list or subscribing to an event:

```cs
private static readonly List<ExampleItemComponent> activeComponents = new();

public ExampleItemComponent(Item item, ContentXElement element) : base(item, element)
{
    IsActive = true;
    activeComponents.Add(this);
}

protected override void RemoveComponentSpecific()
{
    activeComponents.Remove(this);
}
```

You can find more by looking at the overridable methods in the ItemComponent class.

## Register the component

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
}
```

Registered components are automatically unregistered when the plugin is unloaded. See the [`IItemComponentRegistrar` reference](Services/IItemComponentRegistrar.md) for its other methods.


## Using the item component in an item

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

## Drawing and UI

Drawing and UI only exist on the client, so put this code in `#if CLIENT` blocks or in the **ClientProject**.

### Drawing in the world

To draw something on the item in the game world, implement `IDrawableComponent`. `DrawSize` is the size of the area you draw, which the game uses to decide whether the item is on screen:

```cs
public class ExampleItemComponent : ItemComponent, IDrawableComponent
{
    // ...

#if CLIENT
    public Vector2 DrawSize => new Vector2(64, 64);

    public void Draw(SpriteBatch spriteBatch, bool editing, float itemDepth = -1, Color? overrideColor = null)
    {
        Vector2 pos = new Vector2(item.DrawPosition.X, -item.DrawPosition.Y);
        GUI.DrawRectangle(spriteBatch, pos - DrawSize / 2, DrawSize, Color.Red);
    }
#endif
}
```

World positions have Y pointing up, so make sure to flip it when drawing.

### Item interfaces

An item's interface is shown when a character selects the item, or when they equip it if the component has `DrawHudWhenEquipped="true"`. For selecting, the component needs `CanBeSelected="true"`:

```xml
<ExampleItemComponent CanBeSelected="true">
  <GuiFrame relativesize="0.2,0.15" anchor="Center" style="ItemUI" />
</ExampleItemComponent>
```

The `<GuiFrame>` element creates the component's `GuiFrame`, a window you can fill with UI elements. Build the UI by overriding `CreateGUI`:

```cs
public ExampleItemComponent(Item item, ContentXElement element) : base(item, element)
{
#if CLIENT
    CreateGUI();
#endif
}

#if CLIENT
public override bool RecreateGUIOnResolutionChange => true;

protected override void CreateGUI()
{
    if (GuiFrame == null) { return; }

    new GUITextBlock(new RectTransform(Vector2.One, GuiFrame.RectTransform), "Hello from the item!");
}
#endif
```

The game doesn't call `CreateGUI` when the component is created, so call it yourself from the constructor, like the vanilla components do. With `RecreateGUIOnResolutionChange` set to `true`, the game recreates the `GuiFrame` and calls `CreateGUI` again whenever the resolution changes, so the UI keeps the right size.

While the interface is shown, the game also calls these methods every frame, which you can override:

- `UpdateHUDComponentSpecific(Character character, float deltaTime, Camera cam)` to update the UI, for example to refresh a value shown in it.
- `DrawHUD(SpriteBatch spriteBatch, Character character)` to draw directly on the screen.

## Syncing the component in multiplayer

Component properties aren't synced automatically. See [Item components](Networking.md#item-components) on the networking page for how to sync them.
