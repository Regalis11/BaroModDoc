# OscillatorComponent


## Attributes

| Attribute  | Type     | Default value | Description                                                                                                                                                                                                                                                                                                                                                                                          |
|------------|----------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| OutputType | WaveType | Pulse         | What kind of a signal the item outputs. Pulse: periodically sends out a signal of 1. Sawtooth: sends out a periodic wave that increases linearly from 0 to 1. Sine: sends out a sine wave oscillating between -1 and 1. Square: sends out a signal that alternates between 0 and 1. Triangle: sends out a wave that alternates between increasing linearly from -1 to 1 and decreasing from 1 to -1. |
| Frequency  | float    | 1             | How fast the signal oscillates, or how fast the pulses are sent (in Hz).                                                                                                                                                                                                                                                                                                                             |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="oscillator" category="Electrical" Tags="smallitem,signal" maxstacksize="8" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <OscillatorComponent canbeselected="true" outputtype="Pulse" frequency="1" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="set_frequency" displayname="connection.setfrequency" />
    <input name="set_outputtype" displayname="connection.setoutputtype" />
    <output name="signal_out" displayname="connection.signalout" />
  </ConnectionPanel>
  [...]
</Item>
```

