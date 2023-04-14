# Explosion


## Explosion
Explosions are area of effect attacks that can damage characters, items and structures.

This type also supports the attributes defined in: [Attack](/BaroModDoc/Misc/Attack.md)

### Attributes

| Attribute                | Type     | Default value                                                                                 | Description                                                                                                                     |
|--------------------------|----------|-----------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------|
| force                    | float    | 0                                                                                             | How much force the explosion applies to the characters.                                                                         |
| sparks                   | bool     | Same as showEffects                                                                           | Whether a spark particle effect is created when the explosion happens.                                                          |
| shockwave                | bool     | Same as showEffects                                                                           | Whether a shockwave particle effect is created when the explosion happens.                                                      |
| flames                   | bool     | Same as showEffects                                                                           | Whether a flame particle effect is created when the explosion happens.                                                          |
| underwaterBubble         | bool     | Same as showEffects                                                                           | Whether a underwater bubble particle effect is created when the explosion happens.                                              |
| smoke                    | bool     | Same as showEffects                                                                           | Whether a smoke particle effect is created when the explosion happens.                                                          |
| playTinnitus             | bool     | Same as showEffects                                                                           | Whether the explosion plays a tinnitus sound to players who get hit by it.                                                      |
| applyFireEffects         | bool     | true if showEffects is true and flames haven't been explicitly set to false, false otherwise. | Whether the explosion executes 'OnFire' status effects on the items it hits.                                                    |
| ignoreFireEffectsForTags | string[] | []                                                                                            | List of item tags that the explosion ignores when applying fire effects.                                                        |
| ignoreCover              | bool     | false                                                                                         | When set to true, the explosion don't deal less damage when the target is behind a solid object.                                |
| OnlyInside               | bool     | false                                                                                         | Whether the explosion only affects characters inside a submarine.                                                               |
| OnlyOutside              | bool     | false                                                                                         | Whether the explosion only affects characters outside a submarine.                                                              |
| flash                    | bool     | Same as showEffects                                                                           | Whether a flash effect is created when the explosion happens.                                                                   |
| flashDuration            | float    | 0.05                                                                                          | How long the light source created by the explosion lasts.                                                                       |
| flashColor               | Color    | LightYellow                                                                                   | Color of the light source created by the explosion.                                                                             |
| EmpStrength              | float    | 0                                                                                             | Strength of the EMP effect created by the explosion.                                                                            |
| BallastFloraDamage       | float    | 0                                                                                             | How much damage the explosion does to ballast flora.                                                                            |
| itemRepairStrength       | float    | 0                                                                                             | How much the explosion repairs items.                                                                                           |
| decal                    | string   | ""                                                                                            | Identifier of the decal the explosion creates on the background structure it explodes over.<br/>Set to empty string to disable. |
| decalSize,decalsize      | float    | 1                                                                                             | Relative size of the decal created by the explosion.                                                                            |
| cameraShake              | float    | 10% of the range if showEffects is true, 0 otherwise.                                         | Intensity of the screen shake effect.                                                                                           |
| cameraShakeRange         | float    | Same as attack range if showEffects is true, 0 otherwise.                                     | How far away does the camera shake effect reach.                                                                                |
| screenColorRange         | float    | 10% of the range if showEffects is true, 0 otherwise.                                         | How far away can the screen color effect be seen.                                                                               |
| screenColor              | Color    | Transparent                                                                                   | Color tint to apply to the player's screen when in range of the explosion.                                                      |
| screenColorDuration      | float    | 0.1                                                                                           | How long the screen color effect lasts.                                                                                         |
| flashRange               | float?   | 100                                                                                           | How large the light source created by the explosion is.                                                                         |
| showEffects              | bool     | true                                                                                          | Used to enable all particle effects without having to specify them one by one.                                                  |


