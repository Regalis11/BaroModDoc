# OutpostConfig

<sup>Relevant files: [Shared:OutpostConfigFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/OutpostConfigFile.cs)</sup>
- **Required by core package:** Yes

## Attributes


## Examples

### Example 1 - single OutpostConfig

```xml
<OutpostConfig
  identifier="myOutpostConfig" />
```

### Example 2 - multiple OutpostGenerationParameters

```xml
<OutpostGenerationParameters>
  <OutpostConfig
    identifier="myOutpostConfig1" />
  <OutpostConfig
    identifier="myOutpostConfig2" />
</OutpostGenerationParameters>
```

### Example 3 - overriding existing OutpostGenerationParameters

```xml
<override>
  <OutpostConfig
    identifier="myOutpostConfig1" />
  <OutpostConfig
    identifier="myOutpostConfig2" />
</override>
```

