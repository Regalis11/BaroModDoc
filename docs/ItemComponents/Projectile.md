# Projectile


## Example
```xml
<Item identifier="guardianspear" category="hidden" interactthroughwalls="true" tags="mediumitem,harpoonammo" Scale="0.5" impactsoundtag="impact_metal_light" hideinmenus="true" nonplayerteaminteractable="True">
  <Projectile characterusable="false" launchimpulse="25.0" sticktocharacters="true" sticktoitems="false" sticktostructures="false" sticktodeflective="true">
    <Attack structuredamage="20" itemdamage="100" targetforce="10" severlimbsprobability="0">
      <Affliction identifier="bleeding" strength="30" />
      <Affliction identifier="lacerations" strength="30" />
      <Affliction identifier="stun" strength="0.3" />
    </Attack>
    <StatusEffect type="OnActive" target="UseTarget,This" comparison="And">
      <Conditional Snapped="false" />
      <Conditional mass="lt 50" />
      <Affliction identifier="drag" strength="1" />
    </StatusEffect>
  </Projectile>
  <Rope sourcepullforce="0" targetpullforce="300" projectilepullforce="5" maxlength="1000" snaponcollision="true" spritewidth="8" tile="true" origin="0.30,0.5" targetminmass="0" lerpforces="true" SnapWhenNotAimed="False" snapanimduration="0.5">
    <Sprite name="Guardian rope component" texture="Content/Characters/Fractalguardian/Fractalguardian.png" sourcerect="343,622,32,15" depth="0.57" origin="0.5,0.5" />
    <!-- Remove 1 second after snapping -->
    <StatusEffect type="Always" target="This" delay="1" checkconditionalalways="true">
      <Conditional Snapped="true" />
      <Remove />
    </StatusEffect>
    <!-- Snap after 1 seconds if not stuck to anything -->
    <StatusEffect type="OnUse" target="This" Snapped="true" delay="1" checkconditionalalways="true">
      <Conditional targetitemcomponent="Projectile" IsStuckToTarget="false" />
    </StatusEffect>
    <!-- Always snap after 45 seconds -->
    <StatusEffect type="OnUse" target="This" Snapped="true" delay="45" />
  </Rope>
  [...]
</Item>
```

