# WaterDetector


## Attributes

| Attribute       | Type   | Default value | Description                                                                                                          |
|-----------------|--------|---------------|----------------------------------------------------------------------------------------------------------------------|
| MaxOutputLength | int    | 200           | The maximum length of the output strings. Warning: Large values can lead to large memory usage or networking issues. |
| Output          | string | "1"           | The signal the item sends out when it's underwater.                                                                  |
| FalseOutput     | string | "0"           | The signal the item sends out when it's not underwater.                                                              |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="waterdetector" category="Electrical" Tags="smallitem,sensor" maxstacksize="8" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
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

