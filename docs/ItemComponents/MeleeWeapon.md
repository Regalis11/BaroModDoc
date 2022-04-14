# MeleeWeapon


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| Range|float|0.0|An estimation of how close the item has to be to the target for it to hit. Used by AI characters to determine when they're close enough to hit a target. |
| Reload|float|0.5|How long the user has to wait before they can hit with the weapon again (in seconds). |
| AllowHitMultiple|bool|false|Can the weapon hit multiple targets per swing. |
| Swing|bool|true| |
| SwingPos|Vector2|"2.0, 0.0"| |
| SwingForce|Vector2|"3.0, -1.0"| |

This component also supports the attributes defined in: [Holdable](Holdable.md), [Pickable](Pickable.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="crowbarhardened" variantof="crowbar" inventoryiconcolor="110,120,110,255" spritecolor="110,120,110" addedpickingspeedmultiplier="0.4">
  <MeleeWeapon reload="*1.3">
    <Attack targetimpulse="13" penetration="0.25">
      <Affliction identifier="blunttrauma" strength="20" />
      <Affliction identifier="radiationsickness" strength="1" />
      <Affliction identifier="stun" strength="0.6" />
    </Attack>
  </MeleeWeapon>
  [...]
</Item>
```

