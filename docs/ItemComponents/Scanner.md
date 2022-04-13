# Scanner


## Example
```xml
<Item name="" identifier="ruinscanner" description="" category="Equipment" Tags="smallitem,scanner" Scale="0.5" cargocontaineridentifier="metalcrate" impactsoundtag="impact_metal_light" isshootable="true">
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

