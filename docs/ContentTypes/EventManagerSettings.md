# EventManagerSettings

- **Required by core package:** No

**WARNING:** This file likely generated completely incorrectly!

## Examples

### Example 1 - single EventManagerSetting

```xml
<EventManagerSetting
  identifier="myEventManagerSetting"
  TODO="add remaining attributes" />
```

### Example 2 - multiple EventManagerSettings

```xml
<EventManagerSettings>
  <EventManagerSetting
    identifier="myEventManagerSetting1"
    TODO="add remaining attributes" />
  <EventManagerSetting
    identifier="myEventManagerSetting2"
    TODO="add remaining attributes" />
</EventManagerSettings>
```

### Example 3 - overriding existing EventManagerSettings

```xml
<override>
  <EventManagerSetting
    identifier="myEventManagerSetting1"
    TODO="add remaining attributes" />
  <EventManagerSetting
    identifier="myEventManagerSetting2"
    TODO="add remaining attributes" />
</override>
```

