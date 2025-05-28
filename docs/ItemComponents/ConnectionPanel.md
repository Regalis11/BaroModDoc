# ConnectionPanel


## Attributes

| Attribute | Type | Default value | Description                                         |
|-----------|------|---------------|-----------------------------------------------------|
| Locked    | bool | false         | Locked connection panels cannot be rewired in-game. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="detonator" health="30" category="Equipment,Weapon" Tags="smallitem,detonator" DamagedByContainedItemExplosions="true" explosiondamagemultiplier="0.3" Scale="0.5" cargocontaineridentifier="explosivecrate" impactsoundtag="impact_metal_light" isshootable="true">
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="activate" displayname="connection.activate">
      <StatusEffect type="OnUse" target="This" IsOn="true" charge="3.402823466E+38" setvalue="true">
        <Conditional IsOn="false" />
      </StatusEffect>
    </input>
  </ConnectionPanel>
  <Holdable selectkey="Action" pickkey="Use" slots="Any,RightHand,LeftHand" msg="ItemMsgDetachWrench" aimpos="35,-10" handle1="0,0" attachable="true" aimable="true">
    <RequiredItem items="wrench,deattachtool" excludeditems="multitool" type="Equipped" />
  </Holdable>
  <PowerContainer capacity="2.0" charge="3.402823466E+38" canbeselected="false" />
  [...]
</Item>
```

