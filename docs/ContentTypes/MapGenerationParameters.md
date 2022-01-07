# MapGenerationParameters

<sub>Relevant files: [Shared:MapGenerationParametersFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/MapGenerationParametersFile.cs) [Shared:MapGenerationParams.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource\Map\Map\MapGenerationParams.cs)</sub>
- **Required by core package:** No

## Attributes

- `Width` : `int`
- `DifficultyZones` : `int`
- `MinConnectionDistance` : `float`
- `VoronoiSiteInterval` : `Point`
- `SmallLevelConnectionLength` : `float`
- `MinLocationDistance` : `float`
- `ConnectionIndicatorDisplacementMultiplier` : `float`
- `Height` : `int`
- `ConnectionIndicatorIterationMultiplier` : `float`
- `VoronoiSiteVariance` : `Point`
- `LargeLevelConnectionLength` : `float`
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

