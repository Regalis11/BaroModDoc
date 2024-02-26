# CheckMissionAction

Check whether a specific mission is currently active, selected for the next round or available.

## Attributes

| Attribute         | Type        | Default value | Description                                                                             |
|-------------------|-------------|---------------|-----------------------------------------------------------------------------------------|
| Type              | MissionType | Current       | Does the mission need to be currently active, selected for the next round or available. |
| MissionIdentifier | Identifier  | ""            | Identifier of the mission.                                                              |
| MissionTag        | Identifier  | ""            | Tag of the mission. Ignored if MissionIdentifier is set.                                |
| MissionCount      | int         | 1             | Minimum number of matching missions for the check to succeed.                           |



