# TriggerAction

Waits for a player to trigger the action before continuing. Triggering can mean entering a specific trigger area, or interacting with a specific entity.

## Attributes

| Attribute                    | Type        | Default value | Description                                                                                          |
|------------------------------|-------------|---------------|------------------------------------------------------------------------------------------------------|
| Target1Tag                   | Identifier  | ""            | Tag of the first entity that will be used for trigger checks.                                        |
| Target2Tag                   | Identifier  | ""            | Tag of the second entity that will be used for trigger checks.                                       |
| TargetModuleType             | Identifier  | ""            | If set, the first target has to be within an outpost module of this type.                            |
| ApplyToTarget1               | Identifier  | ""            | Tag to apply to the first entity when the trigger check succeeds.                                    |
| ApplyToTarget2               | Identifier  | ""            | Tag to apply to the second entity when the trigger check succeeds.                                   |
| Type                         | TriggerType | Inside        | Determines if the targets must be inside or outside of the radius.                                   |
| Radius                       | float       | 0             | Range to activate the trigger.                                                                       |
| DisableInCombat              | bool        | true          | If true, characters who are being targeted by some enemy cannot trigger the action.                  |
| DisableIfTargetIncapacitated | bool        | true          | If true, dead/unconscious characters cannot trigger the action.                                      |
| WaitForInteraction           | bool        | false         | If true, one target must interact with the other to trigger the action.                              |
| AllowMultipleTargets         | bool        | false         | If true, the action can be triggered by interacting with any matching target (not just the 1st one). |
| CheckAllTargets              | bool        | false         | If true and using multiple targets, all targets must be inside/outside the radius.                   |
| SelectOnTrigger              | bool        | false         | If true, interacting with the target will make the character select it.                              |



