# WreckAIConfig

<sub>Relevant files: [Shared:WreckAIConfigFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/WreckAIConfigFile.cs) [Shared:WreckAIConfig.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource\Characters\AI\Wreck\WreckAIConfig.cs)</sub>
- **Required by core package:** Yes

## Attributes

- `AgentSpawnCountDifficultyMultiplier` : `float`
- `AgentSpawnDelayRandomFactor` : `float`
- `BrainRoomBackground` : `string`
- `DeadEntityColorFadeOutTime` : `float`
- `MaxAgentsInside` : `int`
- `MinAgentsPerBrainRoom` : `int`
- `BrainRoomVerticalWall` : `string`
- `KillAgentsWhenEntityDies` : `bool`
- `BrainRoomHorizontalWall` : `string`
- `AgentSpawnDelayDifficultyMultiplier` : `float`
- `MinAgentsInside` : `int`
- `Spawner` : `string`
- `Brain` : `string`
- `DeadEntityColorMultiplier` : `float`
- `MaxAgentCount` : `int`
- `MaxAgentsPerRoom` : `int`
- `MinWaterLevel` : `float`
- `DefensiveAgent` : `Identifier`
- `MaxAgentsOutside` : `int`
- `OffensiveAgent` : `string`
- `AgentSpawnDelay` : `float`
- `MinAgentsOutside` : `int`
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

