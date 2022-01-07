# UpgradeModules

- **Required by core package:** Yes

## Examples

### Example 1 - single upgrademodule

```xml
<upgrademodule
  identifier="myupgrademodule"
  TODO="add remaining attributes" />
```

### Example 2 - multiple upgrademodules

```xml
<upgrademodules>
  <upgrademodule
    identifier="myupgrademodule1"
    TODO="add remaining attributes" />
  <upgrademodule
    identifier="myupgrademodule2"
    TODO="add remaining attributes" />
</upgrademodules>
```

### Example 3 - overriding existing upgrademodules

```xml
<override>
  <upgrademodule
    identifier="myupgrademodule1"
    TODO="add remaining attributes" />
  <upgrademodule
    identifier="myupgrademodule2"
    TODO="add remaining attributes" />
</override>
```

