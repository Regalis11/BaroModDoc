# LevelGenerationParameters

<sub>Relevant files: [Shared:LevelGenerationParams.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/Levels/LevelGenerationParams.cs) [Shared:Biome.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/Levels/Biome.cs) [Shared:LevelGenerationParametersFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/LevelGenerationParametersFile.cs)</sub>
- **Required by core package:** No

## Attributes

- `MinTunnelRadius` : `int`
- `CaveCount` : `int`
- `MountainHeightMin` : `int`
- `CellSubdivisionLength` : `int`
- `WallColor` : `Color`
- `MainPathNodeIntervalRange` : `Point`
- `CellIrregularity` : `float`
- `MinSideTunnelRadius` : `Point`
- `StartPosition` : `Vector2`
- `BackgroundColor` : `Color`
- `IslandCount` : `int`
- `CaveResourceIntervalRange` : `Point`
- `MinWreckCount` : `int`
- `MinCorpseCount` : `int`
- `RuinCount` : `int`
- `CellRoundingAmount` : `float`
- `WallEdgeExpandInwardsAmount` : `float`
- `SideTunnelVariance` : `float`
- `WallEdgeTextureWidth` : `float`
- `SeaFloorDepth` : `int`
- `MaxCorpseCount` : `int`
- `VoronoiSiteInterval` : `Point`
- `WaterParticleScale` : `float`
- `WreckFloodingHullMinWaterPercentage` : `float`
- `ResourceSpawnChance` : `float`
- `BottomHoleProbability` : `float`
- `InitialDepthMin` : `int`
- `CreateHoleToAbyss` : `bool`
- `AbyssIslandSizeMax` : `Point`
- `MinWidth` : `int`
- `SeaFloorVariance` : `int`
- `MainPathVariance` : `float`
- `MaxWreckCount` : `int`
- `ThalamusProbability` : `float`
- `WreckHullFloodingChance` : `float`
- `ResourceClusterSizeRange` : `Point`
- `BackgroundTextureColor` : `Color`
- `MountainHeightMax` : `int`
- `LevelObjectAmount` : `int`
- `MountainCountMin` : `int`
- `MaxWidth` : `int`
- `AbyssIslandSizeMin` : `Point`
- `CreateHoleNextToEnd` : `bool`
- `Type` : `LevelData.LevelType`
- `Height` : `int`
- `WallTextureSize` : `float`
- `BackgroundCreatureAmount` : `int`
- `EndPosition` : `Vector2`
- `FloatingIceChunkCount` : `int`
- `WallEdgeExpandOutwardsAmount` : `float`
- `IceSpireCount` : `int`
- `VoronoiSiteVariance` : `Point`
- `AbyssIslandCount` : `int`
- `WreckFloodingHullMaxWaterPercentage` : `float`
- `AbyssIslandCaveProbability` : `float`
- `ResourceIntervalRange` : `Point`
- `CaveResourceSpawnChance` : `float`
- `InitialDepthMax` : `int`
- `AmbientLightColor` : `Color`
- `SideTunnelCount` : `Point`
- `ItemCount` : `int`
- `MountainCountMax` : `int`
**WARNING:** This file likely generated completely incorrectly!

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

