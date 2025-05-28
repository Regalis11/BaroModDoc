# Steering


## Attributes

| Attribute              | Type  | Default value | Description                                                                                                                                                                                               |
|------------------------|-------|---------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| AutoPilot              | bool  | false         | Is autopilot currently on or not?                                                                                                                                                                         |
| NeutralBallastLevel    | float | 0.5           | How full the ballast tanks should be when the submarine is not being steered upwards/downwards. Can be used to compensate if the ballast tanks are too large/small relative to the size of the submarine. |
| DockingAssistThreshold | float | 1000          | How close the docking port has to be to another docking port for the docking mode to become active.                                                                                                       |
| LevelStartSelected     | bool  | false         |                                                                                                                                                                                                           |
| LevelEndSelected       | bool  | false         |                                                                                                                                                                                                           |
| MaintainPos            | bool  | false         |                                                                                                                                                                                                           |

This component also supports the attributes defined in: [Powered](Powered.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="shuttlenavterminal" tags="command,navterminal,secondarynavterminal" linkable="true" allowedlinks="statusmonitor" category="Machine,Electrical" scale="0.5" damagedbyexplosions="true" explosiondamagemultiplier="0.2" GrabWhenSelected="true">
  <Steering minvoltage="0.5" canbeselected="true" powerconsumption="10" linkuitocomponent="Sonar" msg="ItemMsgInteractSelect" allowuioverlap="true" hudlayer="-1">
    <GuiFrame relativesize="0.55,0.59" anchor="Center" style="OuterGlow" color="0,0,0,0.8" relativeoffset="0.1,-0.05" draggable="false" />
    <StatusEffect type="InWater" target="This" condition="-0.5" />
    <MaintainPosIndicator texture="Content/Items/Command/NavUI.png" sourcerect="50,0,60,61" />
    <MaintainPosOriginIndicator texture="Content/Items/Command/NavUI.png" sourcerect="0,0,50,54" />
    <SteeringIndicator texture="Content/Items/Command/NavUI.png" sourcerect="0,135,362,16" origin="0,0.5" />
    <poweronsound file="Content/Items/PowerOnLight2.ogg" range="1000" loop="false" />
  </Steering>
  <Sonar canbeselected="true" powerconsumption="100" displaybordersize="-0.1" allowuioverlap="true" hudlayer="-2">
    <GuiFrame relativesize="0.55,0.59" anchor="Center" relativeoffset="0.1,-0.05" draggable="false" />
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
  </Sonar>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.25,0.4" minsize="400,450" maxsize="480,500" anchor="Center" style="ConnectionPanel" />
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
    <input name="velocity_in" displayname="connection.steeringvelocityin" />
    <output name="velocity_x_out" displayname="connection.velocityxout" />
    <output name="velocity_y_out" displayname="connection.velocityyout" />
    <output name="signal_out1" displayname="connection.signaloutx~[num]=1" />
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

