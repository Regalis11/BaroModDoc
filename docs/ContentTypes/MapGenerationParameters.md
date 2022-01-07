# MapGenerationParameters

<sup>Relevant files: [Shared:MapGenerationParametersFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/MapGenerationParametersFile.cs) [Shared:MapGenerationParams.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/Map/MapGenerationParams.cs)</sup>
- **Required by core package:** No

## Attributes

- `ConnectionIndicatorIterationMultiplier` : `float`
- `Height` : `int`
- `SmallLevelConnectionLength` : `float`
- `MinLocationDistance` : `float`
- `LargeLevelConnectionLength` : `float`
- `ConnectionIndicatorDisplacementMultiplier` : `float`
- `VoronoiSiteVariance` : `Point`
- `VoronoiSiteInterval` : `Point`
- `DifficultyZones` : `int`
- `MinConnectionDistance` : `float`
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

