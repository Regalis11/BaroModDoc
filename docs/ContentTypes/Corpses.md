# Corpses

- **Required by core package:** Yes

## Examples

### Example 1 - single corpse

```xml
<corpse
  identifier="mycorpse"
  TODO="add remaining attributes" />
```

### Example 2 - multiple corpses

```xml
<corpses>
  <corpse
    identifier="mycorpse1"
    TODO="add remaining attributes" />
  <corpse
    identifier="mycorpse2"
    TODO="add remaining attributes" />
</corpses>
```

### Example 3 - overriding existing corpses

```xml
<override>
  <corpse
    identifier="mycorpse1"
    TODO="add remaining attributes" />
  <corpse
    identifier="mycorpse2"
    TODO="add remaining attributes" />
</override>
```

