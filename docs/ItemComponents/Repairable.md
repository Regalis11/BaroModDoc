# Repairable


## Example
```xml
<Item name="" identifier="alienpump" category="Alien" tags="alien,alienpump">
  <Repairable selectkey="Action" header="mechanicalrepairsheader" fixDurationHighSkill="5" fixDurationLowSkill="20" msg="ItemMsgRepairWrench" hudpriority="10">
    <GuiFrame relativesize="0.2,0.16" minsize="400,180" maxsize="480,280" anchor="Center" relativeoffset="-0.1,0.27" style="ItemUI" />
    <RequiredSkill identifier="mechanical" level="40" />
    <RequiredItem items="wrench" type="Equipped" />
  </Repairable>
  <Pump canbeselected="false" maxflow="6000" PowerConsumption="300.0" MinVoltage="0.3">
    <sound file="Content/Items/Alien/AlienPump.ogg" type="OnUse" range="1500.0" volumeproperty="CurrFlow" volume="0.01" loop="true" />
    <PumpInEmitter particle="watersplash" particlespersecond="80" position="128,-107" anglemin="90" anglemax="90" velocitymin="400" velocitymax="500" />
    <PumpInEmitter particle="bubbles" particlespersecond="10" position="128,-107" anglemin="90" anglemax="90" velocitymin="100" velocitymax="200" />
    <PumpOutEmitter particle="bubbles" particlespersecond="5" position="128,-107" anglemin="0" anglemax="360" velocitymin="0" velocitymax="0" />
    <PumpOutEmitter particle="bubbles" particlespersecond="5" position="128,-107" anglemin="0" anglemax="360" velocitymin="0" velocitymax="0" />
  </Pump>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <input name="toggle" displayname="connection.togglestate" />
    <input name="set_active" displayname="connection.setstate" />
    <input name="set_speed" displayname="connection.setpumpingspeed" />
    <input name="set_targetlevel" displayname="connection.settargetwaterlevel" />
  </ConnectionPanel>
  [...]
</Item>
```

