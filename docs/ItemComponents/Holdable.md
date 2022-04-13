# Holdable


## Example
```xml
<Item name="" identifier="bandolier" category="Equipment" tags="smallitem,clothing" scale="0.5" cargocontaineridentifier="metalcrate" description="" impactsoundtag="impact_soft">
  <Holdable slots="RightHand+LeftHand" holdpos="0,-70" handle1="0,0-30" handle2="0,-30" holdangle="0" msg="ItemMsgPickUpUse" canbeselected="false" canbepicked="true" pickkey="Use" allowswappingwhenpicked="false" />
  <Wearable slots="OuterClothes" msg="ItemMsgPickUpSelect" canbeselected="false" canbepicked="true" pickkey="Select">
    <sprite name="Security Vest" texture="Content/Items/JobGear/TalentGear.png" limb="Torso" hidelimb="false" sourcerect="215,209,79,97" inherittexturescale="true" origin="0.45,0.6" />
    <SkillModifier skillidentifier="weapons" skillvalue="15" />
    <StatValue stattype="RangedAttackSpeed" value="0.25" />
    <StatValue stattype="TurretAttackSpeed" value="0.25" />
  </Wearable>
  [...]
</Item>
```

