# CheckPurchasedItemsAction

Check whether specific kinds of items have been purchased or sold during the round.

## Attributes

| Attribute      | Type            | Default value | Description                                                             |
|----------------|-----------------|---------------|-------------------------------------------------------------------------|
| Type           | TransactionType | Purchased     | Do the items need to have been purchased or sold?                       |
| ItemIdentifier | Identifier      | ""            | Identifier of the item that must have been purchased or sold.           |
| ItemTag        | Identifier      | ""            | Tag of the item that must have been purchased or sold.                  |
| MinCount       | int             | 1             | Minimum number of matching items that must have been purchased or sold. |



