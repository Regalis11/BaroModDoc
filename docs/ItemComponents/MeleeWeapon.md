# MeleeWeapon


## Attributes

| Attribute         | Type    | Default value | Description                                                                                                                                              |
|-------------------|---------|---------------|----------------------------------------------------------------------------------------------------------------------------------------------------------|
| Range             | float   | 0             | An estimation of how close the item has to be to the target for it to hit. Used by AI characters to determine when they're close enough to hit a target. |
| Reload            | float   | 0.5           | How long the user has to wait before they can hit with the weapon again (in seconds).                                                                    |
| AllowHitMultiple  | bool    | false         | Can the weapon hit multiple targets per swing.                                                                                                           |
| HitOnlyCharacters | bool    | false         | Disable to make the weapon ignore all hit effects when it collides with walls, doors, or other items.                                                    |
| Swing             | bool    | true          |                                                                                                                                                          |
| SwingPos          | Vector2 | "2.0, 0.0"    |                                                                                                                                                          |
| SwingForce        | Vector2 | "3.0, -1.0"   |                                                                                                                                                          |

This component also supports the attributes defined in: [Holdable](Holdable.md), [Pickable](Pickable.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="spinelingspikeloot" category="Weapon" tags="smallitem" cargocontaineridentifier="metalcrate" impactsoundtag="impact_metal_light" scale="0.5" allowasextracargo="true" RequireAimToUse="True" nameidentifier="spinelingspike" translationidentifier="spinelingspike">
  <MeleeWeapon slots="Any,RightHand,LeftHand" aimpos="30,5" handle1="-20,0" holdangle="65" reload="1.0" range="50" combatpriority="10" msg="ItemMsgPickUpSelect">
    <Attack targetimpulse="2" severlimbsprobability="0.1" itemdamage="2" structuredamage="2" structuresoundtype="StructureSlash">
      <Affliction identifier="lacerations" strength="5" />
      <Affliction identifier="bleeding" strength="10" />
      <Affliction identifier="stun" strength="0.2" />
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
  <LightComponent range="200" castshadows="False" drawbehindsubs="False" ison="True" blinkfrequency="0" lightcolor="200,200,255,20" isactive="True" minvoltage="0" powerconsumption="0" vulnerabletoemp="False" pickingtime="0" canbepicked="False" allowingameediting="False" msg="">
    <Sprite texture="Content/Characters/Spineling/Spineling.png" sourcerect="0,230,180,23" origin="0.5,0.6" alpha="0.5" />
  </LightComponent>
  [...]
</Item>
```

