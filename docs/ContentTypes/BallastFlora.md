# BallastFlora

- **Required by core package:** Yes
- **Alternate names:** MapCreature

## Examples

### Example 1 - single ballastflorabehavior

```xml
<ballastflorabehavior
  identifier="myballastflorabehavior"
  TODO="add remaining attributes" />
```

### Example 2 - multiple ballastflorabehaviors

```xml
<ballastflorabehaviors>
  <ballastflorabehavior
    identifier="myballastflorabehavior1"
    TODO="add remaining attributes" />
  <ballastflorabehavior
    identifier="myballastflorabehavior2"
    TODO="add remaining attributes" />
</ballastflorabehaviors>
```

### Example 3 - overriding existing ballastflorabehaviors

```xml
<override>
  <ballastflorabehavior
    identifier="myballastflorabehavior1"
    TODO="add remaining attributes" />
  <ballastflorabehavior
    identifier="myballastflorabehavior2"
    TODO="add remaining attributes" />
</override>
```

