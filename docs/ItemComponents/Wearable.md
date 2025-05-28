# Wearable


## Attributes

| Attribute        | Type | Default value | Description                                                                              |
|------------------|------|---------------|------------------------------------------------------------------------------------------|
| AllowUseWhenWorn | bool | false         | Can the item be used (assuming it has components that are usable in some way) when worn. |

This component also supports the attributes defined in: [Pickable](Pickable.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="toolbelt" category="Equipment" tags="mediumitem,mobilecontainer,tool" showcontentsintooltip="true" Scale="0.5" fireproof="true" impactsoundtag="impact_soft">
  <Wearable slots="Bag" msg="ItemMsgEquipSelect" canbeselected="false" canbepicked="true" pickkey="Select">
    <sprite name="ToolBelt" texture="Content/Items/Tools/tools.png" sourcerect="256,102,112,54" limb="Torso" inherittexturescale="true" origin="0.5,-0.2" />
  </Wearable>
  <Holdable slots="RightHand+LeftHand" holdpos="0,-70" handle1="-5,0" handle2="10,-20" holdangle="0" msg="ItemMsgPickUpUse" canbeselected="false" canbepicked="true" pickkey="Use" allowswappingwhenpicked="false" />
  [...]
</Item>
```

