# NPCFollowAction

Makes an NPC follow or stop following a specific target.

## Attributes

| Attribute      | Type       | Default value | Description                                                                                                                                           |
|----------------|------------|---------------|-------------------------------------------------------------------------------------------------------------------------------------------------------|
| NPCTag         | Identifier | ""            | Tag of the NPC(s) that should follow the target.                                                                                                      |
| TargetTag      | Identifier | ""            | Tag of the target. Can be any type of entity: if it's a static one like a device or a hull, the NPC will just stay at the position of that target.    |
| Follow         | bool       | true          | Should the NPC start or stop following the target?                                                                                                    |
| MaxTargets     | int        | -1            | Maximum number of NPCs to target (e.g. you could choose to only make a specific number of security officers follow the player.)                       |
| AbandonOnReset | bool       | true          | The event actions reset when a GoTo action makes the event jump to a different point. Should the NPC stop following the target when the event resets? |



