# Missions

<sup>Relevant files: [Shared:MissionsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/MissionsFile.cs)</sup>
- **Required by core package:** Yes

## Attributes


## Examples

### Example 1 - single mission

```xml
<mission
  identifier="mymission" />
```

### Example 2 - multiple missions

```xml
<missions>
  <mission
    identifier="mymission1" />
  <mission
    identifier="mymission2" />
</missions>
```

### Example 3 - overriding existing missions

```xml
<override>
  <mission
    identifier="mymission1" />
  <mission
    identifier="mymission2" />
</override>
```

