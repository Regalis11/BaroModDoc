# NPCSets

<sup>Relevant files: [Shared:NPCSetsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/NPCSetsFile.cs) [Shared:NPCSet.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/Outposts/NPCSet.cs)</sup>

- **Required by core package:** Yes

## Attributes



## Examples

### Example 1 - single npcset

```xml
<npcset
  identifier="mynpcset" />
```

### Example 2 - multiple npcsets

```xml
<npcsets>
  <npcset
    identifier="mynpcset1" />
  <npcset
    identifier="mynpcset2" />
</npcsets>
```

### Example 3 - overriding existing npcsets

```xml
<override>
  <npcset
    identifier="mynpcset1" />
  <npcset
    identifier="mynpcset2" />
</override>
```

