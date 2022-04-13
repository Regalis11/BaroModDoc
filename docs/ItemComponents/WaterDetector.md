# WaterDetector


## Example
```xml
<Item name="" identifier="waterdetector" category="Electrical" Tags="smallitem,sensor" maxstacksize="8" cargocontaineridentifier="metalcrate" description="" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <WaterDetector canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <output name="signal_out" displayname="connection.signalout" />
    <output name="water_%" displayname="connection.waterpercentageout" />
    <output name="high_pressure" displayname="connection.highpressureout" />
  </ConnectionPanel>
  [...]
</Item>
```

