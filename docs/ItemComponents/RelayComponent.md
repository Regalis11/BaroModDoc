# RelayComponent


## Example
```xml
<Item identifier="relaycomponent" category="Electrical" Tags="smallitem,signal,logic" maxstacksize="8" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
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

