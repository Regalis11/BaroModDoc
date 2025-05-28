# Sonar


## Attributes

| Attribute            | Type  | Default value             | Description                                                                                                                              |
|----------------------|-------|---------------------------|------------------------------------------------------------------------------------------------------------------------------------------|
| Range                | float | Same as DefaultSonarRange | The maximum range of the sonar.                                                                                                          |
| DetectSubmarineWalls | bool  | false                     | Should the sonar display the walls of the submarine it is inside.                                                                        |
| UseTransducers       | bool  | false                     | Does the sonar have to be connected to external transducers to work.                                                                     |
| CenterOnTransducers  | bool  | false                     | Should the sonar view be centered on the transducers or the submarine's center of mass. Only has an effect if UseTransducers is enabled. |
| HasMineralScanner    | bool  | false                     | Does the sonar have mineral scanning mode.                                                                                               |
| UseMineralScanner    | bool  | true                      |                                                                                                                                          |
| RightLayout          | bool  | false                     |                                                                                                                                          |

This component also supports the attributes defined in: [Powered](Powered.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="sonarmonitor" tags="command,sonarmonitor" linkable="true" allowedlinks="statusmonitor" scale="0.5" category="Machine,Electrical" damagedbyexplosions="true" explosiondamagemultiplier="0.2" GrabWhenSelected="true">
  <Sonar canbeselected="true" powerconsumption="100" displaybordersize="-0.1" allowuioverlap="true" hudlayer="-1" rightlayout="true">
    <GuiFrame relativesize="0.6,0.60" anchor="Center" style="OuterGlow" color="0,0,0,0.8" relativeoffset="0.12,-0.05" />
    <sound file="Content/Items/Command/SonarPing.ogg" type="OnUse" range="1000.0" />
    <sound file="Content/Items/Command/SonarPing2.ogg" type="OnUse" range="1000.0" />
    <StatusEffect type="OnUse" target="This">
      <sound file="Content/Items/Command/SonarPingFar.ogg" type="OnUse" range="6000.0" volume="0.8" />
      <sound file="Content/Items/Command/SonarPingFar2.ogg" type="OnUse" range="6000.0" volume="0.8" />
    </StatusEffect>
    <PingCircle texture="Content/Items/Command/pingCircle.png" origin="0.5,0.5" />
    <DirectionalPingCircle texture="Content/Items/Command/directionalPingCircle.png" origin="0.0,0.5" />
    <ScreenOverlay texture="Content/Items/Command/sonarOverlay.png" origin="0.5,0.5" />
    <ScreenBackground texture="Content/Items/Command/sonarBackground.png" origin="0.5,0.5" />
    <DirectionalPingBackground texture="Content/Items/Command/directionalPingBackground.png" origin="0.5,0.5" />
    <DirectionalPingButton index="0" texture="Content/Items/Command/directionalPingButton.png" sourcerect="0,0,91,266" origin="-4.5275,0.5" />
    <DirectionalPingButton index="1" texture="Content/Items/Command/directionalPingButton.png" sourcerect="133,0,91,266" origin="-4.5275,0.5" />
    <DirectionalPingButton index="2" texture="Content/Items/Command/directionalPingButton.png" sourcerect="266,0,91,266" origin="-4.5275,0.5" />
    <Blip texture="Content/Items/Command/sonarBlip.png" origin="0.5,0.5" />
    <LineSprite texture="Content/Items/Command/NavUI.png" sourcerect="181,141,109,4" origin="0,0.5" />
    <icon identifier="outpost" texture="Content/UI/MainIconsAtlas.png" sourcerect="352,398,16,8" origin="0.5,0.5" />
    <icon identifier="submarine" texture="Content/UI/MainIconsAtlas.png" sourcerect="353,407,14,6" origin="0.5,0.5" />
    <icon identifier="shuttle" texture="Content/UI/MainIconsAtlas.png" sourcerect="336,407,8,6" origin="0.5,0.5" />
    <icon identifier="artifact" texture="Content/UI/MainIconsAtlas.png" sourcerect="336,414,8,8" origin="0.5,0.5" />
    <icon identifier="location" texture="Content/UI/MainIconsAtlas.png" sourcerect="349,435,11,11" origin="0.5,0.5" />
    <icon identifier="mineral" texture="Content/UI/MainIconsAtlas.png" sourcerect="336,434,7,12" origin="0.5,0.5" />
    <icon identifier="" texture="Content/UI/MainIconsAtlas.png" sourcerect="346,416,4,4" origin="0.5,0.5" />
    <LightComponent range="10.0" lightcolor="255,255,255,0" powerconsumption="5" IsOn="true" castshadows="false" alphablend="false" allowingameediting="false">
      <sprite texture="Content/Items/Command/navigatorLights.png" depth="0.025" sourcerect="0,0,384,212" origin="0.5,0.5" alpha="1.0" />
    </LightComponent>
    <LightComponent range="10.0" lightcolor="255,255,255,0" powerconsumption="5" IsOn="true" castshadows="false" alphablend="false" blinkfrequency="1" allowingameediting="false">
      <sprite texture="Content/Items/Command/navigatorLights.png" depth="0.025" sourcerect="400,0,384,212" origin="0.5,0.5" alpha="1.0" />
    </LightComponent>
  </Sonar>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10" allowuioverlap="true">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredSkill identifier="electrical" level="40" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand" AllowWhenBroken="true">
      <Sound file="Content/Sounds/Damage/Electrocution1.ogg" range="1000" />
      <Explosion range="100.0" force="1.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
      <Affliction identifier="stun" strength="4" probability="0.5" />
      <Affliction identifier="electricshock" strength="60" />
      <Affliction identifier="burn" strength="5" />
      <ParticleEmitter particle="ElectricShock" DistanceMin="10" DistanceMax="25" ParticleAmount="5" ScaleMin="0.1" ScaleMax="0.12" />
    </StatusEffect>
    <requireditem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <input name="transducer_in" displayname="connection.sonartransducerin" />
    <output name="condition_out" displayname="connection.conditionout" />
  </ConnectionPanel>
  [...]
</Item>
```

