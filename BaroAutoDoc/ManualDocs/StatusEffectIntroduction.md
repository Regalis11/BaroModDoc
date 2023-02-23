StatusEffects are a feature that can be used to execute various kinds of effects: modifying the state of some entity in some way, spawning things, playing sounds, emitting particles, creating fire and explosions, increasing a characters' skill, and many others. They are a crucial part of modding Barotrauma: all kinds of custom behaviors of an item or a creature for example are generally created using StatusEffects.

There are a couple of terms related to the StatusEffects that are important to understand to be able to make the most use of the documentation:

- **The entity executing the StatusEffect** \- Every effect is always *executed* by some *entity*, for example an item or a character. For example, if you use a gun, it might execute some effect that emits particles.

- **Type** \- The type of the StatusEffect determines *when* the effect is executed. For example, when the item is being worn or used, when a character is underwater or takes damage, or always.

[TODO: list ActionTypes]

- **The target of the StatusEffect** \- StatusEffects need to have a *target*. The target determines which entity the effect affects - this is often the same as the entity executing the effect, but it can be something else too: for example, a diving suit might have a StatusEffect that *targets* the oxygen tank inside it, making it deplete when the suit is worn.

[TODO: list TargetTypes]

## Examples

Here's an exmple of a simple StatusEffect, which makes the item deteriorate by 10 units per second when it's underwater. 

```xml
<Item identifier="watersensitiveitem" name="Water-sensitive Item">
  <ItemComponent>
	<StatusEffect type="InWater" target="This" Condition="-10.0" />
  </ItemComponent>
</Item>
```

Notice the target "This": here it refers to the item itself. 

Another thing to note is how the condition decrease is defined. Status effects can modify any *property* of the target entity (see the [content type documentation](Intro/ContentTypes.md) for a full list of properties of different kinds of entities). In this case we are modifying the "Condition" property of the item. By default, the value is treated as "how much the value changes per second", in this case reducing the condition by 1 per second. If we wanted to instead make the item break down immediately when it's submerged, we would use the attribute 'setvalue' as follows:

```xml
<Item identifier="watersensitiveitem" name="Water-sensitive Item">
  <ItemComponent>
	<StatusEffect type="InWater" target="This" Condition="0.0" setvalue="true" />
  </ItemComponent>
</Item>
```

But what if we wanted to create a gun whose condition decreases by 10 whenever it's fired? We can't use setvalue, nor can we make the value decrease by 10 per second: we want an instant decrease of 10. Here's how we could implement it:

```xml
<Item identifier="fragilegun" name="A Rather Poor Gun">
  <ItemComponent>
	<StatusEffect type="OnUse" target="This" Condition="-10.0" disabledeltatime="true" />
  </ItemComponent>
</Item>
```

The difference here is the *disabledeltatime* attribute. Delta time refers to the amount of elapsed time, which we want to ignore altogether in this case, treating "-10" as an instantaneous decrease.

In other words, the values is treated as an increase per frame, as opposed to an increase per second. Note that you most likely would only want to use this attribute in "one-shot", instant effects that don't run over a period of time. For example, adding this attribute to the previous water-sensitive item would lead to odd results: the item would constantly deteriorate at a rate of 1 units per frame when submerged.