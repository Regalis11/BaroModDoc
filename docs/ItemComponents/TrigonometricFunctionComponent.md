# TrigonometricFunctionComponent


## Attributes

| Attribute  | Type         | Default value | Description                                                                 |
|------------|--------------|---------------|-----------------------------------------------------------------------------|
| Function   | FunctionType | Sin           | Which kind of function to run the input through.                            |
| UseRadians | bool         | false         | If set to true, the trigonometric function uses radians instead of degrees. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="coscomponent" category="Electrical" Tags="smallitem,logic" maxstacksize="8" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <TrigonometricFunctionComponent canbeselected="true" function="Cos" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem identifier="screwdriver" type="Equipped" />
    <input name="signal_in" displayname="connection.signalin" />
    <output name="signal_out" displayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

