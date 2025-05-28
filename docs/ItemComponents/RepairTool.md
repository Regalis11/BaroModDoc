# RepairTool


## Attributes

| Attribute              | Type           | Default value | Description                                                                                                                                                                                            |
|------------------------|----------------|---------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| UsableIn               | UseEnvironment | "Both"        | Can the item be used in air, water or both.                                                                                                                                                            |
| Range                  | float          | 0             | The distance at which the item can repair targets.                                                                                                                                                     |
| Spread                 | float          | 0             | Random spread applied to the firing angle when used by a character with sufficient skills to use the tool (in degrees).                                                                                |
| UnskilledSpread        | float          | 0             | Random spread applied to the firing angle when used by a character with insufficient skills to use the tool (in degrees).                                                                              |
| StructureFixAmount     | float          | 0             | How many units of damage the item removes from structures per second.                                                                                                                                  |
| FireDamage             | float          | 0             | How much damage is applied to ballast flora.                                                                                                                                                           |
| LevelWallFixAmount     | float          | 0             | How many units of damage the item removes from destructible level walls per second.                                                                                                                    |
| ExtinguishAmount       | float          | 0             | How much the item decreases the size of fires per second.                                                                                                                                              |
| WaterAmount            | float          | 0             | How much water the item provides to planters per second.                                                                                                                                               |
| BarrelPos              | Vector2        | "0.0,0.0"     | The position of the barrel as an offset from the item's center (in pixels).                                                                                                                            |
| RepairThroughWalls     | bool           | false         | Can the item repair things through walls.                                                                                                                                                              |
| RepairMultiple         | bool           | false         | Can the item repair multiple things at once, or will it only affect the first thing the ray from the barrel hits.                                                                                      |
| RepairMultipleWalls    | bool           | true          | Can the item repair multiple walls at once? Only relevant if RepairMultiple is true.                                                                                                                   |
| RepairThroughHoles     | bool           | false         | Can the item repair things through holes in walls.                                                                                                                                                     |
| MaxOverlappingWallDist | float          | 100           | How far two walls need to not be considered overlapping and to stop the ray.                                                                                                                           |
| DeattachSpeed          | float          | 1             | How fast the tool detaches level resources (e.g. minerals). Acts as a multiplier on the speed: with a value of 2, detaching an item whose DeattachDuration is set to 30 seconds would take 15 seconds. |
| HitItems               | bool           | true          | Can the item hit doors.                                                                                                                                                                                |
| HitBrokenDoors         | bool           | false         | Can the item hit broken doors.                                                                                                                                                                         |
| IgnoreCharacters       | bool           | false         | Should the tool ignore characters? Enabled e.g. for fire extinguisher.                                                                                                                                 |
| FireProbability        | float          | 0             | The probability of starting a fire somewhere along the ray fired from the barrel (for example, 0.1 = 10% chance to start a fire during a second of use).                                               |
| TargetForce            | float          | 0             | Force applied to the entity the ray hits.                                                                                                                                                              |
| BarrelRotation         | float          | 0             | Rotation of the barrel in degrees.                                                                                                                                                                     |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="extinguisher" category="Equipment" Tags="mediumitem,fireextinguisher,provocative" cargocontaineridentifier="metalcrate" Scale="0.5" impactsoundtag="impact_metal_light" donttransferbetweensubs="true">
  <RepairTool extinguishamount="60.0" range="500" barrelpos="21,25" repairthroughholes="true" hititems="false" IgnoreCharacters="true">
    <ParticleEmitter particle="extinguisher" velocitymin="1000.0" velocitymax="1650.0" particlespersecond="100" />
    <StatusEffect type="OnUse" targettype="This" Condition="-2.0" />
    <sound file="Content/Items/Tools/Extinguisher.ogg" type="OnUse" range="500.0" loop="true" />
  </RepairTool>
  <RepairTool range="500" barrelpos="21,25" IgnoreCharacters="false">
    <!-- Remove 'burning' affliction from characters-->
    <StatusEffect type="OnUse" target="UseTarget">
      <ReduceAffliction identifier="burning" amount="15.0" />
    </StatusEffect>
  </RepairTool>
  [...]
</Item>
```

