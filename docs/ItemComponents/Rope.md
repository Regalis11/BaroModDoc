# Rope


## Attributes

| Attribute                       | Type    | Default value     | Description                                                                                                                                      |
|---------------------------------|---------|-------------------|--------------------------------------------------------------------------------------------------------------------------------------------------|
| SnapAnimDuration                | float   | 1                 |                                                                                                                                                  |
| ProjectilePullForce             | float   | 0                 | How much force is applied to pull the projectile the rope is attached to.                                                                        |
| TargetPullForce                 | float   | 0                 | How much force is applied to pull the target the rope is attached to.                                                                            |
| SourcePullForce                 | float   | 0                 | How much force is applied to pull the source the rope is attached to.                                                                            |
| MaxLength                       | float   | 1000              | How far the source item can be from the projectile until the rope breaks.                                                                        |
| MinPullDistance                 | float   | 200               | At which distance the user stops pulling the target?                                                                                             |
| MaxAngle                        | float   | 360               | The maximum angle from the source to the target until the rope breaks.                                                                           |
| SnapOnCollision                 | bool    | true              | Should the rope snap when it collides with a structure/submarine (if not, it will just go through it).                                           |
| SnapWhenNotAimed                | bool    | true              | Should the rope snap when the character drops the aim?                                                                                           |
| SnapWhenWeaponFiredAgain        | bool    | true              | Should the rope snap when the weapon it was fired from is fired again? I.e. can there be multiple ropes coming from the weapon at the same time? |
| BarrelLengthMultiplier          | float   | 0.9               | Multiplier for the length of the barrel when determining where the rope should start from.                                                       |
| TargetMinMass                   | float   | 30                | How much mass is required for the target to pull the source towards it. Static and kinematic targets are always treated heavy enough.            |
| LerpForces                      | bool    | false             |                                                                                                                                                  |
| IncreaseForceForEscapingTargets | bool    | true              | Should the force be dynamically adjusted to make it more difficult for targets to escape the pull?                                               |
| SpriteWidth                     | int     | 5                 |                                                                                                                                                  |
| SpriteColor                     | Color   | "255,255,255,255" |                                                                                                                                                  |
| Tile                            | bool    | false             |                                                                                                                                                  |
| Origin                          | Vector2 | "0.5,0.5"         |                                                                                                                                                  |
| BreakFromMiddle                 | bool    | true              |                                                                                                                                                  |
| ReelSoundPitchSlide             | Vector2 | "1.0, 1.0"        | When reeling in, the pitch slides from X to Y, depending on the length of the rope.                                                              |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="spear" category="Weapon" maxstacksize="32" maxstacksizecharacterinventory="6" interactthroughwalls="true" cargocontaineridentifier="metalcrate" tags="mediumitem,harpoonammo,handheldammo" Scale="0.5" impactsoundtag="impact_metal_light">
  <Rope sourcepullforce="300" targetpullforce="150" projectilepullforce="5" maxlength="1500" snaponcollision="false" spritewidth="3" tile="true" origin="0.05,0.5" targetminmass="10" lerpforces="true" snapanimduration="0.5" ReelSoundPitchSlide="1.0,2.25">
    <Sprite texture="Content/Map/Thalamus/thalamus.png" sourcerect="617,352,174,32" depth="0.57" origin="0.5,0.5" />
    <SnapSound file="Content/Items/Weapons/HarpoonGun1.ogg" range="500" frequencymultiplier="3.0,4.0" />
    <ReelSound file="Content/Items/Weapons/WEAPON_harpoonGunReelLoop.ogg" range="1000" />
    <!-- Snap after 1 seconds if not stuck to anything -->
    <StatusEffect type="OnUse" target="This" Snapped="true" delay="1" checkconditionalalways="true">
      <Conditional targetitemcomponent="Projectile" IsStuckToTarget="false" />
    </StatusEffect>
  </Rope>
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
  [...]
</Item>
```

