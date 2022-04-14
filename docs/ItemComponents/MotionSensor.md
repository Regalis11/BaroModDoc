# MotionSensor


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| MotionDetected|bool|false|Has the item currently detected movement. Intended to be used by StatusEffect conditionals (setting this value in XML has no effect). |
| Target|TargetType|TargetType.Any|Which kind of targets can trigger the sensor? |
| IgnoreDead|bool|false|Should the sensor ignore the bodies of dead characters? |
| RangeX|float|0.0|Horizontal detection range. |
| RangeY|float|0.0|Vertical movement detection range. |
| DetectOffset|Vector2|"0,0"|The position to detect the movement at relative to the item. For example, 0,100 would detect movement 100 units above the item. |
| UpdateInterval|float|0.1|How often the sensor checks if there's something moving near it. Higher values are better for performance. |
| MaxOutputLength|int|200|The maximum length of the output strings. Warning: Large values can lead to large memory usage or networking issues. |
| Output|string|"1"|The signal the item outputs when it has detected movement. |
| FalseOutput|string|""|The signal the item outputs when it has not detected movement. |
| MinimumVelocity|float|0.01|How fast the objects within the detector's range have to be moving (in m/s). |
| DetectOwnMotion|bool|true|Should the sensor trigger when the item itself moves. |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="alienmotionsensor" category="Alien" Tags="alien,alienmotionsensor" scale="0.5">
  <MotionSensor range="75" output="0" onlyhumans="true" ignoredead="true" />
  <ConnectionPanel canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <RequiredItem items="screwdriver" type="Equipped" />
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <output name="state_out" displayname="connection.stateout" fallbackdisplayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

