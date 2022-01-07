# ItemAssembly

<sup>Relevant files: [Shared:ItemAssemblyFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/ItemAssemblyFile.cs) [Shared:ItemAssemblyPrefab.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/ItemAssemblyPrefab.cs)</sup>

- **Required by core package:** No

## Attributes



## Examples

### Example 1 - single itemassembly

```xml
<itemassembly
  identifier="myitemassembly" />
```

### Example 2 - multiple itemassemblies

```xml
<itemassemblies>
  <itemassembly
    identifier="myitemassembly1" />
  <itemassembly
    identifier="myitemassembly2" />
</itemassemblies>
```

### Example 3 - overriding existing itemassemblies

```xml
<override>
  <itemassembly
    identifier="myitemassembly1" />
  <itemassembly
    identifier="myitemassembly2" />
</override>
```

