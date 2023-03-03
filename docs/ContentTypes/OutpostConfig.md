# OutpostConfig
<sup>Relevant files: [[Shared:OutpostConfigFile.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/OutpostConfigFile.cs) [[Shared:OutpostGenerationParams.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/Outposts/OutpostGenerationParams.cs)</sup>

*This page was generated automatically.*

- **Required by core package:** Yes



## Child elements
- `modulecount`
- `npcs`


## Attributes

| Attribute                     | Type   | Default value | Description                                                                                                                                                                        |
|-------------------------------|--------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| ForceToEndLocationIndex       | int    | -1            | Should this type of outpost be forced to the locations at the end of the campaign map? 0 = first end level, 1 = second end level, and so on.                                       |
| TotalModuleCount              | int    | 10            | Total number of modules in the outpost.                                                                                                                                            |
| AppendToReachTotalModuleCount | bool   | true          | Should the generator append generic (module flag "none") modules to the outpost to reach the total module count.                                                                   |
| MinHallwayLength              | float  | 200.0         | Minimum length of the hallways between modules. If 0, the generator will place the modules directly against each other assuming it can be done without making any modules overlap. |
| AlwaysDestructible            | bool   | false         | Should this outpost always be destructible, regardless if damaging outposts is allowed by the server?                                                                              |
| AlwaysRewireable              | bool   | false         | Should this outpost always be rewireable, regardless if rewiring is allowed by the server?                                                                                         |
| AllowStealing                 | bool   | false         | Should stealing from this outpost be always allowed?                                                                                                                               |
| SpawnCrewInsideOutpost        | bool   | true          | Should the crew spawn inside the outpost (if not, they'll spawn in the submarine).                                                                                                 |
| LockUnusedDoors               | bool   | true          | Should doors at the edges of an outpost module that didn't get connected to another module be locked?                                                                              |
| RemoveUnusedGaps              | bool   | true          | Should gaps at the edges of an outpost module that didn't get connected to another module be removed?                                                                              |
| DrawBehindSubs                | bool   | false         | Should the whole outpost render behind submarines? Only set this to true if the submarine is intended to go inside the outpost.                                                    |
| MinWaterPercentage            | float  | 0.0           | Minimum amount of water in the hulls of the outpost.                                                                                                                               |
| MaxWaterPercentage            | float  | 0.0           | Maximum amount of water in the hulls of the outpost.                                                                                                                               |
| ReplaceInRadiation            | string | ""            | Identifier of the outpost generation parameters that should be used if this outpost has become critically irradiated.                                                              |



