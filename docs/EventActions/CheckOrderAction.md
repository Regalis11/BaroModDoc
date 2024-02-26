# CheckOrderAction

Check whether a specific character has been given a specific order.

## Attributes

| Attribute       | Type          | Default value | Description                                                                                                                                           |
|-----------------|---------------|---------------|-------------------------------------------------------------------------------------------------------------------------------------------------------|
| TargetTag       | Identifier    | ""            | Tag of the character to check.                                                                                                                        |
| OrderIdentifier | Identifier    | ""            | Identifier of the order the target character must have.                                                                                               |
| OrderOption     | Identifier    | ""            | The option that must be selected for the order. If the order has multiple options (such as turning on or turning off a reactor).                      |
| OrderTargetTag  | Identifier    | ""            | Tag of the entity the order must be targeting. Only valid for orders that can target a specific entity (such as orders to operate a specific turret). |
| Priority        | OrderPriority | Any           | Does the order need to have top priority, or is any priority fine?                                                                                    |



