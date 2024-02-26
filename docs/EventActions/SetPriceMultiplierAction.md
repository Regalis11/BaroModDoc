# SetPriceMultiplierAction

Adjusts the price multiplier for stores or mechanical repairs in the current location.

## Attributes

| Attribute        | Type                | Default value | Description                                                                                                                            |
|------------------|---------------------|---------------|----------------------------------------------------------------------------------------------------------------------------------------|
| Multiplier       | float               | 1             | Value to set as the multiplier, or to multiply, min or max the current multiplier with.                                                |
| Operation        | OperationType       | Set           | Do you want to set the value as the multiplier, multiply the existing multiplier with it, or take the smaller or larger of the values. |
| TargetMultiplier | PriceMultiplierType | Store         | Do you want to set the price multiplier for stores or for mechanical services (hull and item repairs and restoring lost shuttles)?     |



