# TagAction

Tags a specific entity. Tags are used by other actions to refer to specific entities. The tags are event\-specific, i.e. you cannot use a tag that was added by another event to refer to an entity.

## Attributes

| Attribute                     | Type       | Default value | Description                                                                                                                                                                                                                                                                                                                                  |
|-------------------------------|------------|---------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Criteria                      | string     | ""            | What criteria to use to select the entities to target. Valid values are players, player, traitor, nontraitor, nontraitorplayer, bot, crew, humanprefabidentifier:[id], jobidentifier:[id], structureidentifier:[id], structurespecialtag:[tag], itemidentifier:[id], itemtag:[tag], hull, hullname:[name], submarine:[type], eventtag:[tag]. |
| Tag                           | Identifier | ""            | The tag to apply to the target.                                                                                                                                                                                                                                                                                                              |
| SubmarineType                 | SubType    | Any           | The type of submarine the target needs to be in.                                                                                                                                                                                                                                                                                             |
| RequiredModuleTag             | Identifier | ""            | If set, the target must be in an outpost module that has this tag.                                                                                                                                                                                                                                                                           |
| IgnoreIncapacitatedCharacters | bool       | true          | Should incapacitated (e.g. dead, paralyzed, unconscious) characters be ignored, i.e. not considered valid targets?                                                                                                                                                                                                                           |
| AllowHiddenItems              | bool       | false         | Can items that have been set to be hidden in-game be tagged?                                                                                                                                                                                                                                                                                 |
| ChooseRandom                  | bool       | false         | If there are multiple matching targets, should all of them be tagged or one chosen randomly?                                                                                                                                                                                                                                                 |
| ContinueIfNoTargetsFound      | bool       | false         | Should the event continue if the TagAction can't find any valid targets?                                                                                                                                                                                                                                                                     |
| ChoosePercentage              | float      | 0             | If larger than 0, the specified percentage of the matching targets are tagged. Between 0-100.                                                                                                                                                                                                                                                |


