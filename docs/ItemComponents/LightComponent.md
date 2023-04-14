# LightComponent


## Attributes

| Attribute              | Type  | Default value     | Description                                                                                                                                                                                                                                                                        |
|------------------------|-------|-------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Range                  | float | 100               | The range of the emitted light. Higher values are more performance-intensive.                                                                                                                                                                                                      |
| CastShadows            | bool  | true              | Should structures cast shadows when light from this light source hits them. Disabling shadows increases the performance of the game, and is recommended for lights with a short range. Lights that are set to be drawn behind subs don't cast shadows, regardless of this setting. |
| DrawBehindSubs         | bool  | false             | Lights drawn behind submarines don't cast any shadows and are much faster to draw than shadow-casting lights. It's recommended to enable this on decorative lights outside the submarine's hull.                                                                                   |
| IsOn                   | bool  | false             | Is the light currently on.                                                                                                                                                                                                                                                         |
| Flicker                | float | 0                 | How heavily the light flickers. 0 = no flickering, 1 = the light will alternate between completely dark and full brightness.                                                                                                                                                       |
| FlickerSpeed           | float | 1                 | How fast the light flickers.                                                                                                                                                                                                                                                       |
| PulseFrequency         | float | 0                 | How rapidly the light pulsates (in Hz). 0 = no blinking.                                                                                                                                                                                                                           |
| PulseAmount            | float | 0                 | How much light pulsates (in Hz). 0 = not at all, 1 = alternates between full brightness and off.                                                                                                                                                                                   |
| BlinkFrequency         | float | 0                 | How rapidly the light blinks on and off (in Hz). 0 = no blinking.                                                                                                                                                                                                                  |
| LightColor             | Color | "255,255,255,255" | The color of the emitted light (R,G,B,A).                                                                                                                                                                                                                                          |
| IgnoreContinuousToggle | bool  | false             | If enabled, the component will ignore continuous signals received in the toggle input (i.e. a continuous signal will only toggle it once).                                                                                                                                         |
| AlphaBlend             | bool  | true              | Should the light sprite be drawn on the item using alpha blending, in addition to being rendered in the light map? Can be used to make the light sprite stand out more.                                                                                                            |

This component also supports the attributes defined in: [Powered](Powered.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="lightcomponent" category="Electrical" Tags="smallitem,lightcomponent" maxstacksize="8" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <LightComponent canbeselected="true" color="255,0.0,0.0,127" alphablend="true" castshadows="false">
    <LightTexture texture="Content/Lights/pointlight_bounce.png" origin="0.5,0.5" />
    <sprite texture="Content/Items/Electricity/signalcomp.png" sourcerect="228,3,23,24" origin="0.38,0.5" />
  </LightComponent>
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="toggle" displayname="connection.togglestate" />
    <input name="set_state" displayname="connection.setstate" />
    <input name="set_color" displayname="connection.setcolor" />
  </ConnectionPanel>
  [...]
</Item>
```

