# WaitForItemFabricatedAction

Waits for some item\(s\) to be fabricated before continuing the execution of the event.

## Attributes

| Attribute      | Type       | Default value | Description                                                                                      |
|----------------|------------|---------------|--------------------------------------------------------------------------------------------------|
| CharacterTag   | Identifier | ""            | Tag of the character who must fabricate the item. If empty, it doesn't matter who fabricates it. |
| ItemIdentifier | Identifier | ""            | Identifier of the item that must be fabricated. Optional if ItemTag is set.                      |
| ItemTag        | Identifier | ""            | Tag of the item that must be fabricated. Optional if ItemIdentifier is set.                      |
| Amount         | int        | 1             | Number of items that need to be fabricated.                                                      |
| ApplyTagToItem | Identifier | ""            | Tag to apply to the fabricated item(s).                                                          |



