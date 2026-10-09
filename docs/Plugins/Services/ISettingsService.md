# ISettingsService

```cs
public interface ISettingsService : IPluginService
```

Allows plugins to create their own settings that can be saved to disk, edited in game and networked.

## Members

| Member                                      | Description                                                                                                                                                                                                             |
|---------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [`ContentPackage`](#contentpackage)         | The content package that this settings service belongs to.                                                                                                                                                              |
| [`SaveFolder`](#savefolder)                 | A save folder dedicated to this plugin that can be used to store files.                                                                                                                                                 |
| [`OverrideUI`](#overrideui)                 | If not null, the default setting UI creation for this plugin is overriden and the specified delegate will be called instead. This is useful if you want to completely take control over the UI of your plugin settings. |
| [`RegisteredSettings`](#registeredsettings) | Returns all registered settings in this settings service.                                                                                                                                                               |
| [`RegisterSetting()`](#registersetting)     | Register a new setting that can be retrieved using the identifier.                                                                                                                                                      |
| [`RetrieveSetting<T>()`](#retrievesetting)  | Retrieve a setting by it's identifier.                                                                                                                                                                                  |
| [`Load()`](#load)                           | Load all settings.                                                                                                                                                                                                      |
| [`Save()`](#save)                           | Save all settings.                                                                                                                                                                                                      |

## Properties

### ContentPackage

```cs
ContentPackage ContentPackage { get; }
```

The content package that this settings service belongs to.

### SaveFolder

```cs
string SaveFolder { get; }
```

A save folder dedicated to this plugin that can be used to store files.

### OverrideUI

```cs
SettingOverrideUIDelegate? OverrideUI { get; set; }
```

*Only available on the client.*

If not null, the default setting UI creation for this plugin is overriden and the specified delegate will be called instead. This is useful if you want to completely take control over the UI of your plugin settings.

**Remarks**

Note that this means that [`ISetting.AddUI`](ISettingsService.md#addui) wont get called automatically for all settings.

### RegisteredSettings

```cs
IEnumerable<ISetting> RegisteredSettings { get; }
```

Returns all registered settings in this settings service.

## Methods

### RegisterSetting

```cs
void RegisterSetting(ISetting setting)
```

Register a new setting that can be retrieved using the identifier.

**Example**

```csharp
SettingsService.RegisterSetting(new FloatSetting($"foobar".ToIdentifier(), 5.0f, label: $"Foobar")
{
    ShowInUI = true,
    ToolTip = "This is foobar",
    LabelFunc = (x) => $"{MathF.Round(x)} foobar(s)",
    StepValue = 1f,
    Range = (0f, 100f),
    SyncMode = SettingSyncMode.ServerAuthority,
    IsAllowedToSet = c => c.Permissions.HasFlag(Barotrauma.Networking.ClientPermissions.ManageRound)
});
```

### RetrieveSetting

```cs
T? RetrieveSetting<T>(Identifier identifier) where T : ISetting
```

Retrieve a setting by it's identifier.

**Example**

```csharp
FloatSetting? setting = settingsService.RetrieveSetting<FloatSetting>($"foobar".ToIdentifier());
if (setting != null)
{
    setting.Set(200f);
    DebugConsole.NewMessage($"Value: {setting.Value}");
}
```

### Load

```cs
void Load()
```

Load all settings.

### Save

```cs
void Save()
```

Save all settings.

## Related types

### SettingOverrideUIDelegate

```cs
public delegate void SettingOverrideUIDelegate(GUIFrame content)
```

*Only available on the client.*

### SettingSyncMode

```cs
public enum SettingSyncMode
```

| Value                    | Description                                                                                                                                                                               |
|--------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `NoSync`                 | Does not sync between the client and server.                                                                                                                                              |
| `ClientPermissiveDesync` | The client is allowed to make changes BUT will not be synced to the server. Any changes made by the server are synced to clients.                                                         |
| `ServerAuthority`        | Clients that pass [`ISetting.IsAllowedToSet`](ISettingsService.md#isallowedtoset) are allowed to set this setting server-side. Any changes made by the server are also synced to clients. |

### SettingIsAllowedToSetDelegate

```cs
public delegate bool SettingIsAllowedToSetDelegate(ISetting setting, Client? client)
```

### ISetting

```cs
public interface ISetting
```

#### Identifier

```cs
Identifier Identifier { get; }
```

An identifier that needs to be unique per settings service

#### SyncMode

```cs
SettingSyncMode SyncMode { get; }
```

Determines how this setting should be synced in multiplayer, see [`SettingSyncMode`](ISettingsService.md#settingsyncmode) for more info.

#### IsAllowedToSet

```cs
SettingIsAllowedToSetDelegate IsAllowedToSet { get; }
```

Determines if this setting can be set, on the server-side it's used to verify if a client is indeed allowed to change this setting and on the client-side it's used to gray out the UI and prevent user interaction.

#### HasPendingChanges

```cs
bool HasPendingChanges { get; }
```

Determines if there's any pending changes in this setting.

#### ResetToDefault

```cs
void ResetToDefault()
```

Resets this setting back to it's default state.

#### Apply

```cs
void Apply()
```

Applies any pending changes in this setting.

#### Revert

```cs
void Revert()
```

Reverts the pending changes back to the previous setting.

#### IsDirty

```cs
bool IsDirty { get; set; }
```

Indicates that this setting was changed and must be propagated to all clients or the server.

#### ShowInUI

```cs
bool ShowInUI { get; set; }
```

Indicates whether this setting should be added to the settings UI automatically.

#### ShouldSave

```cs
bool ShouldSave()
```

Return false to skip saving this setting, used to avoid unnecessarily saving default values.

#### Load

```cs
void Load(XElement element)
```

#### Save

```cs
void Save(XElement element)
```

#### WriteMessage

```cs
abstract void WriteMessage(IWriteMessage writeMessage)
```

#### ReadMessage

```cs
abstract void ReadMessage(IReadMessage readMessage)
```

#### AddUI

```cs
void AddUI(GUIFrame content)
```

*Only available on the client.*

Called by settings UI to add any UI this setting has to the GUIList content.
