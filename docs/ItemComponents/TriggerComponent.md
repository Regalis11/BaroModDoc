# TriggerComponent


## Attributes

| Attribute                         | Type    | Default value | Description                                                                                                                                                                                      |
|-----------------------------------|---------|---------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Force                             | float   | 0             | The maximum amount of force applied to the triggering entitites.                                                                                                                                 |
| DirectionalForce                  | Vector2 | "0,0"         | The maximum amount of directional force applied to the triggering entitites.                                                                                                                     |
| RelativeDirectionalForce          | bool    | false         | If true, DirectionalForce is relative to the angle between the target and the item, Similar to Force.<br/>If false, it always pushes in the same direction, with respect to the item's rotation. |
| VerticalForce                     | bool    | true          | If false, no vertical force will be applied.                                                                                                                                                     |
| HorizontalForce                   | bool    | true          | If false, no horizontal force will be applied.                                                                                                                                                   |
| DistanceBasedForce                | bool    | false         | Determines if the force gets higher the closer the triggerer is to the center of the trigger.                                                                                                    |
| ForceFluctuation                  | bool    | false         | Determines if the force fluctuates over time or if it stays constant.                                                                                                                            |
| ForceFluctuationStrength          | float   | 1             | How much the fluctuation affects the force. 1 is the maximum fluctuation, 0 is no fluctuation.                                                                                                   |
| ForceFluctuationFrequency         | float   | 1             | How fast (cycles per second) the force fluctuates.                                                                                                                                               |
| ForceFluctuationInterval          | float   | 0.01          | How often (in seconds) the force fluctuation is calculated.                                                                                                                                      |
| Radius                            | float   | 0             |                                                                                                                                                                                                  |
| Width                             | float   | 0             |                                                                                                                                                                                                  |
| Height                            | float   | 0             |                                                                                                                                                                                                  |
| BodyOffset                        | Vector2 | "0,0"         |                                                                                                                                                                                                  |
| ApplyEffectsToCharactersInsideSub | bool    | false         |                                                                                                                                                                                                  |
| MoveOutsideSub                    | bool    | false         |                                                                                                                                                                                                  |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="gravitysphere" category="Alien" subcategory="devices" linkable="true" scale="0.5" damagedbyexplosions="true" damagedbymeleeweapons="true" damagedbyprojectiles="true" damagedbyrepairtools="true" health="300">
  <TriggerComponent triggeredby="Human, Item" force="75" radius="1000" distancebasedforce="true">
    <StatusEffect type="OnActive" target="This">
      <ParticleEmitter particle="gravityspherefx" particleamount="1" emitinterval="1.25" />
    </StatusEffect>
  </TriggerComponent>
  <TriggerComponent triggeredby="Human, Item" force="45" radius="500">
    <Upgrade gameversion="1.6.4.0" force="45" />
    <Attack onlyhumans="true">
      <Affliction identifier="radiationsickness" strength="5" />
    </Attack>
    <StatusEffect type="OnActive" target="This">
      <ParticleEmitter particle="gravityspherefx2" particlespersecond="3" />
      <ParticleEmitter particle="skyholderfx2" anglemax="360" distancemin="400" distancemax="500" velocitymin="-500" velocitymax="-900" particlespersecond="200" colormultiplier="255,180,255,255" />
    </StatusEffect>
  </TriggerComponent>
  <TriggerComponent triggeredby="Human, Item" force="25" radius="100">
    <Upgrade gameversion="1.6.4.0" force="25" />
    <Attack onlyhumans="true">
      <Affliction identifier="internaldamage" strength="8" />
    </Attack>
    <StatusEffect type="OnActive" target="This">
      <ParticleEmitter particle="gravityspherefx2" particlespersecond="5" scalemin="0.7" scalemax="0.7" />
      <ParticleEmitter particle="skyholderfx2" anglemax="360" distancemin="300" distancemax="400" velocitymin="-700" velocitymax="-1200" particlespersecond="100" colormultiplier="255,180,255,255" />
    </StatusEffect>
  </TriggerComponent>
  <ConnectionPanel canbeselected="true" hudpriority="10" locked="True" allowingameediting="False">
    <RequiredItem items="screwdriver" type="Equipped" />
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <input name="toggle" displayname="connection.togglestate" />
    <input name="set_state" displayname="connection.setstate" />
    <output name="state_out" displayname="connection.stateout" fallbackdisplayname="connection.signalout" />
    <!--Break the item when a shutdown signal is received-->
    <input name="shutdown" displayname="connection.shutdown">
      <StatusEffect type="OnUse" target="This" condition="-100" setvalue="true" />
    </input>
    <StatusEffect type="OnDamaged" target="This">
      <ParticleEmitter particle="plasmaspark" drawontop="true" particleamount="5" scalemin="0.3" scalemax="0.5" velocitymin="300" velocitymax="800" anglemin="0" anglemax="360" />
      <ParticleEmitter particle="risingbubbles" particleamount="1" anglemin="90" anglemax="90" velocitymin="50" velocitymax="100" scalemin="0.5" scalemax="1" />
      <Sound file="Content/Sounds/Damage/LimbSlash4.ogg" range="2000" />
    </StatusEffect>
    <StatusEffect type="OnDamaged" target="This" isActive="false" Duration="1" stackable="false" />
    <StatusEffect type="OnBroken" target="This" setvalue="true" noninteractable="true">
      <ParticleEmitter particle="underwaterexplosionfast" particleamount="3" anglemin="0" anglemax="360" velocitymin="50" velocitymax="100" scalemin="1" scalemax="2.5" />
      <ParticleEmitter particle="risingbubbles" particleamount="1" anglemin="90" anglemax="90" velocitymin="50" velocitymax="100" scalemin="1" scalemax="2" startdelaymin="0.1" startdelaymax="0.3" />
      <sound file="Content/Sounds/Damage/Implode.ogg" range="50000" dontmuffle="true" soundselectionmode="All" />
      <sound file="Content/Items/Weapons/Emp.ogg" dontmuffle="true" range="100000" />
    </StatusEffect>
  </ConnectionPanel>
  [...]
</Item>
```

