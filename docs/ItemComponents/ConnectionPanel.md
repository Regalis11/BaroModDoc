# ConnectionPanel


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| Locked|bool|false|Locked connection panels cannot be rewired in-game. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="button" category="Electrical" tags="smallitem,button" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true" maxstacksize="8">
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem identifier="screwdriver" type="Equipped" />
    <output name="signal_out" displayname="connection.signalout" />
  </ConnectionPanel>
  <Holdable selectkey="Select" pickkey="Use" slots="Any,RightHand,LeftHand" msg="ItemMsgDetachWrench" PickingTime="10.0" aimpos="35,-10" handle1="0,0" attachable="true" attachedbydefault="true" aimable="true">
    <requireditem identifier="wrench" type="Equipped" />
  </Holdable>
  [...]
</Item>
```

