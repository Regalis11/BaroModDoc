# RangedWeapon


## Attributes

| Attribute                            | Type    | Default value | Description                                                                                                                                              |
|--------------------------------------|---------|---------------|----------------------------------------------------------------------------------------------------------------------------------------------------------|
| BarrelPos                            | string  | "0.0,0.0"     | The position of the barrel as an offset from the item's center (in pixels). Determines where the projectiles spawn.                                      |
| Reload                               | float   | 1             | How long the user has to wait before they can fire the weapon again (in seconds).                                                                        |
| ReloadSkillRequirement               | float   | 0             | Weapons skill requirement to reload at normal speed.                                                                                                     |
| ReloadNoSkill                        | float   | 1             | Reload time at 0 skill level. Reload time scales with skill level up to the Weapons skill requirement.                                                   |
| HoldTrigger                          | bool    | false         | Tells the AI to hold the trigger down when it uses this weapon                                                                                           |
| ProjectileCount                      | int     | 1             | How many projectiles the weapon launches when fired once.                                                                                                |
| Spread                               | float   | 0             | Random spread applied to the firing angle of the projectiles when used by a character with sufficient skills to use the weapon (in degrees).             |
| UnskilledSpread                      | float   | 0             | Random spread applied to the firing angle of the projectiles when used by a character with insufficient skills to use the weapon (in degrees).           |
| LaunchImpulse                        | float   | 0             | The impulse applied to the physics body of the projectile (the higher the impulse, the faster the projectiles are launched). Sum of weapon + projectile. |
| Penetration                          | float   | 0             | Percentage of damage mitigation ignored when hitting armored body parts (deflecting limbs). Sum of weapon + projectile.                                  |
| WeaponDamageModifier                 | float   | 1             | Weapon's damage modifier                                                                                                                                 |
| MaxChargeTime                        | float   | 0             | The time required for a charge-type turret to charge up before able to fire.                                                                             |
| DualWieldReloadTimePenaltyMultiplier | float   | 1             | Penalty multiplier to reload time when dual-wielding.                                                                                                    |
| DualWieldAccuracyPenalty             | float   | 0             | Additive penalty to accuracy (spread angle) when dual-wielding.                                                                                          |
| ChargeSoundWindupPitchSlide          | Vector2 | "0.5, 1.5"    | Pitch slides from X to Y over the charge time                                                                                                            |
| CrossHairScale                       | float   | 1             | The scale of the crosshair sprite (if there is one).                                                                                                     |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="clownexosuit" category="Diving,Equipment" tags="clowngear,diving,deepdiving,deepdivinglarge,clowns,human" scale="0.5" fireproof="true" isshootable="true" allowdroppingonswapwith="diving" impactsoundtag="impact_metal_heavy" botpriority="0.5" cargocontaineridentifier="">
  <RangedWeapon barrelpos="29,11" spread="5" unskilledspread="5" combatPriority="70" drawhudwhenequipped="true" crosshairscale="0.2" reload="0.1" suitableprojectiles="bananapeelprojectile">
    <Crosshair texture="Content/Items/Weapons/Crosshairs.png" sourcerect="0,256,256,256" />
    <CrosshairPointer texture="Content/Items/Weapons/Crosshairs.png" sourcerect="256,256,256,256" />
    <Sound file="Content/Items/Weapons/GrenadeLauncherShot1.ogg" type="OnUse" selectionmode="All" />
    <Sound file="Content/Items/Weapons/honk.ogg" type="OnUse" selectionmode="All" />
    <ParticleEmitter particle="explosionsmoke" particleamount="5" velocitymin="0" velocitymax="0" />
    <StatusEffect type="OnUse" target="This">
      <SpawnItem identifier="bananapeelprojectile" spawnposition="ThisInventory" count="1" />
      <Explosion range="500.0" force="2" shockwave="false" smoke="false" flames="false" flash="true" sparks="false" underwaterbubble="false" applyfireeffects="false" camerashake="6.0" />
    </StatusEffect>
    <StatusEffect type="OnUse" target="This" reload="5.0" setvalue="true" delay="0.5" />
    <StatusEffect type="OnUse" target="This" reload="0.1" setvalue="true" delay="5.0" />
    <RequiredItems items="bananapeelprojectile" type="Contained" />
  </RangedWeapon>
  <ItemContainer capacity="0" maxstacksize="0" hideitems="true" containedstateindicatorstyle="tank" containedstateindicatorslot="0">
    <Containable items="none" />
    <SlotIcon slotindex="0" texture="Content/UI/StatusMonitorUI.png" sourcerect="64,448,64,64" origin="0.5,0.5" />
    <SlotIcon slotindex="1" texture="Content/UI/StatusMonitorUI.png" sourcerect="192,448,64,64" origin="0.5,0.5" />
    <StatusEffect type="OnWearing" target="Contained" playsoundonrequireditemfailure="true">
      <RequiredItem items="oxygensource" type="Contained" matchonempty="true" />
      <Conditional condition="lte 0.0" />
      <Sound file="Content/Items/WarningBeep.ogg" range="500" loop="true" />
    </StatusEffect>
    <SubContainer capacity="1" maxstacksize="1">
      <Containable items="oxygensource,weldingtoolfuel" />
      <Containable items="oxygensource">
        <StatusEffect type="OnWearing" target="Character" OxygenAvailable="1000.0" />
        <StatusEffect type="OnWearing" target="Contained" Condition="-0.2" comparison="And">
          <Conditional TargetContainer="true" TargetGrandparent="true" IsDead="false" />
          <Conditional TargetContainer="true" TargetGrandparent="true" DecreasedOxygenConsumption="lt 99" />
          <Conditional TargetContainer="true" TargetGrandparent="true" NeedsAir="true" />
        </StatusEffect>
        <StatusEffect type="OnWearing" target="Contained">
          <Conditional condition="lt 5.0" />
          <Sound file="Content/Items/WarningBeepSlow.ogg" range="250" loop="true" />
        </StatusEffect>
      </Containable>
      <Containable items="oxygenitetank">
        <StatusEffect type="OnWearing" target="Character" SpeedMultiplier="1.2" setvalue="true" targetslot="0" comparison="And">
          <Conditional IsDead="false" />
          <Conditional DecreasedOxygenConsumption="lt 99" />
          <Conditional NeedsAir="true" />
        </StatusEffect>
      </Containable>
      <Containable items="weldingfueltank" blameequipperfordeath="true">
        <StatusEffect type="OnWearing" target="Contained" Condition="-0.5" comparison="And">
          <Conditional TargetContainer="true" TargetGrandparent="true" IsDead="false" />
          <Conditional TargetContainer="true" TargetGrandparent="true" DecreasedOxygenConsumption="lt 99" />
          <Conditional TargetContainer="true" TargetGrandparent="true" NeedsAir="true" />
        </StatusEffect>
        <StatusEffect type="OnWearing" target="Character" OxygenAvailable="-100.0" Oxygen="-5.0" comparison="And">
          <Conditional IsDead="false" />
          <Conditional DecreasedOxygenConsumption="lt 99" />
          <Conditional NeedsAir="true" />
        </StatusEffect>
      </Containable>
      <Containable items="incendiumfueltank" blameequipperfordeath="true">
        <StatusEffect type="OnWearing" target="Contained" Condition="-0.5" comparison="And">
          <Conditional TargetContainer="true" TargetGrandparent="true" IsDead="false" />
          <Conditional TargetContainer="true" TargetGrandparent="true" DecreasedOxygenConsumption="lt 99" />
          <Conditional TargetContainer="true" TargetGrandparent="true" NeedsAir="true" />
        </StatusEffect>
        <StatusEffect type="OnWearing" target="Character" OxygenAvailable="-100.0" comparison="And" targetlimb="Torso">
          <Affliction identifier="burn" amount="20.0" />
          <Conditional IsDead="false" />
          <Conditional DecreasedOxygenConsumption="lt 99" />
          <Conditional NeedsAir="true" />
        </StatusEffect>
      </Containable>
    </SubContainer>
    <SubContainer capacity="1" maxstacksize="1">
      <Containable items="divingsuitfuel">
        <StatusEffect type="OnContaining" target="This,Character" Voltage="1.0" setvalue="true">
          <Conditional IsDead="false" />
        </StatusEffect>
      </Containable>
    </SubContainer>
  </ItemContainer>
  <ItemContainer capacity="1" maxstacksize="1" hideitems="true" spawnwithid="bananapeelprojectile">
    <Containable items="bananapeelprojectile" />
  </ItemContainer>
  [...]
</Item>
```

