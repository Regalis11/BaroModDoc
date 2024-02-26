# SkillCheckAction

Performs a skill check and executes either the Success or Failure child actions depending on whether the check succeeds.

## Attributes

| Attribute        | Type       | Default value | Description                                                                                                                                                                                                               |
|------------------|------------|---------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| RequiredSkill    | Identifier | ""            | The identifier of the skill to check.                                                                                                                                                                                     |
| RequiredLevel    | float      | 0             | The required skill level for the check to succeed.                                                                                                                                                                        |
| ProbabilityBased | bool       | true          | Should the skill check be probability-based (i.e. if you have half the required skill level, the chance of success is 50%), or should the check always fail when under the required level and always succeed when above?  |
| TargetTag        | Identifier | ""            | Tag of the character(s) whose skill to check. If there are multiple targets, the action succeeds if any of their skill checks succeeds.                                                                                   |



