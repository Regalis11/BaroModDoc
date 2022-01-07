# CaveGenerationParameters

<sup>Relevant files: [Shared:CaveGenerationParams.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/Levels/CaveGenerationParams.cs) [Shared:CaveGenerationParametersFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/CaveGenerationParametersFile.cs)</sup>
- **Required by core package:** Yes

## Attributes

- `MaxBranchCount` : `int`
- `MaxHeight` : `int`
- `MaxWidth` : `int`
- `MinBranchCount` : `int`
- `DestructibleWallRatio` : `float`
- `MinWidth` : `int`
- `MinHeight` : `int`
- `LevelObjectAmount` : `int`
- `Commonness` : `float`
## Examples

### Example 1 - single cave

```xml
<cave
  identifier="mycave" />
```

### Example 2 - multiple cavegenerationparameters

```xml
<cavegenerationparameters>
  <cave
    identifier="mycave1" />
  <cave
    identifier="mycave2" />
</cavegenerationparameters>
```

### Example 3 - overriding existing cavegenerationparameters

```xml
<override>
  <cave
    identifier="mycave1" />
  <cave
    identifier="mycave2" />
</override>
```

