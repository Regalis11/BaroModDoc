# Item

<sup>Relevant files: [Shared:ItemFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/ItemFile.cs)</sup>
- **Required by core package:** Yes

## Attributes


## Examples

### Example 1 - single Item

```xml
<Item
  identifier="myItem" />
```

### Example 2 - multiple items

```xml
<items>
  <Item
    identifier="myItem1" />
  <Item
    identifier="myItem2" />
</items>
```

### Example 3 - overriding existing items

```xml
<override>
  <Item
    identifier="myItem1" />
  <Item
    identifier="myItem2" />
</override>
```

