# StatusHUD


## Attributes

| Attribute          | Type  | Default value   | Description                                                                  |
|--------------------|-------|-----------------|------------------------------------------------------------------------------|
| Range              | float | 500             | How close to a target the user must be to see their health data (in pixels). |
| FadeOutRange       | float | 50              | The range within which the health info texts fades out.                      |
| ThermalGoggles     | bool  | false           |                                                                              |
| DebugWiring        | bool  | false           |                                                                              |
| ShowDeadCharacters | bool  | true            |                                                                              |
| ShowTexts          | bool  | true            |                                                                              |
| OverlayColor       | Color | "72,119,72,120" |                                                                              |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="thermalgoggles" scale="0.5" category="Equipment" tags="smallitem" allowasextracargo="true" cargocontaineridentifier="metalcrate" impactsoundtag="impact_metal_light">
  <StatusHUD drawhudwhenequipped="true" overlaycolor="176,0,0,120" range="3000" thermalgoggles="true" showdeadcharacters="false" showtexts="false" />
  [...]
</Item>
```

