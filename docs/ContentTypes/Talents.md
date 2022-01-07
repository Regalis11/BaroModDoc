# Talents

<sup>Relevant files: [Shared:TalentsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/TalentsFile.cs) [Shared:TalentPrefab.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/Talents/TalentPrefab.cs)</sup>

- **Required by core package:** Yes

## Attributes



## Examples

### Example 1 - single talent

```xml
<talent
  identifier="mytalent" />
```

### Example 2 - multiple talents

```xml
<talents>
  <talent
    identifier="mytalent1" />
  <talent
    identifier="mytalent2" />
</talents>
```

### Example 3 - overriding existing talents

```xml
<override>
  <talent
    identifier="mytalent1" />
  <talent
    identifier="mytalent2" />
</override>
```

