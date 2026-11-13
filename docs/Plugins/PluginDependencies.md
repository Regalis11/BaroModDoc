# Plugin dependencies

A plugin can use the code of another mod's plugin, for example to call its methods or use its types. This page explains how to set that up.

## Declaring the dependency

First, declare a [content package dependency](../Intro/ContentPackageDependencies.md) on the other mod in your mod's `ContentPackageBuilder/filelist.xml`:

```xml
<ContentPackage name="%ModName%">
  <Dependency identifier="othermod" names="Other Mod" ids="1234567890" order="Above" />
  <PluginInfo file="%ModDir%/PluginInfo.xml" />
</ContentPackage>
```

This is required. Each plugin is loaded separately, and the game only lets your plugin use another plugin's assemblies if your content package depends on it.

Use `order="Above"` so your mod is placed above the other mod in the mod list. Mods load from the bottom of the list up, so this makes sure the other plugin is already loaded when yours loads.

## Referencing the other plugin

To use the other plugin's types in your code, reference its assemblies in your project. The client and the server builds of a plugin are separate assemblies (`<ModName>Client.dll` and `<ModName>Server.dll`), so add the reference to both `ClientProject/ClientItems.props` and `ServerProject/ServerItems.props`, pointing to the matching build:

```xml
<!-- ClientProject/ClientItems.props -->
<ItemGroup>
  <Reference Include="OtherModClient">
    <HintPath>$(ModDeployDir)\OtherMod\$(PlatformName)\Client\OtherModClient.dll</HintPath>
    <Private>false</Private>
  </Reference>
</ItemGroup>
```

```xml
<!-- ServerProject/ServerItems.props -->
<ItemGroup>
  <Reference Include="OtherModServer">
    <HintPath>$(ModDeployDir)\OtherMod\$(PlatformName)\Server\OtherModServer.dll</HintPath>
    <Private>false</Private>
  </Reference>
</ItemGroup>
```

The paths above assume the other mod uses the plugin template's folder layout and is installed in your `LocalMods`. Make sure to adjust them to point where its assemblies actually are.

`<Private>false</Private>` keeps the other plugin's assembly out of your mod's files. Don't ship a copy of it with your mod, since that would cause the game load your own copy, which would cause issues.

## Optional dependencies

If the other mod is an optional dependency, your plugin has to keep working when it's missing. You can check for it with [`IContentPackageRetrievalService`](Services/IContentPackageRetrievalService.md) before using it:

```cs
public void Init()
{
    if (ContentPackageRetrieval.IsDependencyPresent("othermod"))
    {
        OtherModIntegration.Init();
    }
}
```