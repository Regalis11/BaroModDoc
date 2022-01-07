# BackgroundCreaturePrefabs

<sup>Relevant files: [Shared:BackgroundCreaturePrefabsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/BackgroundCreaturePrefabsFile.cs)</sup>
- **Required by core package:** No

## Attributes


**WARNING:** This file likely generated completely incorrectly!

## Examples

### Example 1 - single BackgroundCreaturePrefab

```xml
<BackgroundCreaturePrefab
  identifier="myBackgroundCreaturePrefab" />
```

### Example 2 - multiple BackgroundCreaturePrefabs

```xml
<BackgroundCreaturePrefabs>
  <BackgroundCreaturePrefab
    identifier="myBackgroundCreaturePrefab1" />
  <BackgroundCreaturePrefab
    identifier="myBackgroundCreaturePrefab2" />
</BackgroundCreaturePrefabs>
```

### Example 3 - overriding existing BackgroundCreaturePrefabs

```xml
<override>
  <BackgroundCreaturePrefab
    identifier="myBackgroundCreaturePrefab1" />
  <BackgroundCreaturePrefab
    identifier="myBackgroundCreaturePrefab2" />
</override>
```

