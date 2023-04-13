# ItemComponent


## Attributes

| Attribute                     | Type                           | Default value | Description                                                                                                                                                                                                                            |
|-------------------------------|--------------------------------|---------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| PickingTime                   | float                          | 0             | How long it takes to pick up the item (in seconds).                                                                                                                                                                                    |
| PickingMsg                    | string                         | ""            | What to display on the progress bar when this item is being picked.                                                                                                                                                                    |
| IsActiveConditionalComparison | PropertyConditional.Comparison | And           |                                                                                                                                                                                                                                        |
| CanBePicked                   | bool                           | false         | Can the item be picked up (or interacted with, if the pick action does something else than picking up the item).                                                                                                                       |
| DrawHudWhenEquipped           | bool                           | false         | Should the interface of the item (if it has one) be drawn when the item is equipped.                                                                                                                                                   |
| CanBeSelected                 | bool                           | false         | Can the item be selected by interacting with it.                                                                                                                                                                                       |
| CanBeCombined                 | bool                           | false         | Can the item be combined with other items of the same type.                                                                                                                                                                            |
| RemoveOnCombined              | bool                           | false         | Should the item be removed if combining it with an other item causes the condition of this item to drop to 0.                                                                                                                          |
| CharacterUsable               | bool                           | false         | Can the "Use" action of the item be triggered by characters or just other items/StatusEffects.                                                                                                                                         |
| AllowInGameEditing            | bool                           | true          | Can the properties of the component be edited in-game (only applicable if the component has in-game editable properties).                                                                                                              |
| DeleteOnUse                   | bool                           | false         | Should the item be deleted when it's used.                                                                                                                                                                                             |
| Msg                           | string                         | ""            | A text displayed next to the item when it's highlighted (generally instructs how to interact with the item, e.g. "[Mouse1] Pick up").                                                                                                  |
| CombatPriority                | float                          | 0             | How useful the item is in combat? Used by AI to decide which item it should use as a weapon. For the sake of clarity, use a value between 0 and 100 (not enforced). Note that there's also a generic BotPriority for all item prefabs. |
| ManuallySelectedSound         | int                            | 0             |                                                                                                                                                                                                                                        |




## Example
```xml
<Item identifier="incendiumgrenade" category="Weapon" maxstacksize="8" cargocontaineridentifier="explosivecrate" tags="smallitem,weapon,explosive,demolitionsexpert" Scale="0.5" impactsoundtag="impact_metal_heavy">
  <ItemComponent characterusable="false">
    <!-- statuseffect that explodes the grenade when used by something else than a character (e.g. a detonator) -->
    <StatusEffect type="OnUse" target="This" Condition="-100.0" disabledeltatime="true" />
    <StatusEffect type="OnBroken" target="This">
      <sound file="Content/Items/Weapons/IncendiumGrenade.ogg" range="3000" />
      <Explosion range="500" ballastfloradamage="100" itemdamage="200" force="5" smoke="false">
        <Affliction identifier="burn" strength="75" />
        <Affliction identifier="burn" strength="15" probability="0.2" dividebylimbcount="false" />
        <Affliction identifier="stun" strength="7" />
      </Explosion>
      <Remove />
      <Fire size="300.0" />
    </StatusEffect>
  </ItemComponent>
  [...]
</Item>
```

