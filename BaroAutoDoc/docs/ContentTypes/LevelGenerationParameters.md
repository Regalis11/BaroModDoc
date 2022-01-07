# LevelGenerationParameters

- **Required by core package:** No

**WARNING:** This file likely generated completely incorrectly!

## Examples

### Example 1 - single LevelGenerationParameter

```xml
<LevelGenerationParameter
  identifier="myLevelGenerationParameter"
  TODO="add remaining attributes" />
```

### Example 2 - multiple LevelGenerationParameters

```xml
<LevelGenerationParameters>
  <LevelGenerationParameter
    identifier="myLevelGenerationParameter1"
    TODO="add remaining attributes" />
  <LevelGenerationParameter
    identifier="myLevelGenerationParameter2"
    TODO="add remaining attributes" />
</LevelGenerationParameters>
```

### Example 3 - overriding existing LevelGenerationParameters

```xml
<override>
  <LevelGenerationParameter
    identifier="myLevelGenerationParameter1"
    TODO="add remaining attributes" />
  <LevelGenerationParameter
    identifier="myLevelGenerationParameter2"
    TODO="add remaining attributes" />
</override>
```

