# MemoryComponent


## Attributes

| Attribute      | Type   | Default value | Description                                                                                                        |
|----------------|--------|---------------|--------------------------------------------------------------------------------------------------------------------|
| MaxValueLength | int    | 200           | The maximum length of the stored value. Warning: Large values can lead to large memory usage or networking issues. |
| Value          | string | ""            | The currently stored signal the item outputs.                                                                      |
| Writeable      | bool   | true          | Can the value stored in the memory component be changed via signals.                                               |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="memorycomponent" category="Electrical" Tags="smallitem,logic,circuitboxcomponent" maxstacksize="32" maxstacksizecharacterinventory="8" linkable="true" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true" GrabWhenSelected="true" signalcomponentcolor="#a66c6b">
  <MemoryComponent canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <requireditem items="screwdriver" type="Equipped" />
    <input name="signal_in" displayname="connection.signalin" />
    <input name="lock_state" aliases="signal_store" displayname="connection.lockstate" />
    <output name="signal_out" displayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

