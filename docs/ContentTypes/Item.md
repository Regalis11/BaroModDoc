---
has_toc: true
---
# Item
<sup>Relevant files: [[Shared:ItemFile.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/ItemFile.cs) [[Shared:ItemPrefab.cs]](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Items/ItemPrefab.cs)</sup>
- **Required by core package:** Yes

This content type is used to define various interactables, namely machinery and items that can be picked up.

## Attributes
Each XML element that defines an item must have at least the following attribute:
- `identifier`: This is used to be able to make references to the item, as well as determine what to remove when [overriding](../Intro/Overrides.md). If `nameidentifier` and `name` are not defined, it is also used to fetch the name and description of the item from a [Text content file](Text.md) using the tags `entityname.[identifier]` and `entitydescription.[identifier]`.

The following attributes can also be defined:
- `nameidentifier`: If defined, this is the tag used to fetch the item's name.
- `fallbacknameidentifier`: If defined, this is used as a fallback if the tag given by `nameidentifier` is not defined for the current language.
- `descriptionidentifier`: If defined, this is the tag used to fetch the item's description.
- `name`: If defined, and the previous optional identifiers are not defined, and the identifier is not found in a Text file, this string is used directly as the name to display in the player's inventory.
- `aliases`: A list of additional identifiers that can be used to reference this item. Each item in the list is separated by a comma. There is rarely the need to use this; in vanilla content, it's been used to provide backwards compatibility when old items have been removed and replaced with an item that uses a different identifier.
- `tags`: A list of strings that can be used to group items. They are used, for example, by [characters](Character.md) to select targets, by containers to determine which items can be put inside them and by bots to figure out what the items are and where they belong.
- `category`: The category or categories the item is in when interacting with stores and the [submarine editor](../Editors/SubmarineEditor.md). It can be one or more of the following:
  - Decorative
  - Machine
  - Medical
  - Weapon
  - Diving
  - Equipment
  - Fuel
  - Electrical
  - Material
  - Alien
  - Wrecked
  - Misc
- `allowasextracargo`: Whether or not this item can be selected as extra cargo in the server settings. Can be `true` or `false`.
- `InteractDistance` : `float`
- `InteractPriority` : `float`
- `InteractThroughWalls` : `bool`
- `HideConditionBar` : `bool`
- `HideConditionInTooltip` : `bool`
- `RequireBodyInsideTrigger` : `bool`
- `RequireCursorInsideTrigger` : `bool`
- `RequireCampaignInteract` : `bool`
- `FocusOnSelected` : `bool`
- `OffsetOnSelected` : `float`
- `Health` : `float`
- `AllowSellingWhenBroken` : `bool`
- `Indestructible` : `bool`
- `DamagedByExplosions` : `bool`
- `ExplosionDamageMultiplier` : `float`
- `DamagedByProjectiles` : `bool`
- `DamagedByMeleeWeapons` : `bool`
- `DamagedByRepairTools` : `bool`
- `DamagedByMonsters` : `bool`
- `FireProof` : `bool`
- `WaterProof` : `bool`
- `ImpactTolerance` : `float`
- `OnDamagedThreshold` : `float`
- `SonarSize` : `float`
- `UseInHealthInterface` : `bool`
- `DisableItemUsageWhenSelected` : `bool`
- `CargoContainerIdentifier` : `string`
- `UseContainedSpriteColor` : `bool`
- `UseContainedInventoryIconColor` : `bool`
- `AddedRepairSpeedMultiplier` : `float`
- `AddedPickingSpeedMultiplier` : `float`
- `CannotRepairFail` : `bool`
- `EquipConfirmationText` : `string`
- `AllowRotatingInEditor` : `bool`
- `ShowContentsInTooltip` : `bool`
- `CanFlipX` : `bool`
- `CanFlipY` : `bool`
- `IsDangerous` : `bool`
- `MaxStackSize` : `int`
- `AllowDroppingOnSwap` : `bool`
- `ResizeHorizontal` : `bool`
- `ResizeVertical` : `bool`
- `Description` : `LocalizedString`
- `AllowedUpgrades` : `string`
- `HideInMenus` : `bool`
- `Subcategory` : `string`
- `Linkable` : `bool`
- `SpriteColor` : `Color`
- `Scale` : `float`

## Child elements
Each XML element that defines an item can have the following child elements:
- `sprite`: Defines what and how the item is drawn in the game. `texture` specifies the path to the texture file. `sourcerect` defines the section of the texture in pixels (x, y, width, height). `depth` is used for determining which objects are drawn in front and which behind them. Depth ranges from 0 to 1, where 0 is at the front and 1 is at the back. `origin` defines the center point of the sprite relative to the item position.
  - Example:

```xml
<Item identifier="alienwrench" name="Alien Wrench" variantof="wrench" scale="0.2">
  <Sprite texture="%ModDir%/alienwrench.png" sourcerect="0,0,256,112" depth="0.55" origin="0.5,0.1" scale="0.1" />
  <!-- ... -->
```

- `price`: Pricing information for this item in the campaign stores.
- `preferredcontainer`: Which containers this item should be placed into? Preferably use tags, but identifiers are also supported. Used for placing items automatically, spawning loot, and for the bot decisions on where to place items. Note that each `preferredcontainer` element will be processed individually for spawning items: they are additive. However, multiple tags (or identifiers) in the same element are optional, meaning any container item matching these tags are accepted and the item can spawn in any of them. [TODO: Add a note about additive item loadouts with a reference to start items]
  - Example:

```xml
<Item name="" identifier="shotgunshell" category="Weapon" maxstacksize="12" interactthroughwalls="true" cargocontaineridentifier="metalcrate" tags="smallitem,shotgunammo" Scale="0.5" impactsoundtag="impact_metal_light">
  <PreferredContainer primary="shotgun" amount="6" spawnprobability="1"/>
  <PreferredContainer primary="autoshotgun" amount="8" spawnprobability="1"/>
  <PreferredContainer primary="shotgununique" amount="2" spawnprobability="1"/>
  <PreferredContainer primary="secarmcab" minamount="6" maxamount="12" spawnprobability="1" notcampaign="true"/>
  <PreferredContainer primary="outpostsecarmcab" minamount="1" maxamount="6" spawnprobability="0.5"/>
  <PreferredContainer secondary="wreckarmcab,wrecksecarmcab,abandonedarmcab,abandonedsecarmcab" minamount="1" maxamount="3" spawnprobability="0.2"/>
  <PreferredContainer primary="armcab" secondary="locker"/>
  <Price baseprice="40">
    <Price storeidentifier="merchantoutpost" sold="false" multiplier="1.5" />
    <Price storeidentifier="merchantcity" multiplier="1.25" minavailable="4" sold="true"/>
    <Price storeidentifier="merchantresearch" sold="false" multiplier="1.25" />
    <Price storeidentifier="merchantmilitary" multiplier="0.9" minavailable="12" />
    <Price storeidentifier="merchantmine" sold="false" multiplier="1.25" />
    <Price storeidentifier="merchantarmory" multiplier="0.9" minavailable="12" />
  </Price>
  <!-- ... -->
```

- `fabricate`: A fabrication recipe for this item, along with a list of identifiers or tags of suitable fabricators.
- `deconstruct`: The result of deconstructing this item.
  - Example:

```xml
<Item identifier="toolbelt" category="Equipment" tags="smallitem,mobilecontainer,tool" cargocontaineridentifier="metalcrate" showcontentsintooltip="true" Scale="0.5" fireproof="true" description="" impactsoundtag="impact_soft">
  <Deconstruct time="10">
    <Item identifier="organicfiber" />
  </Deconstruct>
  <Fabricate suitablefabricators="fabricator" requiredtime="20">
    <Item identifier="organicfiber" />
  </Fabricate>
  <!-- ... -->
```

- `swappableitem`: A list of items that this item can be swapped to when upgrading the submarine in a campaign.
  - Example:

```xml
<Item identifier="coilgun" Tags="turret" category="Machine,Weapon" interactthroughwalls="true" Scale="0.5" interactdistance="10" spritecolor="1.0,1.0,1.0,1.0" focusonselected="true" offsetonselected="750" linkable="true" allowedlinks="coilgunequipment">
  <SwappableItem price="5000" replacementonuninstall="turrethardpoint" origin="128,215" swapidentifier="basicturret">
    <SchematicSprite texture="Content/UI/WeaponUI.png" sourcerect="256,0,256,389" />
    <SwapConnectedItem tag="periscope" swapto="periscope" />
    <SwapConnectedItem tag="turretammosource" swapto="coilgunloader" />
  </SwappableItem>
  <!-- ... -->
```

- `trigger`: This determines a rectangular area where a character must be within to be able to interact with the item.
  - Example:

```xml
<Item identifier="ladder" tags="ladder" resizevertical="true" scale="0.5" allowrotatingineditor="false">
  <Ladder canbeselected="true" msg="ItemMsgClimbSelect">
    <BackgroundSprite texture="Content/Items/Ladder/ladder.png" depth="0.05" sourcerect="8,8,26,240" origin="1.0,0.0"/>
  </Ladder>
  <trigger x="-40" y="20" width="90"/>
  <!-- ... -->
```

- `suitabletreatment`: This tells the game which [afflictions](Afflictions.md) this item is a suitable treatment for. Used to determine which items to list as the suitable treatments in the health interface, and by bots to figure out which items are suitable for treating a character.
  - Example:

```xml
<Item identifier="tonicliquid" category="Medical" maxstacksize="8" cargocontaineridentifier="chemicalcrate" Tags="smallitem,chem,medical" description="" useinhealthinterface="true" scale="0.5" impactsoundtag="impact_metal_light" RequireAimToUse="True">
  <SuitableTreatment type="damage" suitability="1" />
  <SuitableTreatment type="burn" suitability="-10" />
  <SuitableTreatment identifier="opiateoverdose" suitability="-10" />
  <SuitableTreatment identifier="oxygenlow" suitability="-10" />
  <SuitableTreatment identifier="opiatewithdrawal" suitability="-10" />
  <!-- ... -->
```

### Item component types
On top of the previously mentioned child elements, items also consist of one or more "item components" that determine the functionality of the item. For example, an item could have a "[Holdable](../ItemComponents/Holdable.md)" component that makes it possible to hold it in your hands and an "[ItemContainer](../ItemComponents/ItemContainer.md)" component that lets you contain other items inside it.

If you are familiar with Unity, you can think of items as Unity's GameObjects, and item components as the Components attached to the GameObjects.

As of v0.17.12.0, the game supports the following item component types:
- [ItemComponent](../ItemComponents/ItemComponent.md)
- [AndComponent](../ItemComponents/AndComponent.md)
- [OrComponent](../ItemComponents/OrComponent.md)
- [XorComponent](../ItemComponents/XorComponent.md)
- [AdderComponent](../ItemComponents/AdderComponent.md)
- [DivideComponent](../ItemComponents/DivideComponent.md)
- [MultiplyComponent](../ItemComponents/MultiplyComponent.md)
- [SubtractComponent](../ItemComponents/SubtractComponent.md)
- [ButtonTerminal](../ItemComponents/ButtonTerminal.md)
- [ColorComponent](../ItemComponents/ColorComponent.md)
- [ConnectionPanel](../ItemComponents/ConnectionPanel.md)
- [Controller](../ItemComponents/Controller.md)
- [CustomInterface](../ItemComponents/CustomInterface.md)
- [DelayComponent](../ItemComponents/DelayComponent.md)
- [DockingPort](../ItemComponents/DockingPort.md)
- [EntitySpawnerComponent](../ItemComponents/EntitySpawnerComponent.md)
- [EqualsComponent](../ItemComponents/EqualsComponent.md)
- [GreaterComponent](../ItemComponents/GreaterComponent.md)
- [ExponentiationComponent](../ItemComponents/ExponentiationComponent.md)
- [FunctionComponent](../ItemComponents/FunctionComponent.md)
- [GeneticMaterial](../ItemComponents/GeneticMaterial.md)
- [Growable](../ItemComponents/Growable.md)
- [ItemContainer](../ItemComponents/ItemContainer.md)
- [ItemLabel](../ItemComponents/ItemLabel.md)
- [Ladder](../ItemComponents/Ladder.md)
- [LevelResource](../ItemComponents/LevelResource.md)
- [MemoryComponent](../ItemComponents/MemoryComponent.md)
- [ModuloComponent](../ItemComponents/ModuloComponent.md)
- [MotionSensor](../ItemComponents/MotionSensor.md)
- [NameTag](../ItemComponents/NameTag.md)
- [NotComponent](../ItemComponents/NotComponent.md)
- [OscillatorComponent](../ItemComponents/OscillatorComponent.md)
- [OutpostTerminal](../ItemComponents/OutpostTerminal.md)
- [OxygenDetector](../ItemComponents/OxygenDetector.md)
- [Pickable](../ItemComponents/Pickable.md)
- [Door](../ItemComponents/Door.md)
- [Holdable](../ItemComponents/Holdable.md)
- [MeleeWeapon](../ItemComponents/MeleeWeapon.md)
- [Throwable](../ItemComponents/Throwable.md)
- [IdCard](../ItemComponents/IdCard.md)
- [Planter](../ItemComponents/Planter.md)
- [Wearable](../ItemComponents/Wearable.md)
- [Powered](../ItemComponents/Powered.md)
- [Deconstructor](../ItemComponents/Deconstructor.md)
- [ElectricalDischarger](../ItemComponents/ElectricalDischarger.md)
- [Engine](../ItemComponents/Engine.md)
- [Fabricator](../ItemComponents/Fabricator.md)
- [LightComponent](../ItemComponents/LightComponent.md)
- [MiniMap](../ItemComponents/MiniMap.md)
- [OxygenGenerator](../ItemComponents/OxygenGenerator.md)
- [PowerContainer](../ItemComponents/PowerContainer.md)
- [PowerTransfer](../ItemComponents/PowerTransfer.md)
- [RelayComponent](../ItemComponents/RelayComponent.md)
- [Pump](../ItemComponents/Pump.md)
- [Reactor](../ItemComponents/Reactor.md)
- [Sonar](../ItemComponents/Sonar.md)
- [SonarTransducer](../ItemComponents/SonarTransducer.md)
- [Steering](../ItemComponents/Steering.md)
- [Turret](../ItemComponents/Turret.md)
- [Projectile](../ItemComponents/Projectile.md)
- [Propulsion](../ItemComponents/Propulsion.md)
- [Quality](../ItemComponents/Quality.md)
- [RangedWeapon](../ItemComponents/RangedWeapon.md)
- [Sprayer](../ItemComponents/Sprayer.md)
- [RegExFindComponent](../ItemComponents/RegExFindComponent.md)
- [RemoteController](../ItemComponents/RemoteController.md)
- [Repairable](../ItemComponents/Repairable.md)
- [RepairTool](../ItemComponents/RepairTool.md)
- [Rope](../ItemComponents/Rope.md)
- [Scanner](../ItemComponents/Scanner.md)
- [SignalCheckComponent](../ItemComponents/SignalCheckComponent.md)
- [SmokeDetector](../ItemComponents/SmokeDetector.md)
- [StatusHUD](../ItemComponents/StatusHUD.md)
- [ConcatComponent](../ItemComponents/ConcatComponent.md)
- [Terminal](../ItemComponents/Terminal.md)
- [TriggerComponent](../ItemComponents/TriggerComponent.md)
- [TrigonometricFunctionComponent](../ItemComponents/TrigonometricFunctionComponent.md)
- [Vent](../ItemComponents/Vent.md)
- [WaterDetector](../ItemComponents/WaterDetector.md)
- [WifiComponent](../ItemComponents/WifiComponent.md)
- [Wire](../ItemComponents/Wire.md)
