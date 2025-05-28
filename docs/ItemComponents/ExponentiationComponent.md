# ExponentiationComponent


## Attributes

| Attribute | Type  | Default value | Description                    |
|-----------|-------|---------------|--------------------------------|
| Exponent  | float | 1             | The exponent of the operation. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="powcomponent" category="Electrical" Tags="smallitem,logic,circuitboxcomponent" maxstacksize="32" maxstacksizecharacterinventory="8" linkable="false" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true" GrabWhenSelected="true" signalcomponentcolor="#9b7eff">
  <ExponentiationComponent canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <requireditem items="screwdriver" type="Equipped" />
    <input name="signal_in" displayname="connection.signalin" />
    <input name="set_exponent" displayname="connection.setexponent" />
    <output name="signal_out" displayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

