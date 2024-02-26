# ReputationAction

Adjusts the crew's reputation by some value.

## Attributes

| Attribute  | Type           | Default value | Description                                                                                                      |
|------------|----------------|---------------|------------------------------------------------------------------------------------------------------------------|
| Increase   | float          | 0             | Amount of reputation to add or remove.                                                                           |
| Identifier | Identifier     | ""            | Identifier of the faction you want to adjust the reputation for. Ignored if TargetType is set to Location.       |
| TargetType | ReputationType | None          | Do you want to adjust the reputation for a specific faction, or whichever faction controls the current location? |



