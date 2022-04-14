# LightComponent


## Example
```xml
<Item identifier="lightcomponent" category="Electrical" Tags="smallitem,lightcomponent" maxstacksize="8" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <LightComponent canbeselected="true" color="1.0,0.0,0.0,0.5" castshadows="false">
    <LightTexture texture="Content/Lights/pointlight_bounce.png" origin="0.5,0.5" />
    <sprite texture="Content/Items/Electricity/signalcomp.png" sourcerect="228,3,23,24" origin="0.5,0.5" />
  </LightComponent>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="toggle" displayname="connection.togglestate" />
    <input name="set_state" displayname="connection.setstate" />
    <input name="set_color" displayname="connection.setcolor" />
  </ConnectionPanel>
  [...]
</Item>
```

