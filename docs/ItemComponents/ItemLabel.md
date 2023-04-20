# ItemLabel


## Attributes

| Attribute          | Type    | Default value | Description                                                                                       |
|--------------------|---------|---------------|---------------------------------------------------------------------------------------------------|
| Padding            | Vector4 | "0,0,0,0"     | The amount of padding around the text in pixels (left,top,right,bottom).                          |
| Text               | string  | ""            | The text displayed in the label.                                                                  |
| IgnoreLocalization | bool    | false         | Whether or not to skip localization and always display the raw value.                             |
| TextColor          | Color   | "0,0,0,255"   | The color of the text displayed on the label (R,G,B,A).                                           |
| TextScale          | float   | 1             | The scale of the text displayed on the label.                                                     |
| Scrollable         | bool    | false         | Should the text scroll horizontally across the item if it's too long to be displayed all at once. |
| ScrollSpeed        | float   | 20            | How fast the text scrolls across the item (only valid if Scrollable is set to true).              |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


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

