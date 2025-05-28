# RuinConfig
<sup>Relevant files: [[Shared:RuinConfigFile.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/RuinConfigFile.cs) [[Shared:RuinGenerationParams.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/Levels/Ruins/RuinGenerationParams.cs) [[Shared:OutpostGenerationParams.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/Outposts/OutpostGenerationParams.cs)</sup>

*This page was generated automatically.*

- **Required by core package:** Yes



## Child elements
- `modulecount`
- `npcs`


## Attributes

| Attribute                     | Type       | Default value | Description                                                                                                                                                                              |
|-------------------------------|------------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| IsMissionReady                | bool       | true          | Are these params designed to be used for alien ruins targeted by missions. If false, the params are ignored when there's any missions targeting ruins.                                   |
| ForceToEndLocationIndex       | int        | -1            | Should this type of outpost be forced to the locations at the end of the campaign map? 0 = first end level, 1 = second end level, and so on.                                             |
| PreferredDifficulty           | int        | -1            | The closer to the current level difficulty this value is, the higher the probability of choosing these generation params are. Defaults to -1, which means we use the current difficulty. |
| TotalModuleCount              | int        | 10            | Total number of modules in the outpost.                                                                                                                                                  |
| AppendToReachTotalModuleCount | bool       | true          | Should the generator append generic (module flag "none") modules to the outpost to reach the total module count.                                                                         |
| MinHallwayLength              | float      | 200           | Minimum length of the hallways between modules. If 0, the generator will place the modules directly against each other assuming it can be done without making any modules overlap.       |
| AlwaysGenerateHallways        | bool       | true          | Should hallways of the minimum hallway length be always generated between modules, even if they could be placed directly against each other with no overlaps?                            |
| AlwaysDestructible            | bool       | false         | Should this outpost always be destructible, regardless if damaging outposts is allowed by the server?                                                                                    |
| AlwaysRewireable              | bool       | false         | Should this outpost always be rewireable, regardless if rewiring is allowed by the server?                                                                                               |
| AllowStealing                 | bool       | false         | Should stealing from this outpost be always allowed?                                                                                                                                     |
| SpawnCrewInsideOutpost        | bool       | true          | Should the crew spawn inside the outpost (if not, they'll spawn in the submarine).                                                                                                       |
| LockUnusedDoors               | bool       | true          | Should doors at the edges of an outpost module that didn't get connected to another module be locked?                                                                                    |
| RemoveUnusedGaps              | bool       | true          | Should gaps at the edges of an outpost module that didn't get connected to another module be removed?                                                                                    |
| DrawBehindSubs                | bool       | false         | Should the whole outpost render behind submarines? Only set this to true if the submarine is intended to go inside the outpost.                                                          |
| MinWaterPercentage            | float      | 0             | Minimum amount of water in the hulls of the outpost.                                                                                                                                     |
| MaxWaterPercentage            | float      | 0             | Maximum amount of water in the hulls of the outpost.                                                                                                                                     |
| ReplaceInRadiation            | string     | ""            | Identifier of the outpost generation parameters that should be used if this outpost has become critically irradiated.                                                                    |
| AlwaysShowStructuresOnSonar   | bool       | false         | By default, sonar only shows the outline of the sub/outpost from the outside. Enable this if you want to see each structure individually.                                                |
| OutpostTag                    | Identifier | ""            | If set, a fully pre-built outpost with this tag will be used instead of generating the outpost.                                                                                          |



