# EventLogAction

Adds an entry to the "event log" displayed in the mission tab of the tab menu.

## Attributes

| Attribute | Type       | Default value | Description                                                                                       |
|-----------|------------|---------------|---------------------------------------------------------------------------------------------------|
| Id        | Identifier | ""            | Identifier of the entry. If there's already an entry with the same id, it gets overwritten.       |
| Text      | string     | ""            | Text to add to the event log. Can be the text as-is, or a tag referring to a line in a text file. |
| TargetTag | Identifier | ""            | Tag of the character(s) who should see the entry. If empty, the entry is shown to everyone.       |



