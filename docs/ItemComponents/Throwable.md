# Throwable


## Example
```xml
<Item name="" identifier="food" nameidentifier="food" Tags="smallitem,petfood1,petfood2,petfood3" maxstacksize="8" hideinmenus="true" health="50" damagedbyexplosions="true" category="Misc" scale="0.5" impactsoundtag="impact_metal_light" cargocontaineridentifier="metalcrate">
  <Throwable characterusable="true" slots="Any,RightHand,LeftHand" throwforce="4.0" aimpos="35,-10" msg="ItemMsgPickUpSelect">
    <StatusEffect type="OnBroken" target="This">
      <Remove />
    </StatusEffect>
  </Throwable>
  [...]
</Item>
```

