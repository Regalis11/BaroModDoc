# DivideComponent


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| ClampMax|float|999999.0|The output of the item is restricted below this value. |
| ClampMin|float|-999999.0|The output of the item is restricted above this value. |
| TimeFrame|float|0.0|The item must have received signals to both inputs within this timeframe to output the result. If set to 0, the inputs must be received at the same time. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="dividecomponent" category="Electrical" Tags="smallitem,logic" maxstacksize="8" linkable="false" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <DivideComponent canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <requireditem items="screwdriver" type="Equipped" />
    <input name="signal_in1" displayname="connection.signalinx~[num]=1" />
    <input name="signal_in2" displayname="connection.signalinx~[num]=2" />
    <output name="signal_out" displayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

