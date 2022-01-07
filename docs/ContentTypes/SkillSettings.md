# SkillSettings

<sub>Relevant files: [Shared:SkillSettingsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/SkillSettingsFile.cs) [Shared:SkillSettings.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource\Characters\SkillSettings.cs)</sub>
- **Required by core package:** No

## Attributes

- `SkillIncreasePerFriendlyHealed` : `float`
- `SingleRoundSkillGainMultiplier` : `float`
- `SkillIncreasePerCprRevive` : `float`
- `MaximumOlympianSkill` : `float`
- `SkillIncreasePerSabotage` : `float`
- `SkillIncreasePerHostileDamage` : `float`
- `SkillIncreasePerFabricatorRequiredSkill` : `float`
- `SkillIncreasePerRepairedStructureDamage` : `float`
- `SkillIncreasePerSecondWhenSteering` : `float`
- `SkillIncreasePerSecondWhenOperatingTurret` : `float`
- `AssistantSkillIncreaseMultiplier` : `float`
- `SkillIncreasePerRepair` : `float`
**WARNING:** This file likely generated completely incorrectly!

## Examples

### Example 1 - single SkillSetting

```xml
<SkillSetting
  identifier="mySkillSetting" />
```

### Example 2 - multiple SkillSettings

```xml
<SkillSettings>
  <SkillSetting
    identifier="mySkillSetting1" />
  <SkillSetting
    identifier="mySkillSetting2" />
</SkillSettings>
```

### Example 3 - overriding existing SkillSettings

```xml
<override>
  <SkillSetting
    identifier="mySkillSetting1" />
  <SkillSetting
    identifier="mySkillSetting2" />
</override>
```

