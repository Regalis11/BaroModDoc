# Character
<sup>Relevant files: [[Shared:CharacterFile.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/CharacterFile.cs) [[Shared:CharacterPrefab.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/CharacterPrefab.cs) [[Shared:CharacterParams.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/Params/CharacterParams.cs) [[Shared:RagdollParams.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/Params/Ragdoll/RagdollParams.cs) [[Shared:AnimationParams.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/Params/Animation/AnimationParams.cs)</sup>

- **Required by core package:** Yes

This page describes the XML attributes and child elements for characters, which you may need to edit in a text editor if the [character editor doesn't support what you're trying to do](../Editors/CharacterEditor.md#limitations).

## Root element attributes

- `SpeciesName`: Used for referencing the character, like [Item identifier](../ContentTypes/Item.md).
- `VariantOf`: If defined, this tells the game to inherit the child elements and attributes of another species. More details in the [Variants](#variants) section.
- `SpeciesTranslationOverride`: References to another species. Used only if we want to reuse the name defined for it in the [Text files](../ContentTypes/Text.md).
- `DisplayName`: Overrides the name of the character, shown to the player. Leave blank, if the name is defined in the [Text files](../ContentTypes/Text.md) (e.g. `<character.crawler>Crawler</character.crawler>`)!
- `Group`: If defined, different species of the same group consider each other friendly and do not attack each other.
- `Humanoid`: If set to `true`, the character is a humanoid and has different animation constraints relative to non-humanoid characters.
- `HasInfo`: If set to `true`, [jobs](Jobs.md) can be assigned to characters of this species.
- `SpecifierTags`: If set to `true` and `HasInfo` is also set to true, tags can be attached to characters of this species. More details in the [Characters with specifier tags](#characters-with-specifier-tags) section.
- `CanInteract`: If set to `true`, this character can interact with items.
- `Husk`: If set to `true`, this character is treated as a husk by the AI.
- `UseHuskAppendage`: If set to `true`, this character uses a special husk appendage, attached to the ragdoll, when it turns into a husk. The appendage is defined in the character file of the huskified species, e.g. `<huskappendage affliction="huskinfection" path="Content/Characters/Humanhusk/Huskappendage.xml" />` in Humanhusk.xml.
- `NeedsAir`: If set to `true`, this character needs oxygen to survive. This also makes the character vulnerable to high pressure when swimming outside of the submarine.
- `NeedsWater`: If set to `true`, the character slowly suffocates when it's not in water, like a fish.
- `CanSpeak`: If set to `true`, this character is able to send messages in the chat.
- `UseBossHealthBar`: If set to `true`, this character's health is shown at the top of the player's screen when they are in an active encounter.
- `Noise`: Determines the amount of sound this character makes when it moves, affecting how far the other monsters can detect the character. Also the character's size affects this. Defaults to 100.
- `Visibility`: Defines how visible the character is to the other monsters. Also the character size and the movement speed affects the actual range in which the monsters can detect the character. Defaults to 100.
- `BloodDecal`: The identifier of the [decal](Decals.md) to use when this character bleeds.
- `BleedParticleAir`: The identifier of the [particle](Particles.md) to use when bleeding in dry places.
- `BleedParticleWater`: The identifier of the [particle](Particles.md) to use when bleeding in water.
- `BleedParticleMultiplier`: A multiplier to increase or decrease the number of bleeding particles to create.
- `CanEat`: If set to `true`, this character is able to eat bodies. This only works for non-humanoids.
- `EatingSpeed`: A multiplier for the amount of time it takes to eat a body. Defaults to 10.
- `UsePathFinding`: If set to `true`, the AI for this character will use the waypoints defined in the level to find a path to its targets.
- `PathFinderPriority`: A lower value decreases the intensive path finding call frequency. Set to a lower value for insignificant creatures to improve performance.
- `HideInSonar`: If set to `true`, this character doesn't appear in the sonar.
- `HideInThermalGoggles`: If set to `true`, this character isn't visible when using thermal goggles.
- `SonarDisruption`: If set to a value greater than zero, this character creates disrupting noise on the sonar when within range.
- `DistantSonarRange`: Range at which "long distance" blips for this character will appear on the sonar (used on some of the Abyss monsters).
- `DisableDistance`: The maximum distance (in pixels) from the closest player at which the character will stay active in the level.
- `SoundInterval`: The time the game waits between each time it plays this character's sounds.
- `DrawLast`: If set to `true`, this character will be drawn on top of characters that do not have this set. This currently has no effect if the character has no deformable sprites.

## Child elements

- `ragdolls` & `animations`: These elements are used to determine the folders the game should look to find the ragdoll and animation parameters for this character. The [character editor](../Editors/CharacterEditor.md#file-structure) generates these and it's typically not necessary to edit them directly.

- `damageemitter`, `bloodemitter` & `gibemitter`: These elements define the particles to use when this character is damaged, bleeds, and is gibbed respectively.
  - Example:

```xml
<damageemitter particle="gib" drawontop="True" particleamount="10" velocitymin="50" velocitymax="300" anglemin="0" anglemax="360" scalemin="0.25" scalemax="0.5" emitinterval="0" particlespersecond="0" highqualitycollisiondetection="False" copyentityangle="False" />
<bloodemitter particle="blood" particleamount="10" anglemin="0" anglemax="0" scalemin="1" scalemax="1" velocitymin="0" velocitymax="0" emitinterval="0" particlespersecond="0" highqualitycollisiondetection="False" copyentityangle="False" />
<bloodemitter particle="waterblood" particleamount="1" anglemin="0" anglemax="0" scalemin="1" scalemax="1" velocitymin="0" velocitymax="0" emitinterval="0" particlespersecond="0" highqualitycollisiondetection="False" copyentityangle="False" />
<gibemitter particle="gib" particleamount="20" velocitymin="200" velocitymax="700" anglemin="0" anglemax="360" scalemin="1" scalemax="1" emitinterval="0" particlespersecond="0" highqualitycollisiondetection="False" copyentityangle="False" />
```

- `health`: Defines numerous properties relating to this character's health, such as max vitality, crush depth and health regeneration. In the case of playable characters, it's also used to determine the appearance of the healing UI. Note that the ´healthindex´ defined for each limb on the ragdoll file refers to the limb(health) definitions laid out here.
  - Example:

```xml
<health vitality="80" doesbleed="True" crushdepth="-Infinity" usehealthwindow="False" bleedingreduction="0.2" burnreduction="0" constanthealthregeneration="0" healthregenerationwheneating="10">
  <Limb name="Torso">
    <VitalityMultiplier type="damage,burn" multiplier="1.0" />
  </Limb>
  <Limb name="Head">
    <VitalityMultiplier type="damage,burn" multiplier="1.5" />
  </Limb>
  <Limb name="LeftLeg">
    <VitalityMultiplier type="damage,burn" multiplier="0.75" />
  </Limb>
  <Limb name="RightLeg">
    <VitalityMultiplier type="damage,burn" multiplier="0.75" />
  </Limb>
  <Limb>
    <!--Tail-->
    <VitalityMultiplier type="damage,burn" multiplier="0.75" />
  </Limb>
</health>
```

- `Inventory`: Defines the number and type of slots this character's inventory will have. If omitted, the character will not have an inventory at all. This is both used to define the playable characters' inventory, as well as defining the loot for certain creatures.
  - Example:

```xml
<Inventory slots="Any, Any" accessiblewhenalive="False" commonness="1">
  <Item identifier="crawlermask" />
  <Item identifier="alienblood" />
</Inventory>
<Inventory slots="Any, Any" accessiblewhenalive="False" commonness="50">
  <Item identifier="alienblood" />
</Inventory>
```

- `ai`: Defines the targets and the priorities that the AI uses for making decisions. There are too many parameters to list here, but fortunately you can see them all in the character editor. Note that some of them has an effect only when used with a specific `attackpattern`. Others, like `threshold` are generic parameters that have different meaning depending on the targeting `state`. Check for examples in the game files.
  - Example:

```xml
<ai combatstrength="100" sight="1" hearing="1.0" fleehealththreshold="10" aggressiveboarding="true" aggressiongreed="10" aggressionhurt="200" avoidgunfire="True">
  <target tag="decoy" state="Attack" priority="500" reactdistance="0" ignoreifnotinsamesub="True"/>
  <target tag="stronger" state="Avoid" priority="200" reactdistance="2000" />
  <target tag="husk" state="PassiveAggressive" priority="200" reactdistance="2000" attackdistance="500"/>
  <target tag="provocative" state="Attack" priority="100" reactdistance="0" ignoreifnotinsamesub="True"/>
  <target tag="weapon" state="Attack" priority="100" reactdistance="0" ignoreifnotinsamesub="True"/>
  <target tag="dead" state="Eat" priority="100" reactdistance="0" />
  <!-- ... -->
</ai>
```

[TODO: status effects require a dedicated page]
- `StatusEffect`: Defines actions to perform given one of the following supported statuses the character can be in: `Always`, `OnSpawn`, `OnActive`, `OnEating`, `OnImpact`, `InWater`, `NotInWater`, `OnDamaged`, `OnSevered`, `OnFire`, `OnDeath`.

  - Example:

```xml
<StatusEffect type="OnActive" target="this">
  <Sound file="Content/Characters/Latcher/ABYSSM_movingLoop.ogg" loop="true" range="20000" dontmuffle="true" />
</StatusEffect>
```

- `sound`: Defines a sound to play given the character's state. If specifier tags are defined, sounds can be constrained to only play if the character has a given tag.
  - Example:

```xml
<sound File="Content/Characters/Human/female_damage1.ogg" State="Damage" Range="500" Volume="1" Tags="Female" />
<sound File="Content/Characters/Human/female_damage2.ogg" State="Damage" Range="500" Volume="1" Tags="Female" />
<sound File="Content/Characters/Human/female_damage3.ogg" State="Damage" Range="500" Volume="1" Tags="Female" />
<sound File="Content/Characters/Human/female_damage4.ogg" State="Damage" Range="500" Volume="1" Tags="Female" />
<sound File="Content/Characters/Human/male_damage1.ogg" State="Damage" Range="500" Volume="1" Tags="Male" />
<sound File="Content/Characters/Human/male_damage2.ogg" State="Damage" Range="500" Volume="1" Tags="Male" />
<sound File="Content/Characters/Human/male_damage3.ogg" State="Damage" Range="500" Volume="1" Tags="Male" />
<sound File="Content/Characters/Human/male_damage4.ogg" State="Damage" Range="500" Volume="1" Tags="Male" />
```

- `huskappendage`: Defines a husk appendage to attach to the character. The appendage can be marked to only be active when the character has a certain affliction.
  - Example:

```xml
<huskappendage affliction="huskinfection" path="Content/Characters/Humanhusk/Huskappendage.xml" /> 
<huskappendage affliction="naturalmeleeweapon" path="Content/Characters/Humanhusk/Mudraptorbeak.xml" onlyfromafflictions="true" />
<huskappendage affliction="naturalrangedweapon" path="Content/Characters/Humanhusk/Spinelingspine.xml" onlyfromafflictions="true" /> 
```

### Characters with specifier tags

These child elements can only be used in characters that set `HasInfo` and `SpecifierTags` to `true`.

- `names`: Defines a file to load character names from. This file defines several lists of words to use as parts of the name, and the way to join them together.

- `Vars`: Defines the categories that some tags are under. This is used by the vanilla game to define gender.
  - Example:

```xml
<Vars>
  <Var var="GENDER" tags="female,male" />
  <Var var="SOUNDKIND" tags="pd,metal,decay" />
  <Var var="NEUTRAL" tags="neutral" />
</Vars>
```


- `Heads`: Defines the heads that can be selected when creating the character. Each head must have a unique set of tags associated to it.

The sprites use the same `sourcerect` as the head limb defined in the ragdoll file. For the vanilla human that would be 128x128 pixels. `sheetindex` is then used to offset the position of the sprite in the texture sheet, i.e. the first two components in the `sourcerect` definition (x and y). As a result, you don't need to define the source rect for each head variant separately, just mark where they are in the imaginary grid.
  - Example:

```xml
<Heads>
  <Head tags="head1,male,pd,neutral" sheetindex="0,0"/>
  <Head tags="head2,male,pd,neutral" sheetindex="1,0"/>
  <Head tags="head3,male,pd,neutral" sheetindex="2,0"/>
  <Head tags="head4,male,pd,neutral" sheetindex="3,0"/>
  <Head tags="head1,female,pd,neutral" sheetindex="0,0"/>
  <Head tags="head2,female,pd,neutral" sheetindex="1,0"/>
  <Head tags="head3,female,pd,neutral" sheetindex="2,0"/>
  <Head tags="head4,female,pd,neutral" sheetindex="3,0"/>
</Heads>
```



- `MenuCategory`: Defines the category defined in the `Vars` element to use to split the character head selection in the lobby.
- `Pronouns`: Defines the category defined in the `Vars` element to use to determine the character's pronouns. Currently only used in the messages issued relating to traitor missions.
  - Example:

```xml
<MenuCategory var="GENDER" />
<Pronouns var="GENDER" />
```

- `HeadAttachments`: Defines hair, facial hair, and accessories that can be assigned for each tag or set of tags. Uses the same logic for offsetting as the head definitions.
  - Example:

```xml
<HeadAttachments>
    <Wearable type="Hair" tags="male">
      <sprite name="Hair 1" texture="Content/Characters/Human/Human_male_hair.png" sheetindex="0,0" />
    </Wearable>
    <Wearable type="Hair" tags="female">
      <sprite name="Hair 1" texture="Content/Characters/Human/Human_female_hair.png" sheetindex="0,0" />
    </Wearable>
    <Wearable type="Beard" tags="male">
      <sprite name="Beard 1" texture="Content/Characters/Human/Human_beards.png" sheetindex="0,0" />
    </Wearable>
    <Wearable type="FaceAttachment" commonness="0.01">
      <sprite name="FaceAttachment 1" texture="Content/Characters/Human/Human_head_accessories.png" sheetindex="0,0" />
    </Wearable>
    <Wearable type="Husk">
      <sprite name="Husk" texture="Content/Characters/Human/Human_husk.png" hidewearablesoftype="Beard" sheetindex="0,0" />
    </Wearable>
    <Wearable type="Herpes">
      <sprite name="Herpes" texture="Content/Characters/Human/Human_karma.png" sheetindex="0,0" />
    </Wearable>
  </HeadAttachments>
  ```


### Variants

The following child elements can only be used in characters that define the `VariantOf` attribute. They allow certain modifiers to be applied to the attributes inherited from their base species.

- `ragdoll`: Allows a scale multiplier and impact tolerance to be defined.
- `attack`: Allows damage, range and impact multipliers to be defined.
  - Example:

```xml
<ragdoll scalemultiplier="1.375" impacttolerance="60" />
<attack damagemultiplier="2" rangemultiplier="1.375" impactmultiplier="2" />
```
