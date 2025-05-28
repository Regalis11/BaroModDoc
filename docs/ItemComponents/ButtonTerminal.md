# ButtonTerminal


## Attributes

| Attribute       | Type     | Default value | Description                                                                                                                            |
|-----------------|----------|---------------|----------------------------------------------------------------------------------------------------------------------------------------|
| Signals         | string[] | -             | Signals sent when the corresponding buttons are pressed.                                                                               |
| ActivatingItems | string   | ""            | Identifiers or tags of items that, when contained, allow the terminal buttons to be used. Multiple ones should be separated by commas. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="alienterminal_new" nameidentifier="alienterminal" category="Alien" subcategory="devices" Tags="smallitem,logic" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true" health="100">
  <ButtonTerminal activatingitems="smallalienartifact" canbeselected="true" msg="ItemMsgInteractSelect">
    <GuiFrame relativesize="0.25,0.2" style="ItemUI" anchor="Center" />
    <TerminalButton style="alienbuttonblue" Signal="1" />
    <TerminalButton style="alienbuttonred" Signal="1" />
  </ButtonTerminal>
  <ItemContainer capacity="1" maxstacksize="1" canbeselected="true" hideitems="true" slotsperrow="1" uilabel="" allowuioverlap="true">
    <Containable items="smallitem" />
  </ItemContainer>
  <ConnectionPanel selectkey="Action" canbeselected="true" hudpriority="10" locked="True" allowingameediting="False">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem identifier="screwdriver" type="Equipped" />
    <output name="signal_out1" displayname="connection.signaloutx~[num]=1" />
    <output name="signal_out2" displayname="connection.signaloutx~[num]=2">
      <StatusEffect type="OnUse" target="this" Condition="-1000" setvalue="true" />
    </output>
    <output name="state_out" displayname="connection.stateout" />
    <!--Break the item when a shutdown signal is received-->
    <input name="shutdown" displayname="connection.shutdown">
      <StatusEffect type="OnUse" target="This" condition="-100" setvalue="true" />
    </input>
    <StatusEffect type="OnBroken" target="This" setvalue="true" NonInteractable="true">
      <ParticleEmitter particle="damagebubbles" drawontop="true" particleamount="5" scalemin="0.5" scalemax="1" anglemin="90" anglemax="90" velocitymin="50" velocitymax="100" />
      <ParticleEmitter particle="ElectricShock" drawontop="true" distancemin="2" distancemax="5" particleamount="1" anglemin="0" anglemax="360" scalemin="0.2" scalemax="0.5" />
      <Sound file="Content/Sounds/Damage/HitMetal5.ogg" range="2000" />
    </StatusEffect>
    <StatusEffect type="OnBroken" target="Contained" Condition="-1000" setvalue="true" />
  </ConnectionPanel>
  [...]
</Item>
```

