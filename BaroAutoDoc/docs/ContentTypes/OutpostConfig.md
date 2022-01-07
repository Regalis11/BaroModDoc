# OutpostConfig

- **Required by core package:** Yes

## Examples

### Example 1 - single OutpostConfig

```xml
<OutpostConfig
  identifier="myOutpostConfig"
  TODO="add remaining attributes" />
```

### Example 2 - multiple OutpostGenerationParameters

```xml
<OutpostGenerationParameters>
  <OutpostConfig
    identifier="myOutpostConfig1"
    TODO="add remaining attributes" />
  <OutpostConfig
    identifier="myOutpostConfig2"
    TODO="add remaining attributes" />
</OutpostGenerationParameters>
```

### Example 3 - overriding existing OutpostGenerationParameters

```xml
<override>
  <OutpostConfig
    identifier="myOutpostConfig1"
    TODO="add remaining attributes" />
  <OutpostConfig
    identifier="myOutpostConfig2"
    TODO="add remaining attributes" />
</override>
```

