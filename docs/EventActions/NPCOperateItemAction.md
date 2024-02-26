# NPCOperateItemAction

Makes an NPC select an item, and operate it if it's something AI characters can operate.

## Attributes

| Attribute         | Type       | Default value | Description                                                                                                                                              |
|-------------------|------------|---------------|----------------------------------------------------------------------------------------------------------------------------------------------------------|
| NPCTag            | Identifier | ""            | Tag of the NPC(s) that should operate the item.                                                                                                          |
| TargetTag         | Identifier | ""            | Tag of the item to operate. If it's not something AI characters can or know how to operate, such as a cabinet or an engine, the NPC will just select it. |
| ItemComponentName | Identifier | "Controller"  | Name of the component to operate. For example, the Controller component of a periscope or the Reactor component of a nuclear reactor.                    |
| OrderOption       | Identifier | ""            | Identifier of the option, if there are several ways the item can be operated. For example, "powerup" or "shutdown" when operating a reactor.             |
| RequireEquip      | bool       | false         | Should the character equip the item before attempting to operate it (only valid if the item is equippable).                                              |
| Operate           | bool       | true          | Should the character start or stop operating the item.                                                                                                   |
| MaxTargets        | int        | -1            | Maximum number of NPCs the action can target. For example, you could only make a specific number of security officers man a periscope.                   |
| AbandonOnReset    | bool       | true          | The event actions reset when a GoTo action makes the event jump to a different point. Should the NPC stop operating the item when the event resets?      |



