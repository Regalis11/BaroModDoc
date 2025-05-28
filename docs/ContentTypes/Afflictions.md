# AfflictionPrefab
<sup>Relevant files: [[Shared:AfflictionsFile.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/AfflictionsFile.cs) [[Shared:AfflictionPrefab.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/Health/Afflictions/AfflictionPrefab.cs)</sup>

- **Required by core package:** Yes

## AfflictionPrefab
AfflictionPrefab is a prefab that defines a type of affliction that can be applied to a character.
There are multiple sub\-types of afflictions such as AfflictionPrefabHusk, AfflictionPsychosis and AfflictionBleeding that can be used for additional functionality.
When defining a new affliction, the type will be determined by the element name.
```xml
<Afflictions>
  <!-- Defines a regular affliction. -->
  <Affliction identifier="mycoolaffliction1" />
  <!-- Defines an AfflictionPrefabHusk affliction. -->
  <AfflictionPrefabHusk identifier="mycoolaffliction2" />
  <!-- Defines an AfflictionBleeding affliction. -->
  <AfflictionBleeding identifier="mycoolaffliction3" />
</Afflictions>
```

### Attributes

| Attribute                      | Type         | Default value                                                                                                       | Description                                                                                                                                                                                                                                                                                                                                                    |
|--------------------------------|--------------|---------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Type                           | Identifier   | ""                                                                                                                  | Arbitrary string that is used to identify the type of the affliction.                                                                                                                                                                                                                                                                                          |
| TranslationOverride            | Identifier   | Same as Identifier                                                                                                  | Can be set to the identifier of another affliction to make this affliction<br/>reuse the same name and description.                                                                                                                                                                                                                                            |
| ShowDescriptionInTooltip       | bool         | true                                                                                                                | Should the affliction's description be included in the tooltips on the affliction icons above the health bar?                                                                                                                                                                                                                                                  |
| IsBuff                         | bool         | false                                                                                                               | If set to true, the game will recognize this affliction as a buff.<br/>This means, among other things, that bots won't attempt to treat it,<br/>and the health UI will render the affected limb in green rather than red.                                                                                                                                      |
| AffectMachines                 | bool         | true                                                                                                                | If set to true, this affliction can affect characters that are marked as<br/>machines, such as the Fractal Guardian.                                                                                                                                                                                                                                           |
| ShowBarInHealthMenu            | bool         | true                                                                                                                | If set to false, the health UI will not show the strength of the affliction<br/>as a bar under its indicator.                                                                                                                                                                                                                                                  |
| HealableInMedicalClinic        | bool         | false if the affliction is a buff or has the type "geneticmaterialbuff" or "geneticmaterialdebuff", true otherwise. | If set to true, this affliction can be healed at the medical clinic.                                                                                                                                                                                                                                                                                           |
| HealCostMultiplier             | float        | 1                                                                                                                   | How much each unit of this affliction's strength will add<br/>to the cost of healing at the medical clinic.                                                                                                                                                                                                                                                    |
| BaseHealCost                   | int          | 0                                                                                                                   | The minimum cost of healing this affliction at the medical clinic.                                                                                                                                                                                                                                                                                             |
| Duration                       | float        | 0                                                                                                                   | The duration of the affliction, in seconds. If set to 0, the affliction does not expire.                                                                                                                                                                                                                                                                       |
| LimbSpecific                   | bool         | false                                                                                                               | If set to true, the affliction affects individual limbs. Otherwise, it affects the whole character.                                                                                                                                                                                                                                                            |
| HideIconAfterDelay             | bool         | false                                                                                                               | If set to true, this affliction's icon will be hidden from the HUD after 5 seconds.                                                                                                                                                                                                                                                                            |
| ActivationThreshold            | float        | 0                                                                                                                   | How high the strength has to be for the affliction to take effect                                                                                                                                                                                                                                                                                              |
| ShowIconThreshold              | float        | max( ActivationThreshold , 0.05 )                                                                                   | How high the strength has to be for the affliction icon to be shown in the UI                                                                                                                                                                                                                                                                                  |
| ShowIconToOthersThreshold      | float        | Same as ShowIconThreshold                                                                                           | How high the strength has to be for the affliction icon to be shown to others with a health scanner or via the health interface                                                                                                                                                                                                                                |
| MaxStrength                    | float        | 100                                                                                                                 | The maximum strength this affliction can have.                                                                                                                                                                                                                                                                                                                 |
| GrainBurst                     | float        | 0                                                                                                                   | The strength of the radiation grain effect to apply when the strength of this affliction increases.                                                                                                                                                                                                                                                            |
| ShowInHealthScannerThreshold   | float        | -                                                                                                                   | How high the strength has to be for the affliction icon to be shown with a health scanner                                                                                                                                                                                                                                                                      |
| TreatmentThreshold             | float        | max( ActivationThreshold , 10 )                                                                                     | How strong the affliction needs to be before bots attempt to treat it.<br/>Also effects when the affliction is shown in the suitable treatments list.                                                                                                                                                                                                          |
| TreatmentSuggestionThreshold   | float        | Same as TreatmentThreshold                                                                                          | How strong the affliction needs to be for treatment suggestions to be shown in the health interface.<br/>Defaults to TreatmentThreshold.                                                                                                                                                                                                                       |
| DamageOverlayAlpha             | float        | 0                                                                                                                   | Opacity of the bloody damage overlay on limbs affected by this affliction. 1 = full strength.                                                                                                                                                                                                                                                                  |
| BurnOverlayAlpha               | float        | 0                                                                                                                   | Opacity of the burn effect (darker tint) on limbs affected by this affliction. 1 = full strength.                                                                                                                                                                                                                                                              |
| KarmaChangeOnApplied           | float        | 0                                                                                                                   | How much karma changes when a player applies this affliction to someone (per strength of the affliction)                                                                                                                                                                                                                                                       |
| IconColors                     | Color[]      | -                                                                                                                   | A gradient that defines which color to render this affliction's icon<br/>with, based on the affliction's current strength.                                                                                                                                                                                                                                     |
| AfflictionOverlayAlphaIsLinear | bool         | false                                                                                                               | If set to true and the affliction has an AfflictionOverlay element, the overlay's opacity will be strictly proportional to its strength.<br/>Otherwise, the overlay's opacity will be determined based on its activation threshold and effects.                                                                                                                |
| AchievementOnReceived          | Identifier   | ""                                                                                                                  |                                                                                                                                                                                                                                                                                                                                                                |
| AchievementOnRemoved           | Identifier   | ""                                                                                                                  | Steam achievement given when the affliction is removed from the controlled character.                                                                                                                                                                                                                                                                          |
| Targets                        | Identifier[] | []                                                                                                                  | A list of species this affliction is allowed to affect.                                                                                                                                                                                                                                                                                                        |
| ResetBetweenRounds             | bool         | false                                                                                                               | If set to true, this affliction will not persist between rounds.                                                                                                                                                                                                                                                                                               |
| DamageParticles                | bool         | true                                                                                                                | Should damage particles be emitted when a character receives this affliction?<br/>Only relevant if the affliction is of the type "bleeding" or "damage".                                                                                                                                                                                                       |
| WeaponsSkillGain               | float        | 0                                                                                                                   | An arbitrary modifier that affects how much weapons skill is increased when you apply the affliction on a target.<br/>The skill is increased only when the target is hostile.                                                                                                                                                                                  |
| MedicalSkillGain               | float        | 0                                                                                                                   | An arbitrary modifier that affects how much medical skill is increased when you apply the affliction on a target.<br/>If the affliction causes damage or is of the 'poison' or 'paralysis' type, the skill is increased only when the target is hostile.<br/>If the affliction is of the 'buff' type, the skill is increased only when the target is friendly. |



### Elements

| Element                   | Type                              | Description                                                                                                                                                                                                               |
|---------------------------|-----------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| icon                      | Sprite                            | An icon that’s used in the UI to represent this affliction.                                                                                                                                                               |
| afflictionoverlay         | Sprite                            | A sprite that covers the affected player's entire screen when this affliction is active.<br/>Its opacity is controlled by the active effect's MinAfflictionOverlayAlphaMultiplier and MaxAfflictionOverlayAlphaMultiplier |
| afflictionoverlayanimated | SpriteSheet                       | A sprite that covers the affected player's entire screen when this affliction is active.<br/>Its opacity is controlled by the active effect's MinAfflictionOverlayAlphaMultiplier and MaxAfflictionOverlayAlphaMultiplier |
| description               | [Description](#description)       |                                                                                                                                                                                                                           |
| effect                    | [Effect](#effect)                 | Effects to apply at various strength levels.<br/>Only one effect can be applied at any given moment, so their ranges should be defined with no overlap.                                                                   |
| periodiceffect            | [PeriodicEffect](#periodiceffect) | PeriodicEffect applies StatusEffects to the character periodically.                                                                                                                                                       |



## AfflictionPrefabHusk
AfflictionPrefabHusk is a special type of affliction that has added functionality for husk infection.


### Attributes

| Attribute                 | Type       | Default value           | Description                                                                                                                                                                       |
|---------------------------|------------|-------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| HuskedSpeciesName         | Identifier | ""                      | The species of husk to convert the affected character to<br/>once husk infection reaches its final stage.                                                                         |
| TransferBuffs             | bool       | true                    | If set to true, all buffs are transferred to the converted<br/>character after husk transformation is complete.                                                                   |
| SendMessages              | bool       | true                    | If set to true, the affected player will see on-screen messages describing husk infection symptoms<br/>and affected bots will speak about their current husk infection stage.     |
| CauseSpeechImpediment     | bool       | true                    | If set to true, affected characters will have their speech impeded once the affliction<br/>reaches the dormant stage.                                                             |
| NeedsAir                  | bool       | false                   | If set to false, affected characters will no longer require air<br/>once the affliction reaches the active stage.                                                                 |
| ControlHusk               | bool       | false                   | If set to true, affected players will retain control of their character<br/>after transforming into a husk.                                                                       |
| DormantThreshold          | float      | 50% of MaxStrength      | The minimum strength at which husk infection will be in the dormant stage.<br/>It must be less than or equal to ActiveThreshold.                                                  |
| ActiveThreshold           | float      | 75% of MaxStrength      | The minimum strength at which husk infection will be in the active stage.<br/>It must be greater than or equal to DormantThreshold and less than or equal to TransitionThreshold. |
| TransitionThreshold       | float      | Same as MaxStrength     | The minimum strength at which husk infection will be in its final stage.<br/>It must be greater than or equal to ActiveThreshold.                                                 |
| TransformThresholdOnDeath | float      | Same as ActiveThreshold | The minimum strength the affliction must have for the affected character<br/>to transform into a husk upon death.                                                                 |



## Description
The description element can be used to define descriptions for the affliction which are shown under specific conditions;
for example a description that only shows to other players or only at certain strength levels.


### Attributes

| Attribute      | Type                                                     | Default value | Description                                                |
|----------------|----------------------------------------------------------|---------------|------------------------------------------------------------|
| TextIdentifier | Identifier                                               | ""            | Text tag used to set the text from the localization files. |
| MinStrength    | float                                                    | 0             | Minimum strength required for the description to be shown. |
| MaxStrength    | float                                                    | 100           | Maximum strength required for the description to be shown. |
| Target         | [TargetType](#targettype "Who can see the description.") | Any           | Who can see the description.                               |
| Text           | string                                                   | ""            | Raw text for the description.                              |



## TargetType

| Value          | Description                                                       |
|----------------|-------------------------------------------------------------------|
| Any            | Everyone can see the description.                                 |
| Self           | Only the affected character can see the description.              |
| OtherCharacter | The affected character cannot see the description but others can. |



## Effect
Effects are the primary way to add functionality to afflictions.


### Attributes

| Attribute                           | Type       | Default value                                                       | Description                                                                                                                                                                                     |
|-------------------------------------|------------|---------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| MinStrength                         | float      | 0                                                                   | Minimum affliction strength required for this effect to be active.                                                                                                                              |
| MaxStrength                         | float      | 0                                                                   | Maximum affliction strength for which this effect will be active.                                                                                                                               |
| MinVitalityDecrease                 | float      | 0                                                                   | The amount of vitality that is lost at this effect's lowest strength.                                                                                                                           |
| MaxVitalityDecrease                 | float      | 0                                                                   | The amount of vitality that is lost at this effect's highest strength.                                                                                                                          |
| StrengthChange                      | float      | 0                                                                   | How much the affliction's strength changes every second while this effect is active.                                                                                                            |
| MultiplyByMaxVitality               | bool       | false                                                               | If set to true, MinVitalityDecrease and MaxVitalityDecrease represent a fraction of the affected character's maximum vitality, with 1 meaning 100%, instead of the same amount for all species. |
| MinScreenBlur                       | float      | 0                                                                   | Blur effect strength at this effect's lowest strength.                                                                                                                                          |
| MaxScreenBlur                       | float      | 0                                                                   | Blur effect strength at this effect's highest strength.                                                                                                                                         |
| MinScreenDistort                    | float      | 0                                                                   | Generic distortion effect strength at this effect's lowest strength.                                                                                                                            |
| MaxScreenDistort                    | float      | 0                                                                   | Generic distortion effect strength at this effect's highest strength.                                                                                                                           |
| MinRadialDistort                    | float      | 0                                                                   | Radial distortion effect strength at this effect's lowest strength.                                                                                                                             |
| MaxRadialDistort                    | float      | 0                                                                   | Radial distortion effect strength at this effect's highest strength.                                                                                                                            |
| MinChromaticAberration              | float      | 0                                                                   | Chromatic aberration effect strength at this effect's lowest strength.                                                                                                                          |
| MaxChromaticAberration              | float      | 0                                                                   | Chromatic aberration effect strength at this effect's highest strength.                                                                                                                         |
| GrainColor                          | Color      | <div style="background: rgba(255,255,255,1);">255,255,255,255</div> | Radiation grain effect color.                                                                                                                                                                   |
| MinGrainStrength                    | float      | 0                                                                   | Radiation grain effect strength at this effect's lowest strength.                                                                                                                               |
| MaxGrainStrength                    | float      | 0                                                                   | Radiation grain effect strength at this effect's highest strength.                                                                                                                              |
| ScreenEffectFluctuationFrequency    | float      | 0                                                                   | The maximum rate of fluctuation to apply to visual effects caused by this affliction effect. Effective fluctuation is proportional to the affliction's current strength.                        |
| MinAfflictionOverlayAlphaMultiplier | float      | 1                                                                   | Multiplier for the affliction overlay's opacity at this effect's lowest strength. See the list of elements for more details.                                                                    |
| MaxAfflictionOverlayAlphaMultiplier | float      | 1                                                                   | Multiplier for the affliction overlay's opacity at this effect's highest strength. See the list of elements for more details.                                                                   |
| MinBuffMultiplier                   | float      | 1                                                                   | Multiplier for every buff's decay rate at this effect's lowest strength. Only applies to afflictions of class BuffDurationIncrease.                                                             |
| MaxBuffMultiplier                   | float      | 1                                                                   | Multiplier for every buff's decay rate at this effect's highest strength. Only applies to afflictions of class BuffDurationIncrease.                                                            |
| MinSpeedMultiplier                  | float      | 1                                                                   | Multiplier to apply to the affected character's speed at this effect's lowest strength.                                                                                                         |
| MaxSpeedMultiplier                  | float      | 1                                                                   | Multiplier to apply to the affected character's speed at this effect's highest strength.                                                                                                        |
| MinSkillMultiplier                  | float      | 1                                                                   | Multiplier to apply to all of the affected character's skill levels at this effect's lowest strength.                                                                                           |
| MaxSkillMultiplier                  | float      | 1                                                                   | Multiplier to apply to all of the affected character's skill levels at this effect's highest strength.                                                                                          |
| MinResistance                       | float      | 0                                                                   | The amount of resistance to the afflictions specified by ResistanceFor to apply at this effect's lowest strength.                                                                               |
| MaxResistance                       | float      | 0                                                                   | The amount of resistance to the afflictions specified by ResistanceFor to apply at this effect's highest strength.                                                                              |
| DialogFlag                          | Identifier | ""                                                                  | Identifier used by AI to determine conversation lines to say when this effect is active.                                                                                                        |
| Tag                                 | Identifier | ""                                                                  | Tag that enemy AI may use to target the affected character when this effect is active.                                                                                                          |
| MinFaceTint                         | Color      | <div style="background: rgba(0,0,0,0);">0,0,0,0</div>               | Color to tint the affected character's face with at this effect's lowest strength. The alpha channel is used to determine how much to tint the character's face.                                |
| MaxFaceTint                         | Color      | <div style="background: rgba(0,0,0,0);">0,0,0,0</div>               | Color to tint the affected character's face with at this effect's highest strength. The alpha channel is used to determine how much to tint the character's face.                               |
| MinBodyTint                         | Color      | <div style="background: rgba(0,0,0,0);">0,0,0,0</div>               | Color to tint the affected character's entire body with at this effect's lowest strength. The alpha channel is used to determine how much to tint the character.                                |
| MaxBodyTint                         | Color      | <div style="background: rgba(0,0,0,0);">0,0,0,0</div>               | Color to tint the affected character's entire body with at this effect's highest strength. The alpha channel is used to determine how much to tint the character.                               |
| ThermalOverlayRange                 | float      | 0                                                                   | Range of the "thermal goggles overlay" enabled by the affliction.                                                                                                                               |
| ThermalOverlayColor                 | Color      | <div style="background: rgba(255,0,0,1);">255,0,0,255</div>         | Color of the "thermal goggles overlay" enabled by the affliction. Only has an effect if ThermalOverlayRange is larger than 0.                                                                   |
| ConvulseAmount                      | float      | 0                                                                   | Multiplier for the convulsion/seizure effect on the character's ragdoll when this effect is active.                                                                                             |



### Elements

| Element      | Type                                             | Description                                                                  |
|--------------|--------------------------------------------------|------------------------------------------------------------------------------|
| statuseffect | [StatusEffect](/BaroModDoc/Misc/StatusEffect.md) | statuseffects applied on the character when the affliction is active         |
| abilityflag  | [AppliedAbilityFlag](#appliedabilityflag)        | Enables the specified flag on the character as long as the effect is active. |



## AppliedAbilityFlag
Flag that will be enabled for the character as long as the effect is active.
```xml
<Effect minstrength="0" maxstrength="100">
  <!-- Grants pressure immunity to the character while the effect is active. -->
  <AbilityFlag flagtype="ImmuneToPressure" />
</Effect>
```

### Attributes

| Attribute | Type         | Default value | Description                   |
|-----------|--------------|---------------|-------------------------------|
| FlagType  | AbilityFlags | None          | Which ability flag to enable. |



## AppliedStatValue
StatType that will be applied to the affected character when the effect is active that is proportional to the effect's strength.
```xml
<Effect minstrength="0" maxstrength="100">
  <!-- Walking speed will be increased by 10% at strength 0, 20% at 50 and 30% at 100 -->
  <StatValue stattype="WalkingSpeed" minvalue="0.1" maxvalue="0.3" />
  <!-- Maximum health will be increased by 20% regardless of the effect strength -->
  <StatValue stattype="MaximumHealthMultiplier" value="0.2" />
</Effect>
```

### Attributes

| Attribute | Type      | Default value | Description                                                              |
|-----------|-----------|---------------|--------------------------------------------------------------------------|
| Value     | float     | 0             | Constant value to apply, will be ignored if MinValue or MaxValue are set |
| StatType  | StatTypes | None          | Which StatType to apply                                                  |
| MinValue  | float     | Same as Value | Minimum value to apply                                                   |
| MaxValue  | float     | Same as Value | Maximum value to apply                                                   |



## PeriodicEffect
PeriodicEffect applies StatusEffects to the character periodically.


### Attributes

| Attribute   | Type  | Default value | Description                                                                                                                                  |
|-------------|-------|---------------|----------------------------------------------------------------------------------------------------------------------------------------------|
| Interval    | float | 1.0           | How often the status effect is applied in seconds.<br/>Setting this attribute will set both the min and max interval to the specified value. |
| MinInterval | float | 1.0           | Minimum interval between applying the status effect in seconds.                                                                              |
| MaxInterval | float | 1.0           | Maximum interval between applying the status effect in seconds.                                                                              |



### Elements

| Element      | Type                                    | Description |
|--------------|-----------------------------------------|-------------|
| StatusEffect | [StatusEffect](../Misc/StatusEffect.md) |             |


