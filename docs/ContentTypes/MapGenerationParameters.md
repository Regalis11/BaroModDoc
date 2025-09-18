# MapGenerationParameters
<sup>Relevant files: [[Shared:MapGenerationParametersFile.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/MapGenerationParametersFile.cs) [[Shared:MapGenerationParams.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/Map/MapGenerationParams.cs)</sup>

*This page was generated automatically.*

- **Required by core package:** No



## Child elements
- `radiationparams`


## Attributes

| Attribute                                 | Type  | Default value | Description                                                                                                                                                                                                                                                                                       |
|-------------------------------------------|-------|---------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| DifficultyZones                           | int   | 6             |                                                                                                                                                                                                                                                                                                   |
| Width                                     | int   | 8000          |                                                                                                                                                                                                                                                                                                   |
| Height                                    | int   | 500           |                                                                                                                                                                                                                                                                                                   |
| SmallLevelConnectionLength                | float | 20            | Connections with a length smaller or equal to this generate the smallest possible levels (using the MinWidth parameter in the level generation paramaters).                                                                                                                                       |
| LargeLevelConnectionLength                | float | 200           | Connections with a length larger or equal to this generate the largest possible levels (using the MaxWidth parameter in the level generation paramaters).                                                                                                                                         |
| VoronoiSiteInterval                       | Point | "20,20"       | How far from each other voronoi sites are placed. Sites determine shape of the voronoi graph. Locations are placed at the vertices of the voronoi cells. (Decreasing this value causes the number of sites, and the complexity of the map, to increase exponentially - be careful when adjusting) |
| VoronoiSiteVariance                       | Point | "5,5"         |                                                                                                                                                                                                                                                                                                   |
| MinConnectionDistance                     | float | 10            | Connections smaller than this are removed.                                                                                                                                                                                                                                                        |
| MinLocationDistance                       | float | 5             | Locations that are closer than this to another location are removed.                                                                                                                                                                                                                              |
| ConnectionIndicatorIterationMultiplier    | float | 0.1           | ConnectionIterationMultiplier for the UI indicator lines between locations.                                                                                                                                                                                                                       |
| ConnectionIndicatorDisplacementMultiplier | float | 0.1           | ConnectionDisplacementMultiplier for the UI indicator lines between locations.                                                                                                                                                                                                                    |



