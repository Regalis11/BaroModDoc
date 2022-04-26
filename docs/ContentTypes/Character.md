# Character
<sup>Relevant files: [[Shared:CharacterFile.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/CharacterFile.cs) [[Shared:CharacterPrefab.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/CharacterPrefab.cs)</sup>

- **Required by core package:** Yes

This page describes the XML attributes and child elements for characters, which you may need to edit in a text editor if the [character editor doesn't support what you're trying to do](../Editors/CharacterEditor.md#limitations).

## Root element attributes

- `SpeciesName`: This is used to be able to make references to the item, as well as determine what to remove when [overriding](../Intro/Overrides.md). If `SpeciesTranslationOverride` and `DisplayName` are not defined, it is also used to fetch the required [name](Text.md) to display in the player's inventory.
- `SpeciesTranslationOverride`: If defined, this is the tag used to fetch the name to display when hovering over this character or selecting it in the character editor.
- `DisplayName`: If defined, this is displayed when hovering over this character or selecting it in the character editor.
- `Group`: If defined, different species of the same group consider each other friendly and do not attack each other.
- `Humanoid`: If set to `true`, the character is a humanoid and has different animation constraints relative to non-humanoid characters.
- `HasInfo`: If set to `true`, [jobs](Jobs.md) can be assigned to characters of this species.
- `SpecifierTags`: If set to `true` and `HasInfo` is also set to true, tags can be attached to characters of this species. More details in the [specifier tags](#specifier-tags) section.
- `CanInteract`: If set to `true`, this character can interact with items and other characters in the level.
- `Husk`: If set to `true`, this character is treated as a husk by the AI.
- `UseHuskAppendage`: If set to `true`, and `Husk` is set to `true`, then this husk has a husk appendage.
- `NeedsAir`: If set to `true`, this character needs to be in a hull full with air to survive; this also makes the character vulnerable to high pressure when swimming outside of the submarine.
- `NeedsWater`: If set to `true`, this character needs to be in water to survive.
- `CanSpeak`: If set to `true`, this character is able to send messages in the chat.
- `UseBossHealthBar`: If set to `true`, this character's health is shown at the top of the player's screen when within range.
- `Noise`: How much sound this character makes when moving, which determines the range in which AI can detect it. Defaults to 100.
- `Visibility`: This value is used as part of the calculation AI makes to determine whether or not it can see this character. Defaults to 100.
- `BloodDecal`: The identifier of the [decal](Decals.md) to use when this character bleeds.
- `BleedParticleAir`: The identifier of the [particle](Particles.md) to use when bleeding out of water.
- `BleedParticleWater`: The identifier of the [particle](Particles.md) to use when bleeding in water.
- `BleedParticleMultiplier`: A multiplier to increase or decrease the number of bleeding particles to create.
- `CanEat`: If set to `true`, this character is able to eat bodies. This only works for non-humanoids.
- `EatingSpeed`: A multiplier for the amount of time it takes to eat a body. Defaults to 10.
- `UsePathFinding`: If set to `true`, the AI for this character will use the waypoints defined in the level to find a path to its targets.
- `PathFinderPriority`: A lower value decreases the intensive path finding call frequency. Set to a lower value for insignificant creatures to improve performance.
- `HideInSonar`: If set to `true`, this character doesn't appear in the sonar.
- `HideInThermalGoggles`: If set to `true`, this character isn't visible when using thermal goggles.
- `SonarDisruption`: If set to a value greater than zero, this character creates noise on the sonar when within range.
- `DistantSonarRange`: Range at which "long distance" blips for this character will appear on the sonar.
- `DisableDistance`: The maximum distance from a player where characters of this species will still have physics and AI enabled.
- `SoundInterval`: The time the game waits between each time it plays this character's sounds.
- `DrawLast`: If set to `true`, this character will be drawn on top of characters that do not have this set.

## Specifier tags

