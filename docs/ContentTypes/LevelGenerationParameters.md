# LevelGenerationParameters

<sup>Relevant files: [Shared:Biome.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/Levels/Biome.cs) [Shared:LevelGenerationParametersFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/LevelGenerationParametersFile.cs) [Shared:LevelGenerationParams.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/Levels/LevelGenerationParams.cs)</sup>
- **Required by core package:** No

## Attributes

- `FloatingIceChunkCount` : `int`
- `MountainCountMax` : `int`
- `Height` : `int`
- `InitialDepthMin` : `int`
- `SideTunnelCount` : `Point`
- `Type` : `LevelData.LevelType`
- `WreckFloodingHullMaxWaterPercentage` : `float`
- `RuinCount` : `int`
- `ResourceSpawnChance` : `float`
- `BottomHoleProbability` : `float`
- `CaveCount` : `int`
- `SideTunnelVariance` : `float`
- `MaxWidth` : `int`
- `MinWreckCount` : `int`
- `WaterParticleScale` : `float`
- `WreckHullFloodingChance` : `float`
- `ThalamusProbability` : `float`
- `BackgroundColor` : `Color`
- `AmbientLightColor` : `Color`
- `AbyssIslandCaveProbability` : `float`
- `WallEdgeExpandInwardsAmount` : `float`
- `BackgroundTextureColor` : `Color`
- `AbyssIslandSizeMax` : `Point`
- `MountainHeightMax` : `int`
- `AbyssIslandCount` : `int`
- `AbyssIslandSizeMin` : `Point`
- `CaveResourceSpawnChance` : `float`
- `MinTunnelRadius` : `int`
- `CaveResourceIntervalRange` : `Point`
- `CellIrregularity` : `float`
- `MinCorpseCount` : `int`
- `IslandCount` : `int`
- `MainPathVariance` : `float`
- `CreateHoleNextToEnd` : `bool`
- `VoronoiSiteVariance` : `Point`
- `WallEdgeExpandOutwardsAmount` : `float`
- `MainPathNodeIntervalRange` : `Point`
- `InitialDepthMax` : `int`
- `VoronoiSiteInterval` : `Point`
- `MinWidth` : `int`
- `CreateHoleToAbyss` : `bool`
- `ResourceClusterSizeRange` : `Point`
- `MountainHeightMin` : `int`
- `CellSubdivisionLength` : `int`
- `ResourceIntervalRange` : `Point`
- `ItemCount` : `int`
- `BackgroundCreatureAmount` : `int`
- `StartPosition` : `Vector2`
- `MaxWreckCount` : `int`
- `EndPosition` : `Vector2`
- `SeaFloorDepth` : `int`
- `WallTextureSize` : `float`
- `WallColor` : `Color`
- `LevelObjectAmount` : `int`
- `WallEdgeTextureWidth` : `float`
- `MaxCorpseCount` : `int`
- `IceSpireCount` : `int`
- `CellRoundingAmount` : `float`
- `MountainCountMin` : `int`
- `SeaFloorVariance` : `int`
- `MinSideTunnelRadius` : `Point`
- `WreckFloodingHullMinWaterPercentage` : `float`
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

