# CheckConnectionAction

Check whether a specific connection of an item is wired to a specific kind of connection.

## Attributes

| Attribute           | Type       | Default value | Description                                                                                                                      |
|---------------------|------------|---------------|----------------------------------------------------------------------------------------------------------------------------------|
| ItemTag             | Identifier | ""            | Tag of the item to check.                                                                                                        |
| ConnectionName      | Identifier | ""            | The name of the connection to check on the target item.                                                                          |
| ConnectedItemTag    | Identifier | ""            | Tag of the item the connection must be wired to. If omitted, it doesn't matter what the connection is wired to.                  |
| OtherConnectionName | Identifier | ""            | The name of the other connection the connection must be wired to. If omitted, it doesn't matter what the connection is wired to. |
| MinAmount           | int        | 1             | Minimum number of matching connections for the check to succeed.                                                                 |



