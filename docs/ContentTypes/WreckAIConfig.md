# WreckAIConfig

<sup>Relevant files: [Shared:WreckAIConfigFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/WreckAIConfigFile.cs) [Shared:WreckAIConfig.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/AI/Wreck/WreckAIConfig.cs)</sup>

- **Required by core package:** Yes

## Attributes

- `DefensiveAgent` : `Identifier`
- `OffensiveAgent` : `string`
- `Brain` : `string`
- `Spawner` : `string`
- `BrainRoomBackground` : `string`
- `BrainRoomVerticalWall` : `string`
- `BrainRoomHorizontalWall` : `string`
- `AgentSpawnDelay` : `float`
- `AgentSpawnDelayRandomFactor` : `float`
- `AgentSpawnDelayDifficultyMultiplier` : `float`
- `AgentSpawnCountDifficultyMultiplier` : `float`
- `MinAgentsPerBrainRoom` : `int`
- `MaxAgentsPerRoom` : `int`
- `MinAgentsOutside` : `int`
- `MaxAgentsOutside` : `int`
- `MinAgentsInside` : `int`
- `MaxAgentsInside` : `int`
- `MaxAgentCount` : `int`
- `MinWaterLevel` : `float`
- `KillAgentsWhenEntityDies` : `bool`
- `DeadEntityColorMultiplier` : `float`
- `DeadEntityColorFadeOutTime` : `float`

## Examples

### Example 1 - single wreckaiconfig

```xml
<wreckaiconfig
  identifier="mywreckaiconfig" />
```

### Example 2 - multiple wreckaiconfigs

```xml
<wreckaiconfigs>
  <wreckaiconfig
    identifier="mywreckaiconfig1" />
  <wreckaiconfig
    identifier="mywreckaiconfig2" />
</wreckaiconfigs>
```

### Example 3 - overriding existing wreckaiconfigs

```xml
<override>
  <wreckaiconfig
    identifier="mywreckaiconfig1" />
  <wreckaiconfig
    identifier="mywreckaiconfig2" />
</override>
```

