# TriggerComponent


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| Force|float|0.0|The maximum amount of force applied to the triggering entitites. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="gravitysphere" category="Alien" linkable="true" scale="0.5">
  <TriggerComponent triggeredby="Human, Item" force="75" radius="1000" distancebasedforce="true">
    <StatusEffect type="OnActive" target="This">
      <ParticleEmitter particle="gravityspherefx" particleamount="1" emitinterval="1.25" />
    </StatusEffect>
  </TriggerComponent>
  <TriggerComponent triggeredby="Human, Item" force="0" radius="500">
    <Attack onlyhumans="true">
      <Affliction identifier="internaldamage" strength="3" />
    </Attack>
    <StatusEffect type="OnActive" target="This">
      <ParticleEmitter particle="gravityspherefx2" particlespersecond="3" />
      <ParticleEmitter particle="skyholderfx2" anglemax="360" distancemin="400" distancemax="500" velocitymin="-500" velocitymax="-900" particlespersecond="200" colormultiplier="255,180,255,255" />
    </StatusEffect>
  </TriggerComponent>
  <TriggerComponent triggeredby="Human, Item" force="0" radius="100">
    <Attack onlyhumans="true">
      <Affliction identifier="internaldamage" strength="8" />
    </Attack>
    <StatusEffect type="OnActive" target="This">
      <ParticleEmitter particle="gravityspherefx2" particlespersecond="5" scalemin="0.7" scalemax="0.7" />
      <ParticleEmitter particle="skyholderfx2" anglemax="360" distancemin="300" distancemax="400" velocitymin="-700" velocitymax="-1200" particlespersecond="100" colormultiplier="255,180,255,255" />
    </StatusEffect>
  </TriggerComponent>
  <ConnectionPanel canbeselected="true" hudpriority="10">
    <RequiredItem items="screwdriver" type="Equipped" />
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <input name="toggle" displayname="connection.togglestate" />
    <input name="set_state" displayname="connection.setstate" />
    <output name="state_out" displayname="connection.stateout" fallbackdisplayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

