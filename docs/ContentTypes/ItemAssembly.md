# ItemAssembly

- **Required by core package:** No

## Examples

### Example 1 - single itemassembly

```xml
<itemassembly
  identifier="myitemassembly"
  TODO="add remaining attributes" />
```

### Example 2 - multiple itemassemblies

```xml
<itemassemblies>
  <itemassembly
    identifier="myitemassembly1"
    TODO="add remaining attributes" />
  <itemassembly
    identifier="myitemassembly2"
    TODO="add remaining attributes" />
</itemassemblies>
```

### Example 3 - overriding existing itemassemblies

```xml
<override>
  <itemassembly
    identifier="myitemassembly1"
    TODO="add remaining attributes" />
  <itemassembly
    identifier="myitemassembly2"
    TODO="add remaining attributes" />
</override>
```

