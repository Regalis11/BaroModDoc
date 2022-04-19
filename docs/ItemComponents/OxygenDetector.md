# OxygenDetector


## Attributes

This component supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="oxygendetector" category="Electrical" Tags="smallitem,sensor" maxstacksize="8" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <OxygenDetector canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <output name="signal_out" displayname="connection.signalout" />
    <output name="low_oxygen" displayname="connection.low_oxygen" />
  </ConnectionPanel>
  [...]
</Item>
```

