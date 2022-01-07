# Factions

<sub>Relevant files: [Shared:FactionsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/FactionsFile.cs) [Shared:Factions.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/GameSession/Data/Factions.cs)</sub>
- **Required by core package:** Yes

## Attributes


## Examples

### Example 1 - single faction

```xml
<faction
  identifier="myfaction" />
```

### Example 2 - multiple factions

```xml
<factions>
  <faction
    identifier="myfaction1" />
  <faction
    identifier="myfaction2" />
</factions>
```

### Example 3 - overriding existing factions

```xml
<override>
  <faction
    identifier="myfaction1" />
  <faction
    identifier="myfaction2" />
</override>
```

