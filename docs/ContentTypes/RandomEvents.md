# RandomEvents

<sup>Relevant files: [Shared:RandomEventsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/RandomEventsFile.cs)</sup>

**WARNING:** This file likely generated completely incorrectly!

- **Required by core package:** Yes

## Attributes



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

