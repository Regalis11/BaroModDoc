# UpgradeModules

<sub>Relevant files: [Shared:UpgradePrefab.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Upgrades/UpgradePrefab.cs) [Shared:UpgradeModulesFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/UpgradeModulesFile.cs)</sub>
- **Required by core package:** Yes

## Attributes


## Examples

### Example 1 - single upgrademodule

```xml
<upgrademodule
  identifier="myupgrademodule" />
```

### Example 2 - multiple upgrademodules

```xml
<upgrademodules>
  <upgrademodule
    identifier="myupgrademodule1" />
  <upgrademodule
    identifier="myupgrademodule2" />
</upgrademodules>
```

### Example 3 - overriding existing upgrademodules

```xml
<override>
  <upgrademodule
    identifier="myupgrademodule1" />
  <upgrademodule
    identifier="myupgrademodule2" />
</override>
```

