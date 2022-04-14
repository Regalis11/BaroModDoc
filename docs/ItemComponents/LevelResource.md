# LevelResource


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| DeattachDuration|float|1.0|How long it takes to deattach the item from the level walls (in seconds). |
| DeattachTimer|float|0.0|How far along the item is to being deattached. When the timer goes above DeattachDuration, the item is deattached. |
| RandomOffsetFromWall|float|1.0|How much the position of the item can vary from the wall the item spawns on. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="paralyxis" category="Material" maxstacksize="8" Tags="smallitem" scale="0.5" cargocontaineridentifier="metalcrate" canbepicked="true">
  <LevelResource deattachduration="4" randomoffsetfromwall="20">
    <Commonness commonness="0.02" />
    <RequiredItem items="cuttingequipment" type="Equipped" />
  </LevelResource>
  <Holdable canBeCombined="true" removeOnCombined="true" slots="Any,RightHand,LeftHand" handle1="0,0" msg="ItemMsgPickUpSelect" attachable="true" reattachable="false">
    <!-- Remove the item when fully used -->
    <StatusEffect type="OnBroken" target="This">
      <Remove />
    </StatusEffect>
  </Holdable>
  [...]
</Item>
```

