# ItemContainer


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| Capacity|int|5|How many items can be contained inside this item. |
| MaxStackSize|int|64|How many items can be stacked in one slot. Does not increase the maximum stack size of the items themselves, e.g. a stack of bullets could have a maximum size of 8 but the number of bullets in a specific weapon could be restricted to 6. |
| HideItems|bool|true|Should the items contained inside this item be hidden. If set to false, you should use the ItemPos and ItemInterval properties to determine where the items get rendered. |
| ItemPos|Vector2|"0.0,0.0"|The position where the contained items get drawn at (offset from the upper left corner of the sprite in pixels). |
| ItemInterval|Vector2|"0.0,0.0"|The interval at which the contained items are spaced apart from each other (in pixels). |
| ItemsPerRow|int|100|How many items are placed in a row before starting a new row. |
| DrawInventory|bool|true|Should the inventory of this item be visible when the item is selected. |
| AllowDragAndDrop|bool|true|Allow dragging and dropping items to deposit items into this inventory. |
| AllowSwappingContainedItems|bool|true| |
| AutoInteractWithContained|bool|false|If set to true, interacting with this item will make the character interact with the contained item(s), automatically picking them up if they can be picked up. |
| AllowAccess|bool|true| |
| AccessOnlyWhenBroken|bool|false| |
| SlotsPerRow|int|5|How many inventory slots the inventory has per row. |
| ContainableRestrictions|string|""|Define items (by identifiers or tags) that bots should place inside this container. If empty, no restrictions are applied. |
| AutoFill|bool|true|Should this container be automatically filled with items? |
| ItemRotation|float|0.0|The rotation in which the contained sprites are drawn (in degrees). |
| SpawnWithId|string|""|Specify an item for the container to spawn with. |
| SpawnWithIdWhenBroken|bool|false|Should the items configured using SpawnWithId spawn if this item is broken. |
| AutoInject|bool|false|Should the items be injected into the user. |
| AutoInjectThreshold|float|0.5|The health threshold that the user must reach in order to activate the autoinjection. |
| RemoveContainedItemsOnDeconstruct|bool|false| |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="alienitemcontainersmall" category="Alien" tags="aliencontainer" linkable="true" pickdistance="150" scale="0.5">
  <ItemContainer capacity="5" canbeselected="true" hideitems="true" msg="ItemMsgInteractSelect">
    <GuiFrame relativesize="0.3,0.2" anchor="Center" style="ItemUI" />
    <Containable items="smallitem,mediumitem" />
  </ItemContainer>
  [...]
</Item>
```

