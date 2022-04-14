# Reactor


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| PowerOn|bool|false| |
| LastUserWasPlayer|bool|false| |
| MaxPowerOutput|float|10000.0|How much power (kW) the reactor generates when operating at full capacity. |
| MeltdownDelay|float|120.0|How long the temperature has to stay critical until a meltdown occurs. |
| FireDelay|float|30.0|How long the temperature has to stay critical until the reactor catches fire. |
| Temperature|float|0.0|Current temperature of the reactor (0% - 100%). Indended to be used by StatusEffect conditionals. |
| FissionRate|float|0.0|Current fission rate of the reactor (0% - 100%). Intended to be used by StatusEffect conditionals (setting the value from XML is not recommended). |
| TurbineOutput|float|0.0|Current turbine output of the reactor (0% - 100%). Intended to be used by StatusEffect conditionals (setting the value from XML is not recommended). |
| FuelConsumptionRate|float|0.2|How fast the condition of the contained fuel rods deteriorates per second. |
| TemperatureCritical|bool|false|Is the temperature currently critical. Intended to be used by StatusEffect conditionals (setting the value from XML has no effect). |
| AutoTemp|bool|false|Is the automatic temperature control currently on. Indended to be used by StatusEffect conditionals (setting the value from XML is not recommended). |
| AvailableFuel|float|0.0| |
| Load|float|0.0| |
| TargetFissionRate|float|0.0| |
| TargetTurbineOutput|float|0.0| |
| CorrectTurbineOutput|float|0.0| |
| ExplosionDamagesOtherSubs|bool|true| |

This component also supports the attributes defined in: [Powered](Powered.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item nameidentifier="reactor1" identifier="outpostreactor" tags="reactor" type="Reactor" linkable="true" category="Machine" damagedbyexplosions="true" scale="0.5" explosiondamagemultiplier="0.2">
  <Reactor canbeselected="true" firedelay="20" meltdowndelay="120" maxpoweroutput="20000" fuelconsumptionrate="0.2" vulnerabletoemp="false" msg="ItemMsgInteractSelect">
    <StatusEffect type="InWater" target="This" condition="-0.5">
      <Conditional condition="gt 10" />
    </StatusEffect>
    <GuiFrame relativesize="0.5,0.45" minsize="700,350" maxsize="2688,1166" anchor="Center" relativeoffset="0.1,0" style="ItemUI" />
    <GraphLine texture="Content/Items/Reactor/graphLine.png">
      <Sprite name="ReactorGraphLine" texture="Content/Items/Reactor/graphLine.png" sourcerect="0,0,32,32" />
    </GraphLine>
    <FissionRateMeter>
      <Sprite name="FissionRateMeter" texture="Content/Items/Reactor/reactor.png" sourcerect="603,817,441,240" origin="0.5,1" />
    </FissionRateMeter>
    <TurbineOutputMeter>
      <Sprite name="TurbineOutputMeter" texture="Content/Items/Reactor/reactor.png" sourcerect="603,817,441,240" origin="0.5,1" />
    </TurbineOutputMeter>
    <MeterPointer>
      <Sprite name="MeterPointer" texture="Content/UI/UIAtlasDevices.png" sourcerect="938,846,31,167 " origin="0.5,0.9" />
    </MeterPointer>
    <SectorSprite>
      <Sprite name="SectorSprite" texture="Content/UI/UIAtlasDevices.png" sourcerect="769,326,238,455" origin="0.95,0.5" />
    </SectorSprite>
    <TempMeterFrame>
      <Sprite name="TempMeterFrame" texture="Content/UI/UIAtlasDevices.png" sourcerect="92,517,59,265" origin="0,0" size="0.5,1" />
    </TempMeterFrame>
    <TempMeterBar>
      <Sprite name="TempMeterBar" texture="Content/UI/UIAtlasDevices.png" sourcerect="270,414,106,47" origin="0.5,0" />
    </TempMeterBar>
    <TempRangeIndicator>
      <Sprite name="TempRangeIndicator" texture="Content/UI/UIAtlasDevices.png" sourcerect="31,614,52,25" origin="0.5,0.5" size="0.6,0.6" />
    </TempRangeIndicator>
    <RequiredSkill identifier="electrical" level="50" />
    <RequiredItem items="idcard" type="Picked" msg="ItemMsgUnauthorizedAccess" ignoreineditor="true" />
    <sound file="Content/Items/Reactor/Reactor.ogg" type="OnActive" range="2000.0" volumeproperty="FissionRate" volume="0.02" loop="true" />
    <StatusEffect type="OnBroken" target="This" FissionRate="0.0" disabledeltatime="true">
      <sound file="Content/Items/Weapons/ExplosionLarge2.ogg" range="8000" selectionmode="All" />
      <sound file="Content/Items/Weapons/ExplosionDebris5.ogg" range="8000" />
      <Explosion range="2000" structuredamage="200" force="5.0" camerashake="200" flashrange="10000" flashduration="5.0" screencolor="255,255,255,255" screencolorrange="5000" screencolorduration="3.0">
        <Affliction identifier="burn" strength="200" />
        <Affliction identifier="explosiondamage" strength="80" />
        <Affliction identifier="radiationsickness" strength="80" />
        <Affliction identifier="stun" strength="5" />
      </Explosion>
    </StatusEffect>
    <StatusEffect type="OnBroken" target="Contained" Condition="0.0" setvalue="true" />
  </Reactor>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.3,0.35" minsize="400,350" maxsize="480,460" anchor="Center" style="ConnectionPanel" />
    <RequiredSkill identifier="electrical" level="70" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand">
      <Sound file="Content/Sounds/Damage/Electrocution1.ogg" range="1000" />
      <Explosion range="100.0" stun="0" force="5.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
      <Affliction identifier="stun" strength="5" />
      <Affliction identifier="burn" strength="5" />
    </StatusEffect>
    <RequiredItem items="screwdriver" type="Equipped" />
    <output name="power_out" displayname="connection.powerout" maxwires="1" />
    <output name="temperature_out" displayname="connection.temperatureout" />
    <input name="shutdown" displayname="connection.shutdown" />
    <input name="set_fissionrate" displayname="connection.setfissionrate" />
    <input name="set_turbineoutput" displayname="connection.setturbineoutput" />
    <output name="meltdown_warning" displayname="connection.meltdownwarning">
      <StatusEffect type="OnUse" target="This">
        <ParticleEmitter particle="smoke" particleburstamount="3" particleburstinterval="0.5" particlespersecond="2" scalemin="1" scalemax="2.5" anglemin="0" anglemax="359" velocitymin="-50" velocitymax="50" mincondition="15.0" maxcondition="50.0" />
        <ParticleEmitter particle="swirlysmoke" particlespersecond="3" scalemin="1" scalemax="2" anglemin="0" anglemax="360" velocitymin="0" velocitymax="10" />
        <sound file="Content/Items/Reactor/ReactorOverheatAlarm.ogg" type="OnUse" range="10000.0" loop="true" volume="1.0" />
      </StatusEffect>
    </output>
    <output name="power_value_out" displayname="connection.powervalueout" />
    <output name="load_value_out" displayname="connection.loadvalueout" />
    <output name="fuel_out" displayname="connection.availablefuelout" />
    <output name="condition_out" displayname="connection.conditionout" />
    <output name="fuel_percentage_left" displayname="connection.fuelpercentageout" />
  </ConnectionPanel>
  <ItemContainer capacity="4" maxstacksize="1" canbeselected="true" hudpos="0.5,0.15" slotsperrow="1" uilabel="FuelRods">
    <RequiredItem items="idcard" type="Picked" msg="ItemMsgUnauthorizedAccess" ignoreineditor="true" />
    <Containable items="fuelrod">
      <StatusEffect type="OnContaining" target="This" AvailableFuel="80.0" disabledeltatime="true" />
    </Containable>
    <Containable items="fulguriumfuelrod">
      <StatusEffect type="OnContaining" target="This" AvailableFuel="150.0" disabledeltatime="true" />
    </Containable>
    <Containable items="thoriumfuelrod">
      <StatusEffect type="OnContaining" target="This" AvailableFuel="100.0" disabledeltatime="true" />
    </Containable>
    <Containable items="fulguriumfuelrodvolatile">
      <StatusEffect type="OnContaining" target="This" AvailableFuel="150.0" disabledeltatime="true" />
    </Containable>
  </ItemContainer>
  [...]
</Item>
```

