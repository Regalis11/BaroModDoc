# IContentPackageRetrievalService

```cs
public interface IContentPackageRetrievalService : IPluginService
```

Provides a way for plugins to get their own Content Package and check for dependencies.

## Example

```xml
<ContentPackage name="foobar">
  <Dependency identifier="examplemod" Names="ExampleMod" />
</ContentPackage>
```

```csharp
ContentPackage contentPackage = IContentPackageRetrievalService.ContentPackage;
```

```csharp
if (IContentPackageRetrievalService.IsDependencyPresent("examplemod"))
{
    // Do something
}
```

## Members

| Member                                          | Description                                                                        |
|-------------------------------------------------|------------------------------------------------------------------------------------|
| [`ContentPackage`](#contentpackage)             | Content Package that loaded your Plugin Assembly.                                  |
| [`IsDependencyPresent()`](#isdependencypresent) | Checks if a content package dependency is present using the dependency identifier. |

## Properties

### ContentPackage

```cs
ContentPackage ContentPackage { get; }
```

Content Package that loaded your Plugin Assembly.

## Methods

### IsDependencyPresent

```cs
bool IsDependencyPresent(Identifier dependencyIdentifier)
```

Checks if a content package dependency is present using the dependency identifier.
