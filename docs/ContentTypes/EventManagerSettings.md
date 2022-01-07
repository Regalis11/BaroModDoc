# EventManagerSettings

<sup>Relevant files: [Shared:EventManagerSettingsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/EventManagerSettingsFile.cs) [Shared:EventManagerSettings.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Events/EventManagerSettings.cs)</sup>
- **Required by core package:** No

## Attributes


## Examples

### Example 1 - single EventManagerSetting

```xml
<EventManagerSetting
  identifier="myEventManagerSetting" />
```

### Example 2 - multiple EventManagerSettings

```xml
<EventManagerSettings>
  <EventManagerSetting
    identifier="myEventManagerSetting1" />
  <EventManagerSetting
    identifier="myEventManagerSetting2" />
</EventManagerSettings>
```

### Example 3 - overriding existing EventManagerSettings

```xml
<override>
  <EventManagerSetting
    identifier="myEventManagerSetting1" />
  <EventManagerSetting
    identifier="myEventManagerSetting2" />
</override>
```

