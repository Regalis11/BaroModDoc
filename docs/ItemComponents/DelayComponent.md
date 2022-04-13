# DelayComponent


## Example
```xml
<Item name="" identifier="delaycomponent" category="Electrical" Tags="smallitem,logic" maxstacksize="8" cargocontaineridentifier="metalcrate" description="" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <DelayComponent canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="signal_in" displayname="connection.signalin" />
    <output name="signal_out" displayname="connection.signalout" />
    <input name="set_delay" displayname="connection.setdelay" />
  </ConnectionPanel>
  [...]
</Item>
```

