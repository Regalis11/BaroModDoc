# PowerTransfer


## Attributes

| Attribute       | Type  | Default value | Description                                                                                                                                                                                                                  |
|-----------------|-------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| CanBeOverloaded | bool  | true          | Can the item be damaged if too much power is supplied to the power grid.                                                                                                                                                     |
| OverloadVoltage | float | 2             | How much power has to be supplied to the grid relative to the load before item starts taking damage. E.g. a value of 2 means that the grid has to be receiving twice as much power as the devices in the grid are consuming. |
| FireProbability | float | 0.15          | The probability for a fire to start when the item breaks.                                                                                                                                                                    |
| Overload        | bool  | false         | Is the item currently overloaded. Intended to be used by StatusEffect conditionals (setting the value from XML is not recommended).                                                                                          |

This component also supports the attributes defined in: [Powered](Powered.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="dockingport" tags="dock" linkable="true" indestructible="true" scale="0.5" requirecursorinsidetrigger="true" requirebodyinsidetrigger="false">
  <PowerTransfer CanBeOverloaded="false" FireProbability="0.0" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem identifier="screwdriver" type="Equipped" />
    <input name="toggle" displayname="connection.togglestate" />
    <input name="set_state" displayname="connection.setstate" />
    <output name="power" displayname="connection.power" maxwires="6" maxplayerconnectablewires="5" />
    <output name="state_out" displayname="connection.stateout" fallbackdisplayname="connection.signalout" />
    <output name="proximity_sensor" displayname="connection.dockingproximitysensor" fallbackdisplayname="label.readytodock" />
  </ConnectionPanel>
  [...]
</Item>
```

