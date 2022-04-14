# PowerContainer


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
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand">
      <Sound file="Content/Sounds/Damage/Electrocution1.ogg" range="1000" />
      <Affliction identifier="stun" strength="4" />
      <Affliction identifier="burn" strength="5" />
      <Explosion range="100.0" stun="0" force="5.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
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

