# Projectile


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| LaunchImpulse|float|10.0|The impulse applied to the physics body of the item when it's launched. Higher values make the projectile faster. |
| ImpulseSpread|float|0.0|The random percentage modifier used to add variance to the launch impulse. |
| LaunchRotation|float|0.0|The rotation of the item relative to the rotation of the weapon when launched (in degrees). |
| DoesStick|bool|false|When set to true, the item can stick to any target it hits. |
| StickToCharacters|bool|false|Can the item stick to the character it hits. |
| StickToStructures|bool|false|Can the item stick to the structure it hits. |
| StickToItems|bool|false|Can the item stick to the item it hits. |
| StickToDeflective|bool|false|Can the item stick even to deflective targets. |
| StickToLightTargets|bool|false| |
| Hitscan|bool|false|Hitscan projectiles cast a ray forwards and immediately hit whatever the ray hits. It is recommended to use hitscans for very fast-moving projectiles such as bullets, because using extremely fast launch velocities may cause physics glitches. |
| HitScanCount|int|1|How many hitscans should be done when the projectile is launched. Multiple hitscans can be used to simulate weapons that fire multiple projectiles at the same time without having to actually use multiple projectile items, for example shotguns. |
| MaxTargetsToHit|int|1|How many targets the projectile can hit before it stops. |
| RemoveOnHit|bool|false|Should the item be deleted when it hits something. |
| Spread|float|0.0|Random spread applied to the launch angle of the projectile (in degrees). |
| StaticSpread|bool|false|Override random spread with static spread; hitscan are launched with an equal amount of angle between them. Only applies when firing multiple hitscan. |
| FriendlyFire|bool|true| |
| DeactivationTime|float|0| |
| StickDuration|float|0| |
| MaxJointTranslation|float|-1| |
| Prismatic|bool|true| |



## Example
```xml
<Item identifier="spear" category="Weapon" maxstacksize="6" interactthroughwalls="true" cargocontaineridentifier="metalcrate" tags="mediumitem,harpoonammo" Scale="0.5" impactsoundtag="impact_metal_light">
  <Projectile characterusable="false" launchimpulse="20.0" sticktocharacters="true" sticktoitems="false" sticktostructures="false">
    <Attack structuredamage="20" itemdamage="40" targetforce="5" severlimbsprobability="0.1">
      <Affliction identifier="bleeding" strength="30" />
      <Affliction identifier="lacerations" strength="30" />
      <Affliction identifier="stun" strength="0.3" />
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

