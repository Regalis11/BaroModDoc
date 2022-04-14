# CustomInterface


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| Labels|string|""|The texts displayed on the buttons/tickboxes, separated by commas. |
| Signals|string|""|The signals sent when the buttons are pressed or the tickboxes checked, separated by commas. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="shuttlenavterminal" tags="command,navterminal,secondarynavterminal" linkable="true" allowedlinks="statusmonitor" category="Machine" scale="0.5" damagedbyexplosions="true" explosiondamagemultiplier="0.2">
  <CustomInterface canbeselected="true" allowuioverlap="true">
    <GuiFrame relativesize="0.15,0.1" anchor="CenterLeft" pivot="TopLeft" relativeoffset="0.1125,0.001" style="ItemUI" />
    <Button text="Signal out #1" connection="signal_out1" />
  </CustomInterface>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.25,0.4" minsize="400,450" maxsize="480,500" anchor="Center" style="ConnectionPanel" />
    <RequiredSkill identifier="electrical" level="55" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand">
      <Sound file="Content/Items/Weapons/ElectricalDischarger.ogg" range="1000" />
      <Explosion range="100.0" stun="0" force="5.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
      <Affliction identifier="stun" strength="4" />
      <Affliction identifier="burn" strength="5" />
    </StatusEffect>
    <requireditem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <input name="transducer_in" displayname="connection.sonartransducerin" />
    <input name="velocity_in" displayname="connection.steeringvelocityin" />
    <output name="velocity_x_out" displayname="connection.velocityxout" />
    <output name="velocity_y_out" displayname="connection.velocityyout" />
    <output name="signal_out1" displayname="connection.signaloutx~[num]=1" />
    <output name="toggle_docking" displayname="connection.toggledocking" />
    <output name="current_velocity_x" displayname="connection.currentvelocityx" />
    <output name="current_velocity_y" displayname="connection.currentvelocityy" />
    <output name="current_position_x" displayname="connection.currentpositionx" />
    <output name="current_position_y" displayname="connection.currentpositiony" />
    <output name="condition_out" displayname="connection.conditionout" />
  </ConnectionPanel>
  [...]
</Item>
```

