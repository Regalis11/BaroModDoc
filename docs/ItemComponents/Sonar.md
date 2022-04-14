# Sonar


## Example
```xml
<Item identifier="sonarmonitor" tags="command,sonarmonitor" linkable="true" allowedlinks="statusmonitor" scale="0.5" category="Machine" damagedbyexplosions="true" explosiondamagemultiplier="0.2">
  <Sonar canbeselected="true" powerconsumption="100" displaybordersize="-0.1" allowuioverlap="true" hudlayer="-1" rightlayout="true">
    <GuiFrame relativesize="0.55,0.59" anchor="Center" style="OuterGlow" color="0,0,0,0.8" relativeoffset="0.1,-0.05" />
    <sound file="Content/Items/Command/SonarPing.ogg" type="OnUse" range="1000.0" />
    <sound file="Content/Items/Command/SonarPing2.ogg" type="OnUse" range="1000.0" />
    <StatusEffect type="OnUse">
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
  </Sonar>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10" allowuioverlap="true">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredSkill identifier="electrical" level="55" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand">
      <Sound file="Content/Sounds/Damage/Electrocution1.ogg" range="1000" />
      <Explosion range="100.0" stun="0" force="5.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
      <Affliction identifier="stun" strength="4" />
      <Affliction identifier="burn" strength="5" />
    </StatusEffect>
    <requireditem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <input name="transducer_in" displayname="connection.sonartransducerin" />
    <output name="condition_out" displayname="connection.conditionout" />
  </ConnectionPanel>
  [...]
</Item>
```

