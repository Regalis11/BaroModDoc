# XorComponent


## Attributes

| Attribute       | Type   | Default value | Description                                                                                                                                                    |
|-----------------|--------|---------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------|
| TimeFrame       | float  | 0             | The item sends the output if both inputs have received a non-zero signal within the timeframe. If set to 0, the inputs must receive a signal at the same time. |
| MaxOutputLength | int    | 200           | The maximum length of the output strings. Warning: Large values can lead to large memory usage or networking issues.                                           |
| Output          | string | "1"           | The signal sent when the condition is met.                                                                                                                     |
| FalseOutput     | string | "0"           | The signal sent when the condition is met (if empty, no signal is sent).                                                                                       |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="xorcomponent" category="Electrical" Tags="smallitem,logic,circuitboxcomponent" maxstacksize="32" maxstacksizecharacterinventory="8" linkable="true" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true" GrabWhenSelected="true" signalcomponentcolor="#c3a27a">
  <XorComponent canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <requireditem items="screwdriver" type="Equipped" />
    <input name="signal_in1" displayname="connection.signalinx~[num]=1" />
    <input name="signal_in2" displayname="connection.signalinx~[num]=2" />
    <input name="set_output" displayname="connection.setoutput" />
    <output name="signal_out" displayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

