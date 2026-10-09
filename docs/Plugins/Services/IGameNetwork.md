# IGameNetwork

```cs
public interface IGameNetwork : IPluginService
```

Provides a service to allow client and server side network communication in multiplayer.

## Members

| Member                                                                             | Description                                                                                                                                                                                                                     |
|------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [`RegisterNetworkHeaders<TEnum>()`](#registernetworkheaders)                       | Registers the header enum to be used for network communication.                                                                                                                                                                 |
| [`RegisterAllStaticHandlers()`](#registerallstatichandlers)                        | Will scan the entire assembly for static methods with the `NetworkHandlerAttribute<T>` attribute and register them as network handlers.                                                                                         |
| [`RegisterHandlers<T>()`](#registerhandlers)                                       | Will scan the object instance for methods with the `NetworkHandlerAttribute<T>` attribute and register them as network handlers.                                                                                                |
| [`CreateEntityEvent<TSerializable>()`](#createentityevent)                         | Creates a network event for an entity. Write and read methods for the entity event must be registered via the respective EntityClientWrite, EntityClientRead, EntityServerWrite and EntityServerRead methods in this interface. |
| [`CreateVanillaEntityEvent<TSerializable>()`](#createvanillaentityevent)           | Creates a network event using the vanilla entity write and read methods. Useful for example if you are editing a state of a vanilla entity that doesn't automatically create entity events.                                     |
| [`RegisterCustomEntityEventHandler<TEntity>()`](#registercustomentityeventhandler) | Registers ClientWrite, ClientRead, ServerWrite and ServerRead entity event handlers for a specific entity.                                                                                                                      |
| [`RegisterEntityClientWrite<TSerializable>()`](#registerentityclientwrite)         | Registers a client side event write handler for an entity.                                                                                                                                                                      |
| [`RegisterEntityClientRead<TSerializable>()`](#registerentityclientread)           | Registers a client side event read handler for an entity.                                                                                                                                                                       |
| [`RegisterHandler<TEnum, TData>()`](#registerhandler)                              | Manually register a client side handler for a specific network header.                                                                                                                                                          |
| [`Send<TEnum, TData>()`](#send)                                                    | Sends data to the server.                                                                                                                                                                                                       |
| [`RegisterEntityServerWrite<TSerializable>()`](#registerentityserverwrite)         | Registers a server side event write handler for an entity.                                                                                                                                                                      |
| [`RegisterEntityServerRead<TSerializable>()`](#registerentityserverread)           | Registers a server side event read handler for an entity.                                                                                                                                                                       |
| [`RegisterHandler<TEnum, TData>()`](#registerhandler-1)                            | Manually register a server side handler for a specific network header.                                                                                                                                                          |
| [`Broadcast<TEnum, TData>()`](#broadcast)                                          | Sends data to all connected clients.                                                                                                                                                                                            |
| [`SendToClient<TEnum, TData>()`](#sendtoclient)                                    | Sends data to a specific client.                                                                                                                                                                                                |

## Methods

### RegisterNetworkHeaders

```cs
void RegisterNetworkHeaders<TEnum>() where TEnum : Enum
```

Registers the header enum to be used for network communication.

| Type parameter | Description            |
|----------------|------------------------|
| `TEnum`        | Enum type to register. |

| Exception                   | Condition                                        |
|-----------------------------|--------------------------------------------------|
| `InvalidOperationException` | If an enum is already registered for the plugin. |

**Example**

```csharp
public enum MyNetworkHeaders
{
    ExampleHeader1,
    ExampleHeader2
}
```

```csharp
IGameNetwork.RegisterNetworkHeaders<MyNetworkHeaders>();
```

### RegisterAllStaticHandlers

```cs
void RegisterAllStaticHandlers()
```

Will scan the entire assembly for static methods with the `NetworkHandlerAttribute<T>` attribute and register them as network handlers.

### RegisterHandlers

```cs
void RegisterHandlers<T>(T module)
```

Will scan the object instance for methods with the `NetworkHandlerAttribute<T>` attribute and register them as network handlers.

| Parameter | Type | Description                                |
|-----------|------|--------------------------------------------|
| `module`  | `T`  | Object instance to register handlers from. |

**Example**

Create a class that contains network handlers:

```csharp
public class MyNetworkHandlers
{
    [NetworkHandler<MyNetworkHeaders>(MyNetworkHeaders.ExampleHeader1)]
    public void ExampleHandler1(MyData data)
    {
        // read data from MyData
    }
}
```

Static field:

```csharp
public static readonly MyNetworkHandlers MyHandlers = new();
```

Plugin init:

```csharp
IGameNetwork.RegisterHandlers(MyHandlers);
```

### CreateEntityEvent

```cs
void CreateEntityEvent<TSerializable>(
    TSerializable serializable,
    NetEntityEvent.IData? data) where TSerializable : INetSerializable
```

Creates a network event for an entity. Write and read methods for the entity event must be registered via the respective EntityClientWrite, EntityClientRead, EntityServerWrite and EntityServerRead methods in this interface.

| Type parameter  | Description                                  |
|-----------------|----------------------------------------------|
| `TSerializable` | Entity type implementing `INetSerializable`. |

| Parameter      | Type                    | Description                                                        |
|----------------|-------------------------|--------------------------------------------------------------------|
| `serializable` | `TSerializable`         | Entity to create the event for.                                    |
| `data`         | `NetEntityEvent.IData?` | Extra data to pass to the entity event. Can be null if not needed. |

**Example**

```csharp
item.GetExtraField<MyCustomData>("CustomData").Foo = Bar;
IGameNetwork.CreateEntityEvent(item);
```

### CreateVanillaEntityEvent

```cs
void CreateVanillaEntityEvent<TSerializable>(
    TSerializable serializable,
    NetEntityEvent.IData? data) where TSerializable : INetSerializable
```

Creates a network event using the vanilla entity write and read methods. Useful for example if you are editing a state of a vanilla entity that doesn't automatically create entity events.

| Type parameter  | Description                                  |
|-----------------|----------------------------------------------|
| `TSerializable` | Entity type implementing `INetSerializable`. |

| Parameter      | Type                    | Description                                                        |
|----------------|-------------------------|--------------------------------------------------------------------|
| `serializable` | `TSerializable`         | Entity to create the event for.                                    |
| `data`         | `NetEntityEvent.IData?` | Extra data to pass to the entity event. Can be null if not needed. |

### RegisterCustomEntityEventHandler

```cs
void RegisterCustomEntityEventHandler<TEntity>(
    ICustomSerializable<TEntity> handler) where TEntity : IClientSerializable, IServerSerializable
```

Registers ClientWrite, ClientRead, ServerWrite and ServerRead entity event handlers for a specific entity.

| Type parameter | Description                                                                 |
|----------------|-----------------------------------------------------------------------------|
| `TEntity`      | Entity that registers both `IClientSerializable` and `IServerSerializable`. |

| Parameter | Type                           | Description                                                                      |
|-----------|--------------------------------|----------------------------------------------------------------------------------|
| `handler` | `ICustomSerializable<TEntity>` | Instance of a custom entity event handler implementing `ICustomSerializable<T>`. |

**Example**

Client project:

```csharp
public class CustomItemHandler : ICustomSerializable<Item>
{
    public void ClientWrite(Item entity, IWriteMessage msg, NetEntityEvent.IData? extraData)
    {
        CustomData data = item.GetExtraField<MyCustomData>("CustomData");
        msg.WriteInt32(data.Foo);
    }

    public void ClientRead(Item entity, IReadMessage msg, float sendingTime)
    {
        CustomData data = item.GetExtraField<MyCustomData>("CustomData");
        data.Foo = msg.ReadInt32();
    }
{
```

Server project:

```csharp
public class CustomItemHandler : ICustomSerializable<Item>
{
    public void ServerWrite(Item entity, IWriteMessage msg, Client client, NetEntityEvent.IData? extraData)
    {
        CustomData data = item.GetExtraField<MyCustomData>("CustomData");
        msg.WriteInt32(data.Foo);
    }

    public void ServerRead(Item entity, IReadMessage msg, Client client)
    {
        CustomData data = item.GetExtraField<MyCustomData>("CustomData");
        data.Foo = msg.ReadInt32();
    }
}
```

Register the handler in shared project:

```csharp
IGameNetwork.RegisterCustomEntityEventHandler<Item>(new CustomItemHandler());
```

### RegisterEntityClientWrite

```cs
void RegisterEntityClientWrite<TSerializable>(
    EntityEventClientWriteDelegate<TSerializable> handler) where TSerializable : IClientSerializable
```

*Only available on the client.*

Registers a client side event write handler for an entity.

| Type parameter  | Description                                                                        |
|-----------------|------------------------------------------------------------------------------------|
| `TSerializable` | Entity type implementing `IClientSerializable` we are registering the handler for. |

| Parameter | Type                                            | Description                                                 |
|-----------|-------------------------------------------------|-------------------------------------------------------------|
| `handler` | `EntityEventClientWriteDelegate<TSerializable>` | Method or delegate to call when an entity event is created. |

**Remarks**

The server must have corresponding ServerRead handler registered for the entity.

**Example**

```csharp
IGameNetwork.RegisterEntityClientWrite<Item>((item, msg, extraData) =>
{
    CustomData data = item.GetExtraField<MyCustomData>("CustomData");
    msg.WriteInt32(data.Foo);
});
```

or with a method:

```csharp
IGameNetwork.RegisterEntityClientWrite<Item>(WriteItem);
```

```csharp
public void WriteItem(Item item, IWriteMessage msg, NetEntityEvent.IData? extraData)
{
    CustomData data = item.GetExtraField<MyCustomData>("CustomData");
    msg.WriteInt32(data.Foo);
}
```

### RegisterEntityClientRead

```cs
void RegisterEntityClientRead<TSerializable>(
    EntityEventClientReadDelegate<TSerializable> handler) where TSerializable : IServerSerializable
```

*Only available on the client.*

Registers a client side event read handler for an entity.

| Type parameter  | Description                                                                        |
|-----------------|------------------------------------------------------------------------------------|
| `TSerializable` | Entity type implementing `IServerSerializable` we are registering the handler for. |

| Parameter | Type                                           | Description                                                  |
|-----------|------------------------------------------------|--------------------------------------------------------------|
| `handler` | `EntityEventClientReadDelegate<TSerializable>` | Method or delegate to call when an entity event is received. |

**Remarks**

The server should have corresponding ServerWrite handler registered for the entity.

**Example**

```csharp
IGameNetwork.RegisterEntityClientRead<Item>((item, msg, sendingTime) =>
{
    CustomData data = item.GetExtraField<MyCustomData>("CustomData");
    data.Foo = msg.ReadInt32();
});
```

or with a method:

```csharp
IGameNetwork.RegisterEntityClientRead<Item>(ReadItem);
```

```csharp
public void ReadItem(Item item, IReadMessage msg, float sendingTime)
{
    CustomData data = item.GetExtraField<MyCustomData>("CustomData");
    data.Foo = msg.ReadInt32();
}
```

### RegisterHandler

```cs
void RegisterHandler<TEnum, TData>(
    TEnum header,
    ClientNetworkHandlerDelegate<TData> handler) where TEnum : Enum where TData : INetSerializableStruct
```

*Only available on the client.*

Manually register a client side handler for a specific network header.

| Type parameter | Description                                                                             |
|----------------|-----------------------------------------------------------------------------------------|
| `TData`        | Struct implementing `INetSerializableStruct` to be deserialized from the received data. |
| `TEnum`        | Enum type registered via [`RegisterNetworkHeaders<TEnum>`](#registernetworkheaders).    |

| Parameter | Type                                  | Description                                             |
|-----------|---------------------------------------|---------------------------------------------------------|
| `header`  | `TEnum`                               | Header to register the handler for.                     |
| `handler` | `ClientNetworkHandlerDelegate<TData>` | Method or delegate to call when the header is received. |

| Exception                   | Condition                                                                            |
|-----------------------------|--------------------------------------------------------------------------------------|
| `InvalidOperationException` | A handler for the header is already registered or the enum header is not registered. |

**Example**

```csharp
IGameNetwork.RegisterHandler(MyNetworkHeaders.ExampleHeader1, (MyData myData) =>
{
    // read data from MyData
});
```

or with a method:

```csharp
IGameNetwork.RegisterHandler(MyNetworkHeaders.ExampleHeader1, MyHandler);
```

```csharp
public void MyHandler(MyData myData)
{
    // read data from MyData
}
```

### Send

```cs
void Send<TEnum, TData>(TEnum header, TData data) where TEnum : Enum where TData : INetSerializableStruct
```

*Only available on the client.*

Sends data to the server.

| Type parameter | Description                                                                          |
|----------------|--------------------------------------------------------------------------------------|
| `TData`        | Struct implementing `INetSerializableStruct` to be serialized and sent.              |
| `TEnum`        | Enum type registered via [`RegisterNetworkHeaders<TEnum>`](#registernetworkheaders). |

| Parameter | Type    | Description                          |
|-----------|---------|--------------------------------------|
| `header`  | `TEnum` | Network header to identify the data. |
| `data`    | `TData` | Data to send.                        |

| Exception                   | Condition                                                                                           |
|-----------------------------|-----------------------------------------------------------------------------------------------------|
| `InvalidOperationException` | Given enum header is not registered via [`RegisterNetworkHeaders<TEnum>`](#registernetworkheaders). |

**Remarks**

The server needs to have a handler registered for the enum header to receive the data.

**Example**

```csharp
[NetworkSerialize]
public readonly record struct MyData(int Foo) : INetSerializableStruct;
```

```csharp
IGameNetwork.Send(MyNetworkHeaders.ExampleHeader1, new MyData(42);
```

### RegisterEntityServerWrite

```cs
void RegisterEntityServerWrite<TSerializable>(
    EntityEventServerWriteDelegate<TSerializable> handler) where TSerializable : IServerSerializable
```

*Only available on the server.*

Registers a server side event write handler for an entity.

| Type parameter  | Description                                                                        |
|-----------------|------------------------------------------------------------------------------------|
| `TSerializable` | Entity type implementing `IServerSerializable` we are registering the handler for. |

| Parameter | Type                                            | Description                                                 |
|-----------|-------------------------------------------------|-------------------------------------------------------------|
| `handler` | `EntityEventServerWriteDelegate<TSerializable>` | Method or delegate to call when an entity event is created. |

**Remarks**

The client must have corresponding ClientRead handler registered for the entity.

**Example**

```csharp
IGameNetwork.RegisterEntityServerWrite<Item>((item, msg, client, extraData) =>
{
    CustomData data = item.GetExtraField<MyCustomData>("CustomData");
    msg.WriteInt32(data.Foo);
});
```

or with a method:

```csharp
IGameNetwork.RegisterEntityServerWrite<Item>(WriteItem);
```

```csharp
public void WriteItem(Item item, IWriteMessage msg, Client client, NetEntityEvent.IData? extraData)
{
    CustomData data = item.GetExtraField<MyCustomData>("CustomData");
    msg.WriteInt32(data.Foo);
}
```

### RegisterEntityServerRead

```cs
void RegisterEntityServerRead<TSerializable>(
    EntityEventServerReadDelegate<TSerializable> handler) where TSerializable : IClientSerializable
```

*Only available on the server.*

Registers a server side event read handler for an entity.

| Type parameter  | Description                                                                        |
|-----------------|------------------------------------------------------------------------------------|
| `TSerializable` | Entity type implementing `IClientSerializable` we are registering the handler for. |

| Parameter | Type                                           | Description                                                  |
|-----------|------------------------------------------------|--------------------------------------------------------------|
| `handler` | `EntityEventServerReadDelegate<TSerializable>` | Method or delegate to call when an entity event is received. |

**Remarks**

The client should have corresponding ClientWrite handler registered for the entity.

**Example**

```csharp
IGameNetwork.RegisterEntityServerRead<Item>((item, msg, client) =>
{
    // check if the client is allowed to do this first
    CustomData data = item.GetExtraField<MyCustomData>("CustomData");
    data.Foo = msg.ReadInt32();
});
```

or with a method:

```csharp
IGameNetwork.RegisterEntityServerRead<Item>(ReadItem);
```

```csharp
public void ReadItem(Item item, IReadMessage msg, Client client)
{
    // check if the client is allowed to do this first
    CustomData data = item.GetExtraField<MyCustomData>("CustomData");
    data.Foo = msg.ReadInt32();
}
```

### RegisterHandler

```cs
void RegisterHandler<TEnum, TData>(
    TEnum header,
    ServerNetworkHandlerDelegate<TData> handler) where TEnum : Enum where TData : INetSerializableStruct
```

*Only available on the server.*

Manually register a server side handler for a specific network header.

| Type parameter | Description                                                                             |
|----------------|-----------------------------------------------------------------------------------------|
| `TData`        | Struct implementing `INetSerializableStruct` to be deserialized from the received data. |
| `TEnum`        | Enum type registered via [`RegisterNetworkHeaders<TEnum>`](#registernetworkheaders).    |

| Parameter | Type                                  | Description                                             |
|-----------|---------------------------------------|---------------------------------------------------------|
| `header`  | `TEnum`                               | Header to register the handler for.                     |
| `handler` | `ServerNetworkHandlerDelegate<TData>` | Method or delegate to call when the header is received. |

| Exception                   | Condition                                                                            |
|-----------------------------|--------------------------------------------------------------------------------------|
| `InvalidOperationException` | A handler for the header is already registered or the enum header is not registered. |

**Example**

```csharp
IGameNetwork.RegisterHandler(MyNetworkHeaders.ExampleHeader1, (MyData myData, Client client) =>
{
    // read data from MyData
});
```

or with a method:

```csharp
IGameNetwork.RegisterHandler(MyNetworkHeaders.ExampleHeader1, MyHandler);
```

```csharp
public void MyHandler(MyData myData, Client client)
{
    // read data from MyData
}
```

### Broadcast

```cs
void Broadcast<TEnum, TData>(TEnum header, TData data) where TEnum : Enum where TData : INetSerializableStruct
```

*Only available on the server.*

Sends data to all connected clients.

| Type parameter | Description                                                                          |
|----------------|--------------------------------------------------------------------------------------|
| `TEnum`        | Enum type registered via [`RegisterNetworkHeaders<TEnum>`](#registernetworkheaders). |
| `TData`        | Struct implementing `INetSerializableStruct` to be serialized and sent.              |

| Parameter | Type    | Description                          |
|-----------|---------|--------------------------------------|
| `header`  | `TEnum` | Network header to identify the data. |
| `data`    | `TData` | Data to send.                        |

| Exception                   | Condition                                                                                           |
|-----------------------------|-----------------------------------------------------------------------------------------------------|
| `InvalidOperationException` | Given enum header is not registered via [`RegisterNetworkHeaders<TEnum>`](#registernetworkheaders). |

**Remarks**

The client needs to have a handler registered for the enum header to receive the data.

**Example**

```csharp
[NetworkSerialize]
public readonly record struct MyData(int Foo) : INetSerializableStruct;
```

```csharp
IGameNetwork.Broadcast(MyNetworkHeaders.ExampleHeader1, new MyData(42);
```

### SendToClient

```cs
void SendToClient<TEnum, TData>(
    Client client,
    TEnum header,
    TData data) where TEnum : Enum where TData : INetSerializableStruct
```

*Only available on the server.*

Sends data to a specific client.

| Type parameter | Description                                                                          |
|----------------|--------------------------------------------------------------------------------------|
| `TEnum`        | Enum type registered via [`RegisterNetworkHeaders<TEnum>`](#registernetworkheaders). |
| `TData`        | Struct implementing `INetSerializableStruct` to be serialized and sent.              |

| Parameter | Type     | Description                          |
|-----------|----------|--------------------------------------|
| `client`  | `Client` | Client to send the data to.          |
| `header`  | `TEnum`  | Network header to identify the data. |
| `data`    | `TData`  | Data to send.                        |

| Exception                   | Condition                                                                                           |
|-----------------------------|-----------------------------------------------------------------------------------------------------|
| `InvalidOperationException` | Given enum header is not registered via [`RegisterNetworkHeaders<TEnum>`](#registernetworkheaders). |

**Remarks**

The client needs to have a handler registered for the enum header to receive the data.

**Example**

```csharp
[NetworkSerialize]
public readonly record struct MyRequest(int Foo) : INetSerializableStruct;

[NetworkSerialize]
public readonly record struct MyResponse(int Bar) : INetSerializableStruct;
```

Respond to client in a network handler:

```csharp
IGameNetwork.RegisterHandler(MyNetworkHeaders.ExampleHeader1, (MyRequest request, Client client) =>
{
    MyResponse response = new(request.Foo * 2);
    IGameNetwork.SendToClient(client, MyNetworkHeaders.ExampleHeader2, response);
});
```

## Related types

### ClientNetworkHandlerDelegate

```cs
public delegate void ClientNetworkHandlerDelegate<in T>(T data) where T : INetSerializableStruct
```

Delegate for client side network handlers.

| Parameter | Type | Description                    |
|-----------|------|--------------------------------|
| `data`    | `T`  | Data received from the server. |

### ServerNetworkHandlerDelegate

```cs
public delegate void ServerNetworkHandlerDelegate<in T>(
    T data,
    Client client) where T : INetSerializableStruct
```

Delegate for server side network handlers.

| Parameter | Type     | Description                    |
|-----------|----------|--------------------------------|
| `data`    | `T`      | Data received from the client. |
| `client`  | `Client` | Client that sent the data.     |

### EntityEventClientReadDelegate

```cs
public delegate void EntityEventClientReadDelegate<in TNetSerializable>(
    TNetSerializable entity,
    IReadMessage msg,
    float sendingTime) where TNetSerializable : INetSerializable
```

### EntityEventServerReadDelegate

```cs
public delegate void EntityEventServerReadDelegate<in TNetSerializable>(
    TNetSerializable entity,
    IReadMessage msg,
    Client client) where TNetSerializable : INetSerializable
```

### EntityEventClientWriteDelegate

```cs
public delegate void EntityEventClientWriteDelegate<in TNetSerializable>(
    TNetSerializable entity,
    IWriteMessage msg,
    NetEntityEvent.IData? extraData) where TNetSerializable : INetSerializable
```

### EntityEventServerWriteDelegate

```cs
public delegate void EntityEventServerWriteDelegate<in TNetSerializable>(
    TNetSerializable entity,
    IWriteMessage msg,
    Client client,
    NetEntityEvent.IData? extraData) where TNetSerializable : INetSerializable
```

### ICustomSerializable

```cs
public interface ICustomSerializable<in T> where T : IClientSerializable, IServerSerializable
```

Interface for implementing bidirectional serialization for entities to be registered via [`IGameNetwork.RegisterCustomEntityEventHandler<TSerializable>`](IGameNetwork.md#registercustomentityeventhandler).

#### ClientWrite

```cs
void ClientWrite(T entity, IWriteMessage msg, NetEntityEvent.IData? extraData)
```

*Only available on the client.*

#### ClientRead

```cs
void ClientRead(T entity, IReadMessage msg, float sendingTime)
```

*Only available on the client.*

#### ServerWrite

```cs
void ServerWrite(T entity, IWriteMessage msg, Client client, NetEntityEvent.IData? extraData)
```

*Only available on the server.*

#### ServerRead

```cs
void ServerRead(T entity, IReadMessage msg, Client client)
```

*Only available on the server.*

### NetworkHandlerAttribute

```cs
public class NetworkHandlerAttribute<T>
```

Attribute to assign a header to a network handler method.

**Example**

```csharp
[NetworkHandler<MyNetworkHeaders>(MyNetworkHeaders.ExampleHeader1)]
public static void ExampleHandler1(MyData data) { }
```
