# PowerDistributor


## Attributes

This component supports the attributes defined in: [PowerTransfer](PowerTransfer.md), [Powered](Powered.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="powerdistributor" tags="junctionbox" category="Electrical,Machine" scale="0.5" linkable="true" allowedlinks="reactor" damagedbyexplosions="true" explosiondamagemultiplier="0.2" GrabWhenSelected="true">
  <PowerDistributor canbeselected="true" vulnerabletoemp="true" canbeoverloaded="true" msg="ItemMsgInteractSelect">
    <GuiFrame relativesize="0.2,0.5" minsize="450,250" anchor="Center" style="ItemUI" />
    <StatusEffect type="InWater" target="This" condition="-0.25">
      <Conditional voltage="gt 0.1" />
    </StatusEffect>
  </PowerDistributor>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.5" minsize="600,700" anchor="Center" style="ConnectionPanel" />
    <RequiredSkill identifier="electrical" level="55" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand" AllowWhenBroken="true">
      <Sound file="Content/Sounds/Damage/Electrocution1.ogg" range="1000" />
      <Explosion range="100.0" force="1.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
      <Affliction identifier="stun" strength="4" probability="0.5" />
      <Affliction identifier="electricshock" strength="60" />
      <Affliction identifier="burn" strength="5" />
      <ParticleEmitter particle="ElectricShock" DistanceMin="10" DistanceMax="25" ParticleAmount="5" ScaleMin="0.1" ScaleMax="0.12" />
    </StatusEffect>
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <input name="set_supply_ratio_0" displayname="connection.setsupplyratiox~[num]=0" />
    <input name="set_supply_ratio_1" displayname="connection.setsupplyratiox~[num]=1" />
    <input name="set_supply_ratio_2" displayname="connection.setsupplyratiox~[num]=2" />
    <input name="set_supply_ratio_3" displayname="connection.setsupplyratiox~[num]=3" />
    <input name="set_supply_ratio_4" displayname="connection.setsupplyratiox~[num]=4" />
    <input name="set_supply_ratio_5" displayname="connection.setsupplyratiox~[num]=5" />
    <input name="set_supply_ratio_6" displayname="connection.setsupplyratiox~[num]=6" />
    <input name="set_supply_ratio_7" displayname="connection.setsupplyratiox~[num]=7" />
    <output name="power_out_0" displayname="connection.poweroutx~[num]=0" fallbackdisplayname="connection.powerout" ispower="true" maxwires="1" />
    <output name="supply_ratio_out_0" displayname="connection.supplyratiooutx~[num]=0" />
    <output name="power_out_1" displayname="connection.poweroutx~[num]=1" fallbackdisplayname="connection.powerout" ispower="true" maxwires="1" />
    <output name="supply_ratio_out_1" displayname="connection.supplyratiooutx~[num]=1" />
    <output name="power_out_2" displayname="connection.poweroutx~[num]=2" fallbackdisplayname="connection.powerout" ispower="true" maxwires="1" />
    <output name="supply_ratio_out_2" displayname="connection.supplyratiooutx~[num]=2" />
    <output name="power_out_3" displayname="connection.poweroutx~[num]=3" fallbackdisplayname="connection.powerout" ispower="true" maxwires="1" />
    <output name="supply_ratio_out_3" displayname="connection.supplyratiooutx~[num]=3" />
    <output name="power_out_4" displayname="connection.poweroutx~[num]=4" fallbackdisplayname="connection.powerout" ispower="true" maxwires="1" />
    <output name="supply_ratio_out_4" displayname="connection.supplyratiooutx~[num]=4" />
    <output name="power_out_5" displayname="connection.poweroutx~[num]=5" fallbackdisplayname="connection.powerout" ispower="true" maxwires="1" />
    <output name="supply_ratio_out_5" displayname="connection.supplyratiooutx~[num]=5" />
    <output name="power_out_6" displayname="connection.poweroutx~[num]=6" fallbackdisplayname="connection.powerout" ispower="true" maxwires="1" />
    <output name="supply_ratio_out_6" displayname="connection.supplyratiooutx~[num]=6" />
    <output name="power_out_7" displayname="connection.poweroutx~[num]=7" fallbackdisplayname="connection.powerout" ispower="true" maxwires="1" />
    <output name="supply_ratio_out_7" displayname="connection.supplyratiooutx~[num]=7" />
    <output name="power_value_out" displayname="connection.powervalueout" />
    <output name="load_value_out" displayname="connection.loadvalueout" />
    <output name="condition_out" displayname="connection.conditionout" />
  </ConnectionPanel>
  [...]
</Item>
```

