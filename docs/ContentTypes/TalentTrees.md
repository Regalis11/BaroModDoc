# TalentTrees

<sub>Relevant files: [Shared:TalentTree.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/Talents/TalentTree.cs) [Shared:TalentTreesFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/TalentTreesFile.cs)</sub>
- **Required by core package:** Yes

## Attributes


## Examples

### Example 1 - single talenttree

```xml
<talenttree
  identifier="mytalenttree" />
```

### Example 2 - multiple talenttrees

```xml
<talenttrees>
  <talenttree
    identifier="mytalenttree1" />
  <talenttree
    identifier="mytalenttree2" />
</talenttrees>
```

### Example 3 - overriding existing talenttrees

```xml
<override>
  <talenttree
    identifier="mytalenttree1" />
  <talenttree
    identifier="mytalenttree2" />
</override>
```

