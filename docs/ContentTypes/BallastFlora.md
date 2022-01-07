# BallastFlora

<sup>Relevant files: [Shared:BallastFloraPrefab.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Map/Creatures/BallastFloraPrefab.cs) [Shared:BallastFloraFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/BallastFloraFile.cs)</sup>

- **Required by core package:** Yes
- **Alternate names:** MapCreature

## Attributes



## Examples

### Example 1 - single ballastflorabehavior

```xml
<ballastflorabehavior
  identifier="myballastflorabehavior" />
```

### Example 2 - multiple ballastflorabehaviors

```xml
<ballastflorabehaviors>
  <ballastflorabehavior
    identifier="myballastflorabehavior1" />
  <ballastflorabehavior
    identifier="myballastflorabehavior2" />
</ballastflorabehaviors>
```

### Example 3 - overriding existing ballastflorabehaviors

```xml
<override>
  <ballastflorabehavior
    identifier="myballastflorabehavior1" />
  <ballastflorabehavior
    identifier="myballastflorabehavior2" />
</override>
```

