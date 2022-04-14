# Turret


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| BarrelPos|Vector2|"0,0"|The position of the barrel relative to the upper left corner of the base sprite (in pixels). |
| FiringOffset|Vector2|"0,0"|The projectile launching location relative to transformed barrel position (in pixels). |
| LaunchImpulse|float|0.0|The impulse applied to the physics body of the projectile (the higher the impulse, the faster the projectiles are launched). |
| Reload|float|5.0|The period of time the user has to wait between shots. |
| RetractionDurationMultiplier|float|1.0|Modifies the duration of retraction of the barrell after recoil to get back to the original position after shooting. Reload time affects this too. |
| RecoilTime|float|0.1|How quickly the recoil moves the barrel after launching. |
| RetractionDelay|float|0|How long the barrell stays in place after the recoil and before retracting back to the original position. |
| ProjectileCount|int|1|How many projectiles the weapon launches when fired once. |
| LaunchWithoutProjectile|bool|false|Can the turret be fired without projectiles (causing it just to execute the OnUse effects and the firing animation without actually firing anything). |
| RotationLimits|Vector2|"0.0,0.0"|The range at which the barrel can rotate. |
| Spread|float|0.0|Random spread applied to the firing angle of the projectiles (in degrees). |
| SpringStiffnessLowSkill|float|5.0|How much torque is applied to rotate the barrel when the item is used by a character with insufficient skills to operate it. Higher values make the barrel rotate faster. |
| SpringStiffnessHighSkill|float|2.0|How much torque is applied to rotate the barrel when the item is used by a character with sufficient skills to operate it. Higher values make the barrel rotate faster. |
| SpringDampingLowSkill|float|50.0|How much torque is applied to resist the movement of the barrel when the item is used by a character with insufficient skills to operate it. Higher values make the aiming more "snappy", stopping the barrel from swinging around the direction it's being aimed at. |
| SpringDampingHighSkill|float|10.0|How much torque is applied to resist the movement of the barrel when the item is used by a character with sufficient skills to operate it. Higher values make the aiming more "snappy", stopping the barrel from swinging around the direction it's being aimed at. |
| RotationSpeedLowSkill|float|1.0|Maximum angular velocity of the barrel when used by a character with insufficient skills to operate it. |
| RotationSpeedHighSkill|float|5.0|Maximum angular velocity of the barrel when used by a character with sufficient skills to operate it. |
| FiringRotationSpeedModifier|float|1.0|How fast the turret can rotate while firing (for charged weapons). |
| SingleChargedShot|bool|false|Whether the turret should always charge-up fully to shoot. |
| BaseRotation|float|0.0|The angle of the turret's base in degrees. |
| AIRange|float|3000.0|How close to a target the turret has to be for an AI character to fire it. |
| MaxActiveProjectiles|int|-1|The turret won't fire additional projectiles if the number of previously fired, still active projectiles reaches this limit. If set to -1, there is no limit to the number of projectiles. |
| MaxChargeTime|float|0|The time required for a charge-type turret to charge up before able to fire. |



## Example
```xml
<Item identifier="turrethardpoint" Tags="turret" showinstatusmonitor="false" category="Machine,Weapon" interactthroughwalls="true" Scale="0.5" interactdistance="10" spritecolor="1.0,1.0,1.0,1.0" linkable="true" allowedlinks="turretammosource">
  <Turret canbeselected="false" characterusable="false" linkable="true" barrelpos="128,88">
    <LightComponent LightColor="1.0,0.8,0.8,1.0" Flicker="0.0" range="2500" IsOn="true" drawbehindsubs="true" ignorecontinuoustoggle="true">
      <LightTexture texture="Content/Lights/alphaOne.png" />
    </LightComponent>
  </Turret>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="power_in" displayname="connection.powerin" />
    <input name="position_in" displayname="connection.turretaimingin" />
    <input name="trigger_in" displayname="connection.turrettriggerin" />
    <input name="toggle_light" displayname="connection.togglelight" />
    <input name="set_light" displayname="connection.setlight" />
  </ConnectionPanel>
  [...]
</Item>
```

