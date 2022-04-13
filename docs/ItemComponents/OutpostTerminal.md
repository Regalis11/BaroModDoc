# OutpostTerminal


## Example
```xml
<Item name="" identifier="outpostterminal" category="Machine" Tags="smallitem" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <OutpostTerminal canbeselected="true" msg="ItemMsgInteractSelect" AllowInGameEditing="false" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem identifier="screwdriver" type="Equipped" />
    <input name="signal_in" displayname="connection.signalin" />
    <output name="signal_out" displayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

