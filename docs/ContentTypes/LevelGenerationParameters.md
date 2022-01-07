# LevelGenerationParameters

<sup>Relevant files: [Shared:LevelGenerationParametersFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/LevelGenerationParametersFile.cs)</sup>

**WARNING:** This file likely generated completely incorrectly!

- **Required by core package:** No

## Attributes



## Examples

### Example 1 - single LevelGenerationParameter

```xml
<LevelGenerationParameter
  identifier="myLevelGenerationParameter" />
```

### Example 2 - multiple LevelGenerationParameters

```xml
<LevelGenerationParameters>
  <LevelGenerationParameter
    identifier="myLevelGenerationParameter1" />
  <LevelGenerationParameter
    identifier="myLevelGenerationParameter2" />
</LevelGenerationParameters>
```

### Example 3 - overriding existing LevelGenerationParameters

```xml
<override>
  <LevelGenerationParameter
    identifier="myLevelGenerationParameter1" />
  <LevelGenerationParameter
    identifier="myLevelGenerationParameter2" />
</override>
```

