# PapeTracker
A Paper Mario 64 item tracker for use with the online randomizer [PM64Randomizer](https://pm64randomizer.com)
This was forked from RedBuddha's [KH2Tracker](https://github.com/Red-Buddha/KH2Tracker)
Icons used in this tracker come from the Mario Wiki page on [Paper Mario](https://www.mariowiki.com/Paper_Mario)


## Options
* Save Progress
 * Saves the current tracker state to a file. If hints are loaded they will be saved as well
* Load Progress
 * Loads a saved tracker file.
  
***
  
* Reset Window
 * Resets the main and broadcast windows to their default size
* Reset Tracker
  * Resets the tracker to its default state 
  
## Toggles
* Loading Hints will automatically toggle the settings that the hint file was made with
  
***

* Drag and Drop
  * Toggles between drag and dropping items + selecting a world and double clicking an item or just selecting a world and single clicking an item to track items
  
## How To Use

Drag an item to the location that you found it in. Alternatively highlight worlds by clicking on them and then double click on items to mark them as collected in that world. Clicking on a marked item will return it to the item pool. (if not using drag and drop controls then only a single click on an item is required)

The question marks connected to each world denote the number of important checks in a world. If hints are loaded these will be set automatically as reports are tracked. If hints are not loaded they can be increased or decreased with the scroll wheel or by selecting a world and using page up / page down

If a hint file is loaded into the tracker reports must be tracked correctly. Incorrectly tracking a report 3 times will lock you out of tracking that report and receiving its hint. Hovering over already tracked reports will also display their hint text.

## Thanks

* Tommadness
  * Created the broadcast window and the framework from which the auto tracker was built. Spent a ton of time helping figure out bugs and solutions to get the auto tracker working. (Currently neither of those work but we'll get there)
* RedBuddha
  * Created the version of the KH2Tracker that I use and was the base of this tracker
* Supreme
  * Part of the dynamic duo that came up with the idea and got me inspired to really work on this
  * Chose a lot of icons to represent regions of the world
* Spikevegeta
  * The other half of the dynamic duo that was manually doing this before I made the tracker
  * Gave me good ideas for displaying progress throughout the seed