# Projectile


## Attributes

| Attribute                    | Type  | Default value | Description                                                                                                                                                                                                                                         |
|------------------------------|-------|---------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| LaunchImpulse                | float | 10            | The impulse applied to the physics body of the item when it's launched. Higher values make the projectile faster.                                                                                                                                   |
| ImpulseSpread                | float | 0             | The random percentage modifier used to add variance to the launch impulse.                                                                                                                                                                          |
| LaunchRotation               | float | 0             | The rotation of the item relative to the rotation of the weapon when launched (in degrees).                                                                                                                                                         |
| DoesStick                    | bool  | false         | When set to true, the item can stick to any target it hits.                                                                                                                                                                                         |
| StickToCharacters            | bool  | false         | Can the projectile stick to characters.                                                                                                                                                                                                             |
| StickToStructures            | bool  | false         | Can the projectile stick to walls.                                                                                                                                                                                                                  |
| StickToItems                 | bool  | false         | Can the projectile stick to items.                                                                                                                                                                                                                  |
| StickToDoors                 | bool  | false         | Can the projectile stick to doors. Caution: may cause issues.                                                                                                                                                                                       |
| StickToDeflective            | bool  | false         | Can the item stick even to deflective targets.                                                                                                                                                                                                      |
| StickToLightTargets          | bool  | false         |                                                                                                                                                                                                                                                     |
| GoThroughLightTargets        | bool  | false         |                                                                                                                                                                                                                                                     |
| LightTargetMassThreshold     | float | -1            | Minimum mass of targets to stick to when StickToLightTargets is disabled. Defaults to half of the projectile's mass.                                                                                                                                |
| Hitscan                      | bool  | false         | Hitscan projectiles cast a ray forwards and immediately hit whatever the ray hits. It is recommended to use hitscans for very fast-moving projectiles such as bullets, because using extremely fast launch velocities may cause physics glitches.   |
| HitScanCount                 | int   | 1             | How many hitscans should be done when the projectile is launched. Multiple hitscans can be used to simulate weapons that fire multiple projectiles at the same time without having to actually use multiple projectile items, for example shotguns. |
| MaxTargetsToHit              | int   | 1             | How many targets the projectile can hit before it stops.                                                                                                                                                                                            |
| RemoveOnHit                  | bool  | false         | Should the item be deleted when it hits something.                                                                                                                                                                                                  |
| Spread                       | float | 0             | Random spread applied to the launch angle of the projectile (in degrees).                                                                                                                                                                           |
| StaticSpread                 | bool  | false         | Override random spread with static spread; projectiles are launched with an equal amount of angle between them. Only applies when firing multiple projectiles.                                                                                      |
| FriendlyFire                 | bool  | true          |                                                                                                                                                                                                                                                     |
| DeactivationTime             | float | 0             |                                                                                                                                                                                                                                                     |
| StickDuration                | float | 0             |                                                                                                                                                                                                                                                     |
| MaxJointTranslation          | float | -1            |                                                                                                                                                                                                                                                     |
| JointBreakPoint              | float | 1000          |                                                                                                                                                                                                                                                     |
| Prismatic                    | bool  | true          |                                                                                                                                                                                                                                                     |
| IgnoreProjectilesWhileActive | bool  | false         | Enable only if you want to make the projectile ignore collisions with other projectiles when it's shot. Doesn't have any effect, if the item is not set to be damaged by projectiles.                                                               |
| DamageDoors                  | bool  | false         |                                                                                                                                                                                                                                                     |
| DamageUser                   | bool  | false         | Can the projectile hit the user? Should generally be disabled, unless the projectile is for example something like shrapnel launched by a projectile impact.                                                                                        |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="spear" category="Weapon" maxstacksize="32" maxstacksizecharacterinventory="6" interactthroughwalls="true" cargocontaineridentifier="metalcrate" tags="mediumitem,harpoonammo,handheldammo" Scale="0.5" impactsoundtag="impact_metal_light">
  <Projectile characterusable="false" launchimpulse="5" sticktocharacters="true" sticktoitems="true" sticktostructures="true" sticktodeflective="true" sticktodoors="false" sticktolighttargets="true">
    <Attack structuredamage="15" itemdamage="15" targetforce="20" severlimbsprobability="0.1">
      <Affliction identifier="lacerations" strength="35" />
      <Affliction identifier="bleeding" strength="35" />
      <Affliction identifier="stun" strength="0.5" />
    </Attack>
    <StatusEffect type="OnActive" target="UseTarget,This" checkconditionalalways="true" comparison="And" disabledeltatime="true">
      <Conditional Snapped="false" />
      <Conditional mass="lt 30" />
      <Affliction identifier="drag" strength="1" />
    </StatusEffect>
  </Projectile>
  <Rope sourcepullforce="300" targetpullforce="150" projectilepullforce="5" maxlength="1500" snaponcollision="false" spritewidth="3" tile="true" origin="0.05,0.5" targetminmass="10" lerpforces="true" snapanimduration="0.5" ReelSoundPitchSlide="1.0,2.25">
    <Sprite texture="Content/Map/Thalamus/thalamus.png" sourcerect="617,352,174,32" depth="0.57" origin="0.5,0.5" />
    <SnapSound file="Content/Items/Weapons/HarpoonGun1.ogg" range="500" frequencymultiplier="3.0,4.0" />
    <ReelSound file="Content/Items/Weapons/WEAPON_harpoonGunReelLoop.ogg" range="1000" />
    <!-- Snap after 1 seconds if not stuck to anything -->
    <StatusEffect type="OnUse" target="This" Snapped="true" delay="1" checkconditionalalways="true">
      <Conditional targetitemcomponent="Projectile" IsStuckToTarget="false" />
    </StatusEffect>
  </Rope>
  [...]
</Item>
```

