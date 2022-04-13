# OxygenGenerator


## Example
```xml
<Item name="" nameidentifier="oxygenerator" identifier="outpostoxygenerator" tags="oxygengenerator" category="Machine" linkable="true" allowedlinks="vent" damagedbyexplosions="true" scale="0.5" explosiondamagemultiplier="0.2">
  <OxygenGenerator powerconsumption="1000.0" minvoltage="0.5" generatedamount="5000" canbeselected="true" msg="ItemMsgInteractSelect">
    <poweronsound file="Content/Items/PowerOnLight2.ogg" range="1500" loop="false" />
    <sound file="Content/Items/OxygenGenerator/OxygenGenerator.ogg" type="OnActive" range="1000.0" volumeproperty="CurrFlow" volume="0.001" loop="true" />
    <StatusEffect type="OnFire" target="This" Condition="-0.5" />
    <StatusEffect type="OnActive" targettype="Contained" targets="oxygentank" Condition="2.0" />
    <StatusEffect type="OnBroken" targettype="This" disabledeltatime="true">
      <sound file="Content/Items/Weapons/ExplosionMedium3.ogg" range="8000" selectionmode="All" />
      <sound file="Content/Items/Weapons/ExplosionDebris3.ogg" range="8000" />
      <Explosion range="50" stun="0" force="3.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
    </StatusEffect>
  </OxygenGenerator>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <output name="condition_out" displayname="connection.conditionout" />
  </ConnectionPanel>
  <Repairable selectkey="Action" header="mechanicalrepairsheader" deteriorationspeed="0.125" mindeteriorationdelay="120" maxdeteriorationdelay="750" mindeteriorationcondition="0" AIRepairThreshold="50" fixDurationHighSkill="5" fixDurationLowSkill="25" msg="ItemMsgRepairWrench" hudpriority="10">
    <GuiFrame relativesize="0.2,0.16" minsize="400,180" maxsize="480,280" anchor="Center" relativeoffset="0.0,0.27" style="ItemUI" />
    <RequiredSkill identifier="mechanical" level="55" />
    <RequiredItem items="wrench" type="equipped" />
    <ParticleEmitter particle="damagebubbles" particleburstamount="2" particleburstinterval="2.0" particlespersecond="2" scalemin="0.5" scalemax="1.5" anglemin="0" anglemax="359" velocitymin="-10" velocitymax="10" mincondition="0.0" maxcondition="50.0" />
    <ParticleEmitter particle="smoke" particleburstamount="3" particleburstinterval="0.5" particlespersecond="2" scalemin="1" scalemax="2.5" anglemin="0" anglemax="359" velocitymin="-50" velocitymax="50" mincondition="15.0" maxcondition="50.0" />
    <ParticleEmitter particle="heavysmoke" particleburstinterval="0.25" particlespersecond="2" scalemin="2.5" scalemax="5.0" mincondition="0.0" maxcondition="15.0" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand">
      <Sound file="Content/Items/MechanicalRepairFail.ogg" range="1000" />
      <Affliction identifier="lacerations" strength="15" />
    </StatusEffect>
  </Repairable>
  [...]
</Item>
```

