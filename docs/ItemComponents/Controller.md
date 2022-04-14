# Controller


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| IsToggle|bool|false|When enabled, the item will continuously send out a 0/1 signal and interacting with it will flip the signal (making the item behave like a switch). When disabled, the item will simply send out 1 when interacted with. |
| State|bool|false|Whether the item is toggled on/off. Only valid if IsToggle is set to true. |
| HideHUD|bool|true|Should the HUD (inventory, health bar, etc) be hidden when this item is selected. |
| UsableIn|UseEnvironment|UseEnvironment.Both|Can the item be selected in air, underwater or both. |
| DrawUserBehind|bool|false|Should the character using the item be drawn behind the item. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="periscope" tags="periscope" category="Machine,Weapon" type="Controller" disableitemusagewhenselected="true" scale="0.5" isshootable="true" requireaimtouse="false" requireaimtosecondaryuse="false">
  <Controller UserPos="-35.0, -50.0" direction="Right" canbeselected="true" msg="ItemMsgInteractSelect">
    <limbposition limb="Head" position="-10,-135" />
    <limbposition limb="Torso" position="-10,-200" />
    <limbposition limb="LeftHand" position="67,-170" />
    <limbposition limb="RightHand" position="67,-170" />
  </Controller>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <output name="position_out" displayname="connection.turretaimingout" fallbackdisplayname="inputtype.aim" />
    <output name="trigger_out" displayname="connection.turrettriggerout" fallbackdisplayname="inputtype.shoot" />
  </ConnectionPanel>
  [...]
</Item>
```

