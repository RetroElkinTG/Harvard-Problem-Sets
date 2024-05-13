# Malevolent-Shrine
#### Video Demo: https://youtu.be/lhc28-55SNs
#### Description:
A 2D Zelda-like RPG that will challenge the player and provide an enjoyable albeit unforgiving experience. I created this game using the Unity game engine, and along the way I learnt the C# programming language. Unity has exhaustive resources on 2D game development, and its documentation along with YouTube tutorials were the main resources used
to effectively complete my game. The main aim of the game is to reach Malevolent Shrine, providing a rich exploration and combat experience which may be unforgiving to some.

SYNOPSIS
I was extremely ambitious for the project as I planned a much bigger game, but through time and experience I learnt that it takes an exceedingly long time to make a video game. The sheer number and complexity of some systems make the development process exceptionally long, and thus I have not met my own personal criteria of making a big, adventurous game. Despite the slow development times the game has met the quality assurance criteria in 5 different ways:

The definitive version of the video game features hidden secrets and has an especially fun movement system that ensures plays do not get bored of playing over multiple playthroughs. The hidden secrets, specifically, are in the form of buffs and cool enemies that players might not be able to find in their first playthrough. Thus, the software solution features excellent reusability that prevents the feeling of repetitiveness.

The final game features a highly detailed and expansive level design that will awe players, inspiring exploration of the game world and becoming curious about the game’s lore, which is further made ambiguous and led up to the player’s imagination. This particular design provides a memorable experience for casual and hardcore gamers alike, meeting the testability requirements of the quality assurance criteria.

The game is run on very efficient code that does not loop or execute routines unnecessarily, and it is also highly optimized to run on Windows computers with very low-end specs (such as mine). It can also run an extremely high frame rate and resolution on higher-end computers with no latency felt at all. The video game can also be played on Mac using Whisky, an application that can run most Windows executable files on Mac without the loss of performance seen in virtual machines. Thus, my video game is highly performance-oriented.

As integrity is crucial to a high-quality software solution, the completed project boasts a lot of feedback from a diverse array of players. The game also features icons to identify core gameplay mechanics, allowing users of all languages and cultures to download and play the game. It also does not attach stereotypes to its players or segregates them, and thus player integrity is maintained as the video games respects the needs and identities of its players.

ERRORS
The most common errors I encountered included logic errors and two particular runtime errors named ‘NullReferenceException’ and ‘UnassignedReferenceException,’ where scripts try to access a reference variable which has no object attached to it. This happens quite frequently due to forgetting to assign objects but is easily solved by assigning the desired object in the Unity inspector. Logic errors were the most difficult to solve as they were not immediately understood. This was due to the substantial number of systems interacting with each other.

Some syntax errors were encountered but were easily fixed as Visual Studio immediately notifies the developer of a typo. Speaking of debug logs, the debug log was an incredibly useful tool used to find what the outputs were of particular subroutines. For example, when the enemy was not dying in-game, the debug log helped me realise that the enemy was not losing any health per hit. Thus, debugging was useful in fixing bugs. Desk-checking also helped when ensuring that the order instructions were reliable, considering spam inputs and such.

The most difficult bug that occurred in my video game was collision glitches, where sometimes the knockback of enemies and players was so strong that the physics update didn’t have time to register colliding objects. Although occurring randomly, it was a known issue throughout the entire development process, and fixing it was thought to be impossible. But with problem solving skills and debugging strategies, the issue was solved. Considering the rarity of the bug, providing evidence of it wasn’t available.

While other strategies such as using stubs and flags were useful in fixing bugs, debugging output statements was the most effective strategy that prevented errors before they even happened, making the software development process much simpler.

USER DOCS
The provided user documentation is designed to provide only the minimal information required to play the game, allowing the player to discover the game’s mechanics and world at their own pace. This ensures a fulfilling user experience and leaves a long-lasting impression for players to contemplate. The documentation is located in the README.txt of the game’s install folder, and is as follows:

MALEVOLENT SHRINE USER DOCUMENTATION

CREATOR'S NOTE:
- This game is meant to be unforgiving, and some features that are usually thought
  of as bugs or laziness are actually intended features meant to provide you a
  sense of challenge. Thanks for playing my game!

INSTALL:
- For Windows, open the Malevolent Shrine.exe
  application with the key logo on it.
- For Mac, open the Whisky application and then follow
  the above steps.

CONTROLS:
- WASD/Arrows to move.
- Space/LMB to attack/interact.
- Escape to pause.
- Alt+Enter to enter Fullscreen.

CREDITS:
- Game by: RetroElkinTG
- Assets by: OpenGameArt.org & Raven Fantasy

