# CheckConditionalAction

Checks whether an arbitrary condition is met. The conditionals work the same way as they do in StatusEffects.

## Attributes

| Attribute             | Type                                    | Default value | Description                                                                                                                 |
|-----------------------|-----------------------------------------|---------------|-----------------------------------------------------------------------------------------------------------------------------|
| TargetTag             | Identifier                              | ""            | Tag of the target to check.                                                                                                 |
| LogicalOperator       | PropertyConditional.LogicalOperatorType | Or            | Do all of the conditions need to be met, or is it enough if at least one is? Only valid if there are multiple conditionals. |
| ApplyTagToLinkedHulls | Identifier                              | ""            | A tag to apply to the hull the target is currently in when the check succeeds, as well as all the hulls linked to it.       |
| ApplyTagToHull        | Identifier                              | ""            | A tag to apply to the hull the target is currently in when the check succeeds.                                              |



