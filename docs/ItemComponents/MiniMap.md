# MiniMap


## Example
```xml
<Item name="" identifier="statusmonitor" tags="command,statusmonitor" aliases="MiniMap" category="Machine" linkable="true" scale="0.5" allowedlinks="navterminal" damagedbyexplosions="true" explosiondamagemultiplier="0.2">
  <MiniMap MinVoltage="0.5" PowerConsumption="100" canbeselected="true" msg="ItemMsgInteractSelect" allowuioverlap="true">
    <GuiFrame relativesize="0.5,0.5" anchor="Center" style="ItemUI" />
    <AlternativeLayout relativesize="0.2725,0.3" anchor="CenterLeft" pivot="BottomLeft" relativeoffset="0.05,-0.001" />
    <poweronsound file="Content/Items/PowerOnLight3.ogg" range="1000" loop="false" />
  </MiniMap>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <AlternativeLayout anchor="CenterRight" />
    <RequiredSkill identifier="electrical" level="55" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand">
      <Sound file="Content/Sounds/Damage/Electrocution1.ogg" range="1000" />
      <Explosion range="100.0" stun="0" force="5.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
      <Affliction identifier="stun" strength="4" />
      <Affliction identifier="burn" strength="5" />
    </StatusEffect>
    <requireditem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <input name="water_data_in" displayname="connection.waterdatain" />
    <input name="oxygen_data_in" displayname="connection.oxygendatain" />
    <output name="condition_out" displayname="connection.conditionout" />
  </ConnectionPanel>
  [...]
</Item>
```

