# MessageBoxAction

Displays a message box, or modifies an existing one.

## Attributes

| Attribute               | Type       | Default value | Description                                                                                                                                 |
|-------------------------|------------|---------------|---------------------------------------------------------------------------------------------------------------------------------------------|
| Type                    | ActionType | Create        | What do you want to do with the message box (Create, ConnectObjective, Close, Clear)?                                                       |
| Identifier              | Identifier | ""            | Optional identifier of the tutorial "segment" that can be referenced by other event actions.                                                |
| Tag                     | string     | ""            | An arbitrary tag given to the message box. Only required if you're intending to close or clear the box with another MessageBoxAction later. |
| Header                  | Identifier | ""            | Text displayed in the header of the message box. Can be either the text as-is, or a tag referring to a line in a text file.                 |
| Text                    | Identifier | ""            | Text displayed in the body of the message box. Can be either the text as-is, or a tag referring to a line in a text file.                   |
| IconStyle               | string     | ""            | Style of the icon displayed in the corner of the message box (optional). The style must be defined in a UIStyle file.                       |
| HideCloseButton         | bool       | false         | Should the button that closes the box be hidden? If it is hidden, you must close the box manually using another MessageBoxAction.           |
| TargetTag               | Identifier | ""            | Tag of the character(s) to show the message box to.                                                                                         |
| CloseOnInput            | string     | ""            | The message box is automatically closed on some input (e.g. Select, Use, CrewOrders).                                                       |
| CloseOnSelectTag        | Identifier | ""            | The message box is automatically closed when the user selects an item that has this tag.                                                    |
| CloseOnPickUpTag        | Identifier | ""            | The message box is automatically closed when the user picks up an item that has this tag.                                                   |
| CloseOnEquipTag         | Identifier | ""            | The message box is automatically closed when the user equips an item that has this tag.                                                     |
| CloseOnExitRoomName     | Identifier | ""            | The message box is automatically closed when the user exits a room with this name.                                                          |
| CloseOnInRoomName       | Identifier | ""            | The message box is automatically closed when the user is in a room with this name.                                                          |
| ObjectiveTag            | Identifier | ""            | Optional tag that will be used to get the text for the objective that is displayed on the screen.                                           |
| ObjectiveCanBeCompleted | bool       | true          |                                                                                                                                             |
| ParentObjectiveId       | Identifier | ""            |                                                                                                                                             |



