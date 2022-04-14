# Holdable


## Example
```xml
<Item identifier="paralyxis" category="Material" maxstacksize="8" Tags="smallitem" scale="0.5" cargocontaineridentifier="metalcrate" canbepicked="true">
  <Holdable canBeCombined="true" removeOnCombined="true" slots="Any,RightHand,LeftHand" handle1="0,0" msg="ItemMsgPickUpSelect" attachable="true" reattachable="false">
    <!-- Remove the item when fully used -->
    <StatusEffect type="OnBroken" target="This">
      <Remove />
    </StatusEffect>
  </Holdable>
  <LevelResource deattachduration="4" randomoffsetfromwall="20">
    <Commonness commonness="0.02" />
    <RequiredItem items="cuttingequipment" type="Equipped" />
  </LevelResource>
  [...]
</Item>
```

