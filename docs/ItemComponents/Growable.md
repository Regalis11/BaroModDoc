# Growable


## Example
```xml
<Item identifier="saltvineseed" category="Misc" Tags="smallitem,seed,plantitem" maxstacksize="8" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_soft" spritecolor="215,245,77,255">
  <Growable growthspeed="0.076" pickingtime="3" maximumvines="18" maxhealth="375" hardiness="0.162" floodtolerance="0.2" seedrate="0" productrate="0.0056" flowerquantity="8" baseflowerscale="0.40" vinescale="0.25" vinetint="135,187,61,255" flowertint="240,180,17,255">
    <ProducedItem identifier="saltbulb" probability="1.0" />
    <VineSprites vineatlas="Content/Items/Gardening/branches.png" decayatlas="Content/Items/Gardening/branches_overlay.png">
      <LeafSprite texture="Content/Items/Gardening/GrowablePlants_Temp.png" sourcerect="0,512,256,256" />
      <LeafSprite texture="Content/Items/Gardening/GrowablePlants_Temp.png" sourcerect="256,512,256,256" />
      <FlowerSprite texture="Content/Items/Gardening/GrowablePlants_Temp.png" sourcerect="384,768,128,128" />
      <FlowerSprite texture="Content/Items/Gardening/GrowablePlants_Temp.png" sourcerect="512,768,128,128" />
      <FlowerSprite texture="Content/Items/Gardening/GrowablePlants_Temp.png" sourcerect="640,768,128,128" />
      <VineSprite type="Stem" sourcerect="640,640,128,128" />
      <VineSprite type="CrossJunction" sourcerect="512,640,128,128" />
      <VineSprite type="VerticalLane" sourcerect="512,512,128,128" />
      <VineSprite type="HorizontalLane" sourcerect="640,512,128,128" />
      <VineSprite type="TurnTopRight" sourcerect="0,512,128,128" />
      <VineSprite type="TurnTopLeft" sourcerect="384,512,128,128" />
      <VineSprite type="TurnBottomLeft" sourcerect="256,512,128,128" />
      <VineSprite type="TurnBottomRight" sourcerect="128,512,128,128" />
      <VineSprite type="TSectionTop" sourcerect="128,640,128,128" />
      <VineSprite type="TSectionLeft" sourcerect="256,640,128,128" />
      <VineSprite type="TSectionBottom" sourcerect="384,640,128,128" />
      <VineSprite type="TSectionRight" sourcerect="0,640,128,128" />
      <VineSprite type="StumpTop" sourcerect="768,512,128,128" />
      <VineSprite type="StumpLeft" sourcerect="896,512,128,128" />
      <VineSprite type="StumpBottom" sourcerect="768,640,128,128" />
      <VineSprite type="StumpRight" sourcerect="896,640,128,128" />
    </VineSprites>
  </Growable>
  [...]
</Item>
```

