# ButtonTerminal


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| Signals|string[]|new string[0]|Signals sent when the corresponding buttons are pressed. |
| ActivatingItems|string|""|Identifiers or tags of items that, when contained, allow the terminal buttons to be used. Multiple ones should be separated by commas. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="alienterminal" category="Alien" Tags="smallitem,logic" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <ButtonTerminal activatingitems="smallalienartifact" canbeselected="true" msg="ItemMsgInteractSelect">
    <GuiFrame relativesize="0.25,0.2" style="ItemUI" anchor="Center" />
    <TerminalButton style="alienbuttongreen" />
    <TerminalButton style="alienbuttonred" />
  </ButtonTerminal>
  <ItemContainer capacity="1" canbeselected="true" hideitems="true" slotsperrow="1" uilabel="" allowuioverlap="true">
    <Containable items="smallitem" />
  </ItemContainer>
  <ConnectionPanel selectkey="Action" canbeselected="true" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem identifier="screwdriver" type="Equipped" />
    <output name="signal_out1" displayname="connection.signaloutx~[num]=1" />
    <output name="signal_out2" displayname="connection.signaloutx~[num]=2" />
  </ConnectionPanel>
  [...]
</Item>
```

