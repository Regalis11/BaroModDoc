# Deconstructor


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| DeconstructItemsSimultaneously|bool|false| |
| DeconstructionSpeed|float|1.0| |

This component also supports the attributes defined in: [Powered](Powered.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="deconstructor" tags="deconstructor,donttakeitems" category="Machine" linkable="true" allowedlinks="fabricator,medicalfabricator,locker" scale="0.5" damagedbyexplosions="true" explosiondamagemultiplier="0.2">
  <Deconstructor canbeselected="true" powerconsumption="500.0" msg="ItemMsgInteractSelect">
    <GuiFrame relativesize="0.25,0.27" style="ItemUI" anchor="Center" />
    <sound file="Content/Items/Fabricators/Deconstructor.ogg" type="OnActive" range="1000.0" loop="true" />
    <poweronsound file="Content/Items/PowerOnLight3.ogg" range="600" loop="false" />
    <StatusEffect type="InWater" target="This" condition="-0.5" />
  </Deconstructor>
  <ConnectionPanel selectkey="Action" canbeselected="true" hudpriority="10" msg="ItemMsgRewireScrewdriver">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredSkill identifier="electrical" level="55" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand">
      <Sound file="Content/Sounds/Damage/Electrocution1.ogg" range="1000" />
      <Explosion range="100.0" stun="0" force="5.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
      <Affliction identifier="stun" strength="4" />
      <Affliction identifier="burn" strength="5" />
    </StatusEffect>
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <output name="condition_out" displayname="connection.conditionout" />
  </ConnectionPanel>
  <Repairable selectkey="Action" header="mechanicalrepairsheader" deteriorationspeed="0.50" mindeteriorationdelay="60" maxdeteriorationdelay="120" RepairThreshold="80" fixDurationHighSkill="5" fixDurationLowSkill="25" msg="ItemMsgRepairWrench" hudpriority="10">
    <GuiFrame relativesize="0.2,0.16" minsize="400,180" maxsize="480,280" anchor="Center" relativeoffset="0.0,0.27" style="ItemUI" />
    <RequiredSkill identifier="mechanical" level="55" />
    <RequiredItem items="wrench" type="equipped" />
    <ParticleEmitter particle="damagebubbles" particleburstamount="2" particleburstinterval="2.0" particlespersecond="2" scalemin="0.5" scalemax="1.5" anglemin="0" anglemax="359" velocitymin="-10" velocitymax="10" mincondition="0.0" maxcondition="50.0" />
    <ParticleEmitter particle="smoke" particleburstamount="3" particleburstinterval="0.5" particlespersecond="2" scalemin="1" scalemax="2.5" anglemin="0" anglemax="359" velocitymin="-50" velocitymax="50" mincondition="15.0" maxcondition="50.0" />
    <ParticleEmitter particle="heavysmoke" particleburstinterval="0.25" particlespersecond="2" scalemin="2.5" scalemax="5.0" mincondition="0.0" maxcondition="15.0" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand">
      <Sound file="Content/Items/MechanicalRepairFail.ogg" range="1000" />
      <Affliction identifier="lacerations" strength="5" />
      <Affliction identifier="stun" strength="4" />
    </StatusEffect>
  </Repairable>
  <ItemContainer capacity="3" canbeselected="true" hideitems="true" hudpos="0.5, 0.4" slotsperrow="3" uilabel="" allowuioverlap="true" />
  <ItemContainer capacity="5" canbeselected="true" hideitems="true" hudpos="0.5, 0.8" slotsperrow="5" uilabel="" allowuioverlap="true" />
  [...]
</Item>
```

