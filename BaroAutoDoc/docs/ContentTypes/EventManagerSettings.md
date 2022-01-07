# EventManagerSettings

- **Required by core package:** No

**WARNING:** This file likely generated completely incorrectly!

## Examples

### Example 1 - single EventManagerSettings

```xml
<EventManagerSettings
  identifier="myEventManagerSettings"
  TODO="add remaining attributes" />
```

### Example 2 - multiple EventManagerSettings

```xml
<EventManagerSettings>
  <EventManagerSettings
    identifier="myEventManagerSettings1"
    TODO="add remaining attributes" />
  <EventManagerSettings
    identifier="myEventManagerSettings2"
    TODO="add remaining attributes" />
</EventManagerSettings>
```

### Example 3 - overriding existing EventManagerSettings

```xml
<override>
  <EventManagerSettings
    identifier="myEventManagerSettings1"
    TODO="add remaining attributes" />
  <EventManagerSettings
    identifier="myEventManagerSettings2"
    TODO="add remaining attributes" />
</override>
```

