# Structure

<sub>Relevant files: [Shared:StructureFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/StructureFile.cs) [Shared:StructurePrefab.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/StructurePrefab.cs)</sub>
- **Required by core package:** Yes

## Attributes

- `BodyWidth` : `float`
- `IndestructibleInOutposts` : `bool`
- `Size` : `Vector2`
- `StairAngle` : `float`
- `BodyHeight` : `float`
- `Platform` : `bool`
- `TextureScale` : `Vector2`
- `DamageSound` : `string`
- `BodyOffset` : `Vector2`
- `AllowAttachItems` : `bool`
- `CastShadow` : `bool`
- `StairDirection` : `Direction`
- `NoAITarget` : `bool`
- `MinHealth` : `float`
- `Health` : `float`
- `Body` : `bool`
- `BodyRotation` : `float`
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

