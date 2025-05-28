# GeneticMaterial


## Attributes

| Attribute                             | Type       | Default value           | Description                                                                                   |
|---------------------------------------|------------|-------------------------|-----------------------------------------------------------------------------------------------|
| Effect                                | string     | ""                      |                                                                                               |
| TaintedEffect                         | Identifier | "geneticmaterialdebuff" | Either the identifier or the type for the tainted effect prefab                               |
| Tainted                               | bool       | false                   |                                                                                               |
| SetTaintedOnDeath                     | bool       | false                   |                                                                                               |
| CanBeUntainted                        | bool       | false                   |                                                                                               |
| SelectedTaintedEffect                 | Identifier | ""                      |                                                                                               |
| ConditionIncreaseOnCombineMin         | float      | 0                       |                                                                                               |
| ConditionIncreaseOnCombineMax         | float      | 0                       |                                                                                               |
| ConditionIncreaseOnLowQualityCombine  | float      | 3                       | When refining, min value for condition increase bonus based on the quality of the worse gene. |
| ConditionIncreaseOnHighQualityCombine | float      | 25                      | When refining, max value for condition increase bonus based on the quality of the worse gene. |
| TooltipValueMin                       | float      | 0                       |                                                                                               |
| TooltipValueMax                       | float      | 0                       |                                                                                               |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="geneticmaterialmoloch" variantof="geneticmaterialcrawler" nameidentifier="geneticmaterial">
  <GeneticMaterial nameidentifier="character.moloch" effect="damageresistance" tooltipvaluemin="10" tooltipvaluemax="25" />
  [...]
</Item>
```

