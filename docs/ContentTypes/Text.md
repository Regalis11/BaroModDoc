# Text

<sub>Relevant files: [Shared:TextFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/TextFile.cs) [Shared:TextPack.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource\Text\TextPack.cs)</sub>
- **Required by core package:** No

## Attributes


**WARNING:** This file likely generated completely incorrectly!

## Examples

### Example 1 - single Text

```xml
<Text
  identifier="myText" />
```

### Example 2 - multiple Texts

```xml
<Texts>
  <Text
    identifier="myText1" />
  <Text
    identifier="myText2" />
</Texts>
```

### Example 3 - overriding existing Texts

```xml
<override>
  <Text
    identifier="myText1" />
  <Text
    identifier="myText2" />
</override>
```

