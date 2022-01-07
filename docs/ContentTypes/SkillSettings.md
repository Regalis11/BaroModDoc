# SkillSettings

<sup>Relevant files: [Shared:SkillSettings.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/SkillSettings.cs) [Shared:SkillSettingsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/SkillSettingsFile.cs)</sup>
- **Required by core package:** No

## Attributes

- `SkillIncreasePerCprRevive` : `float`
- `SkillIncreasePerRepair` : `float`
- `SkillIncreasePerSabotage` : `float`
- `SkillIncreasePerFabricatorRequiredSkill` : `float`
- `AssistantSkillIncreaseMultiplier` : `float`
- `MaximumOlympianSkill` : `float`
- `SkillIncreasePerRepairedStructureDamage` : `float`
- `SkillIncreasePerHostileDamage` : `float`
- `SkillIncreasePerSecondWhenSteering` : `float`
- `SkillIncreasePerSecondWhenOperatingTurret` : `float`
- `SkillIncreasePerFriendlyHealed` : `float`
- `SingleRoundSkillGainMultiplier` : `float`
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

