# Controller


## Example
```xml
<Item name="" identifier="alienbutton" category="Alien" tags="smallitem,alien,alienbutton" scale="0.5">
  <Controller direction="None" canbepicked="true" msg="ItemMsgPressSelect">
    <sound file="Content/Items/Alien/AlienButton.ogg" type="OnUse" range="500.0" />
  </Controller>
  <ConnectionPanel canbeselected="true">
    <RequiredItem items="screwdriver" type="Equipped" />
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <output name="signal_out" displayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

