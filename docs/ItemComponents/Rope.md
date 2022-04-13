# Rope


## Example
```xml
<Item name="" identifier="spearunique" category="Weapon" maxstacksize="6" interactthroughwalls="true" cargocontaineridentifier="metalcrate" tags="mediumitem,harpoonammo" Scale="0.5" impactsoundtag="impact_metal_light" hideinmenus="true" allowasextracargo="true">
  <Rope sourcepullforce="300" targetpullforce="100" projectilepullforce="5" maxlength="1500" snaponcollision="false" spritewidth="3" tile="true" origin="0.05,0.5" targetminmass="30" lerpforces="true" snapanimduration="0.5">
    <Sprite texture="Content/Map/Thalamus/thalamus.png" sourcerect="617,352,174,32" depth="0.57" origin="0.5,0.5" />
    <!-- Snap after 1 seconds if not stuck to anything -->
    <StatusEffect type="OnUse" target="This" Snapped="true" delay="1" checkconditionalalways="true">
      <Conditional targetitemcomponent="Projectile" IsStuckToTarget="false" />
    </StatusEffect>
  </Rope>
  <Projectile characterusable="false" launchimpulse="20.0" sticktocharacters="true" sticktoitems="false" sticktostructures="false" sticktodeflective="true">
    <Attack structuredamage="40" itemdamage="100" targetforce="20" severlimbsprobability="0.2">
      <Affliction identifier="bleeding" strength="50" />
      <Affliction identifier="lacerations" strength="70" />
      <Affliction identifier="stun" strength="3" />
    </Attack>
    <StatusEffect type="OnActive" target="UseTarget,This" checkconditionalalways="true" comparison="And">
      <Conditional Snapped="false" />
      <Conditional mass="lt 30" />
      <Affliction identifier="drag" strength="1" />
    </StatusEffect>
  </Projectile>
  [...]
</Item>
```

