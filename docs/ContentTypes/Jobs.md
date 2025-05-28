# Jobs
<sup>Relevant files: [[Shared:JobsFile.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/JobsFile.cs) [[Shared:JobPrefab.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/Jobs/JobPrefab.cs)</sup>

*This page was generated automatically.*

- **Required by core package:** Yes



## Child elements
- `itemset`
- `skills`
- `autonomousobjectives`
- `appropriateobjectives`
- `jobicon`
- `jobiconsmall`


## Attributes

| Attribute             | Type                         | Default value | Description                                                                                                                                                               |
|-----------------------|------------------------------|---------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| UIColor               | Color                        | "1,1,1,1"     |                                                                                                                                                                           |
| IdleBehavior          | AIObjectiveIdle.BehaviorType | Passive       | How should the character behave when idling (not doing any particular task)?                                                                                              |
| OnlyJobSpecificDialog | bool                         | false         | Can the character speak any random lines, or just ones specifically meant for the job?                                                                                    |
| InitialCount          | int                          | 0             | The number of these characters in the crew the player starts with in the single player campaign.                                                                          |
| CampaignSetupUIOrder  | int                          | 10            | Determines the order of the characters in the campaign setup ui.                                                                                                          |
| AllowAlways           | bool                         | false         | If set to true, a client that has chosen this as their preferred job will get it regardless of the maximum number or the amount of spawnpoints in the sub.                |
| MaxNumber             | int                          | 100           | How many crew members can have the job (e.g. only one captain etc).                                                                                                       |
| MinNumber             | int                          | 0             | How many crew members are required to have the job. I.e. if one captain is required, one captain is chosen even if all the players have set captain to lowest preference. |
| MinKarma              | float                        | 0             | Minimum amount of karma a player must have to get assigned this job.                                                                                                      |
| PriceMultiplier       | float                        | 1             | Multiplier on the base hiring cost when hiring the character from an outpost.                                                                                             |
| VitalityModifier      | float                        | 0             | How much the vitality of the character is increased/reduced from the default value (e.g. 10 = 110 total vitality if the default vitality is 100.).                        |
| HiddenJob             | bool                         | false         | Hidden jobs are not selectable by players, but can be used by e.g. outpost NPCs.                                                                                          |



