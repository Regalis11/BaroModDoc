Scripted events are a flexible system that make various things happen in outposts or during missions. In vanilla Barotrauma, scripted events are used for the outpost events, traitor events, the faction event chains, tutorials, and to "spice up" some missions such as the jailbreak missions.

There is a visual node-based editor for the scripted events, which can be accessed using the console command "eventeditor". The events can also be edited purely in XML.

## Examples

Let's dive right in to an example event and see how it works:

```xml
<ScriptedEvent identifier="firefighting" commonness="100">
  <TagAction criteria="player" tag="player" />
  <TagAction criteria="itemidentifier:opdeco_trashcan" tag="potentialtrashcan" submarinetype="outpost" />
  <TriggerAction target1tag="potentialtrashcan" target2tag="player" applytotarget1="selectedtrashcan" applytotarget2="triggerer_player" radius="100" />
  <ConversationAction targettag="triggerer_player" text="You notice a thin trail of smoke rising from a trash can." eventsprite="trashcan">
    <Option text="Investigate.">
      <ConversationAction targettag="triggerer_player" text="There is a lit cigarette in the trash. It seems dangerously close to igniting a larger flame.">
        <Option text="Put it out.">
          <ConversationAction targettag="triggerer_player" text="You get some minor burns when you fish the cigarette out and stomp out the fledgling fire, but nothing major." />
          <AfflictionAction targettag="triggerer_player" affliction="burn" strength="5" limbtype="rightarm" />
        </Option>
        <Option text="See what happens." endconversation="true">
          <WaitAction time="30" />
          <FireAction size="10" targettag="selectedtrashcan" />
        </Option>
      </ConversationAction>
    </Option>
    <Option text="Not my problem." endconversation="true">
      <WaitAction time="30" />
      <FireAction size="10" targettag="selectedtrashcan" />
    </Option>
  </ConversationAction>
</ScriptedEvent>
```

The first action in this event is [TagAction](../EventActions/TagAction.html). *Tagging* is a crucial part of the event system: it's used to tell the event which entities it should be targeting: in this case, a player character and a trash can in an outpost. Tags be applied to either multiple or single entities: in this case, the event tags all player characters and all trash cans. It's also worth noting here that the tags are local to the event; other events cannot refer to the trash can we tagged with "potentialtrashcan" unless they tag it themselves.

After the tagging, we find a [TriggerAction](../EventActions/TriggerAction.html). This action checks whether any of the trash cans we tagged as "potentialtrashcan" are within 100 pixels of a target tagged as "player". Something to note here is how we apply new tags to the targets: "selectedtrashcan" and "triggerer_player". This is so that we can refer to the specific player who went near the trash can, and that specific trash can. The tags "player" and "potentialtrashcan" still refer to all players and trash cans, and if we used those later in the event, it'd lead to the conversation prompts to be shown to all players and fires starting at every trash can.

Then we get to a [ConversationAction](../EventActions/ConversationAction.html). Until now, the event has executed actions in order from top to bottom, but here we introduce branching to the event in the form of making the player choose what to do with the smoking trash can they run into. There are two Option elements inside the ConversationAction, "Investigate." and "Not my problem.". 

Let's look at the latter option ("Not my problem.") first: under that, we have a [WaitAction](../EventActions/WaitAction.html), which simply makes the event wait for 30 seconds before continuing to the next event. And finally, after that 30 seconds has passed a [FireAction](../EventActions/FireAction.html) starts a fire at the position of the trash can.

The first option ("Investigate.") is a little more complex: here we have another ConversationAction which introduces another branch into the event. The player can choose to "Put it out", which gives them a minor burn affliction using [AfflictionAction](../EventActions/AfflictionAction.html). The second branch is identical to the "Not my problem." one: the event waits for 30 seconds, and then starts a fire.

Here's another example that shows a different way of controlling the flow of the event:

```xml
<ScriptedEvent identifier="missionevent_optionalkillmonster">
  <Label name="start" />
  <ConversationAction text="Hello. I have a mission for you, are you interested?" speakertag="outpostmanager" dialogtype="Small">
  <Option text="Yes.">
     <MissionAction missiontag="custommission" />
  </Option>  
  <Option text="No.">
     <GoTo name="start" />
  </Option>
  </ConversationAction>
</ScriptedEvent>
```

Here we have *Label* at the start of the event. This is used to mark a certain point in the event, and is used later in the event by the [GoTo](../EventActions/GoTo.html) action to make the event return back to the start - meaning that if a player declines the mission, they or another player could later return to the outpost manager and be asked again whether they're interested in the mission. GoTo actions can be a powerful tool in implementing thing like looping and making multiple branches eventually lead to the same point of the event, but we recommend using them sparingly: if you make heavy use of GoTo, the flow of the event can easily get very hard to follow.

## Triggering events

After you've made a scripted event, you need to make it trigger at some point in the game. There are several ways to do this:

### Event sets

The game can choose the events that occur during a round from pre-configured sets of events. This is how the vast majority of the scripted events and events such as monster spawns are configured in the vanilla game.

Here's an example of a simple event set. This set would randomly choose 2 of the four events, with the earlier events in the set being more likely to get chosen than the later ones. The events can only occur in outpost levels, in research and military outposts, past 15% difficulty.

```xml
<EventSet identifier="outpostevents.custom" leveltype="outpost" locationtype="research,military" minleveldifficulty="15" maxleveldifficulty="100" allowatstart="true" chooserandom="true" eventcount="2">
    <ScriptedEvent identifier="customevent1" commonness="100" />
    <ScriptedEvent identifier="customevent2" commonness="75" />
    <ScriptedEvent identifier="customevent3" commonness="50" />
    <ScriptedEvent identifier="customevent4" commonness="25" />
</EventSet>
```

You can also nest event sets. In this example, one event (a or b) is chosen from the first child set, and another (1, 2 or 3) from the second child set.

```xml
<EventSet identifier="outpostevents.custom" leveltype="outpost" locationtype="research,military" minleveldifficulty="15" maxleveldifficulty="100" allowatstart="true">
    <EventSet chooserandom="true">
        <ScriptedEvent identifier="customevent_a" commonness="100" />
        <ScriptedEvent identifier="customevent_b" commonness="75" />
    </EventSet>
    <EventSet chooserandom="true"">
        <ScriptedEvent identifier="customevent_1" commonness="100" />
        <ScriptedEvent identifier="customevent_2" commonness="75" />
        <ScriptedEvent identifier="super_rare_customevent_3" commonness="1" />
    </EventSet>
</EventSet>
```

So, where are you supposed to put the event set? The vanilla scripted events are configured in a file called OutpostEvents.xml, with includes a big multi-level event set that configures the events for all different kinds of outposts. Overriding and modifying this whole vanilla event set, and keeping it up-to-date with the vanilla game would be cumbersome (not to mention keeping it up-to-date and compatible with other mods). That's where additive event sets come in.

Additive event sets simply add more events on top of the existing events, which is often how mods should configure events (unless you're prepared to override all the events in the vanilla game).

Making an event set additive is simply just a matter of setting the attribute "additive" to true. The following set would create one of the custom events on top of the other events:

```xml
<EventSet identifier="outpostevents.custom" additive="true" leveltype="outpost" chooserandom="true" eventcount="1">
    <ScriptedEvent identifier="customevent1" commonness="100" />
    <ScriptedEvent identifier="customevent2" commonness="75" />
</EventSet>
```

### Status effects

Events can also be triggered by any status effect, for example when some item is used, some monster spawned or killed, or when a player touches a trigger on a level object. See the [status effect documentation](../StatusEffectIntroduction.html) for more details.


### Missions

Missions can also trigger events, as soon as the mission starts or when it reaches a certain state. Scripted events are a good way to add some extra flavor and functionality to missions: the vanilla jailbreak missions for example use events to make the outpost security react to the player's actions.

The following, when placed in a mission config, would trigger the event "somecustommissionevent" 10 seconds after the mission has started (the initial state of the mission being 0).

```xml
<TriggerEvent state="0" delay="10" eventidentifier="somecustommissionevent" campaignonly="true"/>
```

### Event editor

Barotrauma also includes a built-in editor for creating and editing scripted events. You can access the editor using the console command "editevents".

### Event actions

Here's a full list of all the available event actions:

[TODO: list EventActions]