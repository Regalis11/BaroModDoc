# NPCSets

- **Required by core package:** Yes

## Examples

### Example 1 - single npcset

```xml
<npcset
  identifier="mynpcset"
  TODO="add remaining attributes" />
```

### Example 2 - multiple npcsets

```xml
<npcsets>
  <npcset
    identifier="mynpcset1"
    TODO="add remaining attributes" />
  <npcset
    identifier="mynpcset2"
    TODO="add remaining attributes" />
</npcsets>
```

### Example 3 - overriding existing npcsets

```xml
<override>
  <npcset
    identifier="mynpcset1"
    TODO="add remaining attributes" />
  <npcset
    identifier="mynpcset2"
    TODO="add remaining attributes" />
</override>
```

