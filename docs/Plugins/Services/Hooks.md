# Hooks

Hooks are callbacks that get called at specific points in the game. Register one with [ISimpleHookService](ISimpleHookService.md)'s `RegisterHook<T>()`, where `T` is one of the delegate types listed here.

| Name                                                                                                      | Description                                                                                                                                                                                                                                                          |
|-----------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [`PluginOnPreUpdateDelegate`](#pluginonpreupdatedelegate)                                                 | Called in the main game loop before the game logic is updated.                                                                                                                                                                                                       |
| [`PluginOnPostUpdateDelegate`](#pluginonpostupdatedelegate)                                               | Called in the main game loop after the game logic is updated.                                                                                                                                                                                                        |
| [`PluginRoundStartedDelegate`](#pluginroundstarteddelegate)                                               | Called after a round has finished loading and starts.                                                                                                                                                                                                                |
| [`PluginRoundEndedDelegate`](#pluginroundendeddelegate)                                                   | Called when a round ends.                                                                                                                                                                                                                                            |
| [`PluginCampaignLoadDelegate`](#plugincampaignloaddelegate)                                               | Called when a new campaign is started or loaded.                                                                                                                                                                                                                     |
| [`PluginCampaignSaveDelegate`](#plugincampaignsavedelegate)                                               | Called when the campaign is saved.                                                                                                                                                                                                                                   |
| [`PluginGameModeUpdateDelegate`](#plugingamemodeupdatedelegate)                                           | Called when the game mode is updated. Only called when the game mode is running and not paused.                                                                                                                                                                      |
| [`PluginShouldDisableJovianRadiation`](#pluginshoulddisablejovianradiation)                               |                                                                                                                                                                                                                                                                      |
| [`PluginGameModeAddToGUIUpdateListDelegate`](#plugingamemodeaddtoguiupdatelistdelegate)                   | Called when adding game mode specific GUI elements to the update list. Can be used to add custom GUI elements to the list.                                                                                                                                           |
| [`PluginCampaignMapGenerationFinishedDelegate`](#plugincampaignmapgenerationfinisheddelegate)             |                                                                                                                                                                                                                                                                      |
| [`PluginLocationSaveDelegate`](#pluginlocationsavedelegate)                                               |                                                                                                                                                                                                                                                                      |
| [`PluginLocationLoadDelegate`](#pluginlocationloaddelegate)                                               |                                                                                                                                                                                                                                                                      |
| [`PluginCampaignOnLeaveOutpostDelegate`](#plugincampaignonleaveoutpostdelegate)                           | Called when the player leaves an outpost in the campaign. Not invoked in outpost levels without an actual outpost "submarine".                                                                                                                                       |
| [`PluginCampaignOnEnterOutpostDelegate`](#plugincampaignonenteroutpostdelegate)                           | Called when the player enters an outpost in the campaign. Not invoked in outpost levels without an actual outpost "submarine".                                                                                                                                       |
| [`PluginCampaignOnOutpostNPCsChangedDelegate`](#plugincampaignonoutpostnpcschangeddelegate)               | Called when the outpost NPCs change during a round (a new NPC appears or another one disappears)                                                                                                                                                                     |
| [`PluginMultiplayerRoundStartedDelegate`](#pluginmultiplayerroundstarteddelegate)                         | Called when a multiplayer round has started and fully initialized.                                                                                                                                                                                                   |
| [`PluginOnProgressWorldDelegate`](#pluginonprogressworlddelegate)                                         | Called when the campaign world is progressed. Affects things such as daily deals, store refreshes and radiation.                                                                                                                                                     |
| [`PluginOnLocationTypeChangedDelegate`](#pluginonlocationtypechangeddelegate)                             | Called when a location changes its type.                                                                                                                                                                                                                             |
| [`PluginCampaignMoneySpentAtLocationDelegate`](#plugincampaignmoneyspentatlocationdelegate)               | Called when the player spends money at the current location for any purpose (merchants, submarines and upgrades, clinic, HR) except events.                                                                                                                          |
| [`PluginCampaignItemBuyPriceMultiplier`](#plugincampaignitembuypricemultiplier)                           | Called when calculating the item buy price in a location, can be used to add a custom multiplier to the price.                                                                                                                                                       |
| [`PluginCampaignItemSellPriceMultiplier`](#plugincampaignitemsellpricemultiplier)                         | Called when calculating the item sell price in a location, can be used to add a custom multiplier to the price.                                                                                                                                                      |
| [`PluginCampaignItemModifyAvailability`](#plugincampaignitemmodifyavailability)                           | Called when determining the item availability in a location, can be used to modify the item availability.                                                                                                                                                            |
| [`PluginOnItemReplacedDelegate`](#pluginonitemreplaceddelegate)                                           | Called when an item is replaced with a new item.                                                                                                                                                                                                                     |
| [`PluginCanSwapItemDelegate`](#plugincanswapitemdelegate)                                                 | Called when checking if a submarine item can be swapped to. Return false to disallow the swap. Not called for swaps that have already been purchased, or for swapping back to the item's current prefab.                                                             |
| [`PluginOnCharacterCreated`](#pluginoncharactercreated)                                                   | Called after the constructor of the character has ran.                                                                                                                                                                                                               |
| [`PluginModifyRequiredNPCConversationFlags`](#pluginmodifyrequirednpcconversationflags)                   | Can be used to modify the mandatory conversation flags for the given situation. Used to e.g. enforce the EnterOutpost NPC conversations when entering an outpost.                                                                                                    |
| [`PluginModifyCharacterSpecificNPCConversationFlags`](#pluginmodifycharacterspecificnpcconversationflags) | Can be used to modify the conversation flags assigned to a specific NPC, to add support for custom conversations in specific situations.                                                                                                                             |
| [`PluginSelectHighestSubmarineTierAvailable`](#pluginselecthighestsubmarinetieravailable)                 | Overrides the maximum submarine tier available for a location type. Return a value between 1 and 3 to set the highest available tier, or return -1 to use the default tier selection logic.                                                                          |
| [`PluginCrewChangedDelegate`](#plugincrewchangeddelegate)                                                 | Called when a character is added to or removed from the crew on the client side. This is called after the crew change network event is processed.                                                                                                                    |
| [`PluginHandleEscapeKeyDelegate`](#pluginhandleescapekeydelegate)                                         | Called when the ESC key is pressed, before the default ESC handling logic. Return true to prevent the default ESC handling from running.                                                                                                                             |
| [`PluginGameModeDrawDelegate`](#plugingamemodedrawdelegate)                                               | Called when the game mode overlays are drawn in `Draw`.                                                                                                                                                                                                              |
| [`PluginCampaignMapUpdateDelegate`](#plugincampaignmapupdatedelegate)                                     | Called when the campaign map is updated in `Update`.                                                                                                                                                                                                                 |
| [`PluginCampaignMapBeforeDrawDelegate`](#plugincampaignmapbeforedrawdelegate)                             | Called at the beginning of the campaign draw method `Draw`, right after rendering the map background. Can be used to draw custom content on the map before the locations and other content on the map are drawn.                                                     |
| [`PluginCampaignMapAfterDrawDelegate`](#plugincampaignmapafterdrawdelegate)                               | Called at the end of the campaign draw method `Draw`, after everything on the map has been drawn.                                                                                                                                                                    |
| [`PluginCampaignMapDrawLocationDelegate`](#plugincampaignmapdrawlocationdelegate)                         |                                                                                                                                                                                                                                                                      |
| [`PluginCampaignSelectTabDelegate`](#plugincampaignselecttabdelegate)                                     |                                                                                                                                                                                                                                                                      |
| [`PluginCampaignDrawUIDelegate`](#plugincampaigndrawuidelegate)                                           |                                                                                                                                                                                                                                                                      |
| [`PluginCreateLocationInfoOverlayDelegate`](#plugincreatelocationinfooverlaydelegate)                     | Called when a map tooltip is created for a location on the campaign map in `CreateLocationInfoOverlay`.                                                                                                                                                              |
| [`PluginDrawSlotBackgroundDelegate`](#plugindrawslotbackgrounddelegate)                                   | Called when the background for an item in inventory slot is drawn in `DrawSlot`. Useful for drawing custom backgrounds for items in inventory slots.                                                                                                                 |
| [`PluginDrawSlotForegroundDelegate`](#plugindrawslotforegrounddelegate)                                   | Called when the foreground for an item in inventory slot is drawn in `DrawSlot`. Useful for drawing custom content on top of the item.                                                                                                                               |
| [`PluginItemSlotGetTooltipDelegate`](#pluginitemslotgettooltipdelegate)                                   | Called when the tooltip for an item in inventory slot is created in `GetTooltip`.                                                                                                                                                                                    |
| [`PluginMidRoundSyncReceiveStartedDelegate`](#pluginmidroundsyncreceivestarteddelegate)                   | Called when the client starts receiving midround synchronization data from the server.                                                                                                                                                                               |
| [`PluginMidRoundSyncReceiveCompletedDelegate`](#pluginmidroundsyncreceivecompleteddelegate)               | Called when the client has completed receiving midround synchronization data from the server.                                                                                                                                                                        |
| [`PluginClientJoinedRoundDelegate`](#pluginclientjoinedrounddelegate)                                     | Called after a client has joined the round (enters the game screen, either mid-round or at the start of the round), and has been gotten in sync with the server.                                                                                                     |
| [`PluginClientDisconnectedDelegate`](#pluginclientdisconnecteddelegate)                                   | Called when a client has disconnected from the server.                                                                                                                                                                                                               |
| [`OutpostFactionCandidate`](#outpostfactioncandidate)                                                     | A candidate for outpost faction assignment.                                                                                                                                                                                                                          |
| [`PluginNewOutpostFactionCandidateDelegate`](#pluginnewoutpostfactioncandidatedelegate)                   | Called when a location becomes an outpost and faction assignment is needed. Plugins can add candidates to the mix to participate in weighted selection. Note that if a plugin candidate wins, it will always set both primary and secondary factions, even if empty. |

## PluginOnPreUpdateDelegate

```cs
public delegate void PluginOnPreUpdateDelegate(float deltaTime)
```

Called in the main game loop before the game logic is updated.

## PluginOnPostUpdateDelegate

```cs
public delegate void PluginOnPostUpdateDelegate(float deltaTime)
```

Called in the main game loop after the game logic is updated.

## PluginRoundStartedDelegate

```cs
public delegate void PluginRoundStartedDelegate(GameSession gameSession)
```

Called after a round has finished loading and starts.

## PluginRoundEndedDelegate

```cs
public delegate void PluginRoundEndedDelegate(GameSession gameSession)
```

Called when a round ends.

## PluginCampaignLoadDelegate

```cs
public delegate void PluginCampaignLoadDelegate(
    CampaignMode mode,
    Option<XElement> loadElement,
    CampaignSettings settings)
```

Called when a new campaign is started or loaded.

| Parameter     | Type               | Description                                                                               |
|---------------|--------------------|-------------------------------------------------------------------------------------------|
| `mode`        | `CampaignMode`     | The campaign mode instance.                                                               |
| `loadElement` | `Option<XElement>` | The XML element that was loaded from the save file or None if a new campaign was started. |
| `settings`    | `CampaignSettings` | The campaign settings of the campaign.                                                    |

## PluginCampaignSaveDelegate

```cs
public delegate void PluginCampaignSaveDelegate(CampaignMode mode, XElement save)
```

Called when the campaign is saved.

| Parameter | Type           | Description                                                                                                            |
|-----------|----------------|------------------------------------------------------------------------------------------------------------------------|
| `mode`    | `CampaignMode` | The campaign mode instance.                                                                                            |
| `save`    | `XElement`     | The XML element that gets written to the save file. If you want to save custom data add it as a child to this element. |

## PluginGameModeUpdateDelegate

```cs
public delegate void PluginGameModeUpdateDelegate(GameMode mode, float deltaTime)
```

Called when the game mode is updated. Only called when the game mode is running and not paused.

| Parameter   | Type       | Description                 |
|-------------|------------|-----------------------------|
| `mode`      | `GameMode` | The campaign mode instance. |
| `deltaTime` | `float`    |                             |

## PluginShouldDisableJovianRadiation

```cs
public delegate bool PluginShouldDisableJovianRadiation(out RichString disabledTooltip)
```

## PluginGameModeAddToGUIUpdateListDelegate

```cs
public delegate void PluginGameModeAddToGUIUpdateListDelegate(GameMode mode)
```

Called when adding game mode specific GUI elements to the update list. Can be used to add custom GUI elements to the list.

| Parameter | Type       | Description                 |
|-----------|------------|-----------------------------|
| `mode`    | `GameMode` | The campaign mode instance. |

## PluginCampaignMapGenerationFinishedDelegate

```cs
public delegate void PluginCampaignMapGenerationFinishedDelegate(
    CampaignMode campaign,
    Map map,
    List<Location> locations)
```

## PluginLocationSaveDelegate

```cs
public delegate void PluginLocationSaveDelegate(Location location, Map map, XElement locationElement)
```

## PluginLocationLoadDelegate

```cs
public delegate void PluginLocationLoadDelegate(
    Location location,
    CampaignMode campaign,
    XElement locationElement)
```

## PluginCampaignOnLeaveOutpostDelegate

```cs
public delegate void PluginCampaignOnLeaveOutpostDelegate(CampaignMode mode)
```

Called when the player leaves an outpost in the campaign. Not invoked in outpost levels without an actual outpost "submarine".

| Parameter | Type           | Description                 |
|-----------|----------------|-----------------------------|
| `mode`    | `CampaignMode` | The campaign mode instance. |

## PluginCampaignOnEnterOutpostDelegate

```cs
public delegate void PluginCampaignOnEnterOutpostDelegate(
    CampaignMode mode,
    Location location,
    Submarine outpost)
```

Called when the player enters an outpost in the campaign. Not invoked in outpost levels without an actual outpost "submarine".

| Parameter  | Type           | Description                               |
|------------|----------------|-------------------------------------------|
| `mode`     | `CampaignMode` | The campaign mode instance.               |
| `location` | `Location`     | The location that the player is entering. |
| `outpost`  | `Submarine`    | The outpost that the player is entering.  |

## PluginCampaignOnOutpostNPCsChangedDelegate

```cs
public delegate void PluginCampaignOnOutpostNPCsChangedDelegate(
    CampaignMode mode,
    Location location,
    Submarine outpost)
```

Called when the outpost NPCs change during a round (a new NPC appears or another one disappears)

| Parameter  | Type           | Description                               |
|------------|----------------|-------------------------------------------|
| `mode`     | `CampaignMode` | The campaign mode instance.               |
| `location` | `Location`     | The location that the player is entering. |
| `outpost`  | `Submarine`    | The outpost that the player is entering.  |

## PluginMultiplayerRoundStartedDelegate

```cs
public delegate void PluginMultiplayerRoundStartedDelegate(GameSession gameSession)
```

Called when a multiplayer round has started and fully initialized.

## PluginOnProgressWorldDelegate

```cs
public delegate void PluginOnProgressWorldDelegate(Map map, CampaignMode campaign)
```

Called when the campaign world is progressed. Affects things such as daily deals, store refreshes and radiation.

| Parameter  | Type           | Description                 |
|------------|----------------|-----------------------------|
| `map`      | `Map`          | The campaign map instance.  |
| `campaign` | `CampaignMode` | The campaign mode instance. |

## PluginOnLocationTypeChangedDelegate

```cs
public delegate void PluginOnLocationTypeChangedDelegate(Location location, LocationType oldType)
```

Called when a location changes its type.

| Parameter  | Type           | Description                      |
|------------|----------------|----------------------------------|
| `location` | `Location`     | The location whose type changed. |
| `oldType`  | `LocationType` | The old type of the location.    |

## PluginCampaignMoneySpentAtLocationDelegate

```cs
public delegate void PluginCampaignMoneySpentAtLocationDelegate(int price, Location? location)
```

Called when the player spends money at the current location for any purpose (merchants, submarines and upgrades, clinic, HR) except events.

## PluginCampaignItemBuyPriceMultiplier

```cs
public delegate float PluginCampaignItemBuyPriceMultiplier(
    Location.StoreInfo storeInfo,
    ItemPrefab itemPrefab,
    PriceInfo? priceInfo = null)
```

Called when calculating the item buy price in a location, can be used to add a custom multiplier to the price.

## PluginCampaignItemSellPriceMultiplier

```cs
public delegate float PluginCampaignItemSellPriceMultiplier(
    Location.StoreInfo storeInfo,
    ItemPrefab itemPrefab,
    PriceInfo? priceInfo = null)
```

Called when calculating the item sell price in a location, can be used to add a custom multiplier to the price.

## PluginCampaignItemModifyAvailability

```cs
public delegate int PluginCampaignItemModifyAvailability(
    Location location,
    ItemPrefab itemPrefab,
    PriceInfo priceInfo,
    int currentQuantity)
```

Called when determining the item availability in a location, can be used to modify the item availability.

## PluginOnItemReplacedDelegate

```cs
public delegate void PluginOnItemReplacedDelegate(Item oldItem, Item newItem)
```

Called when an item is replaced with a new item.

**Remarks**

Avoid performing any operations that would modify the old item, as it is removed and might cause errors.

| Parameter | Type   | Description                             |
|-----------|--------|-----------------------------------------|
| `oldItem` | `Item` | The item that was replaced and removed. |
| `newItem` | `Item` | The new item that was created.          |

## PluginCanSwapItemDelegate

```cs
public delegate bool PluginCanSwapItemDelegate(Item item, ItemPrefab replacement, Location? location)
```

Called when checking if a submarine item can be swapped to. Return false to disallow the swap. Not called for swaps that have already been purchased, or for swapping back to the item's current prefab.

| Parameter     | Type         | Description                                    |
|---------------|--------------|------------------------------------------------|
| `item`        | `Item`       | The item that would be swapped.                |
| `replacement` | `ItemPrefab` | The item the swap would replace the item with. |
| `location`    | `Location?`  | The location where the swap is done.           |

## PluginOnCharacterCreated

```cs
public delegate void PluginOnCharacterCreated(Character character)
```

Called after the constructor of the character has ran.

| Parameter   | Type        | Description           |
|-------------|-------------|-----------------------|
| `character` | `Character` | The created character |

## PluginModifyRequiredNPCConversationFlags

```cs
public delegate void PluginModifyRequiredNPCConversationFlags(List<Identifier> flags)
```

Can be used to modify the mandatory conversation flags for the given situation. Used to e.g. enforce the EnterOutpost NPC conversations when entering an outpost.

## PluginModifyCharacterSpecificNPCConversationFlags

```cs
public delegate void PluginModifyCharacterSpecificNPCConversationFlags(
    Character character,
    List<Identifier> flags)
```

Can be used to modify the conversation flags assigned to a specific NPC, to add support for custom conversations in specific situations.

## PluginSelectHighestSubmarineTierAvailable

```cs
public delegate int PluginSelectHighestSubmarineTierAvailable(
    SubmarineClass subClass,
    Identifier locationType)
```

Overrides the maximum submarine tier available for a location type. Return a value between 1 and 3 to set the highest available tier, or return -1 to use the default tier selection logic.

## PluginCrewChangedDelegate

```cs
public delegate void PluginCrewChangedDelegate(Character character, bool isAdded)
```

*Only available on the client.*

Called when a character is added to or removed from the crew on the client side. This is called after the crew change network event is processed.

| Parameter   | Type        | Description                                                |
|-------------|-------------|------------------------------------------------------------|
| `character` | `Character` | The character that was added or removed.                   |
| `isAdded`   | `bool`      | True if the character was added to crew, false if removed. |

## PluginHandleEscapeKeyDelegate

```cs
public delegate bool PluginHandleEscapeKeyDelegate()
```

*Only available on the client.*

Called when the ESC key is pressed, before the default ESC handling logic. Return true to prevent the default ESC handling from running.

## PluginGameModeDrawDelegate

```cs
public delegate void PluginGameModeDrawDelegate(GameMode mode, SpriteBatch spriteBatch)
```

*Only available on the client.*

Called when the game mode overlays are drawn in `Draw`.

| Parameter     | Type          | Description                                               |
|---------------|---------------|-----------------------------------------------------------|
| `mode`        | `GameMode`    | The game mode instance.                                   |
| `spriteBatch` | `SpriteBatch` | The sprite batch that can be used to draw custom content. |

## PluginCampaignMapUpdateDelegate

```cs
public delegate void PluginCampaignMapUpdateDelegate(CampaignMode mode, Map map, float deltaTime)
```

*Only available on the client.*

Called when the campaign map is updated in `Update`.

**Remarks**

This is different from `PluginCampaignUpdateDelegate` as it is only called when the map is visible and in single player the game is paused when the map is open.

| Parameter   | Type           | Description                 |
|-------------|----------------|-----------------------------|
| `mode`      | `CampaignMode` | The campaign mode instance. |
| `map`       | `Map`          | The campaign map instance.  |
| `deltaTime` | `float`        |                             |

## PluginCampaignMapBeforeDrawDelegate

```cs
public delegate void PluginCampaignMapBeforeDrawDelegate(
    CampaignMode campaign,
    Map map,
    SpriteBatch spriteBatch,
    GUICustomComponent mapContainer,
    float zoom,
    MapGenerationParams generationParams)
```

*Only available on the client.*

Called at the beginning of the campaign draw method `Draw`, right after rendering the map background. Can be used to draw custom content on the map before the locations and other content on the map are drawn.

## PluginCampaignMapAfterDrawDelegate

```cs
public delegate void PluginCampaignMapAfterDrawDelegate(
    CampaignMode campaign,
    Map map,
    SpriteBatch spriteBatch,
    GUICustomComponent mapContainer,
    float zoom,
    MapGenerationParams generationParams)
```

*Only available on the client.*

Called at the end of the campaign draw method `Draw`, after everything on the map has been drawn.

## PluginCampaignMapDrawLocationDelegate

```cs
public delegate void PluginCampaignMapDrawLocationDelegate(
    Location location,
    CampaignMode campaign,
    Map map,
    GUICustomComponent mapContainer,
    SpriteBatch spriteBatch,
    Vector2 mapIconPosition,
    float zoom,
    MapGenerationParams generationParams)
```

*Only available on the client.*

## PluginCampaignSelectTabDelegate

```cs
public delegate void PluginCampaignSelectTabDelegate(
    CampaignUI campaignUI,
    CampaignMode.InteractionType selectedTab)
```

*Only available on the client.*

## PluginCampaignDrawUIDelegate

```cs
public delegate void PluginCampaignDrawUIDelegate(CampaignMode campaign, CampaignUI campaignUI)
```

*Only available on the client.*

## PluginCreateLocationInfoOverlayDelegate

```cs
public delegate void PluginCreateLocationInfoOverlayDelegate(Location location, GUIComponent parent)
```

*Only available on the client.*

Called when a map tooltip is created for a location on the campaign map in `CreateLocationInfoOverlay`.

| Parameter  | Type           | Description                                                                                                                              |
|------------|----------------|------------------------------------------------------------------------------------------------------------------------------------------|
| `location` | `Location`     | The location that the tooltip is for.                                                                                                    |
| `parent`   | `GUIComponent` | The GUI component that the tooltip is created in. If you want to add custom content to the tooltip, add it as a child to this component. |

## PluginDrawSlotBackgroundDelegate

```cs
public delegate void PluginDrawSlotBackgroundDelegate(
    SpriteBatch spriteBatch,
    Inventory inventory,
    VisualSlot slot,
    Item item,
    int slotIndex,
    bool drawItem,
    InvSlotType type)
```

*Only available on the client.*

Called when the background for an item in inventory slot is drawn in `DrawSlot`. Useful for drawing custom backgrounds for items in inventory slots.

| Parameter     | Type          | Description                                               |
|---------------|---------------|-----------------------------------------------------------|
| `spriteBatch` | `SpriteBatch` | The sprite batch that can be used to draw custom content. |
| `inventory`   | `Inventory`   | The inventory that the item is in.                        |
| `slot`        | `VisualSlot`  | The slot that the item is in.                             |
| `item`        | `Item`        | The item that is in the slot.                             |
| `slotIndex`   | `int`         | The index of the slot.                                    |
| `drawItem`    | `bool`        | Whether the item should be drawn.                         |
| `type`        | `InvSlotType` | The type of the inventory slot.                           |

## PluginDrawSlotForegroundDelegate

```cs
public delegate void PluginDrawSlotForegroundDelegate(
    SpriteBatch spriteBatch,
    Inventory inventory,
    VisualSlot slot,
    Item item,
    int slotIndex,
    bool drawItem,
    InvSlotType type)
```

*Only available on the client.*

Called when the foreground for an item in inventory slot is drawn in `DrawSlot`. Useful for drawing custom content on top of the item.

| Parameter     | Type          | Description                                               |
|---------------|---------------|-----------------------------------------------------------|
| `spriteBatch` | `SpriteBatch` | The sprite batch that can be used to draw custom content. |
| `inventory`   | `Inventory`   | The inventory that the item is in.                        |
| `slot`        | `VisualSlot`  | The slot that the item is in.                             |
| `item`        | `Item`        | The item that is in the slot.                             |
| `slotIndex`   | `int`         | The index of the slot.                                    |
| `drawItem`    | `bool`        | Whether the item should be drawn.                         |
| `type`        | `InvSlotType` | The type of the inventory slot.                           |

## PluginItemSlotGetTooltipDelegate

```cs
public delegate void PluginItemSlotGetTooltipDelegate(
    ref LocalizedString existingTooltip,
    Item item,
    IEnumerable<Item> itemsInSlot,
    Character character)
```

*Only available on the client.*

Called when the tooltip for an item in inventory slot is created in `GetTooltip`.

| Parameter         | Type                | Description                                            |
|-------------------|---------------------|--------------------------------------------------------|
| `existingTooltip` | `LocalizedString`   | The existing tooltip that was created by vanilla game. |
| `item`            | `Item`              | The item that the tooltip is for.                      |
| `itemsInSlot`     | `IEnumerable<Item>` | The items that are in the slot.                        |
| `character`       | `Character`         | The currently controlled character.                    |

## PluginMidRoundSyncReceiveStartedDelegate

```cs
public delegate void PluginMidRoundSyncReceiveStartedDelegate()
```

*Only available on the client.*

Called when the client starts receiving midround synchronization data from the server.

## PluginMidRoundSyncReceiveCompletedDelegate

```cs
public delegate void PluginMidRoundSyncReceiveCompletedDelegate()
```

*Only available on the client.*

Called when the client has completed receiving midround synchronization data from the server.

## PluginClientJoinedRoundDelegate

```cs
public delegate void PluginClientJoinedRoundDelegate(Client client)
```

*Only available on the server.*

Called after a client has joined the round (enters the game screen, either mid-round or at the start of the round), and has been gotten in sync with the server.

## PluginClientDisconnectedDelegate

```cs
public delegate void PluginClientDisconnectedDelegate(Client client)
```

*Only available on the server.*

Called when a client has disconnected from the server.

## OutpostFactionCandidate

```cs
public readonly record struct OutpostFactionCandidate(
    Faction? PrimaryFaction,
    Faction? SecondaryFaction,
    float Weight = 1f,
    bool OverrideCompletely = false)
```

A candidate for outpost faction assignment.

## PluginNewOutpostFactionCandidateDelegate

```cs
public delegate void PluginNewOutpostFactionCandidateDelegate(
    Location location,
    CampaignMode campaign,
    ICollection<OutpostFactionCandidate> candidates)
```

Called when a location becomes an outpost and faction assignment is needed. Plugins can add candidates to the mix to participate in weighted selection. Note that if a plugin candidate wins, it will always set both primary and secondary factions, even if empty.

| Parameter    | Type                                   | Description                                                                   |
|--------------|----------------------------------------|-------------------------------------------------------------------------------|
| `location`   | `Location`                             | The location becoming an outpost.                                             |
| `campaign`   | `CampaignMode`                         | The campaign mode instance.                                                   |
| `candidates` | `ICollection<OutpostFactionCandidate>` | Collection to add candidates to. Use null factions to explicitly assign none. |
