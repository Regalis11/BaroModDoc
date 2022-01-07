# LevelObjectPrefabs

<sub>Relevant files: [Shared:LevelObjectPrefabsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/LevelObjectPrefabsFile.cs)</sub>
- **Required by core package:** Yes

## Attributes


## Examples

### Example 1 - single levelobject

```xml
<levelobject
  identifier="mylevelobject" />
```

### Example 2 - multiple levelobjects

```xml
<levelobjects>
  <levelobject
    identifier="mylevelobject1" />
  <levelobject
    identifier="mylevelobject2" />
</levelobjects>
```

### Example 3 - overriding existing levelobjects

```xml
<override>
  <levelobject
    identifier="mylevelobject1" />
  <levelobject
    identifier="mylevelobject2" />
</override>
```

