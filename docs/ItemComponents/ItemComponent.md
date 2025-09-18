# ItemComponent


## Attributes

| Attribute                      | Type                                    | Default value | Description                                                                                                                                                                                                                          |
|--------------------------------|-----------------------------------------|---------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| InheritParentIsActive          | bool                                    | true          | If this is a child component of another component, should this component inherit the IsActive state of the parent?                                                                                                                   |
| PickingTime                    | float                                   | 0             | How long it takes to pick up the item (in seconds).                                                                                                                                                                                  |
| PickingMsg                     | string                                  | ""            | What to display on the progress bar when this item is being picked.                                                                                                                                                                  |
| IsActiveConditionalComparison  | PropertyConditional.LogicalOperatorType | And           |                                                                                                                                                                                                                                      |
| CanBePicked                    | bool                                    | false         | Can the item be picked up (or interacted with, if the pick action does something else than picking up the item).                                                                                                                     |
| DrawHudWhenEquipped            | bool                                    | false         | Should the interface of the item (if it has one) be drawn when the item is equipped.                                                                                                                                                 |
| LockGuiFramePosition           | bool                                    | false         |                                                                                                                                                                                                                                      |
| GuiFrameOffset                 | Point                                   | "0,0"         |                                                                                                                                                                                                                                      |
| CanBeSelected                  | bool                                    | false         | Can the item be selected by interacting with it.                                                                                                                                                                                     |
| CanBeCombined                  | bool                                    | false         | Can the item be combined with other items of the same type.                                                                                                                                                                          |
| RemoveOnCombined               | bool                                    | false         | Should the item be removed if combining it with an other item causes the condition of this item to drop to 0.                                                                                                                        |
| CharacterUsable                | bool                                    | false         | Can the "Use" action of the item be triggered by characters or just other items/StatusEffects.                                                                                                                                       |
| AllowInGameEditing             | bool                                    | true          | Can the properties of the component be edited in-game (only applicable if the component has in-game editable properties).                                                                                                            |
| DeleteOnUse                    | bool                                    | false         | Should the item be deleted when it's used.                                                                                                                                                                                           |
| Msg                            | string                                  | ""            | A text displayed next to the item when it's highlighted (generally instructs how to interact with the item, e.g. "[Mouse1] Pick up").                                                                                                |
| CombatPriority                 | float                                   | 0             | How useful the item is in combat? Used by AI to decide which item it should use as a weapon. For the sake of clarity, use a value between 0 and 100 (not forced). Note that there's also a generic BotPriority for all item prefabs. |
| ManuallySelectedSound          | int                                     | 0             |                                                                                                                                                                                                                                      |
| UpdateWhenBroken               | bool                                    | false         | If true, the component will retain its normal functionality when the item reaches 0 condition.                                                                                                                                       |
| AllowUIOverlap                 | bool                                    | false         |                                                                                                                                                                                                                                      |
| CloseByClickingOutsideGUIFrame | bool                                    | true          |                                                                                                                                                                                                                                      |
| LinkUIToComponent              | string                                  | ""            |                                                                                                                                                                                                                                      |
| HudPriority                    | int                                     | 0             |                                                                                                                                                                                                                                      |
| HudLayer                       | int                                     | 0             |                                                                                                                                                                                                                                      |




## Example
```xml
<Item identifier="surveillancecamera" variantof="camera" category="Electrical" Tags="smallitem,camera" focusonselected="true" offsetonselected="500" cargocontaineridentifier="metalcrate" Scale="0.5">
  <ItemComponent>
    <HUDOverlayAnimated texture="Content/UI/CameraOverlay.png" sourcerect="0,0,2048,1536" origin="0.5,0.5" alpha="1.0" animspeed="20" columns="2" rows="2" />
  </ItemComponent>
</Item>
```

