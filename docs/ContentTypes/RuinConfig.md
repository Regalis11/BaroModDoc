# RuinConfig

<sub>Relevant files: [Shared:RuinConfigFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/RuinConfigFile.cs) [Shared:RuinGenerationParams.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource\Map\Levels\Ruins\RuinGenerationParams.cs)</sub>
- **Required by core package:** Yes

## Attributes


## Examples

### Example 1 - single RuinConfig

```xml
<RuinConfig
  identifier="myRuinConfig" />
```

### Example 2 - multiple RuinGenerationParameters

```xml
<RuinGenerationParameters>
  <RuinConfig
    identifier="myRuinConfig1" />
  <RuinConfig
    identifier="myRuinConfig2" />
</RuinGenerationParameters>
```

### Example 3 - overriding existing RuinGenerationParameters

```xml
<override>
  <RuinConfig
    identifier="myRuinConfig1" />
  <RuinConfig
    identifier="myRuinConfig2" />
</override>
```

