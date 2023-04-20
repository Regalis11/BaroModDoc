# Rope


## Attributes

| Attribute           | Type    | Default value     | Description                                                                                                                           |
|---------------------|---------|-------------------|---------------------------------------------------------------------------------------------------------------------------------------|
| SnapAnimDuration    | float   | 1                 |                                                                                                                                       |
| ProjectilePullForce | float   | 0                 | How much force is applied to pull the projectile the rope is attached to.                                                             |
| TargetPullForce     | float   | 0                 | How much force is applied to pull the target the rope is attached to.                                                                 |
| SourcePullForce     | float   | 0                 | How much force is applied to pull the source the rope is attached to.                                                                 |
| MaxLength           | float   | 1000              | How far the source item can be from the projectile until the rope breaks.                                                             |
| MaxAngle            | float   | 360               | The maximum angle from the source to the target until the rope breaks.                                                                |
| SnapOnCollision     | bool    | true              | Should the rope snap when it collides with a structure/submarine (if not, it will just go through it).                                |
| SnapWhenNotAimed    | bool    | true              | Should the rope snap when the character drops the aim?                                                                                |
| TargetMinMass       | float   | 30                | How much mass is required for the target to pull the source towards it. Static and kinematic targets are always treated heavy enough. |
| LerpForces          | bool    | false             |                                                                                                                                       |
| SpriteWidth         | int     | 5                 |                                                                                                                                       |
| SpriteColor         | Color   | "255,255,255,255" |                                                                                                                                       |
| Tile                | bool    | false             |                                                                                                                                       |
| Origin              | Vector2 | "0.5,0.5)"        |                                                                                                                                       |
| BreakFromMiddle     | bool    | true              |                                                                                                                                       |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="spear" category="Weapon" maxstacksize="6" interactthroughwalls="true" cargocontaineridentifier="metalcrate" tags="mediumitem,harpoonammo" Scale="0.5" impactsoundtag="impact_metal_light">
  <Rope sourcepullforce="300" targetpullforce="100" projectilepullforce="5" maxlength="1500" snaponcollision="false" spritewidth="3" tile="true" origin="0.05,0.5" targetminmass="30" lerpforces="true" snapanimduration="0.5">
    <Sprite texture="Content/Map/Thalamus/thalamus.png" sourcerect="617,352,174,32" depth="0.57" origin="0.5,0.5" />
    <!-- Snap after 1 seconds if not stuck to anything -->
    <StatusEffect type="OnUse" target="This" Snapped="true" delay="1" checkconditionalalways="true">
      <Conditional targetitemcomponent="Projectile" IsStuckToTarget="false" />
    </StatusEffect>
  </Rope>
  <Projectile characterusable="false" launchimpulse="5" sticktocharacters="true" sticktoitems="true" sticktostructures="true" sticktodoors="false">
    <Attack structuredamage="15" itemdamage="15" targetforce="20" severlimbsprobability="0.1">
      <Affliction identifier="lacerations" strength="35" />
      <Affliction identifier="bleeding" strength="35" />
      <Affliction identifier="stun" strength="0.5" />
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

