# ItemContainer


## Example
```xml
<Item identifier="alienitemcontainersmall" category="Alien" tags="aliencontainer" linkable="true" pickdistance="150" scale="0.5">
  <ItemContainer capacity="5" canbeselected="true" hideitems="true" msg="ItemMsgInteractSelect">
    <GuiFrame relativesize="0.3,0.2" anchor="Center" style="ItemUI" />
    <Containable items="smallitem,mediumitem" />
  </ItemContainer>
  [...]
</Item>
```

