# DemultiplexerComponent


## Attributes

| Attribute            | Type | Default value | Description                                                                                             |
|----------------------|------|---------------|---------------------------------------------------------------------------------------------------------|
| SelectedConnection   | int  | 0             | The index of the selected connection.                                                                   |
| WrapAround           | bool | true          | Should the selected connection go back to the first one when moving past the last one?                  |
| SkipEmptyConnections | bool | true          | Should empty connections (connections with no wires in them) be skipped over when moving the selection? |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="demultiplexercomponent" category="Electrical" Tags="smallitem,logic,circuitboxcomponent" maxstacksize="32" maxstacksizecharacterinventory="8" cargocontaineridentifier="metalcrate" signalcomponentcolor="130,157,92,255" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true" GrabWhenSelected="true">
  <DemultiplexerComponent canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="signal_in" displayname="connection.signalin" />
    <input name="set_output" displayname="connection.setoutput" />
    <input name="move_output" displayname="connection.moveoutput" />
    <output name="signal_out0" displayname="connection.signaloutx~[num]=0" />
    <output name="signal_out1" displayname="connection.signaloutx~[num]=1" />
    <output name="signal_out2" displayname="connection.signaloutx~[num]=2" />
    <output name="signal_out3" displayname="connection.signaloutx~[num]=3" />
    <output name="signal_out4" displayname="connection.signaloutx~[num]=4" />
    <output name="signal_out5" displayname="connection.signaloutx~[num]=5" />
    <output name="signal_out6" displayname="connection.signaloutx~[num]=6" />
    <output name="signal_out7" displayname="connection.signaloutx~[num]=7" />
    <output name="signal_out8" displayname="connection.signaloutx~[num]=8" />
    <output name="signal_out9" displayname="connection.signaloutx~[num]=9" />
    <output name="selected_output_out" displayname="connection.selectedoutputout" />
  </ConnectionPanel>
  [...]
</Item>
```

