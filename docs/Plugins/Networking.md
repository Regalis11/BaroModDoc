# Networking

Plugins send their own data between the server and clients with the [`IGameNetwork`](Services/IGameNetwork.md) service. This page shows you how to write some basic code to send messages and sync over the network in multiplayer.

## Authoritative server-client network model

Barotrauma uses an authoritative server-client network model, meaning that in multiplayer you have a server and multiple client instances that connect to that server, with the server holding the "true" state of the game and having the final say in every action.

This means that net messages must always go to the server or originate from the server. A client can't send net messages directly to other clients.

## Singleplayer, client and server

Your plugin's code can run in three situations:

| Situation | Build that runs | `GameMain.NetworkMember` |
|---|---|---|
| Singleplayer | Client| `null`|
| Multiplayer, on a client | Client | `IsClient` is `true` |
| Multiplayer, on the server | Server | `IsServer` is `true` |

In multiplayer, the server is always a separate server process, even when a player hosts the game from their own client. That player's game is a client like any other.

In singleplayer, only the client build runs and there is no server. Code inside `#if SERVER` never runs, and `Send` and `Broadcast` do nothing. So don't put game logic only in `#if SERVER` blocks, or it won't work in singleplayer. Put it in shared code, and skip it on multiplayer clients if it's something that should only run on whoever has authority over the logic.

```cs
public void DoSomething()
{
    if (GameMain.NetworkMember is { IsClient: true })
    {
        return;
    }

    // Runs in singleplayer and on the server
    // ...change the game state...

#if SERVER
    // Tell the clients about the change
    GameNetwork.Broadcast(FooBarHeader.FooBar, new FooBar(42, true));
#endif
}
```

The [item component example](#item-components) below follows the same pattern.

## Sending network messages

To send network messages in your plugin, you must first create an enum containing the different types of messages your plugin can send:

```cs
public enum FooBarHeader 
{ 
    FooBar, 
    RequestFooBar 
}
```

Then to use this you also need to define the data that the network message will contain:

```cs
[NetworkSerialize]
public readonly record struct FooBar(float Foo, bool Bar) : INetSerializableStruct;

[NetworkSerialize]
public readonly record struct RequestFooBar() : INetSerializableStruct;
```

And then to receive these net messages and send them:

```cs
public static readonly IGameNetwork GameNetwork = PluginServiceProvider.GetService<IGameNetwork>();

public void Init()
{
    GameNetwork.RegisterNetworkHeaders<FooBarHeader>();
#if SERVER
    GameNetwork.RegisterHandler<FooBarHeader, RequestFooBar>(FooBarHeader.RequestFooBar, OnFooBarRequested);
#elif CLIENT
    GameNetwork.RegisterHandler<FooBarHeader, FooBar>(FooBarHeader.FooBar, OnFooBarReceived);
#endif
}


#if SERVER
private static void OnFooBarRequested(RequestFooBar request, Client client)
{
    if (!client.HasPermission(ClientPermissions.ManageRound)) 
    { 
        return; 
    }
    
    DebugConsole.Log($"FooBar requested by {client.Name}");

    GameNetwork.Broadcast(FooBarHeader.FooBar, new FooBar(42, true));
}
#endif

#if CLIENT
private static void OnFooBarReceived(FooBar data)
{
    DebugConsole.Log($"FooBar received {data.Foo} {data.Bar}");
}

// Call this on the client to ask the server for a FooBar
public static void RequestFooBarFromServer()
{
    GameNetwork.Send(FooBarHeader.RequestFooBar, new RequestFooBar());
}
#endif

```

The server sends with `Broadcast` or `SendToClient` (for specific clients), and clients send with `Send`.

### Network serializable structs

`[NetworkSerialize]` on a struct serializes all its fields and properties. You can also put it on individual members, with settings such as `MinValueInt`/`MaxValueInt`, `MinValueFloat`/`MaxValueFloat`, `NumberOfBits` and `ArrayMaxSize` to make the message smaller. Supported types are:

- the numeric types, `bool` and `string`
- `Identifier`, `Color`, `Vector2` and `AccountId`
- enums
- other `INetSerializableStruct` types
- arrays and `ImmutableArray<T>`
- `Option<T>` and nullable structs

To refer to an item or a character, send its `ID` and look it up with `Entity.FindEntityByID`.

## Clients joining mid-round

If you ever need to sync state to mid-round joining clients, you can use the `PluginClientJoinedRoundDelegate` [hook](Services/Hooks.md):

```cs
public static readonly ISimpleHookService HookService = PluginServiceProvider.GetService<ISimpleHookService>();

// In Init()
#if SERVER
HookService.RegisterHook<PluginClientJoinedRoundDelegate>(client =>
    GameNetwork.SendToClient(client, FooBarHeader.FooBar, new FooBar(42, true)));
#endif
```

`PluginClientDisconnectedDelegate` (server) and `PluginMidRoundSyncReceiveCompletedDelegate` (client) are also useful here.

## Item components

A [custom item component](CreatingItemComponents.md) can implement `IServerSerializable` and/or `IClientSerializable` to allow the component to emit network events.

```cs
public class ChargeCounter : ItemComponent, IServerSerializable
{
    [Serialize(0, IsPropertySaveable.Yes, "How many charges the item has.")]
    public int Charges { get; set; }

    public ChargeCounter(Item item, ContentXElement element) : base(item, element) { }

    public void AddCharge()
    {
        if (GameMain.NetworkMember is { IsClient: true }) 
        { 
            return; 
        }

        Charges++;
#if SERVER
        item.CreateServerEvent(this);
#endif
    }

#if SERVER
    public void ServerEventWrite(IWriteMessage msg, Client c, NetEntityEvent.IData? extraData = null)
    {
        msg.WriteInt32(Charges);
    }
#elif CLIENT
    public void ClientEventRead(IReadMessage msg, float sendingTime)
    {
        Charges = msg.ReadInt32();
    }
#endif
}
```

`ServerEventWrite` is called when the event is sent, so it always writes the current state, and clients that join mid-round receive it too. `ClientEventRead` must read exactly what was written. For requests from the client, implement `IClientSerializable` the same way (`ClientEventWrite` and `ServerEventRead`, sent with `item.CreateClientEvent(this)`).
