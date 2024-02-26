# EventObjectiveAction

Displays an objective in the top\-right corner of the screen, or modifies an existing objective in some way.

## Attributes

| Attribute         | Type              | Default value | Description                                                                                                                                            |
|-------------------|-------------------|---------------|--------------------------------------------------------------------------------------------------------------------------------------------------------|
| Type              | SegmentActionType | Add           | Should the action add a new objective, or do something to an existing objective?                                                                       |
| Identifier        | Identifier        | ""            | Arbitrary identifier given to the objective. Can be used to complete/remove/fail the objective later. Also used to fetch the text from the text files. |
| ObjectiveTag      | Identifier        | ""            | Legacy support. Tag of the text to display as an objective in info box segments.                                                                       |
| CanBeCompleted    | bool              | true          | Legacy support. Is this objective possible to complete if it's used in an info box segment.                                                            |
| ParentObjectiveId | Identifier        | ""            | Identifier of a parent objective. If set, this objective is displayed as a subobjective under the parent objective.                                    |
| AutoPlayVideo     | bool              | false         | Legacy support. Should the video defined by VideoFile play automatically, or wait for the user to play it.                                             |
| TextTag           | Identifier        | ""            | Legacy support. Tag of the main text to display in info box segments.                                                                                  |
| VideoFile         | string            | ""            | Legacy support. Path of a video file to display in info box segments.                                                                                  |
| Width             | int               | 450           | Legacy support. Width of the info box segment.                                                                                                         |
| Height            | int               | 80            | Legacy support. Height of the info box segment.                                                                                                        |
| TargetTag         | Identifier        | ""            | Tag of the character(s) to show the objective to.                                                                                                      |



