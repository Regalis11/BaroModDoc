# ItemLabel


## Attributes

This component supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="textdisplay" category="Electrical" scale="0.5" Tags="mediumitem,logic" impactsoundtag="impact_metal_light" cargocontaineridentifier="metalcrate" isshootable="true">
  <ItemLabel scrollable="true" padding="10,5,10,12" textcolor="1,1,1,1">
    <Upgrade gameversion="0.12.0.0" padding="10,5,10,12" />
  </ItemLabel>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <requireditem items="screwdriver" type="Equipped" />
    <input name="set_text" displayname="connection.set_text" fallbackdisplayname="connection.setoutput" />
    <input name="set_color" displayname="connection.setcolor" />
    <input name="set_text_color" displayname="connection.settextcolor" />
  </ConnectionPanel>
  [...]
</Item>
```

