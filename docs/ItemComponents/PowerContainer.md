# PowerContainer


## Attributes

| Attribute                    | Type    | Default value | Description                                                                                                                                                                    |
|------------------------------|---------|---------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| IndicatorPosition            | Vector2 | "0,0"         | The position of the progress bar indicating the charge of the item. In pixels as an offset from the upper left corner of the sprite.                                           |
| IndicatorSize                | Vector2 | "0,0"         | The size of the progress bar indicating the charge of the item (in pixels).                                                                                                    |
| IsHorizontal                 | bool    | false         | Should the progress bar indicating the charge of the item fill up horizontally or vertically.                                                                                  |
| MaxOutPut                    | float   | 10            | Maximum output of the device when fully charged (kW).                                                                                                                          |
| Capacity                     | float   | 10            | The maximum capacity of the device (kW * min). For example, a value of 1000 means the device can output 100 kilowatts of power for 10 minutes, or 1000 kilowatts for 1 minute. |
| Charge                       | float   | 0             | The current charge of the device.                                                                                                                                              |
| MaxRechargeSpeed             | float   | 10            | How fast the device can be recharged. For example, a recharge speed of 100 kW and a capacity of 1000 kW*min would mean it takes 10 minutes to fully charge the device.         |
| RechargeSpeed                | float   | 0             | The current recharge speed of the device.                                                                                                                                      |
| ExponentialRechargeSpeed     | bool    | false         | If true, the recharge speed (and power consumption) of the device goes up exponentially as the recharge rate is increased.                                                     |
| Efficiency                   | float   | 0.95          | The amount of power you can get out of a item relative to the amount of power that's put into it.                                                                              |
| FlipIndicator                | bool    | false         | Should the progress bar indicating the charge be flipped to fill from the other side.                                                                                          |
| RechargeWarningIndicatorLow  | float   | 0             |                                                                                                                                                                                |
| RechargeWarningIndicatorHigh | float   | 0             |                                                                                                                                                                                |

This component also supports the attributes defined in: [Powered](Powered.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="shuttlebattery" tags="battery,batterycellrecharger" category="Electrical,Machine" Scale="0.5" damagedbyexplosions="false">
  <PowerContainer capacity="2000.0" maxrechargespeed="500.0" maxoutput="1000.0" canbeselected="true" indicatorposition="21,12" indicatorsize="54,35" ishorizontal="true" msg="ItemMsgInteractSelect">
    <GuiFrame relativesize="0.25,0.23" minsize="350,200" anchor="Center" style="ItemUI" />
    <!--minsize="350,250" maxsize="420,300"-->
    <StatusEffect type="OnActive" targettype="Contained" targets="loadable" Condition="2.0">
      <!-- the statuseffect targets the contained item (a battery cell that's being charged), but the conditional targets the container (this battery) -->
      <Conditional ChargePercentage="gt 0.01" targetcontainer="true" targetitemcomponent="PowerContainer" />
    </StatusEffect>
  </PowerContainer>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredSkill identifier="electrical" level="55" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand" AllowWhenBroken="true">
      <Sound file="Content/Sounds/Damage/Electrocution1.ogg" range="1000" />
      <Explosion range="100.0" force="1.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
      <Affliction identifier="stun" strength="4" />
      <Affliction identifier="burn" strength="5" />
    </StatusEffect>
    <RequiredItem items="screwdriver" type="Equipped" />
    <output name="power_out" displayname="connection.powerout" />
    <input name="power_in" displayname="connection.powerin" />
    <input name="set_rate" displayname="connection.batterysetrechargespeed" />
    <output name="charge" displayname="connection.batterychargestatusout" />
    <output name="charge_%" displayname="connection.batterychargepercentage" />
    <output name="charge_rate" displayname="connection.batteryrechargespeedout" />
    <output name="condition_out" displayname="connection.conditionout" />
    <output name="load_value_out" displayname="connection.loadvalueout" />
    <output name="power_value_out" displayname="connection.powervalueout" />
  </ConnectionPanel>
  [...]
</Item>
```

