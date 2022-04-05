# Overrides

Usually you are not creating a total conversion mod, but you might still want to replace parts of the original content. This can be done by using `override` elements in the XML configuration files.

Overriding is based on identifiers, meaning that the identifier of the new item must match the identifier of the old item.

The following example replaces the vanilla harpoon gun with a potato gun, and the corresponding ammo with a potato:
```xml
<Override>
  <Item identifier="harpoongun" name="Potato Gun" tags="mediumitem,weapon,gun">
    <Sprite texture="potatogun.png" sourcerect="0,64,282,60" depth="0.55" origin="0.5,0.5"/>
    <!-- ... -->
  </Item>
  <Item identifier="spear" name="Potato" tags="mediumitem,harpoonammo">
    <Sprite texture="potato.png" sourcerect="0,0,64,40" depth="0.55" origin="0.5,0.5"/>
    <!-- ... -->
  </Item>
</Override>
```

In order to get the new item to function like the vanilla counterpart, make sure to include all the tags of the original item.

It's also possible to override items created in another mod. For example, we can override the example presented in [this page](ContentPackages.md):
```xml
<Override>
  <Item identifier="alienwrench" name="Alien Wrench Override" variantof="wrench" scale="0.2">
    <Sprite texture="%ModDir%/alienwrench2.png" sourcerect="0,0,256,112" depth="0.55" origin="0.5,0.1" scale="0.1" />
  </Item>
</Override>
```
