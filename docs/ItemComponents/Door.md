# Door


## Example
```xml
<Item identifier="door" tags="door" scale="0.5" health="100" damagedbyrepairtools="true" damagedbymonsters="true" damagedbyexplosions="true" explosiondamagemultiplier="0.1" allowrotatingineditor="false" allowedlinks="structure,item" ondamagedthreshold="10" linkable="true">
  <Door canbeselected="true" canbepicked="true" pickkey="Action" msg="ItemMsgForceOpenCrowbar" PickingTime="10.0" shadowscale="0.7,1">
    <RequiredItem items="crowbar" type="Equipped" />
    <Sprite texture="door.png" sourcerect="158,0,42,416" depth="0.05" origin="0.5,0.0" />
    <WeldedSprite texture="door.png" sourcerect="203,0,65,377" depth="0.0" origin="0.5,0.5" />
    <BrokenSprite texture="door.png" sourcerect="271,0,121,416" depth="0.509" origin="0.5,0.0" scale="true" />
    <sound file="Content/Items/Door/Door1.ogg" type="OnUse" range="500.0" />
    <sound file="Content/Items/Door/Door2.ogg" type="OnUse" range="500.0" />
    <sound file="Content/Items/Door/Door3.ogg" type="OnUse" range="500.0" />
    <sound file="Content/Items/Door/Door4.ogg" type="OnUse" range="500.0" />
    <sound file="Content/Items/Tools/Crowbar.ogg" type="OnPicked" range="4000.0" onlyinsamesub="true" />
    <sound file="Content/Items/Door/Duct1.ogg" type="OnFailure" selectionmode="Random" range="300" />
    <sound file="Content/Items/Door/Duct2.ogg" type="OnFailure" range="300" />
    <sound file="Content/Items/Door/DoorBreak1.ogg" type="OnBroken" selectionmode="Random" range="3000" />
    <sound file="Content/Items/Door/DoorBreak2.ogg" type="OnBroken" range="3000" />
    <StatusEffect type="OnDamaged" target="This">
      <sound file="Content/Items/Door/DoorBreak1.ogg" selectionmode="Random" range="800" />
      <sound file="Content/Items/Door/DoorBreak2.ogg" range="800" />
    </StatusEffect>
  </Door>
  <Repairable selectkey="Action" header="mechanicalrepairsheader" fixDurationHighSkill="10" fixDurationLowSkill="25" msg="ItemMsgRepairWrench" hudpriority="10">
    <GuiFrame relativesize="0.2,0.16" minsize="400,180" maxsize="480,280" anchor="Center" relativeoffset="0.0,0.27" style="ItemUI" />
    <RequiredSkill identifier="mechanical" level="40" />
    <RequiredItem items="wrench" type="equipped" />
  </Repairable>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="toggle" displayname="connection.togglestate" />
    <input name="set_state" displayname="connection.setstate" />
    <output name="state_out" displayname="connection.stateout" fallbackdisplayname="connection.signalout" />
    <output name="condition_out" displayname="connection.conditionout" />
  </ConnectionPanel>
  [...]
</Item>
```

