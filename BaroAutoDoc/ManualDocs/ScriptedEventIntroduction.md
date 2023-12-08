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

After you've made a scripted event, you need to make it trigger at some point in the game.

----

- **The entity executing the StatusEffect** \- Every effect is always *executed* by some *entity*, for example an item or a character. For example, if you use a gun, it might execute some effect that emits particles.

- **Type** \- The type of the StatusEffect determines *when* the effect is executed. For example, when the item is being worn or used, when a character is underwater or takes damage, or always.

[TODO: list ActionTypes]

- **The target of the StatusEffect** \- StatusEffects need to have a *target*. The target determines which entity the effect affects - this is often the same as the entity executing the effect, but it can be something else too: for example, a diving suit might have a StatusEffect that *targets* the oxygen tank inside it, making it deplete when the suit is worn.

[TODO: list TargetTypes]

## Examples

Here's an exmple of a simple StatusEffect, which makes the item deteriorate by 10 units per second when it's underwater. 

```xml
<Item identifier="watersensitiveitem" name="Water-sensitive Item">
  <ItemComponent>
	<StatusEffect type="InWater" target="This" Condition="-10.0" />
  </ItemComponent>
</Item>
```

Notice the target "This": here it refers to the item itself. 

Another thing to note is how the condition decrease is defined. Status effects can modify any *property* of the target entity (see the [content type documentation](../Intro/ContentTypes.html) for a full list of properties of different kinds of entities). In this case we are modifying the "Condition" property of the item. By default, the value is treated as "how much the value changes per second", in this case reducing the condition by 1 per second. If we wanted to instead make the item break down immediately when it's submerged, we would use the attribute 'setvalue' as follows:

```xml
<Item identifier="watersensitiveitem" name="Water-sensitive Item">
  <ItemComponent>
	<StatusEffect type="InWater" target="This" Condition="0.0" setvalue="true" />
  </ItemComponent>
</Item>
```

But what if we wanted to create a gun whose condition decreases by 10 whenever it's fired? We can't use setvalue, nor can we make the value decrease by 10 per second: we want an instant decrease of 10. Here's how we could implement it:

```xml
<Item identifier="fragilegun" name="A Rather Poor Gun">
  <ItemComponent>
	<StatusEffect type="OnUse" target="This" Condition="-10.0" disabledeltatime="true" />
  </ItemComponent>
</Item>
```

The difference here is the *disabledeltatime* attribute. Delta time refers to the amount of elapsed time, which we want to ignore altogether in this case, treating "-10" as an instantaneous decrease.

In other words, the values is treated as an increase per frame, as opposed to an increase per second. Note that you most likely would only want to use this attribute in "one-shot", instant effects that don't run over a period of time. For example, adding this attribute to the previous water-sensitive item would lead to odd results: the item would constantly deteriorate at a rate of 10 units per frame when submerged.