# Sounds

<sub>Relevant files: [Shared:SoundsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/SoundsFile.cs)</sub>
- **Required by core package:** Yes

## Attributes


**WARNING:** This file likely generated completely incorrectly!

## Examples

### Example 1 - single Sound

```xml
<Sound
  identifier="mySound" />
```

### Example 2 - multiple Sounds

```xml
<Sounds>
  <Sound
    identifier="mySound1" />
  <Sound
    identifier="mySound2" />
</Sounds>
```

### Example 3 - overriding existing Sounds

```xml
<override>
  <Sound
    identifier="mySound1" />
  <Sound
    identifier="mySound2" />
</override>
```

