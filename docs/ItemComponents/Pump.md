# Pump


## Attributes

| Attribute      | Type  | Default value | Description                                                                                                                                                                            |
|----------------|-------|---------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| FlowPercentage | float | 0             | How fast the item is currently pumping water (-100 = full speed out, 100 = full speed in). Intended to be used by StatusEffect conditionals (setting this value in XML has no effect). |
| MaxFlow        | float | 80            | How fast the item pumps water in/out when operating at 100%.                                                                                                                           |
| IsOn           | bool  | true          |                                                                                                                                                                                        |

This component also supports the attributes defined in: [Powered](Powered.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="smallpump" tags="pump" linkable="true" category="Machine" scale="0.5" damagedbyexplosions="true" explosiondamagemultiplier="0.2" allowrotatingineditor="true">
  <Pump canbeselected="true" maxflow="100" PowerConsumption="60.0" MinVoltage="0.3" IsOn="true" msg="ItemMsgInteractSelect">
    <!-- TODO: define max size-->
    <GuiFrame relativesize="0.25,0.2" minsize="420,220" anchor="Center" style="ItemUI" />
    <sound file="Content/Items/Pump/Pump.ogg" type="OnActive" range="500.0" volumeproperty="CurrFlow" volume="0.005" loop="true" />
    <sound file="Content/Items/Pump/PumpBroken.ogg" type="Always" range="500.0" volumeproperty="CurrentBrokenVolume" volume="0.004" loop="true" />
    <PumpInEmitter particle="bubbles" particlespersecond="5" position="7,-51" anglemin="180" anglemax="180" velocitymin="100" velocitymax="200" scalemin="0.5" scalemax="0.6" />
    <PumpInEmitter particle="watersplash" particlespersecond="50" position="7,-51" anglemin="180" anglemax="180" velocitymin="200" velocitymax="400" scalemin="0.5" scalemax="0.6" />
    <PumpOutEmitter particle="bubbles" particlespersecond="5" position="7,-51" anglemin="0" anglemax="360" velocitymin="0" velocitymax="0" scalemin="0.5" scalemax="0.6" />
  </Pump>
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
    <input name="toggle" displayname="connection.togglestate" />
    <input name="set_active" displayname="connection.setstate" />
    <input name="set_speed" displayname="connection.setpumpingspeed" />
    <input name="set_targetlevel" displayname="connection.settargetwaterlevel" />
    <output name="condition_out" displayname="connection.conditionout" />
  </ConnectionPanel>
  <Repairable selectkey="Action" header="mechanicalrepairsheader" deteriorationspeed="0.2" mindeteriorationdelay="60" maxdeteriorationdelay="240" MinDeteriorationCondition="0" RepairThreshold="80" fixDurationHighSkill="5" fixDurationLowSkill="25" msg="ItemMsgRepairWrench" hudpriority="10">
    <GuiFrame relativesize="0.2,0.16" minsize="400,180" maxsize="480,280" anchor="Center" relativeoffset="-0.1,0.27" style="ItemUI" />
    <RequiredSkill identifier="mechanical" level="55" />
    <RequiredItem items="wrench" type="Equipped" />
    <ParticleEmitter particle="damagebubbles" particleburstamount="2" particleburstinterval="2.0" particlespersecond="2" scalemin="0.5" scalemax="1.5" anglemin="0" anglemax="359" velocitymin="-10" velocitymax="10" mincondition="0.0" maxcondition="50.0" />
    <ParticleEmitter particle="smoke" particleburstamount="3" particleburstinterval="0.5" particlespersecond="2" scalemin="1" scalemax="2.5" anglemin="0" anglemax="359" velocitymin="-50" velocitymax="50" mincondition="15.0" maxcondition="50.0" />
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

