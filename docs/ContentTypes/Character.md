# Character

<sub>Relevant files: [Shared:CharacterFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/CharacterFile.cs) [Shared:CharacterPrefab.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/CharacterPrefab.cs)</sub>
- **Required by core package:** Yes

## Attributes


**WARNING:** This file likely generated completely incorrectly!

## Examples

### Example 1 - single Character

```xml
<Character
  identifier="myCharacter" />
```

### Example 2 - multiple Characters

```xml
<Characters>
  <Character
    identifier="myCharacter1" />
  <Character
    identifier="myCharacter2" />
</Characters>
```

### Example 3 - overriding existing Characters

```xml
<override>
  <Character
    identifier="myCharacter1" />
  <Character
    identifier="myCharacter2" />
</override>
```

