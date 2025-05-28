# Fabricator


## Attributes

| Attribute                   | Type   | Default value               | Description |
|-----------------------------|--------|-----------------------------|-------------|
| FabricationSpeed            | float  | 1                           |             |
| SkillRequirementMultiplier  | float  | 1                           |             |
| AmountToFabricate           | int    | 1                           |             |
| CreateButtonText            | string | "FabricatorCreate"          |             |
| FabricationLimitReachedText | string | "vendingmachine.outofstock" |             |
| ShowSortByDropdown          | bool   | true                        |             |
| ShowAvailableOnlyTickBox    | bool   | true                        |             |
| ShowCategoryButtons         | bool   | true                        |             |

This component also supports the attributes defined in: [Powered](Powered.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="medicalfabricator" tags="medicalfabricator,donttakeitems,dontsellitems" category="Machine" linkable="true" allowedlinks="deconstructor,medcabinet,locker,toxcontainer" scale="0.5" damagedbyexplosions="true" explosiondamagemultiplier="0.2">
  <Fabricator canbeselected="true" powerconsumption="500.0" msg="ItemMsgInteractSelect">
    <GuiFrame relativesize="0.4,0.6" style="ItemUI" anchor="Center" />
    <sound file="Content/Items/Fabricators/Fabricator.ogg" type="OnActive" range="1000.0" volumeproperty="RelativeVoltage" loop="true" />
    <poweronsound file="Content/Items/PowerOnLight2.ogg" range="600" loop="false" />
    <StatusEffect type="InWater" target="This" condition="-0.5" />
  </Fabricator>
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
  <Repairable selectkey="Action" header="mechanicalrepairsheader" deteriorationspeed="0.50" mindeteriorationdelay="60" maxdeteriorationdelay="120" RepairThreshold="80" fixDurationHighSkill="5" fixDurationLowSkill="25" msg="ItemMsgRepairWrench" hudpriority="10">
    <GuiFrame relativesize="0.2,0.16" minsize="400,180" maxsize="480,280" anchor="Center" relativeoffset="0.0,0.27" style="ItemUI" />
    <RequiredSkill identifier="mechanical" level="55" />
    <RequiredItem items="wrench" type="equipped" />
    <ParticleEmitter particle="damagebubbles" particlespersecond="2" scalemin="0.5" scalemax="1.5" anglemax="360" velocitymin="-10" velocitymax="10" mincondition="0.0" maxcondition="50.0" />
    <ParticleEmitter particle="smoke" particlespersecond="2" scalemin="1" scalemax="2.5" anglemax="360" velocitymin="-50" velocitymax="50" mincondition="15.0" maxcondition="50.0" />
    <ParticleEmitter particle="heavysmoke" particlespersecond="2" scalemin="1.0" scalemax="2.5" anglemax="360" distancemax="60" mincondition="0.0" maxcondition="15.0" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand" AllowWhenBroken="true">
      <Sound file="Content/Items/MechanicalRepairFail.ogg" range="1000" />
      <Affliction identifier="lacerations" strength="5" />
      <Affliction identifier="stun" strength="4" />
    </StatusEffect>
  </Repairable>
  <ItemContainer capacity="5" canbeselected="true" hideitems="true" slotsperrow="5" uilabel="" allowuioverlap="true" />
  <ItemContainer capacity="1" canbeselected="true" hideitems="true" slotsperrow="1" uilabel="" allowuioverlap="true" />
  [...]
</Item>
```

