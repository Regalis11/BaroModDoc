# LevelGenerationParameters

<sub>Relevant files: [Shared:LevelGenerationParams.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource\Map\Levels\LevelGenerationParams.cs) [Shared:LevelGenerationParametersFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/LevelGenerationParametersFile.cs) [Shared:Biome.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource\Map\Levels\Biome.cs)</sub>
- **Required by core package:** No

## Attributes

- `MinTunnelRadius` : `int`
- `RuinCount` : `int`
- `MainPathNodeIntervalRange` : `Point`
- `IceSpireCount` : `int`
- `ItemCount` : `int`
- `AbyssIslandCount` : `int`
- `CaveResourceIntervalRange` : `Point`
- `CellIrregularity` : `float`
- `FloatingIceChunkCount` : `int`
- `WallEdgeTextureWidth` : `float`
- `AmbientLightColor` : `Color`
- `VoronoiSiteInterval` : `Point`
- `CreateHoleToAbyss` : `bool`
- `AbyssIslandCaveProbability` : `float`
- `LevelObjectAmount` : `int`
- `WallColor` : `Color`
- `ResourceIntervalRange` : `Point`
- `MountainHeightMin` : `int`
- `SideTunnelVariance` : `float`
- `WreckFloodingHullMaxWaterPercentage` : `float`
- `MaxWreckCount` : `int`
- `Height` : `int`
- `ResourceClusterSizeRange` : `Point`
- `CaveCount` : `int`
- `BackgroundTextureColor` : `Color`
- `Type` : `LevelData.LevelType`
- `WallEdgeExpandInwardsAmount` : `float`
- `MountainHeightMax` : `int`
- `VoronoiSiteVariance` : `Point`
- `InitialDepthMax` : `int`
- `ResourceSpawnChance` : `float`
- `BackgroundColor` : `Color`
- `ThalamusProbability` : `float`
- `StartPosition` : `Vector2`
- `MinWidth` : `int`
- `IslandCount` : `int`
- `MountainCountMin` : `int`
- `WaterParticleScale` : `float`
- `SeaFloorVariance` : `int`
- `EndPosition` : `Vector2`
- `CaveResourceSpawnChance` : `float`
- `MainPathVariance` : `float`
- `MaxWidth` : `int`
- `MaxCorpseCount` : `int`
- `InitialDepthMin` : `int`
- `MinSideTunnelRadius` : `Point`
- `WallTextureSize` : `float`
- `AbyssIslandSizeMax` : `Point`
- `MountainCountMax` : `int`
- `CellSubdivisionLength` : `int`
- `WallEdgeExpandOutwardsAmount` : `float`
- `CellRoundingAmount` : `float`
- `WreckHullFloodingChance` : `float`
- `WreckFloodingHullMinWaterPercentage` : `float`
- `AbyssIslandSizeMin` : `Point`
- `CreateHoleNextToEnd` : `bool`
- `MinCorpseCount` : `int`
- `BottomHoleProbability` : `float`
- `SeaFloorDepth` : `int`
- `BackgroundCreatureAmount` : `int`
- `MinWreckCount` : `int`
- `SideTunnelCount` : `Point`
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

