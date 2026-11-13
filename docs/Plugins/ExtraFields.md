# Extra fields

Extra fields let a plugin attach its own data to any object in the game, such as an item, a character or a submarine, without keeping a dictionary of its own.

```cs
using Barotrauma.Plugins;

public class ChargeData
{
    public int Charges;
}

void AddCharge(Item item)
{
    ChargeData data = item.GetOrAddExtraField("mymod_charges", new ChargeData());
    data.Charges++;
}
```

| Method | Description|
|---|---|
| `SetExtraField<T>(string key, T value)`| Stores a value under the key, replacing any previous value.|
| `GetExtraField<T>(string key)`| Returns the value, or `default` if there's none. Throws if the value isn't a `T`.|
| `TryGetExtraField<T>(string key, out T value)`| Returns `true` and the value if there's a `T` stored under the key.|
| `GetOrAddExtraField<T>(string key, T value)`| Returns the stored value, or stores `value` and returns it if there's none.|
| `GetExtraFields()`| Returns the object's `ExtraFields`, which has the same methods as `Set`, `Get` and `TryGet`.|

## Notes

- Each plugin has its own fields, so two plugins using the same key on the same object don't see each other's values.
- Extra fields cleared when the plugin unloads, so they won't keep your plugin from [unloading](PluginUnloading.md).
- Extra fields aren't saved, meaning that they are gone after the game is restarted or a save is loaded.
