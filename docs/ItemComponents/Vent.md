# Vent


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="vent" tags="vent" category="Machine" interactdistance="0" linkable="true" allowedlinks="oxygenerator" scale="0.5">
  <Vent>
    <sound file="Content/Items/OxygenGenerator/Ventilation.ogg" type="OnActive" range="400.0" volumeproperty="OxygenFlow" volume="0.0005f" loop="true" />
  </Vent>
  [...]
</Item>
```

