# CaveGenerationParameters
<sup>Relevant files: [[Shared:CaveGenerationParametersFile.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/CaveGenerationParametersFile.cs) [[Shared:CaveGenerationParams.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/Levels/CaveGenerationParams.cs)</sup>

*This page was generated automatically.*

- **Required by core package:** Yes



## Child elements
- `wall`
- `walledge`
- `overridecommonness`


## Attributes

| Attribute             | Type  | Default value | Description                                                                                                   |
|-----------------------|-------|---------------|---------------------------------------------------------------------------------------------------------------|
| Commonness            | float | 1.0           |                                                                                                               |
| MinWidth              | int   | 8000          |                                                                                                               |
| MaxWidth              | int   | 10000         |                                                                                                               |
| MinHeight             | int   | 8000          |                                                                                                               |
| MaxHeight             | int   | 10000         |                                                                                                               |
| MinBranchCount        | int   | 2             | Minimum number of tunnel branches in the cave.                                                                |
| MaxBranchCount        | int   | 4             | Maximum number of tunnel branches in the cave.                                                                |
| LevelObjectAmount     | int   | 50            | Total amount of level objects in the cave.                                                                    |
| DestructibleWallRatio | float | 0.1           | What portion of the empty cells in the cave should be turned into destructible walls? For example, 0.1 = 10%. |



