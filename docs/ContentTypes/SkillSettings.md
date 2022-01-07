# SkillSettings

<sup>Relevant files: [Shared:SkillSettingsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/SkillSettingsFile.cs) [Shared:SkillSettings.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/SkillSettings.cs)</sup>

**WARNING:** This file likely generated completely incorrectly!

- **Required by core package:** No

## Attributes

- `SingleRoundSkillGainMultiplier` : `float`
- `SkillIncreasePerRepair` : `float`
- `SkillIncreasePerSabotage` : `float`
- `SkillIncreasePerCprRevive` : `float`
- `SkillIncreasePerRepairedStructureDamage` : `float`
- `SkillIncreasePerSecondWhenSteering` : `float`
- `SkillIncreasePerFabricatorRequiredSkill` : `float`
- `SkillIncreasePerHostileDamage` : `float`
- `SkillIncreasePerSecondWhenOperatingTurret` : `float`
- `SkillIncreasePerFriendlyHealed` : `float`
- `AssistantSkillIncreaseMultiplier` : `float`
- `MaximumOlympianSkill` : `float`

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

