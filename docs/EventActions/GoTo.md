# GoTo

Makes the event jump to a Label somewhere else in the event. Note that using GoTo resets the effects of other, currently active EventActions \(e.g. closing conversation prompts and stopping the objectives NPCs were forced to do using actions such as CombatAction or NPCWaitAction\).

## Attributes

| Attribute       | Type   | Default value | Description                                                                                                                                                          |
|-----------------|--------|---------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Name            | string | ""            | Name of the label to jump to.                                                                                                                                        |
| MaxTimes        | int    | -1            | How many times can this GoTo action be repeated? Can be used to make some parts of an event repeat a limited number of times. If negative or zero, there's no limit. |
| EndConversation | bool   | true          | By default, jumping to another part in the event closes the active conversation prompt. Use this if if you want to keep it open instead.                             |



