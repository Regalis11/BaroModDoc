# ItemComponent


## Example
```xml
<Item identifier="incendiumgrenade" category="Weapon" maxstacksize="8" cargocontaineridentifier="explosivecrate" tags="smallitem,weapon,explosive,demolitionsexpert" Scale="0.5" impactsoundtag="impact_metal_heavy">
  <ItemComponent characterusable="false">
    <!-- statuseffect that explodes the grenade when used by something else than a character (e.g. a detonator) -->
    <StatusEffect type="OnUse" target="This" Condition="-100.0" disabledeltatime="true" />
    <StatusEffect type="OnBroken" target="This">
      <sound file="Content/Items/Weapons/IncendiumGrenade.ogg" />
      <Explosion range="500" ballastfloradamage="50" itemdamage="200" force="0.1" smoke="false">
        <Affliction identifier="explosiondamage" strength="5" />
        <Affliction identifier="burn" strength="100" />
        <Affliction identifier="stun" strength="8" />
      </Explosion>
      <Remove />
      <Fire size="300.0" />
    </StatusEffect>
  </ItemComponent>
  [...]
</Item>
```

