# Quality


## Attributes

| Attribute    | Type | Default value | Description |
|--------------|------|---------------|-------------|
| QualityLevel | int  | 0             |             |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="boardingaxe" category="Weapon" Tags="mediumitem,weapon,gunsmith,mountableweapon" scale="0.5" allowasextracargo="true" requireaimtouse="true" impactsoundtag="impact_metal_light">
  <Quality>
    <QualityStat stattype="StrikingPowerMultiplier" value="0.1" />
  </Quality>
  <MeleeWeapon slots="Any,RightHand+LeftHand" aimpos="50,0" handle1="-22,2" handle2="-32,5" holdangle="30" aimangle="10" reload="1.2" range="155" combatPriority="40" msg="ItemMsgPickUpSelect">
    <Attack targetimpulse="5" severlimbsprobability="7.5" itemdamage="30" structuredamage="25">
      <Affliction identifier="lacerations" strength="36" />
      <Affliction identifier="bleeding" strength="10" />
      <Affliction identifier="stun" strength="0.5" />
      <StatusEffect type="OnUse" target="UseTarget">
        <Conditional entitytype="eq Character" />
        <Sound file="Content/Sounds/Damage/LimbSlash1.ogg" selectionmode="random" range="500" />
        <Sound file="Content/Sounds/Damage/LimbSlash2.ogg" range="500" />
        <Sound file="Content/Sounds/Damage/LimbSlash3.ogg" range="500" />
        <Sound file="Content/Sounds/Damage/LimbSlash4.ogg" range="500" />
        <Sound file="Content/Sounds/Damage/LimbSlash5.ogg" range="500" />
        <Sound file="Content/Sounds/Damage/LimbSlash6.ogg" range="500" />
      </StatusEffect>
    </Attack>
  </MeleeWeapon>
  [...]
</Item>
```

