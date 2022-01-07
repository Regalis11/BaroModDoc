# Talents

- **Required by core package:** Yes

## Examples

### Example 1 - single talent

```xml
<talent
  identifier="mytalent"
  TODO="add remaining attributes" />
```

### Example 2 - multiple talents

```xml
<talents>
  <talent
    identifier="mytalent1"
    TODO="add remaining attributes" />
  <talent
    identifier="mytalent2"
    TODO="add remaining attributes" />
</talents>
```

### Example 3 - overriding existing talents

```xml
<override>
  <talent
    identifier="mytalent1"
    TODO="add remaining attributes" />
  <talent
    identifier="mytalent2"
    TODO="add remaining attributes" />
</override>
```

