# RandomEvents

<sup>Relevant files: [Shared:EventPrefab.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Events/EventPrefab.cs) [Shared:RandomEventsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/RandomEventsFile.cs)</sup>
- **Required by core package:** Yes

## Attributes


**WARNING:** This file likely generated completely incorrectly!

## Examples

### Example 1 - single RandomEvent

```xml
<RandomEvent
  identifier="myRandomEvent" />
```

### Example 2 - multiple RandomEvents

```xml
<RandomEvents>
  <RandomEvent
    identifier="myRandomEvent1" />
  <RandomEvent
    identifier="myRandomEvent2" />
</RandomEvents>
```

### Example 3 - overriding existing RandomEvents

```xml
<override>
  <RandomEvent
    identifier="myRandomEvent1" />
  <RandomEvent
    identifier="myRandomEvent2" />
</override>
```

