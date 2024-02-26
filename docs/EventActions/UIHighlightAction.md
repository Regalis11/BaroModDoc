# UIHighlightAction

Highlights an UI element of some kind. Generally used in tutorials.

## Attributes

| Attribute         | Type          | Default value | Description                                                                                                                                                                        |
|-------------------|---------------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Id                | ElementId     | None          | An arbitrary identifier that must match the userdata of the UI element. The userdatas of the element are hard-coded, so this option is generally intended for the developers' use. |
| EntityIdentifier  | Identifier    | ""            | If the element's userdata is an entity or an entity prefab, it's identifier must match this value.                                                                                 |
| OrderCategory     | OrderCategory | Emergency     | If the element's userdata is an order category, it must match this.                                                                                                                |
| OrderIdentifier   | Identifier    | ""            | If the element's userdata is an order, it must match this identifier.                                                                                                              |
| OrderOption       | Identifier    | ""            | If the element's userdata is an order with options, it must match this.                                                                                                            |
| OrderTargetTag    | Identifier    | ""            | If the element's userdata is an order, the order must target an entity with this tag.                                                                                              |
| Bounce            | bool          | true          | Should the element bounce up an down in addition to being highlighted.                                                                                                             |
| HighlightMultiple | bool          | false         | Should the action highlight the first matching element it finds, or all of them?                                                                                                   |



