# AfflictionAction

Gives an affliction to a specific character.

## Attributes

| Attribute             | Type       | Default value | Description                                                                                    |
|-----------------------|------------|---------------|------------------------------------------------------------------------------------------------|
| Affliction            | Identifier | ""            | Identifier of the affliction.                                                                  |
| Strength              | float      | 0             | Strength of the affliction.                                                                    |
| LimbType              | LimbType   | None          | Type of the limb(s) to apply the affliction on. Only valid if the affliction is limb-specific. |
| TargetTag             | Identifier | ""            | Tag of the character to apply the affliction on.                                               |
| MultiplyByMaxVitality | bool       | false         | Should the strength be multiplied by the maximum vitality of the target?                       |



