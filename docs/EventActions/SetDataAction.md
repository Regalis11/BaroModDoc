# SetDataAction

Sets a campaign metadata value. The metadata can be any arbitrary data you want to save: for example, whether some event has been completed, the number of times something has been done during the campaign, or at what stage of some multi\-part event chain the crew is at.

## Attributes

| Attribute  | Type          | Default value | Description                                                                                                                         |
|------------|---------------|---------------|-------------------------------------------------------------------------------------------------------------------------------------|
| Operation  | OperationType | Set           | Do you want to set the metadata to a specific value, multiply it, or add to it.                                                     |
| Value      | string        | -             | Depending on the operation, the value you want to set the metadata to, multiply it with, or add to it.                              |
| Identifier | Identifier    | ""            | Identifier of the metadata to set. Can be any arbitrary identifier, e.g. itemscollected, my_custom_event_state, specialnpckilled... |



