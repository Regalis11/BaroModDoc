# WreckAIConfig

<sup>Relevant files: [Shared:WreckAIConfigFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/WreckAIConfigFile.cs) [Shared:WreckAIConfig.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/AI/Wreck/WreckAIConfig.cs)</sup>
- **Required by core package:** Yes

## Attributes

- `OffensiveAgent` : `string`
- `BrainRoomVerticalWall` : `string`
- `DeadEntityColorMultiplier` : `float`
- `AgentSpawnCountDifficultyMultiplier` : `float`
- `DefensiveAgent` : `Identifier`
- `BrainRoomBackground` : `string`
- `KillAgentsWhenEntityDies` : `bool`
- `AgentSpawnDelayRandomFactor` : `float`
- `DeadEntityColorFadeOutTime` : `float`
- `Spawner` : `string`
- `MinAgentsPerBrainRoom` : `int`
- `BrainRoomHorizontalWall` : `string`
- `MaxAgentsPerRoom` : `int`
- `AgentSpawnDelayDifficultyMultiplier` : `float`
- `MinAgentsInside` : `int`
- `MaxAgentCount` : `int`
- `AgentSpawnDelay` : `float`
- `MinAgentsOutside` : `int`
- `MaxAgentsOutside` : `int`
- `Brain` : `string`
- `MaxAgentsInside` : `int`
- `MinWaterLevel` : `float`
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

