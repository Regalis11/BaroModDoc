When creating custom content for Barotrauma, there are some things you should be aware of to ensure your mod runs as well as possible and doesn't cause any unnecessary performance problems.

In general, large numbers of things that need to be updated very frequently tend to be bad for performance. StatusEffects in particular can easily cause a noticeable performance hit if not used carefully, and/or if there's for example a very large number of items that are all executing StatusEffects. Here's some tips for making StatusEffects as performant as possible:

## ActionType

The ActionType, i.e. *when* an effect executes, can make a difference. The Always type is generally the worst for performance, since (as the name implies), these execute every frame. If you can for example use an effect that uses the ActionType InWater or Contained instead, it's usually preferable.

## Interval

The interval at which an effect executes can also make a significant difference. By default, the interval is 0, meaning the effect executes every frame. An effect that only runs once per second is generally 60 times more performant than one that runs every frame!

```xml
<!-- The condition of this item reduces by 0.1 per second, giving it a lifetime of 1000 seconds. But we can make this more performant! -->
<Item identifier="someitem">
  <ItemComponent>
	<StatusEffect type="OnContained" target="This" Condition="-0.1" />
	<StatusEffect type="OnBroken">
		<Remove />
	</StatusEffect>
  </ItemComponent>
</Item>

<!-- The condition of this item reduces by 0.1 per second, but it only runs once per second, reducing the condition 0.1 each time.
	The end result is the same, but the effect only needs to run every 60 frames! -->
<Item identifier="someitem">
  <ItemComponent>
	<StatusEffect type="OnContained" target="This" Condition="-0.1" interval="1" disabledeltatime="true" />
	<StatusEffect type="OnBroken">
		<Remove />
	</StatusEffect>
  </ItemComponent>
</Item>
```

This is also something you should keep in mind if your mod adds custom talents or afflictions. If they include StatusEffects, it may be a good idea to only make them execute every x seconds if possible.

## Containables

It's common for StatusEffects to only run if the item is contained inside another item - or if there's an item of some sort inside the item. These can be implemented using an effect with a conditional that checks what kind of a container the item is inside, or a RequiredItem element that makes the effect only execute when there's a specific kind of item inside it, but this is not good for performance: the item needs to be constantly checking if it's inside the right kind of container, or if there's the right kind of item inside it.

```xml
<!-- Example of an effect that might be a part of a diving suit: when there's an oxygenite tank inside it, set the SpeedMultiplier of the wearer to 1.5.
	This is not the most performant way to go about implementing this kind of effect: the effect needs to keep checking if there's the right kind of item inside it. -->
<StatusEffect type="OnWearing" target="Contained,Character" SpeedMultiplier="1.5" setvalue="true">
	<RequiredItem items="oxygenitetank" type="Contained" />
</StatusEffect>
```

It's possible to also configure these kinds of effects in the ItemContainer component, making them only execute if the right kind of item is inside the container.

```xml
<!-- This only executes if there's an oxygenite tank inside the container, otherwise it does nothing and causes no performance impact. -->
<ItemContainer capacity="1" maxstacksize="1" hideitems="true" containedstateindicatorstyle="tank">
  <Containable items="oxygenitetank">
	<StatusEffect type="OnContaining" target="Character" SpeedMultiplier="1.5" setvalue="true" />
  </Containable>	  
</ItemContainer>
```