# Propulsion


## Attributes

| Attribute    | Type           | Default value | Description                                                           |
|--------------|----------------|---------------|-----------------------------------------------------------------------|
| UsableIn     | UseEnvironment | Both          | Can the item be used in air, underwater or both.                      |
| Force        | float          | 0             | The force to apply to the user's body.                                |
| ApplyToHands | bool           | true          | If the item is held in RightHand or LeftHand, apply extra force there |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="extinguisher" category="Equipment" Tags="mediumitem,fireextinguisher,provocative" cargocontaineridentifier="metalcrate" Scale="0.5" impactsoundtag="impact_metal_light" donttransferbetweensubs="true">
  <Propulsion force="-80" usablein="water" particles="bubbles" />
  [...]
</Item>
```

