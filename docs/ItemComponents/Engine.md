# Engine


## Attributes

| Attribute              | Type    | Default value | Description                                                                                                                                                                                                                   |
|------------------------|---------|---------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| MaxForce               | float   | 500           | The amount of force exerted on the submarine when the engine is operating at full power.                                                                                                                                      |
| PropellerPos           | Vector2 | "0.0,0.0"     | The position of the propeller as an offset from the item's center (in pixels). Determines where the particles spawn and the position that causes characters to take damage from the engine if the PropellerDamage is defined. |
| DisablePropellerDamage | bool    | false         |                                                                                                                                                                                                                               |

This component also supports the attributes defined in: [Powered](Powered.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="shuttleengine" tags="engine" category="Machine" Scale="0.5" damagedbyexplosions="true" explosiondamagemultiplier="0.2">
  <Engine minvoltage="0.5" powerconsumption="500.0" maxforce="300" canbeselected="true" propellerpos="-200,0" msg="ItemMsgInteractSelect">
    <Upgrade gameversion="0.11.0.9" propellerpos="-200,0" />
    <GuiFrame relativesize="0.2,0.18" minsize="450,200" anchor="Center" style="ItemUI" />
    <sound file="Content/Items/Engine/Engine.ogg" type="OnActive" range="3000.0" volumeproperty="CurrentVolume" loop="true" />
    <sound file="Content/Items/Engine/EngineBroken.ogg" type="Always" range="6000.0" volumeproperty="CurrentBrokenVolume" loop="true" />
    <propellerdamage damagerange="30" targetforce="500" severlimbsprobability="1.0">
      <Affliction identifier="lacerations" strength="5" />
      <Affliction identifier="bleeding" strength="10" />
    </propellerdamage>
  </Engine>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredSkill identifier="electrical" level="55" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand" AllowWhenBroken="true">
      <Sound file="Content/Sounds/Damage/Electrocution1.ogg" range="1000" />
      <Explosion range="100.0" force="1.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
      <Affliction identifier="stun" strength="4" />
      <Affliction identifier="burn" strength="5" />
    </StatusEffect>
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <input name="set_force" displayname="connection.setforce" />
    <output name="condition_out" displayname="connection.conditionout" />
  </ConnectionPanel>
  <Repairable selectkey="Action" header="mechanicalrepairsheader" fixDurationHighSkill="5" fixDurationLowSkill="25" msg="ItemMsgRepairWrench" hudpriority="10">
    <GuiFrame relativesize="0.2,0.16" minsize="400,200" maxsize="480,280" anchor="Center" relativeoffset="0.0,0.27" style="ItemUI" />
    <RequiredSkill identifier="mechanical" level="55" />
    <RequiredItem items="wrench" type="equipped" />
    <ParticleEmitter particle="damagebubbles" particleburstamount="2" particleburstinterval="2.0" particlespersecond="2" scalemin="0.5" scalemax="1.5" anglemin="0" anglemax="359" velocitymin="-10" velocitymax="10" mincondition="0.0" maxcondition="50.0" />
    <ParticleEmitter particle="DarkSmoke" particleburstamount="3" particleburstinterval="0.5" particlespersecond="8" scalemin="1.8" scalemax="2.5" anglemin="0" anglemax="359" velocitymin="-50" velocitymax="50" mincondition="0.0" maxcondition="50.0" />
    <ParticleEmitter particle="heavysmoke" particleburstinterval="0.25" particlespersecond="2" scalemin="2.5" scalemax="5.0" mincondition="0.0" maxcondition="15.0" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand" AllowWhenBroken="true">
      <Sound file="Content/Items/MechanicalRepairFail.ogg" range="1000" />
      <Affliction identifier="lacerations" strength="5" />
      <Affliction identifier="stun" strength="4" />
    </StatusEffect>
  </Repairable>
  [...]
</Item>
```

