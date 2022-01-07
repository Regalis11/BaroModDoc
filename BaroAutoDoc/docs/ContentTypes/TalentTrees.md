# TalentTrees

- **Required by core package:** Yes

## Examples

### Example 1 - single talenttree

```xml
<talenttree
  identifier="mytalenttree"
  TODO="add remaining attributes" />
```

### Example 2 - multiple talenttrees

```xml
<talenttrees>
  <talenttree
    identifier="mytalenttree1"
    TODO="add remaining attributes" />
  <talenttree
    identifier="mytalenttree2"
    TODO="add remaining attributes" />
</talenttrees>
```

### Example 3 - overriding existing talenttrees

```xml
<override>
  <talenttree
    identifier="mytalenttree1"
    TODO="add remaining attributes" />
  <talenttree
    identifier="mytalenttree2"
    TODO="add remaining attributes" />
</override>
```

