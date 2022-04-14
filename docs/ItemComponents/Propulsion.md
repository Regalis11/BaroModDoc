# Propulsion


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| UsableIn|UseEnvironment|UseEnvironment.Both|Can the item be used in air, underwater or both. |
| Force|float|0.0|The force to apply to the user's body. |



## Example
```xml
<Item identifier="extinguisher" category="Equipment" Tags="mediumitem,fireextinguisher,provocative" cargocontaineridentifier="metalcrate" Scale="0.5" impactsoundtag="impact_metal_light">
  <Propulsion force="-80" usablein="water" particles="bubbles" />
  [...]
</Item>
```

