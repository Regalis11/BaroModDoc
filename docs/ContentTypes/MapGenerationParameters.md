# MapGenerationParameters

- **Required by core package:** No

**WARNING:** This file likely generated completely incorrectly!

## Examples

### Example 1 - single MapGenerationParameter

```xml
<MapGenerationParameter
  identifier="myMapGenerationParameter"
  TODO="add remaining attributes" />
```

### Example 2 - multiple MapGenerationParameters

```xml
<MapGenerationParameters>
  <MapGenerationParameter
    identifier="myMapGenerationParameter1"
    TODO="add remaining attributes" />
  <MapGenerationParameter
    identifier="myMapGenerationParameter2"
    TODO="add remaining attributes" />
</MapGenerationParameters>
```

### Example 3 - overriding existing MapGenerationParameters

```xml
<override>
  <MapGenerationParameter
    identifier="myMapGenerationParameter1"
    TODO="add remaining attributes" />
  <MapGenerationParameter
    identifier="myMapGenerationParameter2"
    TODO="add remaining attributes" />
</override>
```

