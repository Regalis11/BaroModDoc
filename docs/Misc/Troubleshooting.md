---
has_toc: true
---
# Troubleshooting
## I've added or renamed files in the filelist.xml but changes in those files aren't showing up.
You must restart the game to pick up new or renamed files in the LocalMods directory.

## The custom submarines/item assemblies/mods I made before v0.17 have disappeared
The game will not attempt to delete any of the mods you've created in past versions of the game, so the files should still exist.

Upon first launch of v0.17, the game will check for any existing old mods and submarines and ask you to transfer them to your LocalMods folder. It's possible that the game didn't automatically enable the resulting mods, so first you may want to click on the `Steam Workshop` button on the main menu and see if your submarines are in the `Disabled regular packages` list. If they are there, select them (**TIP:** hold Ctrl or Shift to select multiple mods) and drag them into the `Enabled regular packages` list, and hit `Apply`. Now your submarines should appear in the submarine editor.

If your old mods are not present in the list, it's possible that the process to copy them over failed or was cancelled. To trigger it again to retry, go into the game's local files and delete `Mods/LOCALMODS_README.txt`. Then, restart the game and select your missing mods. Then, for good measure, check that they are enabled as described previously.

If this still doesn't work, you may have encountered a bug. Please report this issue on our [GitHub issue tracker](https://github.com/Regalis11/Barotrauma/issues), along with any information you can give us, such as console errors and mod files that refuse to transfer correctly.

## I made a mod before v0.17 and I'm unsure of how to publish changes now
To edit a mod you've published in a previous version of the game, follow these steps:
- If needed, create a local copy of your existing mod by going into the Publish tab and selecting your mod
- Edit the copy of your mod in the `LocalMods` folder, which can be accessed by clicking on the pencil icon next to your local copy in the `Installed Mods` list.
- Once you're done making changes to that copy of the mod, go back into the Publish tab and submit it.

This is, admittedly, fairly unintuitive. This is subject to change in future updates.

## A mod I used to be able to play with now refuses to be enabled
It's likely that this mod has some sort of error in its XML that is making it unable to function properly; the game now disables mods with such errors to prevent obscure problems further down the road. If you're the develop of this mod, you may need to edit it to correct these errors. It's also entirely possible that you've encountered a bug with the game, so if you suspect that this is the case, let us know by [submitting a detailed report on our issue tracker](https://github.com/Regalis11/Barotrauma/issues)!

If the error you see is "Failed to add the prefab X from MyMod: a prefab with the same identifier from OtherMod already exists; try overriding": This occurs when two prefabs with the same identifier are defined, and neither of them is marked to [override](../Intro/Overrides.md) the other. To solve this, you will either need to change the identifier of the mentioned prefab in your mod, or wrap it in `<override>...</override>` tags.
