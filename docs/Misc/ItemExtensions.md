# Item extensions

Item extensions add [item components](../ContentTypes/Item.md) to items that already exist, such as vanilla items or items from another mod, without overriding them. Because the original item isn't replaced, several mods can extend the same item, and your mod keeps working when the vanilla item changes.

To change or remove the components an item already has, use an [override](../Intro/Overrides.md) instead.

## Adding the file

Item extensions are defined in an XML file listed in your mod's `filelist.xml` with the `ItemExtensions` content type:

```xml
<contentpackage name="Glowing Wrench" modversion="1.0.0" corepackage="False">
  <ItemExtensions file="%ModDir%/ItemExtensions.xml" />
</contentpackage>
```

## Writing an extension

The file has an `<ItemExtensions>` root element with one `<Extension>` element per extension. Each child of `<Extension>` is added to the item as a list of item components:

```xml
<ItemExtensions>
  <Extension identifier="glowingwrench" items="wrench">
    <LightComponent LightColor="50,255,50,255" Flicker="0.0" range="250" IsOn="true" />
  </Extension>
</ItemExtensions>
```

This makes every wrench glow green.

Only item components can be added. Other elements an `<Item>` can contain, such as `<Sprite>`, `<Body>`, `<Price>`, `<Fabricate>` or `<Upgrade>`, aren't supported in an extension.

| Attribute| Description|
|--- | --- |
| `identifier`| Unique identifier of the extension. Can be used to override an existing item extension.|
| `items`| Comma-separated identifiers of the items to extend.|
| `tags`| Comma-separated tags. The extension is applied to every item that has any of these tags.|
