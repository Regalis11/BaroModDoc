# WifiComponent


## Attributes

| Attribute                    | Type              | Default value | Description                                                                                                                                                                                                                                                                                          |
|------------------------------|-------------------|---------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| TeamID                       | CharacterTeamType | None          | WiFi components can only communicate with components that have the same Team ID.                                                                                                                                                                                                                     |
| Range                        | float             | 20000         | How close the recipient has to be to receive a signal from this WiFi component.                                                                                                                                                                                                                      |
| Channel                      | int               | 0             | WiFi components can only communicate with components that use the same channel.                                                                                                                                                                                                                      |
| AllowCrossTeamCommunication  | bool              | false         | Can the component communicate with wifi components in another team's submarine (e.g. enemy sub in Combat missions, respawn shuttle). Needs to be enabled on both the component transmitting the signal and the component receiving it.                                                               |
| LinkToChat                   | bool              | false         | If enabled, any signals received from another chat-linked wifi component are displayed as chat messages in the chatbox of the player holding the item.                                                                                                                                               |
| MinChatMessageInterval       | float             | 1             | How many seconds have to pass between signals for a message to be displayed in the chatbox. Setting this to a very low value is not recommended, because it may cause an excessive amount of chat messages to be created if there are chat-linked wifi components that transmit a continuous signal. |
| DiscardDuplicateChatMessages | bool              | false         | If set to true, the component will only create chat messages when the received signal changes.                                                                                                                                                                                                       |

This component also supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="wificomponent" category="Electrical" Tags="smallitem,signal" maxstacksize="8" cargocontaineridentifier="metalcrate" scale="0.5" impactsoundtag="impact_metal_light" isshootable="true">
  <WifiComponent canbeselected="true" MinChatMessageInterval="1.0" DiscardDuplicateChatMessages="true" />
  <ConnectionPanel selectkey="Action" canbeselected="true" msg="ItemMsgRewireScrewdriver" hudpriority="10">
    <GuiFrame relativesize="0.2,0.32" minsize="400,350" maxsize="480,420" anchor="Center" style="ConnectionPanel" />
    <RequiredItem items="screwdriver" type="Equipped" />
    <input name="signal_in" displayname="connection.signalin" />
    <output name="signal_out" displayname="connection.signalout" />
    <input name="set_channel" displayname="connection.setchannel" />
  </ConnectionPanel>
  [...]
</Item>
```

