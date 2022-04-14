# Holdable


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| CanPush|bool|true|Is the item currently able to push characters around? True by default. Only valid if blocksplayers is set to true. |
| Attached|bool|false|Is the item currently attached to a wall (only valid if Attachable is set to true). |
| Aimable|bool|true|Can the item be pointed to a specific direction or do the characters always hold it in a static pose. |
| ControlPose|bool|false|Should the character adjust its pose when aiming with the item. Most noticeable underwater, where the character will rotate its entire body to face the direction the item is aimed at. |
| UseHandRotationForHoldAngle|bool|false|Use the hand rotation instead of torso rotation for the item hold angle. Enable this if you want the item just to follow with the arm when not aiming instead of forcing the arm to a hold pose. |
| Attachable|bool|false|Can the item be attached to walls. |
| Reattachable|bool|true|Can the item be reattached to walls after it has been deattached (only valid if Attachable is set to true). |
| LimitedAttachable|bool|false|Can the item only be attached in limited amount? Uses permanent stat values to check for legibility. |
| AttachedByDefault|bool|false|Should the item be attached to a wall by default when it's placed in the submarine editor. |
| HoldPos|Vector2|"0.0,0.0"|The position the character holds the item at (in pixels, as an offset from the character's shoulder). For example, a value of 10,-100 would make the character hold the item 100 pixels below the shoulder and 10 pixels forwards. |
| AimPos|Vector2|"0.0,0.0"|The position the character holds the item at when aiming (in pixels, as an offset from the character's shoulder). Works similarly as HoldPos, except that the position is rotated according to the direction the player is aiming at. For example, a value of 10,-100 would make the character hold the item 100 pixels below the shoulder and 10 pixels forwards when aiming directly to the right. |
| HoldAngle|float|0.0|The rotation at which the character holds the item (in degrees, relative to the rotation of the character's hand). |
| SwingAmount|Vector2|"0.0,0.0"|How much the item swings around when aiming/holding it (in pixels, as an offset from AimPos/HoldPos). |
| SwingSpeed|float|0.0|How fast the item swings around when aiming/holding it (only valid if SwingAmount is set). |
| SwingWhenHolding|bool|false|Should the item swing around when it's being held. |
| SwingWhenAiming|bool|false|Should the item swing around when it's being aimed. |
| SwingWhenUsing|bool|false|Should the item swing around when it's being used (for example, when firing a weapon or a welding tool). |
| SpriteDepthWhenDropped|float|0.55|Sprite depth that's used when the item is NOT attached to a wall. |



## Example
```xml
<Item identifier="paralyxis" category="Material" maxstacksize="8" Tags="smallitem" scale="0.5" cargocontaineridentifier="metalcrate" canbepicked="true">
  <Holdable canBeCombined="true" removeOnCombined="true" slots="Any,RightHand,LeftHand" handle1="0,0" msg="ItemMsgPickUpSelect" attachable="true" reattachable="false">
    <!-- Remove the item when fully used -->
    <StatusEffect type="OnBroken" target="This">
      <Remove />
    </StatusEffect>
  </Holdable>
  <LevelResource deattachduration="4" randomoffsetfromwall="20">
    <Commonness commonness="0.02" />
    <RequiredItem items="cuttingequipment" type="Equipped" />
  </LevelResource>
  [...]
</Item>
```

