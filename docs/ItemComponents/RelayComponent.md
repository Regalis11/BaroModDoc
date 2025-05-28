# RelayComponent


## Attributes

| Attribute | Type  | Default value | Description                                                 |
|-----------|-------|---------------|-------------------------------------------------------------|
| MaxPower  | float | 1000          | The maximum amount of power that can pass through the item. |
| IsOn      | bool  | true          | Can the relay currently pass power and signals through it.  |

This component also supports the attributes defined in: [PowerTransfer](PowerTransfer.md), [Powered](Powered.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="relaycomponent" category="Electrical" Tags="smallitem,signal,logic,circuitboxcomponent" maxstacksize="32" maxstacksizecharacterinventory="8" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true" GrabWhenSelected="true" signalcomponentcolor="#694341">
  <RelayComponent canbeselected="true" vulnerabletoemp="false" canbeoverloaded="false">
    <GuiFrame relativesize="0.2,0.14" minsize="450,160" anchor="Center" style="ItemUI" />
  </RelayComponent>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <input name="signal_in1" displayname="connection.signalinx~[num]=1" />
    <input name="signal_in2" displayname="connection.signalinx~[num]=2" />
    <input name="toggle" displayname="connection.togglestate" />
    <input name="set_state" displayname="connection.setstate" />
    <output name="power_out" displayname="connection.powerout" />
    <output name="signal_out1" displayname="connection.signaloutx~[num]=1" />
    <output name="signal_out2" displayname="connection.signaloutx~[num]=2" />
    <output name="state_out" displayname="connection.stateout" fallbackdisplayname="connection.signalout" />
    <output name="load_value_out" displayname="connection.loadvalueout" />
    <output name="power_value_out" displayname="connection.powervalueout" />
  </ConnectionPanel>
  [...]
</Item>
```

