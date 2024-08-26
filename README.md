# About this Project
This game project is made by Pieter Vögele and Christoph Meyer in the scope of our 6th semester App Development course at XU University with Prof. Peter Bergner as the leading teacher.

The purpose of the project is to give insight into the choices we made during development, and explain certain aspects of the game, as there is also no tutorial.

# Title Scene
## Icon
X
## Highscore Board
The highscore board shows the players' top 10 scores, together with the dates when they got them. This was designed by taking a look at other arcade-y games like [Minesweeper by Dustland Design](https://play.google.com/store/apps/details?id=ee.dustland.android.minesweeper). We will look at how those scores are calculated at a later point in this document.

# Level Scene
## Prefabs
### Level Manager
X
### Main Camera
X
### Player
X
### Player UI
X
### Enemies
All enemies, except for the boss, reward the player with a configurable amount of score points.
#### Frog
X
#### Eagle
X
#### Spring
The spring enemy is designed not directly as an enemy, but also as a tool, as it can be used to reach high places. It has a higher "Player Bounce" value, which causes the player to bounce a lot higher than normal when jumping on them. They share the same behavior controller as the frog, but are configured differently.
#### Boss
X
##### Fireball
X
##### Lever
X
### Collectibles
#### Gems
Gems only serve the purpose of increasing the overall score. The amount of score each gem gives can be modified easily and individually, but has been left at 25 for all in this case.
#### Health Pickups
Milk bottles that fill up the players' health by a configurable amount. In most cases it's one, but for some extra rewards or to make sure the player survives the walk to the finish line, it has been increased to two.
#### Special Collectibles (Strawberries & Cherries)
A single strawberry needs to be a collected, and then its hierarchical children will spawn into the world. Upon collecting all children (in the prefab it's 5, but to add more you simply add more children) the player will be rewarded with an additional 1000 points to the score counter. This value can also be adjusted easily. We found the best way to make it a challenge to get the children, which are represented as cherries, is to spread them across the whole level, motivating to backtrack as well.
### Checkpoint
X. The default score reward for activating a checkpoint is 200.
### Checkpoint Controller
X
### Environmental Hazards
#### Spikes
A simple floor-mounted spike trap that deals one point of contact damage to the player.
#### Water Droplets
Placing a Water Leak prefab causes water droplets to repeatedly spawn and fall downwards, damaging the player if they stand in its way.
#### Pits & Plattforms
By simply making pits in the terrain that are hard to cross, the player may fall down into the "kill-zone". To accompany the pits, we've also created plattforms, some of which can move. Those platforms can obviously also be used to create alternate paths or change the overall movement options, not just over pits.
### Background
X

## Level Design
X

# Victory Scene
## Score
X
## Star Rating
Stars are awarded for the amount of score gained during gameplay. In the Level Manager, three threshholds can be set, for when each star should be granted.
## Highscore List
This is the same as the one in the main menu, except for that you may see one of your new highscores in there, if you happen to have achieved one.

# Other Scenes
## Testing scenes
The other scenes that you may find in the files, and are not accessible via the game, are the scenes we have used to test and develop the features that we assigned to ourselves, to interfere with the other person less when making changes but still having the advantage of always being up-to-date on everything by being on the same branch.
