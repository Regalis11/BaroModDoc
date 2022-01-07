# LocationTypes

<sup>Relevant files: [Shared:LocationTypesFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/LocationTypesFile.cs)</sup>

- **Required by core package:** Yes

## Attributes



## Examples

### Example 1 - single locationtype

```xml
<locationtype
  identifier="mylocationtype" />
```

### Example 2 - multiple locationtypes

```xml
<locationtypes>
  <locationtype
    identifier="mylocationtype1" />
  <locationtype
    identifier="mylocationtype2" />
</locationtypes>
```

### Example 3 - overriding existing locationtypes

```xml
<override>
  <locationtype
    identifier="mylocationtype1" />
  <locationtype
    identifier="mylocationtype2" />
</override>
```

