# StatusEffect


## StatusEffect
StatusEffects are a feature that can be used to execute various kinds of effects: modifying the state of some entity in some way, spawning things, playing sounds, emitting particles, creating fire and explosions, increasing a characters' skill, and many others. They are a crucial part of modding Barotrauma: all kinds of custom behaviors of an item or a creature for example are generally created using StatusEffects.

There are a couple of terms related to the StatusEffects that are important to understand to be able to make the most use of the documentation:

- **The entity executing the StatusEffect** \- Every effect is always *executed* by some *entity*, for example an item or a character. For example, if you use a gun, it might execute some effect that emits particles.

- **Type** \- The type of the StatusEffect determines *when* the effect is executed. For example, when the item is being worn or used, when a character is underwater or takes damage, or always.

## ActionType

| Value            | Description                                                                                                                                                                                               |
|------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Always           | Executes every frame regardless of the state of the entity.                                                                                                                                               |
| OnPicked         | Executes when the item is picked up. Only valid for items.                                                                                                                                                |
| OnUse            | Executes when the item is used. The meaning of "using" an item depends on the item, but generally it means the action that happens when holding the item and clicking LMB. Only valid for items.          |
| OnSecondaryUse   | Executes when an item is held and the aim key is held. Only valid for items.                                                                                                                              |
| OnWearing        | Executes continuously while the item is being worn. Only valid for wearable items.                                                                                                                        |
| OnContaining     | Executes continuously when a specific Containable is inside an ItemContainer. Only valid for Containables defined in an ItemContainer component.                                                          |
| OnContained      | Executes continuously when the item is contained in some inventory. Only valid for items.                                                                                                                 |
| OnNotContained   | Executes continuously when the item is NOT contained in an inventory. Only valid for items.                                                                                                               |
| OnActive         | Executes continuously when the item is active. The meaning of "active" depends on the item, but generally means the item is on, powered, and doing the thing it's intended for. Only valid for items.     |
| OnFailure        | Executes when using the item fails due to a failed skill check. Only valid for items.                                                                                                                     |
| OnBroken         | Executes when using the item's condition drops to 0. Only valid for items.                                                                                                                                |
| OnFire           | Executes continuously when the entity is within the damage range of fire. Valid for items and characters.                                                                                                 |
| InWater          | Executes continuously when the entity is submerged. Valid for items and characters.                                                                                                                       |
| NotInWater       | Executes continuously when the entity is NOT submerged. Valid for items and characters.                                                                                                                   |
| OnImpact         | Executes when the entity hits something hard enough. For items, the threshold is determined by ItemPrefab.ImpactTolerance,<br/>for characters by Ragdoll.ImpactTolerance. Valid for items and characters. |
| OnEating         | Executes continuously when the character is eating another character. Only valid for characters.                                                                                                          |
| OnDamaged        | Executes when the entity receives damage from an external source (i.e. an affliction that increases in severity, or an item degrading by itself don't count).<br/>Valid for items and characters.         |
| OnSevered        | Executes when the limb gets severed. Only valid for limbs.                                                                                                                                                |
| OnProduceSpawned | Executes when a Items.Components.Growable produces an item (e.g. when a plant grows a fruit). Only valid for Growable items.                                                                              |
| OnOpen           | Executes when a Items.Components.Door is opened. Only valid for doors.                                                                                                                                    |
| OnClose          | Executes when a Items.Components.Door is closed. Only valid for doors.                                                                                                                                    |
| OnSpawn          | Executes when the entity spawns. Valid for items and characters.                                                                                                                                          |
| OnSuccess        | Executes when using the item succeeds based on a skill check. Only valid for items.                                                                                                                       |
| OnAbility        | Executes when an Ability (an effect from a talent) triggers the status effect. Only valid in Abilities, the target can be either a character or an item depending on the type of Ability.                 |
| OnDeath          | Executes when the character dies. Only valid for characters.                                                                                                                                              |




- **The target of the StatusEffect** \- StatusEffects need to have a *target*. The target determines which entity the effect affects - this is often the same as the entity executing the effect, but it can be something else too: for example, a diving suit might have a StatusEffect that *targets* the oxygen tank inside it, making it deplete when the suit is worn.

## TargetType

| Value            | Description                                                                                                                                    |
|------------------|------------------------------------------------------------------------------------------------------------------------------------------------|
| This             | The entity (item, character, limb) the StatusEffect is defined in.                                                                             |
| Parent           | In the context of items, the container the item is inside (if any). In the context of limbs, the character the limb belongs to.                |
| Character        | The character the StatusEffect is defined in. In the context of items and attacks, the character using the item/attack.                        |
| Contained        | The item(s) contained in the inventory of the entity the StatusEffect is defined in.                                                           |
| NearbyCharacters | Characters near the entity the StatusEffect is defined in. The range is defined using Range.                                                   |
| NearbyItems      | Items near the entity the StatusEffect is defined in. The range is defined using Range.                                                        |
| UseTarget        | The entity the item/attack is being used on.                                                                                                   |
| Hull             | The hull the entity is inside.                                                                                                                 |
| Limb             | The entity the item/attack is being used on. In the context of characters, one of the character's limbs (specify which one using targetLimbs). |
| AllLimbs         | All limbs of the character the effect is being used on.                                                                                        |
| LastLimb         | Last limb of the character the effect is being used on.                                                                                        |




### Examples

Here's an exmple of a simple StatusEffect, which makes the item deteriorate by 10 units per second when it's underwater. 

```xml
<Item identifier="watersensitiveitem" name="Water-sensitive Item">
  <ItemComponent>
	<StatusEffect type="InWater" target="This" Condition="-10.0" />
  </ItemComponent>
</Item>
```

Notice the target "This": here it refers to the item itself. 

Another thing to note is how the condition decrease is defined. Status effects can modify any *property* of the target entity (see the [content type documentation](../Intro/ContentTypes.html) for a full list of properties of different kinds of entities). In this case we are modifying the "Condition" property of the item. By default, the value is treated as "how much the value changes per second", in this case reducing the condition by 1 per second. If we wanted to instead make the item break down immediately when it's submerged, we would use the attribute 'setvalue' as follows:

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

In other words, the values is treated as an increase per frame, as opposed to an increase per second. Note that you most likely would only want to use this attribute in "one-shot", instant effects that don't run over a period of time. For example, adding this attribute to the previous water-sensitive item would lead to odd results: the item would constantly deteriorate at a rate of 10 units per frame when submerged.

### Attributes

| Attribute                        | Type       | Default value                   | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
|----------------------------------|------------|---------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| OnlyInside                       | bool       | false                           | If enabled, this effect can only execute inside a hull.                                                                                                                                                                                                                                                                                                                                                                                                                            |
| OnlyOutside                      | bool       | false                           | If enabled, this effect can only execute outside hulls.                                                                                                                                                                                                                                                                                                                                                                                                                            |
| OnlyPlayerTriggered              | bool       | -                               | If enabled, the effect only executes when the entity receives damage from a player character<br/>(a character controlled by a human player). Only valid for characters, and effects of the type OnDamaged.                                                                                                                                                                                                                                                                         |
| AllowWhenBroken                  | bool       | false                           | Can the StatusEffect be applied when the item applying it is broken?                                                                                                                                                                                                                                                                                                                                                                                                               |
| Interval                         | float      | 0                               | The interval at which the effect is executed. The difference between delay and interval is that effects with a delay find the targets, check the conditions, etc<br/>immediately when Apply is called, but don't apply the effects until the delay has passed. Effects with an interval check if the interval has passed when Apply is<br/>called and apply the effects if it has, otherwise they do nothing.                                                                      |
| Duration                         | float      | 0                               | How long the effect runs (in seconds). Note that if Stackable is true,<br/>there can be multiple instances of the effect running at a time.<br/>In other words, if the effect has a duration and executes every frame, you probably want<br/>to make it non-stackable or it'll lead to a large number of overlapping effects running at the same time.                                                                                                                             |
| disableDeltaTime                 | bool       | false                           | If set to true, the values will not be multiplied by the elapsed time.<br/>In other words, the values are treated as an increase per frame, as opposed to an increase per second.<br/>Useful for effects that are intended to just run for one frame (e.g. firing a gun, an explosion).                                                                                                                                                                                            |
| setValue                         | bool       | false                           | If set to true, the effect will set the properties of the target to the given values, instead of incrementing them by the given value.                                                                                                                                                                                                                                                                                                                                             |
| Stackable                        | bool       | true                            | Only valid if the effect has a duration or delay. Can the effect be applied on the same target(s) if the effect is already being applied?                                                                                                                                                                                                                                                                                                                                          |
| CheckConditionalAlways           | bool       | false                           | Only applicable for StatusEffects with a duration or delay. Should the conditional checks only be done when the effect triggers,<br/>or for the whole duration it executes / when the delay runs out and the effect executes? In other words, if false, the conditionals<br/>are only checked once when the effect triggers, but after that it can keep running for the whole duration, or is<br/>guaranteed to execute after the delay.                                           |
| TargetItemComponent              | string     | Empty                           | If set to the name of one of the target's ItemComponents, the effect is only applied on that component.<br/>Only works on items.                                                                                                                                                                                                                                                                                                                                                   |
| TargetSlot                       | int        | -1                              | Index of the slot the target must be in. Only valid when targeting a Contained item.                                                                                                                                                                                                                                                                                                                                                                                               |
| Range                            | float      | 0                               | How close to the entity executing the effect the targets must be. Only applicable if targeting NearbyCharacters or NearbyItems.                                                                                                                                                                                                                                                                                                                                                    |
| Offset                           | Vector2    | Zero                            | An offset added to the position of the effect is executed at. Only relevant if the effect does something where position matters,<br/>for example emitting particles or explosions, spawning something or playing sounds.                                                                                                                                                                                                                                                           |
| SeverLimbs,severlimbsprobability | float      | 0                               | The probability of severing a limb damaged by this status effect. Only valid when targeting characters or limbs.                                                                                                                                                                                                                                                                                                                                                                   |
| EventTargetTag                   | Identifier | Same as triggeredEventTargetTag | If the effect triggers a scripted event, the target of this effect is added as a target for the event using the specified tag.<br/>For example, an item could have an effect that executes when used on some character, and triggers an event that makes said character say something.                                                                                                                                                                                             |
| EventEntityTag                   | Identifier | Same as triggeredEventEntityTag | If the effect triggers a scripted event, the entity executing this effect is added as a target for the event using the specified tag.<br/>For example, a character could have an effect that executes when the character takes damage, and triggers an event that makes said character say something.                                                                                                                                                                              |
| EventUserTag                     | Identifier | Same as triggeredEventUserTag   | If the effect triggers a scripted event, the user of the StatusEffect (= the character who caused it to happen, e.g. a character who used an item) is added as a target for the event using the specified tag.<br/>For example, a gun could have an effect that executes when a character uses it, and triggers an event that makes said character say something.                                                                                                                  |
| spawnItemRandomly                | bool       | false                           | If enabled, one of the items this effect is configured to spawn is selected randomly, as opposed to spawning all of them.                                                                                                                                                                                                                                                                                                                                                          |
| delay                            | float      | 0.0                             | Can be used to delay the execution of the effect. For example, you could have an effect that triggers when a character receives damage,<br/>but takes 5 seconds before it starts to do anything.                                                                                                                                                                                                                                                                                   |
| tags                             | string[]   |                                 | An arbitrary tag (or a list of tags) that describe the status effect and can be used by Conditionals to check whether some StatusEffect is running.<br/>For example, an item could execute a StatusEffect with the tag "poisoned" on some character, and the character could have an effect that makes<br/>the character do something when an effect with that tag is active.                                                                                                      |
| conditionalComparison            | Comparison | Or                              | And/Or. Do all of the Conditionals defined in the effect be true for the effect to execute, or should the effect execute when any of them is true?                                                                                                                                                                                                                                                                                                                                 |
| Any property of the target       | Any        |                                 | These are the meat of the StatusEffects. You can set, increment or decrement any value of the target, be it an item, character, limb or hull.<br/>By default, the value is added to the existing value. If you want to instead set the value, use the setValue attribute.<br/>For example, Condition="-5" would decrease the condition of the item the effect is targeting by 5 per second. If the target has no property<br/>with the specified name, the attribute does nothing. |



### Elements


| Element            | Type                              | Description                                                                                                                                |
|--------------------|-----------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------|
| explosion          | [Explosion](Explosion.html)       | Creates an explosion when the effect triggers.                                                                                             |
| fire               | -                                 | Starts a fire of the specified size when the effect triggers.                                                                              |
| removeitem         | -                                 | Removes all of the items the effect is targeting.                                                                                          |
| dropcontaineditems | -                                 | Drops all the items from the inventories of the items or characters the effect is targeting.                                               |
| dropitem           | -                                 | Makes all the items the effect is targeting drop from the inventories they are in.                                                         |
| removecharacter    | -                                 | Removes all of the characters the effect is targeting, or if the effect is targeting limbs, the characters those limbs belong to.          |
| breaklimb          | -                                 | Severs all the limbs the effect is targeting.                                                                                              |
| hidelimb           | -                                 | Hides all the limbs the effect is targeting.                                                                                               |
| requireditem       | [RelatedItem](RelatedItem.html)   | Defines item(s) the entity executing the effect must have. Can be used in a variety of ways: e.g. to check if a character has something in their inventory or hands, if an item is inside a specific kind of container, or if there's a specific kind of item inside the item.                                                                                                                                    |
| requireditems      | [RelatedItem](RelatedItem.html)   | Alias of "requireditem". Functionally identical.                                                                                           |
| requiredaffliction | -                                 | Which type of afflictions the target must receive for the StatusEffect to be applied. Only valid when the type of the effect is OnDamaged. |
| affliction         | -                                 | An affliction to give to the character or limb the effect is targeting.                                                                    |
| aitrigger          | -                                 | Can be used to trigger a behavior change of some kind on an AI character. Only applicable for enemy characters, not humans.                |
| triggeranimation   | -                                 | Used for temporarily triggering custom animations.                                                                            			 |
| talenttrigger      | -                                 | Can be used by AbilityConditionStatusEffectIdentifier to react to a specific kind of status effect triggering.                             |
| giveexperience     | -                                 | Can be used to give experience points to the character(s) the effect is targeting.                                                         |
| giveskill          | GiveSkill                         | Can be used to give skill points to the character(s) the effect is targeting.                                                              |
| conditional        | [Conditional](Conditional.html)   | Some type of condition that needs to be met for the effect to execute. See the [Conditional](Conditional.html) page for more info.         |

### Fire
Starts a fire of the specified size when the effect triggers.  

#### Attributes

| Attribute              | Type              | Default value   | Description                                                                                                         |
|------------------------|-------------------|-----------------|---------------------------------------------------------------------------------------------------------------------|
| size                   | float             | 10.0f           | Initial width of the fire in pixels.                                                                                |

### RequiredAffliction
Which type of afflictions the target must receive for the StatusEffect to be applied. Only valid when the type of the effect is OnDamaged.

#### Attributes

| Attribute              | Type              | Default value   | Description                                                                                                         |
|------------------------|-------------------|-----------------|---------------------------------------------------------------------------------------------------------------------|
| identifier, type       | Identifier[]      |                 | Identifier or type, or a list of identifiers or types of the afflictions.                                           |
| minstrength            | float             | 0.0f            | Minimum strength of the affliction the target must receive.                                                         |

### Affliction
An affliction to give to the character or limb the effect is targeting. 

#### Attributes

| Attribute              | Type              | Default value   | Description                                                                                                         |
|------------------------|-------------------|-----------------|---------------------------------------------------------------------------------------------------------------------|
| identifier             | Identifier        |                 | Identifier of the affliction to give to the target.                                                                 |
| strength               | float             | 0.0f            | Strength of the affliction to give to the target.                                                                   |
| probability            | float             | 1.0f            | Probability of giving the affliction to the target (0 = never, 0.5 = 50% change, 1.0 = always)                      |


### TalentTrigger
Can be used by AbilityConditionStatusEffectIdentifier to react to a specific kind of status effect triggering.

#### Attributes

| Attribute              | Type              | Default value   | Description                                                                                                         |
|------------------------|-------------------|-----------------|---------------------------------------------------------------------------------------------------------------------|
| effectidentifier       | Identifier        |                 | An arbitrary identifier that talents can check using AbilityConditionStatusEffectIdentifier.                        |

### TriggerAnimation
Custom animations can be temporarily triggered via status effects. The animation is automatically switched back to the default, unless continuously kept alive by a status effect.
Can be used for example on items or afflictions.

#### Attributes

| Attribute       | Type          | Default value | Description                                                                                                                      |
|-----------------|---------------|---------------|----------------------------------------------------------------------------------------------------------------------------------|
| type            | AnimationType |               | Has to be defined. Available options: Walk, Run, SwimSlow, SwimFast, Crouch.                                                     |
| filename        | string        |               | File name of the animation file without the extension (.xml). The file has to be in the character's Animations folder.           |
| path            | ContentPath   |               | Path to the file E.g. "%ModDir%/CustomRun.xml". Can be in any folder and also in a different content package than the character. |
| priority        | float         | 0.0f          | Use for dodging cases where multiple status effects would "fight" equally over triggering the animations.                        |
| expectedspecies | Identifier    |               | Use for defining the species name(s) that should have the animations. Throws errors when the animations aren't found.            |


### GiveExperience
Can be used to give experience points to the character(s) the effect is targeting.

#### Attributes

| Attribute              | Type              | Default value   | Description                                                                                                         |
|------------------------|-------------------|-----------------|---------------------------------------------------------------------------------------------------------------------|
| amount                 | int               |                 | Amount of experience points to give.                                                                                |


## ItemSpawnInfo
Defines items spawned by the effect, and where and how they're spawned.

### Attributes

| Attribute              | Type              | Default value | Description                                                                                                                                |
|------------------------|-------------------|---------------|--------------------------------------------------------------------------------------------------------------------------------------------|
| SpawnIfInventoryFull   | bool              | false         | Should the item spawn even if the container is already full?                                                                               |
| SpawnIfNotInInventory  | bool              | false         | Should the item spawn even if this item isn't in an inventory? Only valid if the SpawnPosition is set to SameInventory. Defaults to false. |
| SpawnIfCantBeContained | bool              | true          | Should the item spawn even if the container can't contain items of this type or if it's already full?                                      |
| Impulse                | float             | -             | Impulse applied to the item when it spawns (i.e. how fast the item launched off).                                                          |
| Condition              | float             | 1             | Condition of the item when it spawns (1.0 = max).                                                                                          |
| Rotation               | float             | 0             |                                                                                                                                            |
| Count                  | int               | 1             | How many items to spawn.                                                                                                                   |
| Spread                 | float             | 0             | Random offset added to the spawn position in pixels.                                                                                       |
| AimSpread              | float             | 0             | Amount of random variance in the initial rotation of the item (in degrees).                                                                |
| Equip                  | bool              | false         | Should the item be automatically equipped when it spawns? Only valid if the item spawns in a character's inventory.                        |
| SpawnPosition          | SpawnPositionType | This          | Where should the item spawn?                                                                                                               |
| InheritEventTags       | bool              | false         |                                                                                                                                            |



### SpawnPositionType

| Value              | Description                                                                                                                   |
|--------------------|-------------------------------------------------------------------------------------------------------------------------------|
| This               | The position of the entity (item, character, limb) the StatusEffect is defined in.                                            |
| ThisInventory      | The inventory of the entity (item, character, limb) the StatusEffect is defined in.                                           |
| SameInventory      | The same inventory the StatusEffect's target entity is in. Only valid if the target is an Item.                               |
| ContainedInventory | The inventory of an item in the inventory of the StatusEffect's target entity (e.g. a container in the character's inventory) |
| Target             | The position of the entity the StatusEffect is targeting. If there are multiple targets, an item is spawned at all of them.   |



### SpawnRotationType

| Value    | Description                                                                                    |
|----------|------------------------------------------------------------------------------------------------|
| None     | Neutral (0) rotation. Can be rotated further using the Rotation attribute.                     |
| This     | The rotation of the entity executing the StatusEffect                                          |
| Target   | The rotation from the position of the spawned entity to the target of the StatusEffect         |
| Limb     | The rotation of the limb executing the StatusEffect, or the limb the StatusEffect is targeting |
| MainLimb | The rotation of the main limb (usually torso) of the character executing the StatusEffect      |
| Collider | The rotation of the collider of the character executing the StatusEffect                       |
| Random   | Random rotation between 0 and 360 degrees.                                                     |



## AbilityStatusEffectIdentifier
Can be used by AbilityConditionStatusEffectIdentifier to check whether some specific StatusEffect is running.

### Attributes

| Attribute        | Type       | Default value | Description                                        |
|------------------|------------|---------------|----------------------------------------------------|
| EffectIdentifier | identifier |               | An arbitrary identifier the Ability can check for. |



## GiveTalentInfo
Unlocks a talent, or multiple talents when the effect executes. Only valid if the target is a character or a limb.

### Attributes

| Attribute         | Type         | Default value | Description                                                                                                     |
|-------------------|--------------|---------------|-----------------------------------------------------------------------------------------------------------------|
| TalentIdentifiers | Identifier[] | []            | The identifier(s) of the talents that should be unlocked.                                                       |
| GiveRandom        | bool         | false         | If true and there's multiple identifiers defined, a random one will be chosen instead of unlocking all of them. |



## GiveSkill
Increases a character's skills when the effect executes. Only valid if the target is a character or a limb.

### Attributes

| Attribute       | Type       | Default value | Description                                                                                 |
|-----------------|------------|---------------|---------------------------------------------------------------------------------------------|
| SkillIdentifier | Identifier | ""            | The identifier of the skill to increase.                                                    |
| Amount          | float      | 0             | How much to increase the skill.                                                             |
| TriggerTalents  | bool       | true          | Should the talents that trigger when the character gains skills be triggered by the effect? |



## CharacterSpawnInfo
Defines characters spawned by the effect, and where and how they're spawned.

### Attributes

| Attribute               | Type       | Default value | Description                                                                                                                                                                                                                                  |
|-------------------------|------------|---------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| SpeciesName             | Identifier | ""            | The species name (identifier) of the character to spawn.                                                                                                                                                                                     |
| Count                   | int        | 1             | How many characters to spawn.                                                                                                                                                                                                                |
| TransferBuffs           | bool       | false         | Should the buffs of the character executing the effect be transferred to the spawned character? Useful for effects that "transform" a character to something else by deleting the character and spawning a new one on its place.             |
| TransferAfflictions     | bool       | false         | Should the afflictions of the character executing the effect be transferred to the spawned character? Useful for effects that "transform" a character to something else by deleting the character and spawning a new one on its place.       |
| TransferInventory       | bool       | false         | Should the the items from the character executing the effect be transferred to the spawned character? Useful for effects that "transform" a character to something else by deleting the character and spawning a new one on its place.       |
| TotalMaxCount           | int        | 0             | The maximum number of creatures of the given species and team that can exist in the current level before this status effect stops spawning any more.                                                                                         |
| Stun                    | int        | 0             | Amount of stun to apply on the spawned character.                                                                                                                                                                                            |
| AfflictionOnSpawn       | Identifier | ""            | An affliction to apply on the spawned character.                                                                                                                                                                                             |
| AfflictionStrength      | int        | 1             | The strength of the affliction applied on the spawned character. Only relevant if AfflictionOnSpawn is defined.                                                                                                                              |
| TransferControl         | bool       | false         | Should the player controlling the character that executes the effect gain control of the spawned character? Useful for effects that "transform" a character to something else by deleting the character and spawning a new one on its place. |
| RemovePreviousCharacter | bool       | false         | Should the character that executes the effect be removed when the effect executes? Useful for effects that "transform" a character to something else by deleting the character and spawning a new one on its place.                          |
| Spread                  | float      | 0             | Amount of random spread to add to the spawn position. Can be used to prevent all the characters from spawning at the exact same position if the effect spawns multiple ones.                                                                 |
| Offset                  | Vector2    | "0,0"         | Offset added to the spawn position. Can be used to for example spawn a character a bit up from the center of an item executing the effect.                                                                                                   |
| InheritEventTags        | bool       | false         |                                                                                                                                                                                                                                              |



## AITrigger
Can be used to trigger a behavior change of some kind on an AI character. Only applicable for enemy characters, not humans.

### Attributes

| Attribute           | Type    | Default value | Description                                                                                                                                         |
|---------------------|---------|---------------|-----------------------------------------------------------------------------------------------------------------------------------------------------|
| State               | AIState | Idle          | The AI state the character should switch to.                                                                                                        |
| Duration            | float   | 0             | How long should the character stay in the specified state? If 0, the effect is permanent (unless overridden by another AITrigger).                  |
| Probability         | float   | 1             | How likely is the AI to change the state when this effect executes? 1 = always, 0.5 = 50% chance, 0 = never.                                        |
| MinDamage           | float   | 0             | How much damage the character must receive for this AITrigger to become active? Checks the amount of damage the latest attack did to the character. |
| AllowToOverride     | bool    | true          | Can this AITrigger override other active AITriggers?                                                                                                |
| AllowToBeOverridden | bool    | true          | Can this AITrigger be overridden by other AITriggers?                                                                                               |


