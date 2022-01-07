# Structure

<sub>Relevant files: [Shared:StructurePrefab.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource\Map\StructurePrefab.cs) [Shared:StructureFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/StructureFile.cs)</sub>
- **Required by core package:** Yes

## Attributes

- `MinHealth` : `float`
- `BodyHeight` : `float`
- `BodyRotation` : `float`
- `BodyOffset` : `Vector2`
- `TextureScale` : `Vector2`
- `BodyWidth` : `float`
- `Platform` : `bool`
- `IndestructibleInOutposts` : `bool`
- `Health` : `float`
- `CastShadow` : `bool`
- `StairAngle` : `float`
- `DamageSound` : `string`
- `StairDirection` : `Direction`
- `Size` : `Vector2`
- `NoAITarget` : `bool`
- `AllowAttachItems` : `bool`
- `Body` : `bool`
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

