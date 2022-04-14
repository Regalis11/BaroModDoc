# Projectile


## Example
```xml
<Item name="" identifier="alienspear" category="Weapon,Alien" maxstacksize="6" interactthroughwalls="true" cargocontaineridentifier="metalcrate" tags="mediumitem,harpoonammo" Scale="0.5" impactsoundtag="impact_metal_light">
  <Projectile characterusable="false" launchimpulse="20.0" sticktocharacters="true" sticktoitems="false" sticktostructures="false">
    <Attack structuredamage="30" itemdamage="60" targetforce="10" severlimbsprobability="0.2">
      <Affliction identifier="bleeding" strength="35" />
      <Affliction identifier="lacerations" strength="57.5" />
      <Affliction identifier="stun" strength="1" />
    </Attack>
    <StatusEffect type="OnActive" target="UseTarget,This" checkconditionalalways="true" comparison="And">
      <Conditional Snapped="false" />
      <Conditional mass="lt 30" />
      <Affliction identifier="drag" strength="1" />
    </StatusEffect>
  </Projectile>
  <Rope sourcepullforce="300" targetpullforce="100" projectilepullforce="5" maxlength="1500" snaponcollision="false" spritewidth="3" tile="true" origin="0.05,0.5" targetminmass="30" lerpforces="true" snapanimduration="0.5">
    <Sprite texture="Content/Map/Thalamus/thalamus.png" sourcerect="617,352,174,32" depth="0.57" origin="0.5,0.5" />
    <!-- Snap after 1 seconds if not stuck to anything -->
    <StatusEffect type="OnUse" target="This" Snapped="true" delay="1" checkconditionalalways="true">
      <Conditional targetitemcomponent="Projectile" IsStuckToTarget="false" />
    </StatusEffect>
  </Rope>
  [...]
</Item>
```

