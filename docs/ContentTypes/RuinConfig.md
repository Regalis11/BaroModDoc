# RuinConfig

- **Required by core package:** Yes

## Examples

### Example 1 - single RuinConfig

```xml
<RuinConfig
  identifier="myRuinConfig"
  TODO="add remaining attributes" />
```

### Example 2 - multiple RuinGenerationParameters

```xml
<RuinGenerationParameters>
  <RuinConfig
    identifier="myRuinConfig1"
    TODO="add remaining attributes" />
  <RuinConfig
    identifier="myRuinConfig2"
    TODO="add remaining attributes" />
</RuinGenerationParameters>
```

### Example 3 - overriding existing RuinGenerationParameters

```xml
<override>
  <RuinConfig
    identifier="myRuinConfig1"
    TODO="add remaining attributes" />
  <RuinConfig
    identifier="myRuinConfig2"
    TODO="add remaining attributes" />
</override>
```

