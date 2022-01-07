# Corpses

<sub>Relevant files: [Shared:CorpsePrefab.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/CorpsePrefab.cs) [Shared:CorpsesFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/CorpsesFile.cs)</sub>
- **Required by core package:** Yes

## Attributes

- `SpawnPosition` : `Level.PositionType`
## Examples

### Example 1 - single corpse

```xml
<corpse
  identifier="mycorpse" />
```

### Example 2 - multiple corpses

```xml
<corpses>
  <corpse
    identifier="mycorpse1" />
  <corpse
    identifier="mycorpse2" />
</corpses>
```

### Example 3 - overriding existing corpses

```xml
<override>
  <corpse
    identifier="mycorpse1" />
  <corpse
    identifier="mycorpse2" />
</override>
```

