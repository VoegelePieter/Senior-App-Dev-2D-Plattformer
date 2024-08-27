# About this Project
This game project is made by Pieter Vögele and Christoph Meyer in the scope of our 6th semester App Development course at XU University with Prof. Peter Bergner as the leading teacher.

The purpose of the project is to give insight into the choices we made during development, and explain certain aspects of the game, as there is also no tutorial.

# Title Scene
## Icon
The icon is what we already made for our 5th semester AppDev class, using generative AI. We decided to keep it, as we've seen it as apropriate for this game.
## Highscore Board
The highscore board shows the players' top 10 scores, together with the dates when they got them. This was designed by taking a look at other arcade-y games like [Minesweeper by Dustland Design](https://play.google.com/store/apps/details?id=ee.dustland.android.minesweeper). We will look at how those scores are calculated at a later point in this document.

# Level Scene
## Prefabs
### Level Manager
Manages Star Requirements, Respawning, Score, and some Collectibles
### Main Camera
Follows specific Target (Player) and manages the Background Movement
### Player
Literal Milkman who needs to bring his milk to the flag pole. Damaging the player results in loosing milk (HP). Milk bottle Collectibles restore lost milk. Milk also degrades over time, making the player have to rush to not take too much damage.
### Player UI
Shows milk count, collected gem amound and current Score. contains also 3 buttons to control the player. The buttons may not work in the editor (Depending on the used Unity Version), and the `A`, `D`, and `space` buttons may be used for controls in that case.
### Enemies
All enemies, except for the boss, reward the player with a configurable amount of score points.
#### Frog
Jumps around between two pre defined points and damages the player on contact. Can be killed if you jump on his head
#### Eagle
Flies around between a distinct amound of points, if the player is in its attack distance, it will fly to the player and attack them. 
#### Spring
The spring enemy is designed not directly as an enemy, but also as a tool, as it can be used to reach high places. It has a higher "Player Bounce" value, which causes the player to bounce a lot higher than normal when jumping on them. They share the same behavior controller as the frog, but are configured differently.
#### Boss
Super Mario NES inspired Bowser bossfight, where he shoots fireballs and the player has to slip through under him to reach the lever to win the fight and unlock the finish.
##### Lever
If the player collides with it, destroys the ground underneath the boss and the wall that is blocking the finish.
### Collectibles
#### Gems
Gems only serve the purpose of increasing the overall score. The amount of score each gem gives can be modified easily and individually, but has been left at 25 for all in this case.
#### Health Pickups
Milk bottles that fill up the players' health by a configurable amount. In most cases it's one, but for some extra rewards or to make sure the player survives the walk to the finish line, it has been increased to two.
#### Special Collectibles (Strawberries & Cherries)
A single strawberry needs to be a collected, and then its hierarchical children will spawn into the world. Upon collecting all children (in the prefab it's 5, but to add more you simply add more children) the player will be rewarded with an additional 1000 points to the score counter. This value can also be adjusted easily. We found the best way to make it a challenge to get the children, which are represented as cherries, is to spread them across the whole level, motivating to backtrack as well.
### Checkpoint
They allow the player to respawn at other spots throughout the level, to accomodate for running out of health in a non-frustrating way. The default score reward for activating a checkpoint is 200.
### Checkpoint Controller
Manages which checkpoints are active, and where the player will respawn when running out of health.
### Environmental Hazards
#### Spikes
A simple floor-mounted spike trap that deals one point of contact damage to the player.
#### Water Droplets
Placing a Water Leak prefab causes water droplets to repeatedly spawn and fall downwards, damaging the player if they stand in its way.
#### Pits & Plattforms
By simply making pits in the terrain that are hard to cross, the player may fall down into the "kill-zone". To accompany the pits, we've also created plattforms, some of which can move. Those platforms can obviously also be used to create alternate paths or change the overall movement options, not just over pits.
### Background
Has a far background which moves exactly like the camera and a middle background which moves in a parallax like way.

## Level Design
The level scene we've created starts with some simple frog enemies and a plattform above them, similar to how Mario games tend to start. Then the player has to cross over a pit with two moving plattforms, all while an eagle tries to damage them. Then the player reaches an intersection, where they can either make a skillful bounce off of a spring enemy to climb up some plattforms, or they can go on further. If they continue further without bouncing off the spring, they'll cross another pit and a some spikes before getting to a small cave. The cave has another frog enemy and plenty of water dropping from the ceiling, making the player have to move carefully. Once out of the cave, there's another skill jump off a spring that players may use to grab some healing and gems. Right after that, comes the bossfight, with a flat arena and Bowser. To defeat bowser, the player needs to slip by him and trigger the lever at the end. Once triggered, the ground beneath Bowser will disappear, and a wall will open for the player to go and reach the goal.

That's not all though, as if the player actually took the alternate path, they jump across a few floating plattforms up high, and do another spring jump on top of the map, also inspired by old Mario games. Once there, they can fall into a secret room with the Special Collectible, with the appearance of a strawberry. Once collected, several cherries will spawn in across the entire level. Once the player has collected all the cherries as well, they'll be awarded with a huge score payout. Fomr the secret room, the player can leave right behind the previously mentioned dripstone cave, but will need to backtrack if they want the cherries.

# Victory Scene
## Score
The final score is calculated based on several actions in the level. Here's what adds to the score:
- Killing an enemy
- Collecting Gems & the Special Collectible
- Reaching checkpoints
Things that reduce the score are either running out of health, or falling down a pit.
## Star Rating
Stars are awarded for the amount of score gained during gameplay. In the Level Manager, three threshholds can be set, for when each star should be granted.
## Highscore List
This is the same as the one in the main menu, except for that you may see one of your new highscores in there, if you happen to have achieved one.

# Other Scenes
## Testing scenes
The other scenes that you may find in the files, and are not accessible via the game, are the scenes we have used to test and develop the features that we assigned to ourselves, to interfere with the other person less when making changes but still having the advantage of always being up-to-date on everything by being on the same branch.
