# AddScoreAction

Modifies the win score of a team in the PvP mode.

## Attributes

| Attribute | Type              | Default value | Description                                                          |
|-----------|-------------------|---------------|----------------------------------------------------------------------|
| TargetTag | Identifier        | ""            | Tag of a target (character) whose team the score should be given to. |
| Team      | CharacterTeamType | None          | Which team's score to add to? Ignored if TargetTag is set.           |
| Amount    | int               | 1             | How much to add to the score? Can also be negative.                  |



