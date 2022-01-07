# SkillSettings

<sub>Relevant files: [Shared:SkillSettingsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/SkillSettingsFile.cs) [Shared:SkillSettings.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/SkillSettings.cs)</sub>
- **Required by core package:** No

## Attributes

- `AssistantSkillIncreaseMultiplier` : `float`
- `SkillIncreasePerFriendlyHealed` : `float`
- `SkillIncreasePerSecondWhenOperatingTurret` : `float`
- `SkillIncreasePerFabricatorRequiredSkill` : `float`
- `SingleRoundSkillGainMultiplier` : `float`
- `SkillIncreasePerRepair` : `float`
- `SkillIncreasePerRepairedStructureDamage` : `float`
- `SkillIncreasePerSabotage` : `float`
- `MaximumOlympianSkill` : `float`
- `SkillIncreasePerSecondWhenSteering` : `float`
- `SkillIncreasePerCprRevive` : `float`
- `SkillIncreasePerHostileDamage` : `float`
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

