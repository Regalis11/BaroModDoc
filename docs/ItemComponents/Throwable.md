# Throwable


## Example
```xml
<Item name="" identifier="chlorine" category="Material" maxstacksize="8" cargocontaineridentifier="chemicalcrate" Tags="smallitem,chem,medical" description="" useinhealthinterface="true" scale="0.5" impactsoundtag="impact_metal_light">
  <Throwable characterusable="true" canBeCombined="true" removeOnCombined="true" slots="Any,RightHand,LeftHand" throwforce="4.0" aimpos="35,-10">
    <!-- Remove the item when fully used -->
    <StatusEffect type="OnBroken" target="This">
      <Remove />
    </StatusEffect>
  </Throwable>
  [...]
</Item>
```

