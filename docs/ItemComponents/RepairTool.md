# RepairTool


## Example
```xml
<Item identifier="guardiansteamcannon" category="hidden" Tags="smallitem,weapon,gun" cargocontaineridentifier="metalcrate" Scale="0.5" impactsoundtag="impact_metal_light" hideinmenus="true" nonplayerteaminteractable="True">
  <RepairTool structurefixamount="0.0" range="500" barrelpos="45,0" fireprobability="0.0" spread="25">
    <ParticleEmitterHitCharacter particle="fleshsmoke" particlespersecond="3" anglemin="-5" anglemax="5" velocitymin="10" velocitymax="100" />
    <ParticleEmitter particle="steamspray" particlespersecond="80" anglemin="0" anglemax="0" velocitymin="300" velocitymax="500" highqualitycollisiondetection="true" />
    <ParticleEmitter particle="bubbles" particlespersecond="20" anglemin="-10" anglemax="10" scalemin="0.3" scalemax="0.7" velocitymin="5" velocitymax="100" copyentityangle="true" />
    <sound file="Content/Items/Weapons/FlameThrowerLoop.ogg" type="OnUse" range="500.0" loop="true" />
    <StatusEffect type="OnUse" target="UseTarget">
      <Affliction identifier="burn" amount="1.25" />
    </StatusEffect>
  </RepairTool>
  [...]
</Item>
```

