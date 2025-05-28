# MultiplexerComponent


## Attributes

| Attribute            | Type | Default value | Description                                                                                             |
|----------------------|------|---------------|---------------------------------------------------------------------------------------------------------|
| SelectedConnection   | int  | 0             | The index of the selected connection.                                                                   |
| WrapAround           | bool | true          | Should the selected connection go back to the first one when moving past the last one?                  |
| SkipEmptyConnections | bool | true          | Should empty connections (connections with no wires in them) be skipped over when moving the selection? |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="multiplexercomponent" category="Electrical" Tags="smallitem,logic,circuitboxcomponent" maxstacksize="32" maxstacksizecharacterinventory="8" cargocontaineridentifier="metalcrate" signalcomponentcolor="228,76,67,255" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true" GrabWhenSelected="true">
  <MultiplexerComponent canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="signal_in0" displayname="connection.signalinx~[num]=0" />
    <input name="signal_in1" displayname="connection.signalinx~[num]=1" />
    <input name="signal_in2" displayname="connection.signalinx~[num]=2" />
    <input name="signal_in3" displayname="connection.signalinx~[num]=3" />
    <input name="signal_in4" displayname="connection.signalinx~[num]=4" />
    <input name="signal_in5" displayname="connection.signalinx~[num]=5" />
    <input name="signal_in6" displayname="connection.signalinx~[num]=6" />
    <input name="signal_in7" displayname="connection.signalinx~[num]=7" />
    <input name="signal_in8" displayname="connection.signalinx~[num]=8" />
    <input name="signal_in9" displayname="connection.signalinx~[num]=9" />
    <input name="set_input" displayname="connection.setinput" />
    <input name="move_input" displayname="connection.moveinput" />
    <output name="signal_out" displayname="connection.signalout" />
    <output name="selected_input_out" displayname="connection.selectedinputout" />
  </ConnectionPanel>
  [...]
</Item>
```

