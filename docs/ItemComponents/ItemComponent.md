# ItemComponent


## Example
```xml
<Item name="ballastfloratoxins" identifier="ballastfloratoxins" Scale="1" tags="" sonarsize="0" hideinmenus="true" health="10" depth="0.1" isdangerous="true">
  <ItemComponent>
    <StatusEffect type="Always" target="This" condition="-1">
      <ParticleEmitter particle="toxins" particlespersecond="1" scalemin="0.2" scalemax="0.3" anglemin="0" anglemax="360" />
      <sound file="Content/Items/Alien/AlienPump.ogg" range="500.0" loop="true" />
    </StatusEffect>
    <StatusEffect type="Always" target="NearbyCharacters" range="200" disabledeltatime="false">
      <Affliction identifier="burn" strength="0.15" />
    </StatusEffect>
    <StatusEffect type="OnBroken" target="This">
      <Remove />
    </StatusEffect>
  </ItemComponent>
  [...]
</Item>
```

