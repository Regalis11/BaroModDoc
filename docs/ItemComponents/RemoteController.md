# RemoteController


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| Target|string|""|Tag or identifier of the item that should be controlled. |
| OnlyInOwnSub|bool|false| |
| Range|float|10000.0| |



## Example
```xml
<Item identifier="reactorpda" category="Equipment" Tags="smallitem" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light">
  <RemoteController target="reactor" onlyinownsub="true" msg="ItemMsgInteractSelect" AllowInGameEditing="false" drawhudwhenequipped="true" />
  [...]
</Item>
```

