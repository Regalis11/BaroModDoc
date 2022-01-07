# WreckAIConfig

<sub>Relevant files: [Shared:WreckAIConfigFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/WreckAIConfigFile.cs) [Shared:WreckAIConfig.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/AI/Wreck/WreckAIConfig.cs)</sub>
- **Required by core package:** Yes

## Attributes

- `BrainRoomBackground` : `string`
- `AgentSpawnDelayDifficultyMultiplier` : `float`
- `DeadEntityColorFadeOutTime` : `float`
- `AgentSpawnCountDifficultyMultiplier` : `float`
- `MaxAgentCount` : `int`
- `Spawner` : `string`
- `MinAgentsPerBrainRoom` : `int`
- `OffensiveAgent` : `string`
- `MaxAgentsOutside` : `int`
- `MaxAgentsInside` : `int`
- `Brain` : `string`
- `BrainRoomHorizontalWall` : `string`
- `MinAgentsOutside` : `int`
- `MinWaterLevel` : `float`
- `DefensiveAgent` : `Identifier`
- `BrainRoomVerticalWall` : `string`
- `MinAgentsInside` : `int`
- `AgentSpawnDelay` : `float`
- `KillAgentsWhenEntityDies` : `bool`
- `MaxAgentsPerRoom` : `int`
- `AgentSpawnDelayRandomFactor` : `float`
- `DeadEntityColorMultiplier` : `float`
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

