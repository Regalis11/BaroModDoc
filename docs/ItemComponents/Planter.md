# Planter


## Example
```xml
<Item name="" identifier="smallplanter" category="Misc" Tags="planter,mediumitem" scale="0.5" isshootable="true">
  <Planter selectkey="Select" canbepicked="true" pickingtime="5.0" msg="ItemMsgPlantSeed">
    <PlantSlot slot="0" offset="0,32" size="0.25" />
    <SuitableFertilizer items="fertilizer" />
    <SuitableSeed items="seed" />
  </Planter>
  <Holdable selectkey="Select" pickkey="Use" slots="Any,RightHand,LeftHand" msg="ItemMsgDetach" PickingTime="5.0" aimpos="85,-10" handle1="0,0" attachable="true" aimable="true" />
  <ItemContainer capacity="1" maxstacksize="1" hideitems="true" drawinventory="false" uilabel="" allowuioverlap="true" allowdraganddrop="false" showcontainedstateindicator="false">
    <Containable items="seed" />
  </ItemContainer>
  <LightComponent range="10.0" lightcolor="255,255,255,0" IsOn="true" castshadows="false" allowingameediting="false">
    <sprite texture="Content/Items/Gardening/GrowablePlants_Temp.png" depth="0.025" sourcerect="896,512,128,128" origin="0.5,0.5" alpha="1.0" />
  </LightComponent>
  [...]
</Item>
```

