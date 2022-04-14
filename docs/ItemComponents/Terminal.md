# Terminal


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| WelcomeMessage|string|""|Message to be displayed on the terminal display when it is first opened. |
| UseMonospaceFont|bool|false|The terminal will use a monospace font if this box is ticked. |
| TextColor|Color|"50,205,50,255"|Color of the terminal text. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="terminal" category="Electrical" Tags="smallitem,logic" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <Terminal canbeselected="true" msg="ItemMsgInteractSelect" AllowInGameEditing="false">
    <GuiFrame relativesize="0.35,0.35" anchor="Center" style="ItemUI" />
  </Terminal>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem identifier="screwdriver" type="Equipped" />
    <input name="signal_in" displayname="connection.signalin" />
    <output name="signal_out" displayname="connection.signalout" />
    <input name="set_text_color" displayname="connection.settextcolor" />
    <input name="clear_text" displayname="connection.cleartext" />
  </ConnectionPanel>
  [...]
</Item>
```

