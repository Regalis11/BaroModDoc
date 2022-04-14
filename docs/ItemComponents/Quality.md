# Quality


## Example
```xml
<Item name="" identifier="divingknifeunique" category="Weapon" Tags="smallitem,weapon" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" hideinmenus="true" allowasextracargo="true" RequireAimToUse="True">
  <Quality>
    <QualityStat stattype="StrikingPowerMultiplier" value="0.1" />
  </Quality>
  <MeleeWeapon slots="Any,RightHand,LeftHand" aimpos="50,0" handle1="-10,0" holdangle="30" reload="0.75" range="50" combatPriority="25" msg="ItemMsgPickUpSelect">
    <Attack targetimpulse="2" severlimbsprobability="0.1" itemdamage="10">
      <Affliction identifier="lacerations" strength="10" />
      <Affliction identifier="bleeding" strength="10" />
      <Affliction identifier="stun" strength="0.2" />
      <Affliction identifier="morbusinepoisoning" strength="1" />
      <StatusEffect type="OnUse" target="Character">
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

