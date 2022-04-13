# Door


## Example
```xml
<Item name="" identifier="ductblock" tags="ductblock" scale="0.5" showinstatusmonitor="false" damagedbyrepairtools="true" damagedbyexplosions="true" explosiondamagemultiplier="0.1" allowrotatingineditor="false">
  <Door canbeselected="true" horizontal="true" canbepicked="true" pickkey="Action" impassable="true" msg="ItemMsgForceOpenCrowbar" PickingTime="3.0" autoorientgap="true">
    <RequiredItem items="crowbar" type="Equipped" />
    <Sprite texture="duct.png" sourcerect="108,108,64,64" depth="0.05" origin="0.0,0.5" />
    <WeldedSprite texture="duct.png" sourcerect="65,0,65,65" depth="0.0" origin="0.5,0.5" />
    <BrokenSprite texture="duct.png" sourcerect="0,65,107,107" depth="0.509" origin="0.2,0.5" scale="true" />
    <sound file="Content/Items/Door/Duct1.ogg" type="OnUse" selectionmode="Random" range="300" />
    <sound file="Content/Items/Door/Duct2.ogg" type="OnUse" range="300" />
    <sound file="Content/Items/Tools/Crowbar.ogg" type="OnPicked" range="500.0" />
    <sound file="Content/Items/Door/DuctBreak.ogg" type="OnBroken" range="2000" />
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
  </ConnectionPanel>
  [...]
</Item>
```

