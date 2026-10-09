# IEventActionRegistrar

```cs
public interface IEventActionRegistrar : IPluginService
```

Provides methods for registering custom event actions to be loaded in `ScriptedEvent`.

## Members

| Member                                                  | Description                                                                  |
|---------------------------------------------------------|------------------------------------------------------------------------------|
| [`RegisterEventAction<T>()`](#registereventaction)      | Registers a custom event action to be recognized by `ScriptedEvent`.         |
| [`RegisterEventActions()`](#registereventactions)       | Registers multiple custom event actions to be recognized by `ScriptedEvent`. |
| [`RegisterAllEventActions()`](#registeralleventactions) | Registers all types in the calling plugin that inherit `EventAction`.        |

## Methods

### RegisterEventAction

```cs
void RegisterEventAction<T>() where T : EventAction
```

Registers a custom event action to be recognized by `ScriptedEvent`.

| Type parameter | Description                                  |
|----------------|----------------------------------------------|
| `T`            | Implementation of `EventAction` to register. |

**Example**

MyEventAction.cs:

```csharp
public class MyEventAction(ScriptedEvent parentEvent, ContentXElement element) : EventAction(parentEvent, element)
{
    private bool isFinished;

    public override bool IsFinished(ref string goToLabel)
        => isFinished;

    public override void Reset()
        => isFinished = false;

    public override void Update(float deltaTime)
    {
        // Do something
        isFinished = true;
    }
}
```

Plugin init:

```csharp
IEventActionRegistrar.RegisterEventAction<MyEventAction>();
```

### RegisterEventActions

```cs
void RegisterEventActions(params Type[] types)
```

Registers multiple custom event actions to be recognized by `ScriptedEvent`.

| Parameter | Type     | Description                         |
|-----------|----------|-------------------------------------|
| `types`   | `Type[]` | Types of `EventAction` to register. |

| Exception           | Condition                                 |
|---------------------|-------------------------------------------|
| `ArgumentException` | if a type does not inherit `EventAction`. |

**Example**

MyEventAction.cs:

```csharp
public class MyEventAction(ScriptedEvent parentEvent, ContentXElement element) : EventAction(parentEvent, element)
{
    private bool isFinished;

    public override bool IsFinished(ref string goToLabel)
        => isFinished;

    public override void Reset()
        => isFinished = false;

    public override void Update(float deltaTime)
    {
        // Do something
        isFinished = true;
    }
}
```

Plugin init:

```csharp
IEventActionRegistrar.RegisterEventActions(typeof(MyEventAction), typeof(MyOtherEventAction));
```

### RegisterAllEventActions

```cs
void RegisterAllEventActions()
```

Registers all types in the calling plugin that inherit `EventAction`.

**Remarks**

The method will find all types in the calling assembly that inherit `EventAction` and register them using [`RegisterEventActions`](#registereventactions).

**Example**

Plugin init:

```csharp
IEventActionRegistrar.RegisterAllEventActions();
```
