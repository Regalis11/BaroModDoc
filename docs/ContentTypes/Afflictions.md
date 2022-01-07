# Afflictions

<sub>Relevant files: [Shared:AfflictionsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/AfflictionsFile.cs) [Shared:AfflictionPrefab.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/Health/Afflictions/AfflictionPrefab.cs)</sub>
- **Required by core package:** Yes

## Attributes


**WARNING:** This file likely generated completely incorrectly!

## Examples

### Example 1 - single Affliction

```xml
<Affliction
  identifier="myAffliction" />
```

### Example 2 - multiple Afflictions

```xml
<Afflictions>
  <Affliction
    identifier="myAffliction1" />
  <Affliction
    identifier="myAffliction2" />
</Afflictions>
```

### Example 3 - overriding existing Afflictions

```xml
<override>
  <Affliction
    identifier="myAffliction1" />
  <Affliction
    identifier="myAffliction2" />
</override>
```

