# MissionStateAction

Changes the state of a specific active mission. The way the states are used depends on the type of mission.

## Attributes

| Attribute         | Type          | Default value | Description                                                                                               |
|-------------------|---------------|---------------|-----------------------------------------------------------------------------------------------------------|
| MissionIdentifier | Identifier    | ""            | Identifier of the mission whose state to change.                                                          |
| Operation         | OperationType | Set           | Should the value be added to the state of the mission, or should the state be set to the specified value. |
| State             | int           | 0             | The state to set the mission to, or how much to add to the state of the mission.                          |



