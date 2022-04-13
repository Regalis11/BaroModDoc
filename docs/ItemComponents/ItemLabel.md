# ItemLabel


## Example
```xml
<Item name="" identifier="textdisplay" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <ItemLabel scrollable="true" padding="10,5,10,12" textcolor="1,1,1,1">
    <Upgrade gameversion="0.12.0.0" padding="10,5,10,12" />
    <LightComponent range="10.0" lightcolor="1.0,1.0,1.0,0.1" IsOn="true" castshadows="false">
      <sprite texture="Content/Items/Labels/labels.png" sourcerect="0,48,126,48" depth="0.025" origin="0.5,0.5" alpha="1.0" />
    </LightComponent>
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

