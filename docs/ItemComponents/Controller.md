# Controller


## Example
```xml
<Item identifier="periscope" tags="periscope" category="Machine,Weapon" type="Controller" disableitemusagewhenselected="true" scale="0.5" isshootable="true" requireaimtouse="false" requireaimtosecondaryuse="false">
  <Controller UserPos="-35.0, -50.0" direction="Right" canbeselected="true" msg="ItemMsgInteractSelect">
    <limbposition limb="Head" position="-10,-135" />
    <limbposition limb="Torso" position="-10,-200" />
    <limbposition limb="LeftHand" position="67,-170" />
    <limbposition limb="RightHand" position="67,-170" />
  </Controller>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <output name="position_out" displayname="connection.turretaimingout" fallbackdisplayname="inputtype.aim" />
    <output name="trigger_out" displayname="connection.turrettriggerout" fallbackdisplayname="inputtype.shoot" />
  </ConnectionPanel>
  [...]
</Item>
```

