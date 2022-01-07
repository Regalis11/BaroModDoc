# TraitorMissions

- **Required by core package:** Yes

## Examples

### Example 1 - single TraitorMission

```xml
<TraitorMission
  identifier="myTraitorMission"
  TODO="add remaining attributes" />
```

### Example 2 - multiple TraitorMissions

```xml
<TraitorMissions>
  <TraitorMission
    identifier="myTraitorMission1"
    TODO="add remaining attributes" />
  <TraitorMission
    identifier="myTraitorMission2"
    TODO="add remaining attributes" />
</TraitorMissions>
```

### Example 3 - overriding existing TraitorMissions

```xml
<override>
  <TraitorMission
    identifier="myTraitorMission1"
    TODO="add remaining attributes" />
  <TraitorMission
    identifier="myTraitorMission2"
    TODO="add remaining attributes" />
</override>
```

