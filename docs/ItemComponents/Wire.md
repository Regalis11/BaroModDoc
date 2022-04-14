# Wire


## Example
```xml
<Item name="" identifier="dockinghatch" tags="dock" linkable="true" scale="0.5" indestructible="true" requirecursorinsidetrigger="true" requirebodyinsidetrigger="false">
  <Wire />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="toggle" displayname="connection.togglestate" />
    <input name="set_state" displayname="connection.setstate" />
    <output name="power" displayname="connection.power" maxwires="6" maxplayerconnectablewires="5" />
    <output name="state_out" displayname="connection.stateout" fallbackdisplayname="connection.signalout" />
    <output name="proximity_sensor" displayname="connection.dockingproximitysensor" fallbackdisplayname="label.readytodock" />
  </ConnectionPanel>
  [...]
</Item>
```

