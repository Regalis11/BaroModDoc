# MapGenerationParameters

<sup>Relevant files: [Shared:MapGenerationParametersFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/MapGenerationParametersFile.cs) [Shared:MapGenerationParams.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/Map/MapGenerationParams.cs)</sup>

**WARNING:** This file likely generated completely incorrectly!

- **Required by core package:** No

## Attributes

- `DifficultyZones` : `int`
- `Width` : `int`
- `Height` : `int`
- `SmallLevelConnectionLength` : `float`
- `LargeLevelConnectionLength` : `float`
- `VoronoiSiteInterval` : `Point`
- `VoronoiSiteVariance` : `Point`
- `MinConnectionDistance` : `float`
- `MinLocationDistance` : `float`
- `ConnectionIndicatorIterationMultiplier` : `float`
- `ConnectionIndicatorDisplacementMultiplier` : `float`

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

