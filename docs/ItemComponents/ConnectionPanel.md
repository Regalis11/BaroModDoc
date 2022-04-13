# ConnectionPanel


## Example
```xml
<Item name="" identifier="aliengenerator" category="Alien" Tags="alien,aliengenerator" scale="0.3">
  <ConnectionPanel canbeselected="true" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <!-- break the item when a shutdown signal is received -->
    <input name="shutdown" displayname="connection.shutdown">
      <StatusEffect type="OnUse" target="This" condition="-100" setvalue="true" />
    </input>
    <output name="power_out" displayname="connection.powerout" />
  </ConnectionPanel>
  <PowerContainer capacity="10.0" canbeselected="false" maxrechargespeed="1000.0" maxoutput="10000.0" />
  [...]
</Item>
```

