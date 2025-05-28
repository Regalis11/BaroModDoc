# TriggerEventAction

Triggers another event \(can also trigger things other than scripted events, for example monster events\).

## Attributes

| Attribute  | Type       | Default value | Description                                                                                                                                                          |
|------------|------------|---------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Identifier | Identifier | ""            | Identifier of the event to trigger.                                                                                                                                  |
| EventTag   | Identifier | ""            | Tag of the event to trigger.                                                                                                                                         |
| NextRound  | bool       | false         | If set to true, the event will trigger at the beginning of the next round. Useful for e.g. triggering some scripted event in the outpost after you finish a mission. |



