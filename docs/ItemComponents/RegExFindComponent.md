# RegExFindComponent


## Attributes

| Attribute        | Type   | Default value | Description                                                                                                                            |
|------------------|--------|---------------|----------------------------------------------------------------------------------------------------------------------------------------|
| MaxOutputLength  | int    | 200           | The maximum length of the output string. Warning: Large values can lead to large memory usage or networking issues.                    |
| Output           | string | "1"           | The signal this item outputs when the received signal matches the regular expression.                                                  |
| UseCaptureGroup  | bool   | false         | Should the component output a value of a capture group instead of a constant signal.                                                   |
| FalseOutput      | string | "0"           | The signal this item outputs when the received signal does not match the regular expression.                                           |
| ContinuousOutput | bool   | true          | Should the component keep sending the output even after it stops receiving a signal, or only send an output when it receives a signal. |
| Expression       | string | ""            | The regular expression used to check the incoming signals.                                                                             |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="regexcomponent" category="Electrical" Tags="smallitem,logic" maxstacksize="8" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <RegExFindComponent canbeselected="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="signal_in" displayname="connection.signalin" />
    <output name="signal_out" displayname="connection.signalout" />
    <input name="set_output" displayname="connection.setoutput" />
  </ConnectionPanel>
  [...]
</Item>
```

