# EntitySpawnerComponent


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| ItemIdentifier|string?|""|Identifier of the item to spawn, does nothing if SpeciesName is set. Separate by comma to have multiple items spawn at random. |
| SpeciesName|string?|""|Species name of the creature to spawn, takes priority if ItemIdentifier is set. Separate by comma to have multiple creatures spawn at random. |
| OnlySpawnWhenCrewInRange|bool|true|Only spawn if crew members are within certain area |
| CrewAreaShape|AreaShape|AreaShape.Rectangle|Shape of the area where crew members need to stay |
| CrewAreaBounds|Vector2|"500,500"|Size of the rectangle where crew members need to stay. Does nothing if CrewAreaShape is set to Circle |
| CrewAreaRadius|float|500|Radius of the circle to spawn stuff in. Does nothing if CrewAreaShape is set to Rectangle |
| CrewAreaOffset|Vector2|"0,0"|Offset of the crew area from the center of the item |
| SpawnAreaShape|AreaShape|AreaShape.Rectangle|Shape of the area where enemies or items are spawned |
| SpawnAreaBounds|Vector2|"500,500"|Size of the rectangle where items or creatures will be spawned. Does nothing if SpawnAreaShape is set to Circle |
| SpawnAreaRadius|float|500|Radius of the circle where items or creatures will be spawned. Does nothing if SpawnAreaShape is set to Rectangle |
| SpawnAreaOffset|Vector2|"0,0"|Offset of the spawn area from the center of the item |
| SpawnTimerRange|Vector2|"10,40"|Time range between spawn attempts in seconds. Set both to a negative value to disable automatic spawning. |
| SpawnAmountRange|Vector2|"1,3"|Minumum and maximum amount of items or creatures to spawn in one attempt |
| MaximumAmount|int|8|Total maximum amount of items or creatures that can be spawned. 0 = unrestricted. |
| MaximumAmountInArea|int|8|Amount of items or creatures in the spawn area that will prevent further items or creatures from being spawned. 0 = unrestricted. |
| MaximumAmountRangePadding|float|500|Inflate the circle of rectangle by this value to extend the area that counts towards the maximum amount of items or enemies to be spawned |
| CanSpawn|bool|true| |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item nameidentifier="ruinvent" identifier="ruinvent" width="128" height="192" texturescale="0.5,0.5" scale="0.5" category="Alien">
  <EntitySpawnerComponent />
  [...]
</Item>
```

