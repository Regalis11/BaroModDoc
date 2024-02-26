# CombatAction

Makes an NPC switch to a combat state \(with options for different kinds of behaviors, such as offensive, arresting or retreating\).

## Attributes

| Attribute       | Type                         | Default value | Description                                                                                                                  |
|-----------------|------------------------------|---------------|------------------------------------------------------------------------------------------------------------------------------|
| CombatMode      | AIObjectiveCombat.CombatMode | Offensive     | What kind of combat mode should the NPC switch to (Defensive, Offensive, Arrest, Retreat, None)?                             |
| IsInstigator    | bool                         | false         | Did this NPC start the fight (as an aggressor)? Attacking instigators doesn't reduce reputation or trigger outpost security. |
| GuardReaction   | AIObjectiveCombat.CombatMode | None          | How do guards react to this character attacking others?                                                                      |
| WitnessReaction | AIObjectiveCombat.CombatMode | None          | How do other NPCs react to this character attacking others?                                                                  |
| NPCTag          | Identifier                   | ""            | The tag of the NPC to switch to combat mode.                                                                                 |
| EnemyTag        | Identifier                   | ""            | Tag of the character the NPC should attack.                                                                                  |
| CoolDown        | float                        | 120           | How long it takes for the NPC to "cool down" (stop attacking).                                                               |



