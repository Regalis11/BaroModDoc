# OscillatorComponent


## Example
```xml
<Item name="" identifier="oscillator" category="Electrical" Tags="smallitem,signal" maxstacksize="8" cargocontaineridentifier="metalcrate" description="" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <OscillatorComponent canbeselected="true" outputtype="Pulse" frequency="1" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="set_frequency" displayname="connection.setfrequency" />
    <input name="set_outputtype" displayname="connection.setoutputtype" />
    <output name="signal_out" displayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

