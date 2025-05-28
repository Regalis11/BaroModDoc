# CustomInterface


## Attributes

| Attribute                    | Type   | Default value | Description                                                                                  |
|------------------------------|--------|---------------|----------------------------------------------------------------------------------------------|
| Labels                       | string | ""            | The texts displayed on the buttons/tickboxes, separated by commas.                           |
| Signals                      | string | ""            | The signals sent when the buttons are pressed or the tickboxes checked, separated by commas. |
| ElementStates                | string | ""            |                                                                                              |
| ShowInsufficientPowerWarning | bool   | false         |                                                                                              |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="op_clownjukebox" width="149" height="258" scale="0.5" category="Decorative" subcategory="clownassets">
  <CustomInterface canbeselected="true" ElementStates="true,1,50">
    <GuiFrame relativesize="0.2,0.15" anchor="CenterLeft" pivot="BottomLeft" relativeoffset="0.006,-0.05" style="ItemUI" />
    <TickBox text="speaker.on" targetitemcomponent="Powered" propertyname="IsActive" ContinuousSignal="false" GetValueInterval="1" />
    <IntegerInput text="speaker.trackselection" propertyname="ManuallySelectedSound" targetitemcomponent="Powered" min="0" max="3">
      <StatusEffect type="OnUse" targettype="This">
        <sound file="Content/Sounds/ClubMusic_transitionScratch.ogg" range="1000.0" />
      </StatusEffect>
    </IntegerInput>
    <IntegerInput text="speaker.volume" propertyname="PowerConsumption" targetitemcomponent="Powered" min="0" max="100" step="10" />
  </CustomInterface>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="toggle" displayname="connection.togglestate" />
    <input name="set_state" displayname="connection.setstate" />
  </ConnectionPanel>
  [...]
</Item>
```

