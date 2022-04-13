# Steering


## Example
```xml
<Item name="" identifier="navterminal" tags="command,navterminal,primarynavterminal" linkable="true" allowedlinks="statusmonitor" scale="0.5" category="Machine" damagedbyexplosions="true" explosiondamagemultiplier="0.2">
  <Steering minvoltage="0.5" canbeselected="true" powerconsumption="10" linkuitocomponent="Sonar" msg="ItemMsgInteractSelect" allowuioverlap="true" hudlayer="-1">
    <GuiFrame relativesize="0.55,0.59" anchor="Center" style="OuterGlow" color="0,0,0,0.8" relativeoffset="0.1,-0.05" />
    <StatusEffect type="InWater" target="This" condition="-0.5" />
    <poweronsound file="Content/Items/PowerOnLight1.ogg" range="1000" loop="false" />
    <MaintainPosIndicator texture="Content/Items/Command/NavUI.png" sourcerect="50,0,60,61" />
    <MaintainPosOriginIndicator texture="Content/Items/Command/NavUI.png" sourcerect="0,0,50,54" />
    <SteeringIndicator texture="Content/Items/Command/NavUI.png" sourcerect="0,135,362,16" origin="0,0.5" />
    <LightComponent range="10.0" lightcolor="255,255,255,0" powerconsumption="5" IsOn="true" castshadows="false" allowingameediting="false">
      <Upgrade gameversion="0.9.600.0" lightcolor="255,255,255,0" />
      <sprite texture="Content/Items/Command/navigatorLights.png" depth="0.025" sourcerect="0,0,384,304" origin="0.5,0.5" alpha="1.0" />
    </LightComponent>
    <LightComponent range="10.0" lightcolor="255,255,255,0" powerconsumption="5" IsOn="true" castshadows="false" blinkfrequency="1" allowingameediting="false">
      <Upgrade gameversion="0.9.600.0" lightcolor="255,255,255,0" />
      <sprite texture="Content/Items/Command/navigatorLights.png" depth="0.025" sourcerect="400,0,384,304" origin="0.5,0.5" alpha="1.0" />
    </LightComponent>
  </Steering>
  <Sonar canbeselected="true" powerconsumption="100" displaybordersize="-0.1" allowuioverlap="true" hudlayer="-2">
    <GuiFrame relativesize="0.55,0.59" anchor="Center" relativeoffset="0.1,-0.05" />
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
    <icon identifier="" texture="Content/UI/MainIconsAtlas.png" sourcerect="346,416,4,4" origin="0.5,0.5" />
  </Sonar>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.25,0.4" minsize="400,450" maxsize="480,500" anchor="Center" style="ConnectionPanel" />
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
    <input name="velocity_in" displayname="connection.steeringvelocityin" />
    <output name="velocity_x_out" displayname="connection.velocityxout" />
    <output name="velocity_y_out" displayname="connection.velocityyout" />
    <output name="signal_out1" displayname="connection.signaloutx~[num]=1" />
    <output name="signal_out2" displayname="connection.signaloutx~[num]=2" />
    <output name="signal_out3" displayname="connection.signaloutx~[num]=3" />
    <output name="toggle_docking" displayname="connection.toggledocking" />
    <output name="current_velocity_x" displayname="connection.currentvelocityx" />
    <output name="current_velocity_y" displayname="connection.currentvelocityy" />
    <output name="current_position_x" displayname="connection.currentpositionx" />
    <output name="current_position_y" displayname="connection.currentpositiony" />
    <output name="condition_out" displayname="connection.conditionout" />
  </ConnectionPanel>
  [...]
</Item>
```

