# CaveGenerationParameters

- **Required by core package:** Yes

## Examples

### Example 1 - single cave

```xml
<cave
  identifier="mycave"
  TODO="add remaining attributes" />
```

### Example 2 - multiple cavegenerationparameters

```xml
<cavegenerationparameters>
  <cave
    identifier="mycave1"
    TODO="add remaining attributes" />
  <cave
    identifier="mycave2"
    TODO="add remaining attributes" />
</cavegenerationparameters>
```

### Example 3 - overriding existing cavegenerationparameters

```xml
<override>
  <cave
    identifier="mycave1"
    TODO="add remaining attributes" />
  <cave
    identifier="mycave2"
    TODO="add remaining attributes" />
</override>
```

