# Terminal


## Example
```xml
<Item identifier="terminal" category="Electrical" Tags="smallitem,logic" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <Terminal canbeselected="true" msg="ItemMsgInteractSelect" AllowInGameEditing="false">
    <GuiFrame relativesize="0.35,0.35" anchor="Center" style="ItemUI" />
  </Terminal>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem identifier="screwdriver" type="Equipped" />
    <input name="signal_in" displayname="connection.signalin" />
    <output name="signal_out" displayname="connection.signalout" />
    <input name="set_text_color" displayname="connection.settextcolor" />
    <input name="clear_text" displayname="connection.cleartext" />
  </ConnectionPanel>
  [...]
</Item>
```

