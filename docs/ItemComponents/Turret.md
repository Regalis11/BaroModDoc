# Turret


## Example
```xml
<Item name="" identifier="depthchargetube" tags="depthchargelauncher" category="Machine,Weapon" focusonselected="true" offsetonselected="700" linkable="true" allowedlinks="depthchargeammosource" scale="0.5">
  <Turret canbeselected="false" linkable="true" characterusable="false" barrelpos="42, 149" rotationlimits="90,90" powerconsumption="0.0" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="trigger_in" displayname="connection.turrettriggerin" />
  </ConnectionPanel>
  [...]
</Item>
```

