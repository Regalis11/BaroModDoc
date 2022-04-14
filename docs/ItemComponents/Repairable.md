# Repairable


## Example
```xml
<Item identifier="op_researchterminal" tags="geneticresearchstation" width="494" height="297" texturescale="1.0,1.0" scale="0.5" category="Machine" subcategory="Outpost">
  <Repairable selectkey="Action" header="electricalrepairsheader" deteriorationspeed="0.0" canbeselected="true" RepairThreshold="80" fixDurationHighSkill="5" fixDurationLowSkill="25" msg="ItemMsgRepairScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.16" minsize="400,180" maxsize="480,280" anchor="Center" relativeoffset="0.0,0.27" style="ItemUI" />
    <RequiredSkill identifier="electrical" level="80" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <ParticleEmitter particle="spark" particleburstamount="5" particlespersecond="5" anglemin="0" anglemax="359" velocitymin="100" velocitymax="500" particleburstinterval="2" scalemin="0.5" scalemax="1" mincondition="0.0" maxcondition="15.0" />
  </Repairable>
  <Deconstructor canbeselected="true" powerconsumption="500.0" deconstructitemssimultaneously="true" msg="ItemMsgInteractSelect" activatebuttontext="researchstation.invalidinput" infotext="researchstation.empty.infotext" infoareawidth="0.7">
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
  [...]
</Item>
```

