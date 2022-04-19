# Wearable


## Attributes

This component supports the attributes defined in: [Pickable](Pickable.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="piratebandana" category="Equipment" cargocontaineridentifier="metalcrate" tags="smallitem,clothing" scale="0.5" impactsoundtag="impact_soft">
  <Wearable limbtype="Head" slots="Any,Head" msg="ItemMsgPickUpSelect">
    <sprite texture="Content/Items/Jobgear/headgears.png" limb="Head" inheritlimbdepth="true" inheritscale="true" ignorelimbscale="true" scale="0.8" sourcerect="313,407,100,95" origin="0.55,0.6" />
    <damagemodifier afflictionidentifiers="blunttrauma,lacerations,gunshotwound,bitewounds" armorsector="0.0,360.0" damagemultiplier="0.8" />
    <SkillModifier skillidentifier="mechanical" skillvalue="5" />
    <SkillModifier skillidentifier="electrical" skillvalue="5" />
  </Wearable>
  [...]
</Item>
```

