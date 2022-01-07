# Item

- **Required by core package:** Yes

## Examples

### Example 1 - single Item

```xml
<Item
  identifier="myItem"
  TODO="add remaining attributes" />
```

### Example 2 - multiple items

```xml
<items>
  <Item
    identifier="myItem1"
    TODO="add remaining attributes" />
  <Item
    identifier="myItem2"
    TODO="add remaining attributes" />
</items>
```

### Example 3 - overriding existing items

```xml
<override>
  <Item
    identifier="myItem1"
    TODO="add remaining attributes" />
  <Item
    identifier="myItem2"
    TODO="add remaining attributes" />
</override>
```

