# CheckSelectedAction

Check whether a specific character has selected a specific kind of item.

## Attributes

| Attribute    | Type             | Default value | Description                                                                                                                                                                                                        |
|--------------|------------------|---------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| CharacterTag | Identifier       | ""            | Tag of the character to check.                                                                                                                                                                                     |
| TargetTag    | Identifier       | ""            | If specified, only items that have been given this tag using TagAction are considered valid.                                                                                                                       |
| ItemType     | SelectedItemType | Any           | How does the item need to be selected? Primary item (i.e. any device you're interacting with), secondary item (such as ladders or chairs which allow interacting with a primary item at the same time), or either? |



