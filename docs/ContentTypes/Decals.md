# Decals

<sup>Relevant files: [Shared:DecalsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/DecalsFile.cs)</sup>

**WARNING:** This file likely generated completely incorrectly!

- **Required by core package:** No

## Attributes



## Examples

### Example 1 - single Decal

```xml
<Decal
  identifier="myDecal" />
```

### Example 2 - multiple Decals

```xml
<Decals>
  <Decal
    identifier="myDecal1" />
  <Decal
    identifier="myDecal2" />
</Decals>
```

### Example 3 - overriding existing Decals

```xml
<override>
  <Decal
    identifier="myDecal1" />
  <Decal
    identifier="myDecal2" />
</override>
```

