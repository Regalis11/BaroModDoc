# Throwable


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| ThrowForce|float|1.0|The impulse applied to the physics body of the item when thrown. Higher values make the item be thrown faster. |



## Example
```xml
<Item identifier="chlorine" category="Material" maxstacksize="8" cargocontaineridentifier="chemicalcrate" Tags="smallitem,chem,medical" useinhealthinterface="true" scale="0.5" impactsoundtag="impact_metal_light">
  <Throwable characterusable="true" canBeCombined="true" removeOnCombined="true" slots="Any,RightHand,LeftHand" throwforce="4.0" aimpos="35,-10">
    <!-- Remove the item when fully used -->
    <StatusEffect type="OnBroken" target="This">
      <Remove />
    </StatusEffect>
  </Throwable>
  [...]
</Item>
```

