# Scanner


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| ScanDuration|float|1.0|How long it takes for the scan to be completed. |
| ScanTimer|float|0.0|How far along the scan is. When the timer goes above ScanDuration, the scan is completed. |
| ScanRadius|float|1.0|How far the scanner can be from the target for the scan to be successful. |
| AlwaysDisplayProgressBar|bool|true|Should the progress bar always be displayed when the item has been attached. |



## Example
```xml
<Item identifier="ruinscanner" category="Equipment" Tags="smallitem,scanner" Scale="0.5" cargocontaineridentifier="metalcrate" impactsoundtag="impact_metal_light" isshootable="true">
  <Scanner scanduration="30" scanradius="1000" alwaysdisplayprogressbar="false">
    <StatusEffect type="OnActive" targettype="This">
      <Conditional scantimer="gt 0" />
      <ParticleEmitter particle="scannerwavefx" anglemax="360" particlespersecond="0.5" />
      <ParticleEmitter particle="scannerdot" particlespersecond="0.9" />
    </StatusEffect>
  </Scanner>
  <Holdable selectkey="Action" pickkey="Select" slots="Any,RightHand,LeftHand" msg="itemmsgpickupselect" aimpos="35,-10" handle1="0,0" attachable="true" aimable="true">
    <!--<RequiredItem items="wrench" type="Equipped" />-->
  </Holdable>
  [...]
</Item>
```

