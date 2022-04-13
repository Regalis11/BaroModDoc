# ItemContainer


## Example
```xml
<Item name="" identifier="op_researchterminal" tags="geneticresearchstation" width="494" height="297" texturescale="1.0,1.0" scale="0.5" category="Machine" subcategory="Outpost">
  <ItemContainer capacity="2" maxstacksize="1" canbeselected="true" hideitems="true" hudpos="0.5, 0.4" slotsperrow="3" uilabel="" allowuioverlap="true">
    <Containable items="geneticmaterial,stabilozine,unidentifiedgeneticmaterial" />
  </ItemContainer>
  <ItemContainer capacity="1" maxstacksize="1" canbeselected="true" hideitems="true" hudpos="0.5, 0.8" slotsperrow="5" uilabel="" allowuioverlap="true">
    <Containable items="geneticmaterial,stabilozine,unidentifiedgeneticmaterial" />
  </ItemContainer>
  <ConnectionPanel selectkey="Action" canbeselected="true" hudpriority="10" msg="ItemMsgRewireScrewdriver">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredSkill identifier="electrical" level="55" />
    <StatusEffect type="OnFailure" target="Character" targetlimbs="LeftHand,RightHand">
      <Sound file="Content/Sounds/Damage/Electrocution1.ogg" range="1000" />
      <Explosion range="100.0" stun="0" force="5.0" flames="false" shockwave="false" sparks="true" underwaterbubble="false" />
      <Affliction identifier="stun" strength="4" />
      <Affliction identifier="burn" strength="5" />
    </StatusEffect>
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <output name="condition_out" displayname="connection.conditionout" />
  </ConnectionPanel>
  [...]
</Item>
```

