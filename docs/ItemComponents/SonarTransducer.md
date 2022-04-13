# SonarTransducer


## Example
```xml
<Item name="" identifier="sonartransducer" tags="command,sonartransducer" category="Machine" spritecolor="255,255,255,255" Scale="0.5">
  <SonarTransducer MinVoltage="0.5" PowerConsumption="100" canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <requireditem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <output name="data_out" displayname="connection.sonardataout" fallbackdisplayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

