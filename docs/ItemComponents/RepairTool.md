# RepairTool


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| UsableIn|UseEnvironment|"Both"|Can the item be used in air, water or both. |
| Range|float|0.0|The distance at which the item can repair targets. |
| Spread|float|0.0|Random spread applied to the firing angle when used by a character with sufficient skills to use the tool (in degrees). |
| UnskilledSpread|float|0.0|Random spread applied to the firing angle when used by a character with insufficient skills to use the tool (in degrees). |
| StructureFixAmount|float|0.0|How many units of damage the item removes from structures per second. |
| FireDamage|float|0.0|How much damage is applied to ballast flora. |
| LevelWallFixAmount|float|0.0|How many units of damage the item removes from destructible level walls per second. |
| ExtinguishAmount|float|0.0|How much the item decreases the size of fires per second. |
| WaterAmount|float|0.0|How much water the item provides to planters per second. |
| BarrelPos|Vector2|"0.0,0.0"|The position of the barrel as an offset from the item's center (in pixels). |
| RepairThroughWalls|bool|false|Can the item repair things through walls. |
| RepairMultiple|bool|false|Can the item repair multiple things at once, or will it only affect the first thing the ray from the barrel hits. |
| RepairThroughHoles|bool|false|Can the item repair things through holes in walls. |
| MaxOverlappingWallDist|float|100.0|How far two walls need to not be considered overlapping and to stop the ray. |
| HitItems|bool|true|Can the item hit broken doors. |
| HitBrokenDoors|bool|false|Can the item hit broken doors. |
| FireProbability|float|0.0|The probability of starting a fire somewhere along the ray fired from the barrel (for example, 0.1 = 10% chance to start a fire during a second of use). |
| TargetForce|float|0.0|Force applied to the entity the ray hits. |
| BarrelRotation|float|0.0|Rotation of the barrel in degrees. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="wateringcan" category="Misc" Tags="smallitem,tool" cargocontaineridentifier="metalcrate" Scale="0.5" impactsoundtag="impact_metal_light">
  <RepairTool wateramount="100.0" range="0" barrelpos="28,11" targetstructures="false" hititems="false">
    <ParticleEmitter particle="waterdrop" velocitymin="10.0" velocitymax="50.0" particlespersecond="50" />
  </RepairTool>
  [...]
</Item>
```

