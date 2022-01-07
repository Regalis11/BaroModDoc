# MapGenerationParameters

<sub>Relevant files: [Shared:MapGenerationParams.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/Map/MapGenerationParams.cs) [Shared:MapGenerationParametersFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/MapGenerationParametersFile.cs)</sub>
- **Required by core package:** No

## Attributes

- `DifficultyZones` : `int`
- `LargeLevelConnectionLength` : `float`
- `VoronoiSiteInterval` : `Point`
- `ConnectionIndicatorIterationMultiplier` : `float`
- `Height` : `int`
- `ConnectionIndicatorDisplacementMultiplier` : `float`
- `SmallLevelConnectionLength` : `float`
- `MinConnectionDistance` : `float`
- `VoronoiSiteVariance` : `Point`
- `MinLocationDistance` : `float`
- `Width` : `int`
**WARNING:** This file likely generated completely incorrectly!

## Examples

### Example 1 - single MapGenerationParameter

```xml
<MapGenerationParameter
  identifier="myMapGenerationParameter" />
```

### Example 2 - multiple MapGenerationParameters

```xml
<MapGenerationParameters>
  <MapGenerationParameter
    identifier="myMapGenerationParameter1" />
  <MapGenerationParameter
    identifier="myMapGenerationParameter2" />
</MapGenerationParameters>
```

### Example 3 - overriding existing MapGenerationParameters

```xml
<override>
  <MapGenerationParameter
    identifier="myMapGenerationParameter1" />
  <MapGenerationParameter
    identifier="myMapGenerationParameter2" />
</override>
```

