# SpawnAction

Spawns an entity \(e.g. item, NPC, monster\).

## Attributes

| Attribute               | Type              | Default value | Description                                                                                                                                                       |
|-------------------------|-------------------|---------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| SpeciesName             | Identifier        | ""            | Species name of the character to spawn.                                                                                                                           |
| NPCSetIdentifier        | Identifier        | ""            | Identifier of the NPC set to choose from.                                                                                                                         |
| NPCIdentifier           | Identifier        | ""            | Identifier of the NPC.                                                                                                                                            |
| LootingIsStealing       | bool              | true          | Should taking the items of this npc be considered as stealing?                                                                                                    |
| ItemIdentifier          | Identifier        | ""            | Identifier of the item to spawn.                                                                                                                                  |
| ItemTag                 | Identifier        | ""            | Tag of the item to spawn.                                                                                                                                         |
| TargetTag               | Identifier        | ""            | The spawned entity will be assigned this tag. The tag can be used to refer to the entity by other actions of the event.                                           |
| TargetInventory         | Identifier        | ""            | Tag of an entity with an inventory to spawn the item into.                                                                                                        |
| SpawnLocation           | SpawnLocationType | Any           | Where should the entity spawn? This can be restricted further with the other spawn point options.                                                                 |
| SpawnPointType          | SpawnType         | Human         | Type of spawnpoint to spawn the entity at. Ignored if SpawnPointTag is set.                                                                                       |
| SpawnPointTag           | Identifier        | ""            | Tag of a spawnpoint to spawn the entity at.                                                                                                                       |
| TeamID                  | CharacterTeamType | FriendlyNPC   | Team of the NPC to spawn. Only valid when spawning a character.                                                                                                   |
| RequireSpawnPointTag    | bool              | false         | Should we spawn the entity even when no spawn points with matching tags were found?                                                                               |
| AllowDuplicates         | bool              | true          | If false, we won't spawn another character if one with the same identifier has already been spawned.                                                              |
| Amount                  | int               | 1             | Number of entities to spawn.                                                                                                                                      |
| SpawnIfInventoryFull    | bool              | true          | Should the item be spawned even if the target inventory is full (just spawning it at the position of the target)? Only valid if spawning an item in an inventory. |
| Offset                  | float             | 100           | Random offset to add to the spawn position.                                                                                                                       |
| TargetModuleTags        | string            | ""            | What outpost module tags does the entity prefer to spawn in.                                                                                                      |
| IgnoreByAI              | bool              | false         | Should the AI ignore this item. This will prevent outpost NPCs cleaning up or otherwise using important items intended to be left for the players.                |
| AllowInPlayerView       | bool              | true          | If disabled, the action will choose a spawn position away from players' views if one is available.                                                                |
| ContinueIfFailedToSpawn | bool              | false         | Should the event continue even if the entity failed to spawn for whatever reason?                                                                                 |



