# WifiComponent


## Example
```xml
<Item identifier="wificomponent" category="Electrical" Tags="smallitem,signal" maxstacksize="8" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <WifiComponent canbeselected="true" MinChatMessageInterval="1.0" DiscardDuplicateChatMessages="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="signal_in" displayname="connection.signalin" />
    <output name="signal_out" displayname="connection.signalout" />
    <input name="set_channel" displayname="connection.setchannel" />
  </ConnectionPanel>
  [...]
</Item>
```

