# Repairable


## Attributes

| Attribute                        | Type   | Default value | Description                                                                                                                                                                                                                                                          |
|----------------------------------|--------|---------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| DeteriorationSpeed               | float  | 0             | How fast the condition of the item deteriorates per second.                                                                                                                                                                                                          |
| MinDeteriorationDelay            | float  | 0             | Minimum initial delay before the item starts to deteriorate.                                                                                                                                                                                                         |
| MaxDeteriorationDelay            | float  | 0             | Maximum initial delay before the item starts to deteriorate.                                                                                                                                                                                                         |
| MinDeteriorationCondition        | float  | 50            | The item won't deteriorate spontaneously if the condition is below this value. For example, if set to 10, the condition will spontaneously drop to 10 and then stop dropping (unless the item is damaged further by external factors). Percentages of max condition. |
| MinSabotageCondition             | float  | 0             | How low a traitor must get the item's condition for it to start breaking down.                                                                                                                                                                                       |
| SabotageDeteriorationDuration    | float  | 60            | How long will the item spontaneously deteriorate after being sabotaged.                                                                                                                                                                                              |
| RepairThreshold                  | float  | 80            | The condition of the item has to be below this for it to become repairable. Percentages of max condition.                                                                                                                                                            |
| MaxStressDeteriorationMultiplier | float  | 1             | How much faster the device can deteriorate when under stress (e.g. when operating at full speed/power).                                                                                                                                                              |
| StressDeteriorationThreshold     | float  | 0.5           | At what speed/power must the device be operating at to be considered "under stress".                                                                                                                                                                                 |
| StressDeteriorationIncreaseSpeed | float  | 0.1           | How fast the deterioration speed increases when under stress.                                                                                                                                                                                                        |
| StressDeteriorationDecreaseSpeed | float  | 0.1           | How fast the deterioration speed decreases when not under stress.                                                                                                                                                                                                    |
| FixDurationLowSkill              | float  | 100           | The amount of time it takes to fix the item with insufficient skill levels.                                                                                                                                                                                          |
| FixDurationHighSkill             | float  | 10            | The amount of time it takes to fix the item with sufficient skill levels.                                                                                                                                                                                            |
| SkillRequirementMultiplier       | float  | 1             |                                                                                                                                                                                                                                                                      |
| Description                      | string | ""            | An optional description of the needed repairs displayed in the repair interface.                                                                                                                                                                                     |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="op_researchterminal" tags="geneticresearchstation" width="494" height="297" texturescale="1.0,1.0" scale="0.5" category="Machine">
  <Repairable selectkey="Action" header="electricalrepairsheader" deteriorationspeed="0.0" canbeselected="true" RepairThreshold="80" fixDurationHighSkill="5" fixDurationLowSkill="25" msg="ItemMsgRepairScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.16" minsize="400,180" maxsize="480,280" anchor="Center" relativeoffset="0.0,0.27" style="ItemUI" />
    <RequiredSkill identifier="electrical" level="80" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <ParticleEmitter particle="spark" particleamount="5" emitinterval="2" particlespersecond="5" anglemax="360" distancemax="30" velocitymin="100" velocitymax="500" scalemin="0.5" scalemax="1" mincondition="0.0" maxcondition="15.0" />
  </Repairable>
  <Deconstructor canbeselected="true" showoutput="false" powerconsumption="500.0" deconstructitemssimultaneously="true" msg="ItemMsgInteractSelect" activatebuttontext="researchstation.invalidinput" infotext="researchstation.empty.infotext" infoareawidth="0.7">
    <GuiFrame relativesize="0.25,0.3" style="ItemUI" anchor="Center" />
    <sound file="Content/Items/Fabricators/Deconstructor.ogg" type="OnActive" range="1000.0" loop="true" />
    <poweronsound file="Content/Items/PowerOnLight3.ogg" range="600" loop="false" />
    <StatusEffect type="InWater" target="This" condition="-0.5" />
  </Deconstructor>
  <ConnectionPanel selectkey="Action" canbeselected="true" hudpriority="10" msg="ItemMsgRewireScrewdriver">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredSkill identifier="electrical" level="55" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand" AllowWhenBroken="true">
      <Sound file="Content/Sounds/Damage/Electrocution1.ogg" range="1000" />
      <Explosion range="100.0" force="1.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
      <Affliction identifier="stun" strength="4" probability="0.5" />
      <Affliction identifier="electricshock" strength="60" />
      <Affliction identifier="burn" strength="5" />
      <ParticleEmitter particle="ElectricShock" DistanceMin="10" DistanceMax="25" ParticleAmount="5" ScaleMin="0.1" ScaleMax="0.12" />
    </StatusEffect>
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <output name="condition_out" displayname="connection.conditionout" />
  </ConnectionPanel>
  [...]
</Item>
```

