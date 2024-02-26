# CheckVisibilityAction

Check whether a specific entity is visible from the perspective of another entity.

## Attributes

| Attribute         | Type       | Default value | Description                                                                            |
|-------------------|------------|---------------|----------------------------------------------------------------------------------------|
| EntityTag         | Identifier | ""            | Tag of the entity to do the visibility check from.                                     |
| ExcludedEntityTag | Identifier | ""            | Entities that also have this tag are excluded.                                         |
| TargetTag         | Identifier | ""            | Tag of the entity to do the visibility check to.                                       |
| CheckFacing       | bool       | false         | Does the entity need to be facing the target? Only valid if the entity is a character. |
| MaxDistance       | float      | 1000          | Maximum distance between the targets.                                                  |
| ApplyTagToEntity  | Identifier | ""            | Tag to apply to the entity who saw the target when the check succeeds.                 |
| ApplyTagToTarget  | Identifier | ""            | Tag to apply to the entity that was seen when the check succeeds.                      |
| AllowSameEntity   | bool       | true          | If both the seeing entity and the target are the same, does it count as success?       |



