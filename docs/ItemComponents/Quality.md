# Quality


## Example
```xml
<Item name="" identifier="crowbar" category="Equipment" Tags="smallitem,tool,simpletool,dooropeningtool,crowbaritem" cargocontaineridentifier="metalcrate" Scale="0.5" impactsoundtag="impact_metal_light" RequireAimToUse="True">
  <Quality>
    <QualityStat stattype="StrikingPowerMultiplier" value="0.1" />
  </Quality>
  <MeleeWeapon slots="RightHand+LeftHand,Any" controlpose="true" aimpos="45,10" handle1="-10,0" handle2="0,5" holdangle="60" reload="1" range="100" combatpriority="20" msg="ItemMsgPickUpSelect">
    <Attack structuredamage="10" itemdamage="5" targetimpulse="10">
      <Affliction identifier="blunttrauma" strength="10" />
      <Affliction identifier="stun" strength="0.5" />
      <Sound file="Content/Items/Weapons/Smack2.ogg" range="800" />
    </Attack>
  </MeleeWeapon>
  [...]
</Item>
```

