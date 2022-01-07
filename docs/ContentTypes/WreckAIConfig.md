# WreckAIConfig

- **Required by core package:** Yes

## Examples

### Example 1 - single wreckaiconfig

```xml
<wreckaiconfig
  identifier="mywreckaiconfig"
  TODO="add remaining attributes" />
```

### Example 2 - multiple wreckaiconfigs

```xml
<wreckaiconfigs>
  <wreckaiconfig
    identifier="mywreckaiconfig1"
    TODO="add remaining attributes" />
  <wreckaiconfig
    identifier="mywreckaiconfig2"
    TODO="add remaining attributes" />
</wreckaiconfigs>
```

### Example 3 - overriding existing wreckaiconfigs

```xml
<override>
  <wreckaiconfig
    identifier="mywreckaiconfig1"
    TODO="add remaining attributes" />
  <wreckaiconfig
    identifier="mywreckaiconfig2"
    TODO="add remaining attributes" />
</override>
```

