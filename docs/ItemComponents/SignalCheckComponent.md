# SignalCheckComponent


## Attributes

| Attribute       | Type   | Default value | Description                                                                                                          |
|-----------------|--------|---------------|----------------------------------------------------------------------------------------------------------------------|
| MaxOutputLength | int    | 200           | The maximum length of the output strings. Warning: Large values can lead to large memory usage or networking issues. |
| Output          | string | "1"           | The signal this item outputs when the received signal matches the target signal.                                     |
| FalseOutput     | string | "0"           | The signal this item outputs when the received signal does not match the target signal.                              |
| TargetSignal    | string | ""            | The value to compare the received signals against.                                                                   |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="signalcheckcomponent" category="Electrical" Tags="smallitem,logic" maxstacksize="8" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <SignalCheckComponent canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="signal_in" displayname="connection.signalin" />
    <input name="set_output" displayname="connection.setoutput" />
    <input name="set_targetsignal" displayname="connection.settargetsignal" />
    <output name="signal_out" displayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

