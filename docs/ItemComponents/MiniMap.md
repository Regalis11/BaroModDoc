# MiniMap


## Attributes

| Attribute                | Type | Default value | Description                                                                                            |
|--------------------------|------|---------------|--------------------------------------------------------------------------------------------------------|
| RequireWaterDetectors    | bool | false         | Does the machine require inputs from water detectors in order to show the water levels inside rooms.   |
| RequireOxygenDetectors   | bool | true          | Does the machine require inputs from oxygen detectors in order to show the oxygen levels inside rooms. |
| ShowHullIntegrity        | bool | true          | Should damaged walls be displayed by the machine.                                                      |
| EnableHullStatus         | bool | true          | Enable hull status mode.                                                                               |
| EnableElectricalView     | bool | true          | Enable electrical view mode.                                                                           |
| EnableItemFinder         | bool | true          | Enable item finder mode.                                                                               |
| IsUsableOutsidePlayerSub | bool | false         | If this item is portable, should it be usable outside the player submarine?                            |

This component also supports the attributes defined in: [Powered](Powered.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="statusmonitor" tags="command,statusmonitor" aliases="MiniMap" category="Machine,Electrical" linkable="true" scale="0.5" allowedlinks="navterminal" damagedbyexplosions="true" explosiondamagemultiplier="0.2" GrabWhenSelected="true">
  <MiniMap MinVoltage="0.5" PowerConsumption="100" canbeselected="true" msg="ItemMsgInteractSelect" allowuioverlap="true">
    <GuiFrame relativesize="0.5,0.5" anchor="Center" style="ItemUI" />
    <AlternativeLayout relativesize="0.2725,0.3" anchor="CenterLeft" pivot="BottomLeft" relativeoffset="0.05,-0.001" />
    <poweronsound file="Content/Items/PowerOnLight3.ogg" range="1000" loop="false" />
  </MiniMap>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredSkill identifier="electrical" level="40" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand" AllowWhenBroken="true">
      <Sound file="Content/Sounds/Damage/Electrocution1.ogg" range="1000" />
      <Explosion range="100.0" force="1.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
      <Affliction identifier="stun" strength="4" probability="0.5" />
      <Affliction identifier="electricshock" strength="60" />
      <Affliction identifier="burn" strength="5" />
      <ParticleEmitter particle="ElectricShock" DistanceMin="10" DistanceMax="25" ParticleAmount="5" ScaleMin="0.1" ScaleMax="0.12" />
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

