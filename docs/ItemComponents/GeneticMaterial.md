# GeneticMaterial


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| Effect|string|""| |
| TaintedEffect|Identifier|"geneticmaterialdebuff"| |
| Tainted|bool|false| |
| SelectedTaintedEffect|Identifier|""| |
| ConditionIncreaseOnCombineMin|float|3.0| |
| ConditionIncreaseOnCombineMax|float|8.0| |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="geneticmaterialhusk" variantof="geneticmaterialcrawler" nameidentifier="geneticmaterial">
  <GeneticMaterial nameidentifier="character.husk" effect="husktransformimmunity" />
  [...]
</Item>
```

