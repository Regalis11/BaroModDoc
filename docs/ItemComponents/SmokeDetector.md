# SmokeDetector


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| MaxOutputLength|int|200|The maximum length of the output strings. Warning: Large values can lead to large memory usage or networking issues. |
| Output|string|"1"|The signal the item outputs when it has detected a fire. |
| FalseOutput|string|"0"|The signal the item outputs when it has not detected a fire. |



## Example
```xml
<Item identifier="smokedetector" category="Electrical" Tags="smallitem,sensor" maxstacksize="8" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <SmokeDetector FireSizeThreshold="50" canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem identifier="screwdriver" type="Equipped" />
    <output name="signal_out" displayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

