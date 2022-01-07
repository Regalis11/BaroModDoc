# NPCConversations

<sup>Relevant files: [Shared:NPCConversationsFile.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/ContentManagement/ContentFile/NPCConversationsFile.cs) [Shared:NPCConversation.cs](https://github.com/Regalis11/Barotrauma/blob/master/Barotrauma/BarotraumaShared/SharedSource/Characters/AI/NPCConversation.cs)</sup>
- **Required by core package:** No

## Attributes


**WARNING:** This file likely generated completely incorrectly!

## Examples

### Example 1 - single NPCConversation

```xml
<NPCConversation
  identifier="myNPCConversation" />
```

### Example 2 - multiple NPCConversations

```xml
<NPCConversations>
  <NPCConversation
    identifier="myNPCConversation1" />
  <NPCConversation
    identifier="myNPCConversation2" />
</NPCConversations>
```

### Example 3 - overriding existing NPCConversations

```xml
<override>
  <NPCConversation
    identifier="myNPCConversation1" />
  <NPCConversation
    identifier="myNPCConversation2" />
</override>
```

