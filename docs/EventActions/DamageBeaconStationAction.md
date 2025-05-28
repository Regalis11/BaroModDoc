# DamageBeaconStationAction

Can be used to disconnect wires and break devices and walls in beacon stations. Useful if you want the beacon to be in tact by default, and use events to determine whether it should be e.g. manned by bandits, or destroyed and infested by monsters.

## Attributes

| Attribute                 | Type  | Default value | Description                                                                                                         |
|---------------------------|-------|---------------|---------------------------------------------------------------------------------------------------------------------|
| DisconnectWireProbability | float | 0             | Probability of disconnecting wires (0.5 = 50% chance of disconnecting any given wire, 1 = all wires disconnected).  |
| DamageWallProbability     | float | 0             | Probability of a wall sections leaking (0.5 = 50% creating a leak on any given wall section, 1 = all walls leak).   |
| DamageDeviceProbability   | float | 0             | Probability of devices being damaged (0.5 = 50% chance of damaging any given devices, 1 = all devices are damaged). |



