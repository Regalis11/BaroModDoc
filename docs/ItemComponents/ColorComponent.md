# ColorComponent


## Attributes

| Attribute | Type | Default value | Description                                                                                                                                                                                    |
|-----------|------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| UseHSV    | bool | false         | When enabled makes the component translate the signal from HSV into RGB where red is the hue between 0 and 360, green is the saturation between 0 and 1 and blue is the value between 0 and 1. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="colorcomponent" category="Electrical" Tags="smallitem,logic,circuitboxcomponent" maxstacksize="32" maxstacksizecharacterinventory="8" linkable="false" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true" GrabWhenSelected="true" signalcomponentcolor="#b3b3b4">
  <ColorComponent canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <requireditem items="screwdriver" type="Equipped" />
    <input name="signal_r" displayname="connection.signalr" />
    <input name="signal_g" displayname="connection.signalg" />
    <input name="signal_b" displayname="connection.signalb" />
    <input name="signal_a" displayname="connection.signala" />
    <output name="signal_out" displayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

