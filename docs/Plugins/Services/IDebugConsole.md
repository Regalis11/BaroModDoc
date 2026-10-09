# IDebugConsole

```cs
public interface IDebugConsole : IPluginService
```

Provides an interface to `DebugConsole` for plugins. Allows registering commands and writing messages to F3 debug console.

## Members

| Member                                      | Description                                                                                                                                                                                                                                              |
|---------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [`NewMessage()`](#newmessage)               | Writes a standard message to the debug console. Use for general information messages or as responses to commands.                                                                                                                                        |
| [`ThrowError()`](#throwerror)               | Writes an error message to the debug console. The message will always be displayed in red.                                                                                                                                                               |
| [`AddWarning()`](#addwarning)               | Writes a warning message to the debug console. The message will always be displayed in yellow.                                                                                                                                                           |
| [`RegisterCommand()`](#registercommand)     | Registers a command with the debug console.                                                                                                                                                                                                              |
| [`StartTestMaps()`](#starttestmaps)         | Starts the map testing coroutine used by the testmap/testmaps commands. Generates maps with validation, and optionally calls callbacks for each generated map. Useful for plugins that want to run additional analysis or simulations on generated maps. |
| [`DeregisterCommand()`](#deregistercommand) | Unregisters a command from the debug console.                                                                                                                                                                                                            |
| [`RegisterCommand()`](#registercommand-1)   | Registers a command with the debug console.                                                                                                                                                                                                              |

## Methods

### NewMessage

```cs
void NewMessage(string message, Color? color = null)
```

Writes a standard message to the debug console. Use for general information messages or as responses to commands.

| Parameter | Type     | Description                                                                                        |
|-----------|----------|----------------------------------------------------------------------------------------------------|
| `message` | `string` | Message to write, the message will be prefixed with the plugin and content package name.           |
| `color`   | `Color?` | Optional color to display the message in. If not provided, the message will be displayed in white. |

**Example**

```csharp
[ContentPackageName][PluginName] This is a message.
```

### ThrowError

```cs
void ThrowError(string message, Exception? exception = null)
```

Writes an error message to the debug console. The message will always be displayed in red.

| Parameter   | Type         | Description                                                                                                                                                                                                                             |
|-------------|--------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `message`   | `string`     | Message to write; the message will be prefixed with the plugin and content package name.                                                                                                                                                |
| `exception` | `Exception?` | Optional exception to include in the message. If provided, the exception message and stack trace will be appended to the end of the message and the contents of the error can be copied to the clipboard by right-clicking the message. |

### AddWarning

```cs
void AddWarning(string message)
```

Writes a warning message to the debug console. The message will always be displayed in yellow.

| Parameter | Type     | Description                                                                                              |
|-----------|----------|----------------------------------------------------------------------------------------------------------|
| `message` | `string` | Message to write; the message will be prefixed with "WARNING", the plugin name and content package name. |

**Example**

```csharp
[ContentPackageName][PluginName] WARNING: This is a warning message
```

### RegisterCommand

```cs
void RegisterCommand(
    string command,
    string helpMessage,
    CommandFlags flags,
    OnCommandExecutedDelegate onCommandExecuted,
    Func<string[][]>? getValidArgs = null)
```

*Only available on the client.*

Registers a command with the debug console.

| Parameter           | Type                        | Description                                                                                                            |
|---------------------|-----------------------------|------------------------------------------------------------------------------------------------------------------------|
| `command`           | `string`                    | The name of the command to register. Use '\|' to register multiple aliases for the same command.                       |
| `helpMessage`       | `string`                    | A brief description of what the command does that is shown when using the "help" command.                              |
| `flags`             | `CommandFlags`              | [`CommandFlags`](IDebugConsole.md#commandflags) that modify the behavior of the command.                               |
| `onCommandExecuted` | `OnCommandExecutedDelegate` | [`OnCommandExecutedDelegate`](IDebugConsole.md#oncommandexecuteddelegate) that is called when the command is executed. |
| `getValidArgs`      | `Func<string[][]>?`         | If set, the function will be used to return vaild arguments (autocompletions) for the command.                         |

**Example**

```csharp
IDebugConsole.RegisterCommand(
    command: "hello|helloworld",
    helpMessage: "Prints 'Hello, World!' to the console.",
    flags: CommandFlags.None,
    onCommandExecuted: args => DebugConsole.NewMessage("Hello, World!"));
```

### StartTestMaps

```cs
void StartTestMaps(
    string? fixedSeed = null,
    int? amount = null,
    Action<string, Map, CampaignMode>? onMapGenerated = null,
    Action? onBatchFinished = null)
```

Starts the map testing coroutine used by the testmap/testmaps commands. Generates maps with validation, and optionally calls callbacks for each generated map. Useful for plugins that want to run additional analysis or simulations on generated maps.

| Parameter         | Type                                 | Description                                                            |
|-------------------|--------------------------------------|------------------------------------------------------------------------|
| `fixedSeed`       | `string?`                            | If set, uses this seed instead of generating random ones.              |
| `amount`          | `int?`                               | Number of maps to test. If null, runs until cancelled (by pressing C). |
| `onMapGenerated`  | `Action<string, Map, CampaignMode>?` | Called for each generated map.                                         |
| `onBatchFinished` | `Action?`                            | Called once after all maps have been tested.                           |

### DeregisterCommand

```cs
void DeregisterCommand(string command)
```

Unregisters a command from the debug console.

| Parameter | Type     | Description                                                                                                                                                            |
|-----------|----------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `command` | `string` | One of the aliases of the command to unregister or the '\|' separated list of aliases that was used to register the command via [`RegisterCommand`](#registercommand). |

**Remarks**

Only the plugin that registered the command can unregister it, so you do not need to worry about accidentally unregistering a command from another plugin with the same name.

**Example**

```csharp
IDebugConsole.UnregisterCommand("hello");
IDebugConsole.UnregisterCommand("helloworld");
IDebugConsole.UnregisterCommand("hello|helloworld");
```

### RegisterCommand

```cs
void RegisterCommand(
    string command,
    string helpMessage,
    CommandFlags flags,
    OnCommandExecutedDelegate onCommandExecuted,
    OnCommandExecutedByClientDelegate? onCommandExecutedByClient = null,
    Func<string[][]>? getValidArgs = null)
```

*Only available on the server.*

Registers a command with the debug console.

| Parameter                   | Type                                 | Description                                                                                                                                            |
|-----------------------------|--------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------|
| `command`                   | `string`                             | The name of the command to register. Use '\|' to register multiple aliases for the same command.                                                       |
| `helpMessage`               | `string`                             | A brief description of what the command does that is shown when using the "help" command.                                                              |
| `flags`                     | `CommandFlags`                       | [`CommandFlags`](IDebugConsole.md#commandflags) that modify the behavior of the command.                                                               |
| `onCommandExecuted`         | `OnCommandExecutedDelegate`          | [`OnCommandExecutedDelegate`](IDebugConsole.md#oncommandexecuteddelegate) that is called when the command is executed.                                 |
| `onCommandExecutedByClient` | `OnCommandExecutedByClientDelegate?` | [`OnCommandExecutedByClientDelegate`](IDebugConsole.md#oncommandexecutedbyclientdelegate) that is called when a client requests executing the command. |
| `getValidArgs`              | `Func<string[][]>?`                  | If set, the function will be used to return vaild arguments (autocompletions) for the command.                                                         |

**Example**

```csharp
IDebugConsole.RegisterCommand(
    command: "hello|helloworld",
    helpMessage: "Prints 'Hello, World!' to the console.",
    flags: CommandFlags.None,
    onCommandExecuted: args => DebugConsole.NewMessage("Hello, World!"),
    onCommandExecutedByClient: (client, cursorPos, args) => { GameMain.Server.SendChatMessage($"Hello, {client.Name}!", ChatMessageType.Console); });
```

## Related types

### CommandFlags

```cs
public enum CommandFlags
```

| Value                | Description                                                                                                                                                        |
|----------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `None`               |                                                                                                                                                                    |
| `IsCheat`            | Causes the command to only be executable if "enablecheats" command is used.                                                                                        |
| `DoNotRelayToServer` | Enabling this flag will cause the command to be executed on the client as-is in multiplayer instead of the default behavior of relaying the command to the server. |

### OnCommandExecutedDelegate

```cs
public delegate void OnCommandExecutedDelegate(string[] args)
```

Delegate that is called when a command is executed.

**Example**

Executing

```csharp
myCommand foo bar "baz qux"
```

will result in `args` being `["foo", "bar", "baz qux"]`.

| Parameter | Type       | Description                               |
|-----------|------------|-------------------------------------------|
| `args`    | `string[]` | Array of arguments passed to the command. |

### OnCommandExecutedByClientDelegate

```cs
public delegate void OnCommandExecutedByClientDelegate(Client client, Vector2 cursorPos, string[] args)
```

*Only available on the server.*

Delegate that is called server-side when a client requests executing it.

| Parameter   | Type       | Description                               |
|-------------|------------|-------------------------------------------|
| `client`    | `Client`   | The client executing the command.         |
| `cursorPos` | `Vector2`  |                                           |
| `args`      | `string[]` | Array of arguments passed to the command. |
