# Powered


## Attributes

| Attribute            | Type  | Default value | Description                                                                                                                                                                                                                                                 |
|----------------------|-------|---------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| MinVoltage           | float | 0.5           | The minimum voltage required for the device to function. The voltage is calculated as power / powerconsumption, meaning that a device with a power consumption of 1000 kW would need at least 500 kW of power to work if the minimum voltage is set to 0.5. |
| PowerConsumption     | float | 0             | How much power the device draws (or attempts to draw) from the electrical grid when active.                                                                                                                                                                 |
| IsActive             | bool  | false         | Is the device currently active. Inactive devices don't consume power.                                                                                                                                                                                       |
| CurrPowerConsumption | float | 0             | The current power consumption of the device. Intended to be used by StatusEffect conditionals (setting the value from XML is not recommended).                                                                                                              |
| Voltage              | float | 0             | The current voltage of the item (calculated as power consumption / available power). Intended to be used by StatusEffect conditionals (setting the value from XML is not recommended).                                                                      |
| VulnerableToEMP      | bool  | true          | Can the item be damaged by electomagnetic pulses.                                                                                                                                                                                                           |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="divingsuitlocker" category="Diving" tags="container,divingsuitcontainer,divingsuitcontainerwindow" pickdistance="50" scale="0.5">
  <Powered powerconsumption="10" currpowerconsumption="10" isactive="true">
    <GuiFrame relativesize="0.18,0.15" minsize="350,160" maxsize="420,192" anchor="Center" relativeoffset="0,-0.1" style="ItemUI" msg="ItemMsgInteractSelect" />
  </Powered>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredSkill identifier="electrical" level="25" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand" AllowWhenBroken="true">
      <Sound file="Content/Sounds/Damage/Electrocution1.ogg" range="1000" />
      <Explosion range="100.0" stun="0" force="1.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
      <Affliction identifier="stun" strength="4" probability="0.5" />
      <Affliction identifier="electricshock" strength="60" />
      <Affliction identifier="burn" strength="5" />
      <ParticleEmitter particle="ElectricShock" DistanceMin="10" DistanceMax="25" ParticleAmount="5" ScaleMin="0.1" ScaleMax="0.12" />
    </StatusEffect>
    <RequiredItem items="screwdriver" type="Equipped" />
    <ParticleEmitter particle="spark" particleamount="5" emitinterval="1.05" anglemax="360" distancemax="20" scalemin="0.5" scalemax="1" mincondition="0.0" maxcondition="15.0" />
    <ParticleEmitter particle="fleshsmoke" particlespersecond="2" scalemin="1" scalemax="2" mincondition="0.0" maxcondition="50.0" />
    <input name="power_in" displayname="connection.powerin" />
  </ConnectionPanel>
  [...]
</Item>
```

