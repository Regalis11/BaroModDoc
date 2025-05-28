# Wire


## Attributes

| Attribute      | Type  | Default value | Description                                                                                                       |
|----------------|-------|---------------|-------------------------------------------------------------------------------------------------------------------|
| Width          | float | 0.3           |                                                                                                                   |
| MaxLength      | float | 5000          | The maximum distance the wire can extend (in pixels).                                                             |
| HiddenInGame   | bool  | false         | If enabled, the wire will not be visible in connection panels outside the submarine editor.                       |
| NoAutoLock     | bool  | false         | If enabled, this wire will be ignored by the "Lock all default wires" setting.                                    |
| UseSpriteDepth | bool  | false         | If enabled, this wire will use the sprite depth instead of a constant depth.                                      |
| DropOnConnect  | bool  | true          | If disabled, the wire will not be dropped when connecting. Used in circuit box to store the wires inside the box. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="wire" category="Electrical" Tags="smallitem,wire" maxstacksize="32" maxstacksizecharacterinventory="8" spritecolor="210,215,218,255" InventoryIconColor="210,215,218,255" canbepicked="true" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light">
  <Wire />
  [...]
</Item>
```

