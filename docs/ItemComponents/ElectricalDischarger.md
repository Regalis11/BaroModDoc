# ElectricalDischarger


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| Range|float|500.0|How far the discharge can travel from the item. |
| RangeMultiplierInWalls|float|25.0|How much further can the discharge be carried when moving across walls. |
| Duration|float|0.25|The duration of an individual discharge (in seconds). |
| OutdoorsOnly|bool|false|If set to true, the discharge cannot travel inside the submarine nor shock anyone inside. |

This component also supports the attributes defined in: [Powered](Powered.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="dischargecoil" tags="dischargecoil" category="Machine,Weapon" Scale="0.5">
  <ElectricalDischarger duration="0.25" outdoorsonly="true" powerconsumption="10000">
    <Attack targetimpulse="50">
      <Affliction identifier="stun" strength="8" />
      <Affliction identifier="burn" strength="10" />
    </Attack>
    <StatusEffect type="OnUse">
      <sound file="Content/Items/Weapons/WEAPONS_electricalDischarge1.ogg" range="20000" selectionmode="random" />
      <sound file="Content/Items/Weapons/WEAPONS_electricalDischarge2.ogg" range="20000" />
      <ParticleEmitter particle="risingbubbles" anglemin="0" anglemax="360" particleamount="50" velocitymin="50" velocitymax="100" scalemin="1" scalemax="2" />
      <Explosion range="5000.0" camerashake="5" stun="0" force="0.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
    </StatusEffect>
  </ElectricalDischarger>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <input name="trigger_in" displayname="connection.turrettriggerin" />
  </ConnectionPanel>
  [...]
</Item>
```

