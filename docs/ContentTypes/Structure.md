# Structure
<sup>Relevant files: [[Shared:StructureFile.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/StructureFile.cs) [[Shared:StructurePrefab.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/StructurePrefab.cs) [[Shared:MapEntityPrefab.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/MapEntityPrefab.cs)</sup>

*This page was generated automatically.*

- **Required by core package:** Yes



## Child elements
- `sprite`
- `backgroundsprite`
- `decorativesprite`


## Attributes

| Attribute                | Type            | Default value     | Description                                                                                                                                                                                     |
|--------------------------|-----------------|-------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Body                     | bool            | false             | Does the structure have a physics body?                                                                                                                                                         |
| BodyRotation             | float           | 0.0               | Rotation of the physics body in degrees.                                                                                                                                                        |
| BodyWidth                | float           | 0.0               | Width of the physics body in pixels.                                                                                                                                                            |
| BodyHeight               | float           | 0.0               | Height of the physics body in pixels.                                                                                                                                                           |
| BodyOffset               | Vector2         | "0.0,0.0"         | Offset of the physics body from the center of the structure in pixels.                                                                                                                          |
| Platform                 | bool            | false             | Is the structure a platform (i.e. a "floor" the players can pass through)? Only relevant if the structure has a physics body.                                                                   |
| AllowAttachItems         | bool            | false             | Can items like signal components be attached on this structure? Should be enabled on structures like decorative background walls.                                                               |
| MinHealth                | float           | 0.0               |                                                                                                                                                                                                 |
| Health                   | float           | 100.0             |                                                                                                                                                                                                 |
| IndestructibleInOutposts | bool            | true              | Should the structure be indestructible when used in an outpost?                                                                                                                                 |
| CastShadow               | bool            | false             | Should the structure cast shadows and obstruct visibility when LOS is enabled?                                                                                                                  |
| StairDirection           | Direction       | Direction.None    | Makes the structure function as a staircase.                                                                                                                                                    |
| StairAngle               | float           | 45.0              | Angle of the stairs in degrees. Only relevant if StairDirection is something else than None.                                                                                                    |
| NoAITarget               | bool            | false             | If enabled, monsters will not be able to target this structure.                                                                                                                                 |
| Size                     | Vector2         | "0,0"             | Size of the structure in pixels. If not set, the size is determined, based on the attributes width and height, and if those aren't defined either, based on the size of the structure's sprite. |
| DamageSound              | string          | ""                | Tag of the sound that plays when something damages the wall.                                                                                                                                    |
| DamageParticle           | string          | "shrapnel"        | Identifier of the particles emitted when something damages the wall.                                                                                                                            |
| TextureScale             | Vector2         | "1.0, 1.0"        |                                                                                                                                                                                                 |
| ResizeHorizontal         | bool            | false             |                                                                                                                                                                                                 |
| ResizeVertical           | bool            | false             |                                                                                                                                                                                                 |
| Description              | LocalizedString | ""                |                                                                                                                                                                                                 |
| AllowedUpgrades          | string          | ""                |                                                                                                                                                                                                 |
| HideInMenus              | bool            | false             |                                                                                                                                                                                                 |
| Subcategory              | string          | ""                |                                                                                                                                                                                                 |
| Linkable                 | bool            | false             |                                                                                                                                                                                                 |
| SpriteColor              | Color           | "1.0,1.0,1.0,1.0" |                                                                                                                                                                                                 |
| Scale                    | float           | 1                 |                                                                                                                                                                                                 |



