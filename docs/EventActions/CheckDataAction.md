# CheckDataAction

Can be used to check arbitrary campaign metadata set using SetDataAction.

## Attributes

| Attribute            | Type       | Default value | Description                                                                                                                                                                                                                 |
|----------------------|------------|---------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Identifier           | Identifier | ""            | Identifier of the data to check.                                                                                                                                                                                            |
| Condition            | string     | ""            | The condition that must be met for the check to succeed. Uses the same formatting as conditionals (for example, "gt 5.2", "true", "lt 10".)                                                                                 |
| ForceString          | bool       | false         | Forces the comparison to use string instead of attempting to parse it as a boolean or a float first. Use this if you know the value is a string.                                                                            |
| CheckAgainstMetadata | bool       | false         | Performs the comparison against a metadata by identifier instead of a constant value. Meaning that you could for example check whether the value of "progress_of_some_event" is larger than "progress_of_some_other_event". |



