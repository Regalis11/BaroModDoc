# MemoryComponent


## Example
```xml
<Item name="" identifier="memorycomponent" category="Electrical" Tags="smallitem,logic" maxstacksize="8" linkable="true" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <MemoryComponent canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <requireditem items="screwdriver" type="Equipped" />
    <input name="signal_in" displayname="connection.signalin" />
    <input name="lock_state" aliases="signal_store" displayname="connection.lockstate" />
    <output name="signal_out" displayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

