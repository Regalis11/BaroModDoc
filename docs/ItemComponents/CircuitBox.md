# CircuitBox


## Attributes

| Attribute | Type | Default value | Description                                                      |
|-----------|------|---------------|------------------------------------------------------------------|
| Locked    | bool | false         | Locked circuit boxes can only be viewed and not interacted with. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="circuitbox" category="Electrical" Tags="smallitem" maxstacksize="1" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true" disablecommandmenuwhenselected="true" grabwhenselected="true">
  <CircuitBox selectkey="Select" canbeselected="true" msg="ItemMsgInteractSelect" allowuioverlap="true">
    <Requireditem items="idcard" type="Picked" msg="itemmsgunauthorizedaccess" ignoreineditor="true" />
    <GuiFrame relativesize="0.7,0.8" style="ItemUI" anchor="Center" />
    <WireSprite texture="Content/Items/Electricity/wire.png" sourcerect="0,0,16,12" origin="0.5,0.5" />
    <ConnectionSprite texture="Content/UI/UIAtlasDevices.png" sourcerect="1,1,56,55" origin="0.5,0.5" />
    <WireConnectorSprite texture="Content/UI/UIAtlasDevices.png" sourcerect="69,3,45,85" origin="0.5,0.27" />
    <ConnectionScrewSprite texture="Content/UI/UIAtlasDevices.png" sourcerect="88,92,45,45" origin="0.5,0.5" />
  </CircuitBox>
  <Holdable selectkey="Select" pickkey="Use" slots="Any,RightHand,LeftHand" msg="ItemMsgDetachWrench" MsgWhenDropped="ItemMsgPickupSelect" PickingTime="5.0" aimpos="85,-10" handle1="0,0" attachable="true" aimable="true">
    <RequiredItem items="wrench,deattachtool" excludeditems="multitool" type="Equipped" />
  </Holdable>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.4,0.62" minsize="400,350" maxsize="960,840" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="signal_in1" displayname="connection.signalinx~[num]=1" />
    <input name="signal_in2" displayname="connection.signalinx~[num]=2" />
    <input name="signal_in3" displayname="connection.signalinx~[num]=3" />
    <input name="signal_in4" displayname="connection.signalinx~[num]=4" />
    <input name="signal_in5" displayname="connection.signalinx~[num]=5" />
    <input name="signal_in6" displayname="connection.signalinx~[num]=6" />
    <input name="signal_in7" displayname="connection.signalinx~[num]=7" />
    <input name="signal_in8" displayname="connection.signalinx~[num]=8" />
    <output name="signal_out1" displayname="connection.signaloutx~[num]=1" />
    <output name="signal_out2" displayname="connection.signaloutx~[num]=2" />
    <output name="signal_out3" displayname="connection.signaloutx~[num]=3" />
    <output name="signal_out4" displayname="connection.signaloutx~[num]=4" />
    <output name="signal_out5" displayname="connection.signaloutx~[num]=5" />
    <output name="signal_out6" displayname="connection.signaloutx~[num]=6" />
    <output name="signal_out7" displayname="connection.signaloutx~[num]=7" />
    <output name="signal_out8" displayname="connection.signaloutx~[num]=8" />
  </ConnectionPanel>
  <ItemContainer capacity="64" maxstacksize="1" canbeselected="false" allowdraganddrop="false" drawinventory="false" QuickUseMovesItemsInside="false">
    <Containable items="circuitboxcomponent" />
  </ItemContainer>
  <ItemContainer capacity="10" maxstacksize="32" canbeselected="false" allowdraganddrop="false" drawinventory="false" QuickUseMovesItemsInside="false">
    <Containable items="wire" />
  </ItemContainer>
  [...]
</Item>
```

