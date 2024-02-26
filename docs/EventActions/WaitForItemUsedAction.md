# WaitForItemUsedAction

Waits for some item\(s\) to be used before continuing the execution of the event.

## Attributes

| Attribute             | Type       | Default value | Description                                                                                                                                                                   |
|-----------------------|------------|---------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| ItemTag               | Identifier | ""            | Tag of the item that must be used. Note that the item needs to have been tagged by the event - this does not refer to the tags that can be set per-item in the sub editor.    |
| UserTag               | Identifier | ""            | Tag of the character that must use the item. If there's multiple matching characters, it's enough if any of them use the item. If empty, it doesn't matter who uses the item. |
| TargetItemComponent   | Identifier | ""            | Name of the ItemComponent that the character must use. If empty, the character attempts to use all of them.                                                                   |
| ApplyTagToItem        | Identifier | ""            | Optional tag to apply to the target item when it's used.                                                                                                                      |
| ApplyTagToUser        | Identifier | ""            | Optional tag to apply to the user when the target item is used.                                                                                                               |
| ApplyTagToHull        | Identifier | ""            | Optional tag to apply to the hull the target item is inside when the item is used.                                                                                            |
| ApplyTagToLinkedHulls | Identifier | ""            | Optional tag to apply to the hull the target item is inside, and all the hulls it's linked to, when the item is used.                                                         |
| RequiredUseCount      | int        | 1             | How many times does the item need to be used. Defaults to 1.                                                                                                                  |



