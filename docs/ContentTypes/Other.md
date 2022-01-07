# Other

<sup>Relevant files: [Shared:OtherFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/OtherFile.cs)</sup>
- **Required by core package:** No
- **Alternate names:** None

## Attributes


**WARNING:** This file likely generated completely incorrectly!

## Examples

### Example 1 - single Other

```xml
<Other
  identifier="myOther" />
```

### Example 2 - multiple Others

```xml
<Others>
  <Other
    identifier="myOther1" />
  <Other
    identifier="myOther2" />
</Others>
```

### Example 3 - overriding existing Others

```xml
<override>
  <Other
    identifier="myOther1" />
  <Other
    identifier="myOther2" />
</override>
```

