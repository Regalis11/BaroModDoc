
### Elements

| Element            | Type                              | Description                                                                                                                                |
|--------------------|-----------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------|
| explosion          | [Explosion](Misc/Explosion.md)    | Creates an explosion when the effect triggers.                                                                                             |
| fire               | -                                 | Starts a fire of the specified size when the effect triggers.                                                                              |
| removeitem         | -                                 | Removes all of the items the effect is targeting.                                                                                          |
| dropcontaineditems | -                                 | Drops all the items from the inventories of the items or characters the effect is targeting.                                               |
| dropitem           | -                                 | Makes all the items the effect is targeting drop from the inventories they are in.                                                         |
| removecharacter    | -                                 | Removes all of the characters the effect is targeting, or if the effect is targeting limbs, the characters those limbs belong to.          |
| breaklimb          | -                                 | Severs all the limbs the effect is targeting.                                                                                              |
| hidelimb           | -                                 | Hides all the limbs the effect is targeting.                                                                                               |
| requireditem       | [RelatedItem](Misc/RelatedItem.md)| Defines item(s) the entity executing the effect must have. Can be used in a variety of ways: e.g. to check if a character has something in their inventory or hands, if an item is inside a specific kind of container, or if there's a specific kind of item inside the item.                                                                                                                                    |
| requireditems      | [RelatedItem](Misc/RelatedItem.md)| Alias of "requireditem". Functionally identical.                                                                                           |
| requiredaffliction | -                                 | Which type of afflictions the target must receive for the StatusEffect to be applied. Only valid when the type of the effect is OnDamaged. |
| affliction         | -                                 | An affliction to give to the character or limb the effect is targeting.                                                                    |
| aitrigger          | -                                 | TODO                                                                                                                                       |
| talenttrigger      | -                                 | Can be used by AbilityConditionStatusEffectIdentifier to react to a specific kind of status effect triggering.                             |
| giveexperience     | -                                 | Can be used to give experience points to the character(s) the effect is targeting.                                                         |
| giveskill          | GiveSkill                         | Can be used to give skill points to the character(s) the effect is targeting.                                                              |


## Fire
Starts a fire of the specified size when the effect triggers.  

### Attributes

| Attribute              | Type              | Default value   | Description                                                                                                         |
|------------------------|-------------------|-----------------|---------------------------------------------------------------------------------------------------------------------|
| size                   | float             | 10.0f           | Initial width of the fire in pixels.                                                                                |

## RequiredAffliction
Which type of afflictions the target must receive for the StatusEffect to be applied. Only valid when the type of the effect is OnDamaged.

### Attributes

| Attribute              | Type              | Default value   | Description                                                                                                         |
|------------------------|-------------------|-----------------|---------------------------------------------------------------------------------------------------------------------|
| identifier, type       | Identifier[]      |                 | Identifier or type, or a list of identifiers or types of the afflictions.                                           |
| minstrength            | float             | 0.0f            | Minimum strength of the affliction the target must receive.                                                         |

## Affliction
An affliction to give to the character or limb the effect is targeting. 

### Attributes

| Attribute              | Type              | Default value   | Description                                                                                                         |
|------------------------|-------------------|-----------------|---------------------------------------------------------------------------------------------------------------------|
| identifier             | Identifier        |                 | Identifier of the affliction to give to the target.                                                                 |
| strength               | float             | 0.0f            | Strength of the affliction to give to the target.                                                                   |
| probability            | float             | 1.0f            | Probability of giving the affliction to the target (0 = never, 0.5 = 50% change, 1.0 = always)                      |


## TalentTrigger
Can be used by AbilityConditionStatusEffectIdentifier to react to a specific kind of status effect triggering.

### Attributes

| Attribute              | Type              | Default value   | Description                                                                                                         |
|------------------------|-------------------|-----------------|---------------------------------------------------------------------------------------------------------------------|
| effectidentifier       | Identifier        |                 | An arbitrary identifier that talents can check using AbilityConditionStatusEffectIdentifier.                        |


## GiveExperience
Can be used to give experience points to the character(s) the effect is targeting.

### Attributes

| Attribute              | Type              | Default value   | Description                                                                                                         |
|------------------------|-------------------|-----------------|---------------------------------------------------------------------------------------------------------------------|
| amount                 | int               |                 | Amount of experience points to give.                                                                                |