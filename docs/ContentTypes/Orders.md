# Orders

<sub>Relevant files: [Shared:Order.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/AI/Order.cs) [Shared:OrdersFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/OrdersFile.cs)</sub>
- **Required by core package:** Yes

## Attributes


**WARNING:** This file likely generated completely incorrectly!

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

