# CaveGenerationParameters

<sub>Relevant files: [Shared:CaveGenerationParams.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource\Map\Levels\CaveGenerationParams.cs) [Shared:CaveGenerationParametersFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/CaveGenerationParametersFile.cs)</sub>
- **Required by core package:** Yes

## Attributes

- `LevelObjectAmount` : `int`
- `MaxHeight` : `int`
- `MinWidth` : `int`
- `DestructibleWallRatio` : `float`
- `MaxWidth` : `int`
- `MinHeight` : `int`
- `MaxBranchCount` : `int`
- `Commonness` : `float`
- `MinBranchCount` : `int`
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

