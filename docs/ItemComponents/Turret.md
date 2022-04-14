# Turret


## Example
```xml
<Item identifier="turrethardpoint" Tags="turret" showinstatusmonitor="false" category="Machine,Weapon" interactthroughwalls="true" Scale="0.5" interactdistance="10" spritecolor="1.0,1.0,1.0,1.0" linkable="true" allowedlinks="turretammosource">
  <Turret canbeselected="false" characterusable="false" linkable="true" barrelpos="128,88">
    <LightComponent LightColor="1.0,0.8,0.8,1.0" Flicker="0.0" range="2500" IsOn="true" drawbehindsubs="true" ignorecontinuoustoggle="true">
      <LightTexture texture="Content/Lights/alphaOne.png" />
    </LightComponent>
  </Turret>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <input name="position_in" displayname="connection.turretaimingin" />
    <input name="trigger_in" displayname="connection.turrettriggerin" />
    <input name="toggle_light" displayname="connection.togglelight" />
    <input name="set_light" displayname="connection.setlight" />
  </ConnectionPanel>
  [...]
</Item>
```

