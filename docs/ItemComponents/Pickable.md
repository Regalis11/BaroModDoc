# Pickable


## Attributes

This component supports the attributes defined in: [ItemComponent](ItemComponent.md)


## Example
```xml
<Item nameidentifier="defensebotspawner" identifier="placeabledefensebotspawner" descriptionidentifier="" category="Machine">
  <Pickable slots="Any">
    <StatusEffect type="Always" targettype="This">
      <RemoveItem />
      <SpawnCharacter speciesname="Defensebot" totalmaxcount="2" inheritteam="true" />
    </StatusEffect>
  </Pickable>
  [...]
</Item>
```

