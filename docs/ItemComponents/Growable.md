# Growable


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| GrowthSpeed|float|1|How fast the plant grows. |
| MaxHealth|float|100|How long the plant can go without watering. |
| FloodTolerance|float|1|How much damage the plant takes while in water. |
| Hardiness|float|1|How much damage the plant takes while growing. |
| SeedRate|float|0.01|How often a seed is produced. |
| ProductRate|float|0.01|How often a product item is produced. |
| MutationProbability|float|0.5|Probability of an attribute being randomly modified in a newly produced seed. |
| FlowerTint|Color|"1.0,1.0,1.0,1.0"|Color of the flowers. |
| FlowerQuantity|int|3|Number of flowers drawn when fully grown |
| BaseFlowerScale|float|0.25|Size of the flower sprites. |
| BaseLeafScale|float|0.5|Size of the leaf sprites. |
| LeafTint|Color|"1.0,1.0,1.0,1.0"|Color of the leaves. |
| LeafProbability|float|0.33|Chance of a leaf appearing behind a branch. |
| VineTint|Color|"1.0,1.0,1.0,1.0"|Color of the vines. |
| MaximumVines|int|32|Maximum number of vine tiles the plant can grow. |
| VineScale|float|0.25|Size of the vine sprites. |
| DeadTint|Color|"0.26,0.27,0.29,1.0"|Tint of a dead plant. |
| GrowthWeights|Vector4|"1,1,1,1"|Probability for the plant to grow in a direction. |
| FireVulnerability|float|0.0|How much damage is taken from fires. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


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

