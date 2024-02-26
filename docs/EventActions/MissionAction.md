# MissionAction

Unlocks a mission in a nearby level or location.

## Attributes

| Attribute                | Type       | Default value | Description                                                                                        |
|--------------------------|------------|---------------|----------------------------------------------------------------------------------------------------|
| MissionIdentifier        | Identifier | ""            | Identifier of the mission to unlock.                                                               |
| MissionTag               | Identifier | ""            | Tag of the mission to unlock. If there are multiple missions with the tag, one is chosen randomly. |
| RequiredFaction          | Identifier | ""            | The mission can only be unlocked in a location that's occupied by this faction.                    |
| MinLocationDistance      | int        | 0             | Minimum distance to the location the mission is unlocked in (1 = one path between locations).      |
| UnlockFurtherOnMap       | bool       | true          | If true, the mission has to be unlocked in a location further on the campaign map.                 |
| CreateLocationIfNotFound | bool       | false         | If true, a suitable location is forced on the map if one isn't found.                              |



