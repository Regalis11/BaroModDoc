# ISimpleHookService

```cs
public interface ISimpleHookService : IPluginService
```

Allows plugins to register callback functions that get called at specific points in the game.

## Members

| Member                                   | Description                                                                                |
|------------------------------------------|--------------------------------------------------------------------------------------------|
| [`RegisterHook<T>()`](#registerhook)     | Registers a hook that gets called at a specific point in the game.                         |
| [`DeregisterHook<T>()`](#deregisterhook) | Deregister a hook that was previously registered using [`RegisterHook<T>`](#registerhook). |

## Methods

### RegisterHook

```cs
void RegisterHook<T>(T callback) where T : Delegate
```

Registers a hook that gets called at a specific point in the game.

| Type parameter | Description                                         |
|----------------|-----------------------------------------------------|
| `T`            | The delegate type of the hook you want to register. |

| Parameter  | Type | Description                                                                    |
|------------|------|--------------------------------------------------------------------------------|
| `callback` | `T`  | A method reference, Action, Func or delegate to the hook you want to register. |

**Remarks**

Make sure to specify the correct delegate type for the hook you want to register. A list of available delegate types can be found in SharedSource\Plugins\Services\HookDelegates.cs.

**Example**

To register a hook, you need to specify the delegate type and the method you want to call.

```csharp
ISimpleHookService.RegisterHook<PluginOnPreUpdateDelegate>(MyOnPreUpdate);

public static void MyOnPreUpdate(float deltaTime) { /* Do something */ }
```

You may also use a lambda expression to register a hook:

```csharp
ISimpleHookService.RegisterHook<PluginOnPreUpdateDelegate>(deltaTime => { /* Do something */ });
```

Using a lambda this way doesn't allow you to manually deregister the hook on your own using [`DeregisterHook<T>`](#deregisterhook) unless you store a reference to the lambda in a variable, but at that point you might as well use a method. Regardless the game will still automatically deregister everything when your plugin is unloaded.

### DeregisterHook

```cs
void DeregisterHook<T>(T callback) where T : Delegate
```

Deregister a hook that was previously registered using [`RegisterHook<T>`](#registerhook).

| Type parameter | Description                                       |
|----------------|---------------------------------------------------|
| `T`            | Delegate type of the hook you want to deregister. |

| Parameter  | Type | Description                                            |
|------------|------|--------------------------------------------------------|
| `callback` | `T`  | A method reference to the hook you want to deregister. |

**Remarks**

You do not need to manually deregister hooks when your plugin is unloaded, this is done automatically.

**Example**

To deregister a hook:

```csharp
ISimpleHookService.DeregisterHook(MyOnPreUpdate);
```

You may omit the generic type as the reference to the callback is used to find and deregister it.
