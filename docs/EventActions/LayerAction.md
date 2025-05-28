# LayerAction

Enable or disable a specific layer in a specific submarine.

## Attributes

| Attribute          | Type              | Default value | Description                                                                                    |
|--------------------|-------------------|---------------|------------------------------------------------------------------------------------------------|
| Layer              | Identifier        | ""            | Which layer to enable/disable. Use "All" to apply it to all layers.                            |
| Enabled            | bool              | false         | Whether to enable or disable the layer.                                                        |
| SubmarineType      | TagAction.SubType | Any           | The type of submatine to enable or disable the layer in.                                       |
| ContinueIfNotFound | bool              | true          | Should the action continue if it can't find the specified layer in the specified submarine(s). |



