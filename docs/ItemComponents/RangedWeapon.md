# RangedWeapon


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| BarrelPos|string|"0.0,0.0"|The position of the barrel as an offset from the item's center (in pixels). Determines where the projectiles spawn. |
| Reload|float|1.0|How long the user has to wait before they can fire the weapon again (in seconds). |
| HoldTrigger|bool|false|Tells the AI to hold the trigger down when it uses this weapon |
| ProjectileCount|int|1|How projectiles the weapon launches when fired once. |
| Spread|float|0.0|Random spread applied to the firing angle of the projectiles when used by a character with sufficient skills to use the weapon (in degrees). |
| UnskilledSpread|float|0.0|Random spread applied to the firing angle of the projectiles when used by a character with insufficient skills to use the weapon (in degrees). |
| MaxChargeTime|float|0|The time required for a charge-type turret to charge up before able to fire. |



## Example
```xml
<Item identifier="bikehorn" category="Misc" tags="smallitem,hornitem" scale="0.5" cargocontaineridentifier="metalcrate">
  <RangedWeapon reload="2">
    <Sound file="Content/Items/Weapons/honk.ogg" type="OnUse" />
  </RangedWeapon>
  [...]
</Item>
```

