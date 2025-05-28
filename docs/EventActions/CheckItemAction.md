# CheckItemAction

Can be used to do various kinds of checks on items: whether a specific kind of item exists,
if it's in a specific character's inventory or in a container, or whether some conditions are met on the item.

## Attributes

| Attribute                          | Type       | Default value | Description                                                                                                                                                                                              |
|------------------------------------|------------|---------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| TargetTag                          | Identifier | ""            | Either the tag of the item(s) we want to check, or a character/container the items are inside.                                                                                                           |
| ItemIdentifiers                    | string     | ""            | The target item must have one of these identifiers.                                                                                                                                                      |
| ItemTags                           | string     | ""            | The target item must have at least one of these tags.                                                                                                                                                    |
| Amount                             | int        | 1             | The minimum number of matching items for the check to succeed.                                                                                                                                           |
| HullTag                            | Identifier | ""            | Optional tag of a hull the target must be inside.                                                                                                                                                        |
| ApplyTagToTarget                   | Identifier | ""            | Tag to apply to the first target when the check succeeds.                                                                                                                                                |
| ApplyTagToItem                     | Identifier | ""            | Tag to apply to the found item(s) when the check succeeds.                                                                                                                                               |
| RequireEquipped                    | bool       | false         | Does the item need to be equipped for the check to succeed?                                                                                                                                              |
| RequireWorn                        | bool       | false         | Does the item need to be worn for the check to succeed?                                                                                                                                                  |
| Recursive                          | bool       | true          | If enabled, the doesn't need to be directly inside the container/character we're checking, but can be nested inside multiple containers (e.g. in a toolbelt in a character's inventory).                 |
| ItemContainerIndex                 | int        | -1            | Can be used to require the item to be in a specific ItemContainer of the target container. For example, the input slots of a fabricator (the first ItemContainer of the fabricator, with an index of 0). |
| RequiredConditionalMatchPercentage | float      | 100           | What percentage of targets do the conditionals need to match for the check to succeed?                                                                                                                   |
| CompareToInitialAmount             | bool       | false         | When enabled, the number of matching items is compared to the number of matching items there were at the start of the round. Only valid if RequiredConditionalMatchPercentage is set.                    |



