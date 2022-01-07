# Orders

<sup>Relevant files: [Shared:OrdersFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/OrdersFile.cs)</sup>

**WARNING:** This file likely generated completely incorrectly!

- **Required by core package:** Yes

## Attributes



## Examples

### Example 1 - single Order

```xml
<Order
  identifier="myOrder" />
```

### Example 2 - multiple Orders

```xml
<Orders>
  <Order
    identifier="myOrder1" />
  <Order
    identifier="myOrder2" />
</Orders>
```

### Example 3 - overriding existing Orders

```xml
<override>
  <Order
    identifier="myOrder1" />
  <Order
    identifier="myOrder2" />
</override>
```

