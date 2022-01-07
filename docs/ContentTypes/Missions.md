# Missions

- **Required by core package:** Yes

## Examples

### Example 1 - single mission

```xml
<mission
  identifier="mymission"
  TODO="add remaining attributes" />
```

### Example 2 - multiple missions

```xml
<missions>
  <mission
    identifier="mymission1"
    TODO="add remaining attributes" />
  <mission
    identifier="mymission2"
    TODO="add remaining attributes" />
</missions>
```

### Example 3 - overriding existing missions

```xml
<override>
  <mission
    identifier="mymission1"
    TODO="add remaining attributes" />
  <mission
    identifier="mymission2"
    TODO="add remaining attributes" />
</override>
```

