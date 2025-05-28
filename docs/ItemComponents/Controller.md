# Controller


## Attributes

| Attribute                         | Type           | Default value | Description                                                                                                                                                                                                                 |
|-----------------------------------|----------------|---------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| IsToggle                          | bool           | false         | When enabled, the item will continuously send out a signal and interacting with it will flip the signal (making the item behave like a switch). When disabled, the item will simply send out a signal when interacted with. |
| Output                            | string         | "1"           | The signal sent when the controller is being activated or is toggled on. If empty, no signal is sent.                                                                                                                       |
| FalseOutput                       | string         | "0"           | The signal sent when the controller is toggled off. If empty, no signal is sent. Only valid if IsToggle is true.                                                                                                            |
| State                             | bool           | false         | Whether the item is toggled on/off. Only valid if IsToggle is set to true.                                                                                                                                                  |
| HideHUD                           | bool           | true          | Should the HUD (inventory, health bar, etc) be hidden when this item is selected.                                                                                                                                           |
| UsableIn                          | UseEnvironment | Both          | Can the item be selected in air, underwater or both.                                                                                                                                                                        |
| DrawUserBehind                    | bool           | false         | Should the character using the item be drawn behind the item.                                                                                                                                                               |
| AllowSelectingWhenSelectedByOther | bool           | true          | Can another character select this controller when another character has already selected it?                                                                                                                                |
| AllowSelectingWhenSelectedByBot   | bool           | true          | Can another character select this controller when a bot has already selected it?                                                                                                                                            |
| NonInteractableWhenFlippedX       | bool           | false         |                                                                                                                                                                                                                             |
| NonInteractableWhenFlippedY       | bool           | false         |                                                                                                                                                                                                                             |
| RequirePower                      | bool           | false         | Does the Controller require power to function (= to send signals and move the camera focus to a connected item)?                                                                                                            |
| IsSecondaryItem                   | bool           | false         | If true, other items can be used simultaneously.                                                                                                                                                                            |
| ForceUserToStayAttached           | bool           | false         | If enabled, the user sticks to the position of this item even if the item moves.                                                                                                                                            |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="surveillancecenter" tags="command" scale="0.5" category="Machine,Electrical" type="Controller" GrabWhenSelected="true" disableitemusagewhenselected="true" damagedbyexplosions="true" explosiondamagemultiplier="0.2" isshootable="true" requireaimtouse="false" requireaimtosecondaryuse="false">
  <Controller allowingameediting="false" direction="None" RequirePower="true" canbeselected="true" allowuioverlap="true" AllowSelectingWhenSelectedByBot="true" AllowSelectingWhenSelectedByOther="false" msg="ItemMsgInteractSelect" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10" allowuioverlap="true">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredSkill identifier="electrical" level="40" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand" AllowWhenBroken="true">
      <Sound file="Content/Sounds/Damage/Electrocution1.ogg" range="1000" />
      <Explosion range="100.0" force="1.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
      <Affliction identifier="stun" strength="4" />
      <Affliction identifier="burn" strength="5" />
    </StatusEffect>
    <requireditem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <output name="condition_out" displayname="connection.conditionout" />
    <output name="position_out" displayname="connection.turretaimingout" fallbackdisplayname="inputtype.aim" />
    <output name="trigger_out" displayname="connection.turrettriggerout" fallbackdisplayname="inputtype.shoot" />
    <output name="prev_camera" displayname="connection.prevcamera" signal="-1" />
    <output name="next_camera" displayname="connection.nextcamera" signal="1" />
  </ConnectionPanel>
  <Powered PowerConsumption="100" />
  [...]
</Item>
```

