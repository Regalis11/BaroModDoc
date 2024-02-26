# NPCChangeTeamAction

Changes the team of an NPC. Most common use cases are adding a character to the crew, or turning an NPC hostile to the crew by changing their team to a hostile one.

## Attributes

| Attribute      | Type              | Default value | Description                                                                                                                                                                  |
|----------------|-------------------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| NPCTag         | Identifier        | ""            | Tag of the NPC(s) whose team to change.                                                                                                                                      |
| TeamID         | CharacterTeamType | None          | The team to move the NPC to. None = unspecified, Team1 = player crew, Team2 = the team opposing Team1 (= hostile to player crew), FriendlyNPC = friendly to all other teams. |
| AddToCrew      | bool              | false         | Should the NPC be added to the player crew?                                                                                                                                  |
| RemoveFromCrew | bool              | false         | Should the NPC be removed from the player crew?                                                                                                                              |



