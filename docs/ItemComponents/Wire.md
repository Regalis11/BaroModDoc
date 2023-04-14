# Wire


## Attributes

| Attribute      | Type  | Default value | Description                                                                                 |
|----------------|-------|---------------|---------------------------------------------------------------------------------------------|
| MaxLength      | float | 5000          | The maximum distance the wire can extend (in pixels).                                       |
| HiddenInGame   | bool  | false         | If enabled, the wire will not be visible in connection panels outside the submarine editor. |
| NoAutoLock     | bool  | false         | If enabled, this wire will be ignored by the "Lock all default wires" setting.              |
| UseSpriteDepth | bool  | false         | If enabled, this wire will use the sprite depth instead of a constant depth.                |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="wire" category="Electrical" Tags="smallitem,wire" maxstacksize="8" spritecolor="210,215,218,255" InventoryIconColor="210,215,218,255" canbepicked="true" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light">
  <Wire />
  [...]
</Item>
```

