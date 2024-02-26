# CheckAfflictionAction

Check whether a target has a specific affliction.

## Attributes

| Attribute            | Type       | Default value | Description                                                                                                                           |
|----------------------|------------|---------------|---------------------------------------------------------------------------------------------------------------------------------------|
| Identifier           | Identifier | ""            | Identifier of the affliction.                                                                                                         |
| TargetTag            | Identifier | ""            | Tag of the character to check.                                                                                                        |
| SourceCharacter      | Identifier | ""            | Tag referring to the character who caused the affliction. Can be used to require the affliction to be caused by a specific character. |
| TargetLimb           | LimbType   | None          | Only check afflictions on the specified limb type.                                                                                    |
| AllowLimbAfflictions | bool       | true          | When set to false, limb-specific afflictions are ignored when not checking a specific limb.                                           |
| MinStrength          | float      | 0             | Minimum strength of the affliction.                                                                                                   |



