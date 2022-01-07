# Structure

<sup>Relevant files: [Shared:StructurePrefab.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/StructurePrefab.cs) [Shared:StructureFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/StructureFile.cs)</sup>

- **Required by core package:** Yes

## Attributes

- `Body` : `bool`
- `BodyRotation` : `float`
- `BodyWidth` : `float`
- `BodyHeight` : `float`
- `BodyOffset` : `Vector2`
- `Platform` : `bool`
- `AllowAttachItems` : `bool`
- `MinHealth` : `float`
- `Health` : `float`
- `IndestructibleInOutposts` : `bool`
- `CastShadow` : `bool`
- `StairDirection` : `Direction`
- `StairAngle` : `float`
- `NoAITarget` : `bool`
- `Size` : `Vector2`
- `DamageSound` : `string`
- `TextureScale` : `Vector2`

## Examples

### Example 1 - single Structure

```xml
<Structure
  identifier="myStructure" />
```

### Example 2 - multiple prefabs

```xml
<prefabs>
  <Structure
    identifier="myStructure1" />
  <Structure
    identifier="myStructure2" />
</prefabs>
```

### Example 3 - overriding existing prefabs

```xml
<override>
  <Structure
    identifier="myStructure1" />
  <Structure
    identifier="myStructure2" />
</override>
```

