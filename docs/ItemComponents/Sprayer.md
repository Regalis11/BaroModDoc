# Sprayer


## Example
```xml
<Item name="" identifier="sprayer" category="Equipment" Tags="smallitem,tool" cargocontaineridentifier="metalcrate" description="" Scale="0.5" impactsoundtag="impact_metal_light">
  <Sprayer barrelpos="34,8" spread="0" unskilledspread="0" drawhudwhenequipped="true" crosshairscale="0.1" spraystrength="6.0" range="300">
    <Crosshair texture="Content/Items/Weapons/Crosshairs.png" sourcerect="0,256,256,256" />
    <CrosshairPointer texture="Content/Items/Weapons/Crosshairs.png" sourcerect="256,256,256,256" />
    <ParticleEmitter particle="spray" velocitymin="500.0" velocitymax="650.0" particlespersecond="100" />
    <RequiredItems items="ethanol, paint" type="Contained" msg="ItemMsgPaintOrCleaningAgentRequired" />
    <sound file="Content/Items/Tools/Sprayer.ogg" type="OnUse" range="500.0" loop="true" />
    <!--When containing paint, reduce its condition by 1.5 when used-->
    <StatusEffect type="OnUse" target="Contained" Condition="-1.5">
      <RequiredItem items="paint" type="Contained" />
    </StatusEffect>
    <!--Reduce ethanol condition slower than paint-->
    <StatusEffect type="OnUse" target="Contained" Condition="-0.75">
      <RequiredItem items="ethanol" type="Contained" />
    </StatusEffect>
    <PaintColors>
      <PaintColor paintitem="ethanol" color="200,200,200,0" />
      <PaintColor paintitem="redpaint" color="128,0,0,180" />
      <PaintColor paintitem="greenpaint" color="0,128,0,180" />
      <PaintColor paintitem="bluepaint" color="0,0,128,180" />
      <PaintColor paintitem="blackpaint" color="0,0,0,180" />
      <PaintColor paintitem="whitepaint" color="128,128,128,180" />
    </PaintColors>
  </Sprayer>
  <ItemContainer capacity="1" maxstacksize="1" hideitems="false" itempos="8,-35" containedspritedepth="0.56" containedstateindicatorstyle="tank">
    <Containable items="ethanol, paint" />
  </ItemContainer>
  [...]
</Item>
```

