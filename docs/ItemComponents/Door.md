# Door


## Attributes

| Attribute                | Type      | Default value     | Description                                                                                                                                           |
|--------------------------|-----------|-------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------|
| Stuck                    | float     | 0                 | How badly stuck the door is (in percentages). If the percentage reaches 100, the door needs to be cut open to make it usable again.                   |
| OpeningSpeed             | float     | 3                 | How quickly the door opens.                                                                                                                           |
| ClosingSpeed             | float     | 3                 | How quickly the door closes.                                                                                                                          |
| ToggleCoolDown           | float     | 1                 | The door cannot be opened/closed during this time after it has been opened/closed by another character.                                               |
| Window                   | Rectangle | "0.0,0.0,0.0,0.0" | Position and size of the window on the door. The upper left corner is 0,0. Set the width and height to 0 if you don't want the door to have a window. |
| IsOpen                   | bool      | false             | Is the door currently open.                                                                                                                           |
| HasIntegratedButtons     | bool      | false             | If the door has integrated buttons, it can be opened by interacting with it directly (instead of using buttons wired to it).                          |
| Impassable               | bool      | false             | Characters and items cannot pass through impassable doors. Useful for things such as ducts that should only let water and air through.                |
| UseBetweenOutpostModules | bool      | true              |                                                                                                                                                       |
| BotsShouldKeepOpen       | bool      | false             | If true, bots won't try to close this door behind them.                                                                                               |

This component also supports the attributes defined in: [Pickable](Pickable.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="door" tags="door" scale="0.5" health="100" damagedbyrepairtools="true" damagedbymonsters="true" damagedbyexplosions="true" damagedbyprojectiles="true" damagedbymeleeweapons="true" ShowNameInHealthBar="false" explosiondamagemultiplier="0.1" allowrotatingineditor="false" allowedlinks="structure,item" ondamagedthreshold="10" linkable="true">
  <Door canbeselected="true" canbepicked="true" pickkey="Action" msg="ItemMsgForceOpenCrowbar" PickingTime="7.5" shadowscale="0.7,1">
    <Upgrade gameversion="0.22.0.0" PickingTime="7.5" />
    <RequiredItem items="crowbar" type="Equipped" />
    <Sprite texture="door.png" sourcerect="158,0,42,416" depth="0.05" origin="0.5,0.0" />
    <WeldedSprite texture="door.png" sourcerect="203,0,65,377" depth="0.0" origin="0.5,0.5" />
    <BrokenSprite texture="door.png" sourcerect="271,0,121,416" depth="0.509" origin="0.5,0.0" scale="true" />
    <sound file="Content/Items/Door/Door1.ogg" type="OnUse" range="500.0" />
    <sound file="Content/Items/Door/Door2.ogg" type="OnUse" range="500.0" />
    <sound file="Content/Items/Door/Door3.ogg" type="OnUse" range="500.0" />
    <sound file="Content/Items/Door/Door4.ogg" type="OnUse" range="500.0" />
    <sound file="Content/Items/Tools/Crowbar.ogg" type="OnPicked" range="2000.0" onlyinsamesub="true" />
    <sound file="Content/Items/Door/Duct1.ogg" type="OnFailure" selectionmode="Random" range="300" />
    <sound file="Content/Items/Door/Duct2.ogg" type="OnFailure" range="300" />
    <sound file="Content/Items/Door/DoorBreak1.ogg" type="OnBroken" selectionmode="Random" range="2000" />
    <sound file="Content/Items/Door/DoorBreak2.ogg" type="OnBroken" range="2000" />
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

