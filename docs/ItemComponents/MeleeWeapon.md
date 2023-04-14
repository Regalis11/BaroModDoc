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
<Item identifier="toolbox" category="Equipment" tags="mediumitem,mobilecontainer,tool" cargocontaineridentifier="" showcontentsintooltip="true" Scale="0.5" fireproof="true" impactsoundtag="impact_metal_heavy" RequireAimToUse="True">
  <MeleeWeapon slots="RightHand,LeftHand" controlpose="true" aimpos="45,10" handle1="0,18" holdangle="90" reload="1" range="50" combatpriority="6" msg="ItemMsgPickUpSelect">
    <Attack structuredamage="0" itemdamage="1" targetimpulse="2">
      <Affliction identifier="blunttrauma" strength="2" />
      <Affliction identifier="stun" strength="0.6" />
      <StatusEffect type="OnUse" target="UseTarget">
        <Conditional entitytype="eq Character" />
        <Sound file="Content/Items/Weapons/Smack1.ogg" selectionmode="random" range="500" />
        <Sound file="Content/Items/Weapons/Smack2.ogg" range="500" />
      </StatusEffect>
    </Attack>
  </MeleeWeapon>
  <ItemContainer capacity="12" keepopenwhenequipped="true" movableframe="true">
    <Containable items="smallitem" />
  </ItemContainer>
  [...]
</Item>
```

