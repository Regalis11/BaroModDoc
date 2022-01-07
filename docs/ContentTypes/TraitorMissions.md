# TraitorMissions

<sup>Relevant files: [Shared:TraitorMissionsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/TraitorMissionsFile.cs)</sup>

- **Required by core package:** Yes

## Attributes



## Examples

### Example 1 - single TraitorMission

```xml
<TraitorMission
  identifier="myTraitorMission" />
```

### Example 2 - multiple TraitorMissions

```xml
<TraitorMissions>
  <TraitorMission
    identifier="myTraitorMission1" />
  <TraitorMission
    identifier="myTraitorMission2" />
</TraitorMissions>
```

### Example 3 - overriding existing TraitorMissions

```xml
<override>
  <TraitorMission
    identifier="myTraitorMission1" />
  <TraitorMission
    identifier="myTraitorMission2" />
</override>
```

