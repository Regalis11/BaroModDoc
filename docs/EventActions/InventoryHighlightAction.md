# InventoryHighlightAction

Highlights specific items in a specific inventory.

## Attributes

| Attribute          | Type       | Default value | Description                                                                                                                                                                                                                                                         |
|--------------------|------------|---------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| TargetTag          | Identifier | ""            | Tag of the entity or entities whose inventory the item should be highlighted in. Must be a character or an item with an inventory.                                                                                                                                  |
| ItemIdentifier     | Identifier | ""            | Identifier of the item(s) to highlight.                                                                                                                                                                                                                             |
| ItemContainerIndex | int        | -1            | If the target is an item with multiple ItemContainer components (i.e. multiple inventories), such as a fabricator, this determines which inventory to highlight the item in (0 = first, 1 = second). If negative, it doesn't matter which inventory the item is in. |
| Recursive          | bool       | false         | If enabled, the action will go look through all the containers in the target inventory (e.g. highlighting a tank in a welding tool in the target inventory).                                                                                                        |



