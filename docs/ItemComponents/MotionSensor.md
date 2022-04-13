# MotionSensor


## Example
```xml
<Item name="" identifier="alienmotionsensor" category="Alien" Tags="alien,alienmotionsensor" scale="0.5">
  <MotionSensor range="75" output="0" onlyhumans="true" ignoredead="true" />
  <ConnectionPanel canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <RequiredItem items="screwdriver" type="Equipped" />
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <output name="state_out" displayname="connection.stateout" fallbackdisplayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

