# Throwable


## Attributes

| Attribute  | Type  | Default value | Description                                                                                                    |
|------------|-------|---------------|----------------------------------------------------------------------------------------------------------------|
| ThrowForce | float | 1             | The impulse applied to the physics body of the item when thrown. Higher values make the item be thrown faster. |

This component also supports the attributes defined in: [Holdable](Holdable.md), [Pickable](Pickable.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="rituallantern" category="Equipment" maxstacksize="12" cargocontaineridentifier="metalcrate" Scale="0.5" tags="smallitem,light,huskcult" impactsoundtag="impact_soft" isshootable="true">
  <Throwable slots="Any,RightHand,LeftHand" holdpos="70,-70" handle1="-5,10" holdangle="0" throwforce="3.5" aimpos="30,-30" msg="ItemMsgPickUpSelect" />
  <LightComponent LightColor="219,100,239,80" Flicker="0.2" pulsefrequency="0.2" pulseamount="0.5" range="600" IsOn="false">
    <StatusEffect type="OnUse" targettype="This" IsOn="true" />
    <StatusEffect type="OnActive" targettype="This" Condition="-0.25" />
    <StatusEffect type="OnActive" targettype="This">
      <Conditional PhysicsBodyActive="eq true" />
      <ParticleEmitter particle="flare" particlespersecond="15" scalemin="0.1" scalemax="0.1" />
      <ParticleEmitter particle="ritualsmoke" particlespersecond="5" anglemin="0" anglemax="360" velocitymin="10" velocitymax="150" scalemin="0.5" scalemax="1.0" />
    </StatusEffect>
    <StatusEffect type="OnActive" target="NearbyCharacters" range="800" interval="0.5" disabledeltatime="true">
      <Affliction identifier="disguisedashusk" amount="1.0" />
      <ReduceAffliction type="damage" amount="0.1" />
      <ReduceAffliction type="burn" amount="0.2" />
      <ReduceAffliction type="opiatewithdrawal" amount="0.2" />
    </StatusEffect>
    <StatusEffect type="OnBroken" targettype="This" IsOn="false" />
  </LightComponent>
  [...]
</Item>
```

