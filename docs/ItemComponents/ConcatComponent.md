# ConcatComponent


## Example
```xml
<Item name="" identifier="concatcomponent" category="Electrical" Tags="smallitem,logic" maxstacksize="8" cargocontaineridentifier="metalcrate" description="" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <ConcatComponent canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="signal_in1" displayname="connection.signalinx~[num]=1" />
    <input name="signal_in2" displayname="connection.signalinx~[num]=2" />
    <output name="signal_out" displayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

