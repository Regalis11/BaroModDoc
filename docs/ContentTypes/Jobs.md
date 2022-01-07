# Jobs

<sup>Relevant files: [Shared:JobsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/JobsFile.cs)</sup>

**WARNING:** This file likely generated completely incorrectly!

- **Required by core package:** Yes

## Attributes



## Examples

### Example 1 - single Job

```xml
<Job
  identifier="myJob" />
```

### Example 2 - multiple Jobs

```xml
<Jobs>
  <Job
    identifier="myJob1" />
  <Job
    identifier="myJob2" />
</Jobs>
```

### Example 3 - overriding existing Jobs

```xml
<override>
  <Job
    identifier="myJob1" />
  <Job
    identifier="myJob2" />
</override>
```

