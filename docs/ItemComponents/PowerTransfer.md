# PowerTransfer


## Example
```xml
<Item name="" identifier="junctionbox_tutorial" nameidentifier="junctionbox" tags="junctionbox" category="Electrical" description="" scale="0.5" hideinmenus="true">
  <PowerTransfer canbeselected="true" msg="ItemMsgInteractSelect">
    <GuiFrame relativesize="0.2,0.14" minsize="450,160" anchor="Center" style="ItemUI" />
    <StatusEffect type="InWater" target="This" condition="-0.25">
      <Conditional currPowerConsumption="lt -10" />
    </StatusEffect>
  </PowerTransfer>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredSkill identifier="electrical" level="30" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <output name="power" displayname="connection.power" />
  </ConnectionPanel>
  [...]
</Item>
```

