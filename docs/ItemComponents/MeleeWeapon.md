# MeleeWeapon


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

