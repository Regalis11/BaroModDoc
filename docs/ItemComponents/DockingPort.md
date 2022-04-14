# DockingPort


## Example
```xml
<Item identifier="dockingport" tags="dock" linkable="true" indestructible="true" scale="0.5" requirecursorinsidetrigger="true" requirebodyinsidetrigger="false">
  <DockingPort IsHorizontal="true" DistanceTolerance="200,64" DockedDistance="172">
    <StatusEffect type="OnSecondaryUse" target="This">
      <sound file="Content/Items/Door/DockingPort2.ogg" type="OnSecondaryUse" range="15000.0" />
    </StatusEffect>
    <StatusEffect type="OnUse" target="This">
      <Explosion range="5000.0" camerashake="5" stun="0" force="0.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
      <sound file="Content/Items/Door/DockingPort1.ogg" type="OnUse" range="15000.0" />
    </StatusEffect>
    <StatusEffect type="OnBroken" target="This">
      <sound file="Content/Items/Door/DoorBreak2.ogg" range="3000" />
    </StatusEffect>
  </DockingPort>
  <Wire />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem identifier="screwdriver" type="Equipped" />
    <input name="toggle" displayname="connection.togglestate" />
    <input name="set_state" displayname="connection.setstate" />
    <output name="power" displayname="connection.power" maxwires="6" maxplayerconnectablewires="5" />
    <output name="state_out" displayname="connection.stateout" fallbackdisplayname="connection.signalout" />
    <output name="proximity_sensor" displayname="connection.dockingproximitysensor" fallbackdisplayname="label.readytodock" />
  </ConnectionPanel>
  [...]
</Item>
```

