# DelayComponent


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| Delay|float|1.0|How long the item delays the signals (in seconds). |
| ResetWhenSignalReceived|bool|false|Should the component discard previously received signals when a new one is received. |
| ResetWhenDifferentSignalReceived|bool|false|Should the component discard previously received signals when the incoming signal changes. |



## Example
```xml
<Item identifier="delaycomponent" category="Electrical" Tags="smallitem,logic" maxstacksize="8" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
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

