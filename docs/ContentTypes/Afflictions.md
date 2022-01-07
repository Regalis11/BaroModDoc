# Afflictions

<sup>Relevant files: [Shared:AfflictionsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/AfflictionsFile.cs)</sup>

**WARNING:** This file likely generated completely incorrectly!

- **Required by core package:** Yes

## Attributes



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

