# EqualsComponent


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| MaxOutputLength|int|200|The maximum length of the output strings. Warning: Large values can lead to large memory usage or networking issues. |
| Output|string|"1"|The signal sent when the condition is met. |
| FalseOutput|string|""|The signal sent when the condition is met (if empty, no signal is sent). |
| TimeFrame|float|0.0|The maximum amount of time between the received signals. If set to 0, the signals must be received at the same time. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="equalscomponent" category="Electrical" Tags="smallitem,logic" maxstacksize="8" linkable="false" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <EqualsComponent canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <requireditem items="screwdriver" type="Equipped" />
    <input name="signal_in1" displayname="connection.signalinx~[num]=1" />
    <input name="signal_in2" displayname="connection.signalinx~[num]=2" />
    <output name="signal_out" displayname="connection.signalout" />
    <input name="set_output" displayname="connection.setoutput" />
  </ConnectionPanel>
  [...]
</Item>
```

