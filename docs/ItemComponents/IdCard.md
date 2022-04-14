# IdCard


## Attributes

| Attribute|Type|Default value|Description |
| ---|---|---|--- |
| TeamID|CharacterTeamType|CharacterTeamType.None| |
| SubmarineSpecificID|int|0| |
| OwnerTags|string|""| |
| Description|string|""| |
| OwnerName|string|""| |
| OwnerJobId|Identifier|""| |
| OwnerHairIndex|int|-1| |
| OwnerBeardIndex|int|-1| |
| OwnerMoustacheIndex|int|-1| |
| OwnerFaceAttachmentIndex|int|-1| |
| OwnerHairColor|Color|"#ffffff"| |
| OwnerFacialHairColor|Color|"#ffffff"| |
| OwnerSkinColor|Color|"#ffffff"| |
| OwnerSheetIndex|Vector2|"0,0"| |

This component also supports the attributes defined in: [Pickable](Pickable.md), [ItemComponent](ItemComponent.md)


## Example
```xml
<Item identifier="idcard" category="Equipment" Tags="smallitem,identitycard" cargocontaineridentifier="metalcrate">
  <IdCard slots="Card,Any" msg="ItemMsgPickUpSelect" />
  [...]
</Item>
```

