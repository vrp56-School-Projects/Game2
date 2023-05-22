# mARble meltdown


## Information
This game was developed using Unity and ARFoundation for Android devices. The list of compatible devices can be found [here](https://developers.google.com/ar/discover/supported-devices "here"). We tested development on a Samsung Galaxy S8. 


## Gameplay

### Board Placement
When the game opens up, the players has to move their phone around to detect a plane to play on. This can be any horizontal surface, preferably one with distinguishing features. After a plane has been detected, the board prefab will move to that position and the players can tap anywhere on the plane to lock the board in place. 

![](https://github.com/vrp56/Game2/blob/AR_dev/Screenshots/PlaceBoard.PNG)

### Stage Selection
Once the board position has been set, the players will see a stage selection menu where they can cycle through the differnt stages by tapping the option arrows. Once on the desired stage, it can be confirmed by tapping on the check button. 

![](https://github.com/vrp56/Game2/blob/AR_dev/Screenshots/BoardSelection.PNG)

### Marble Selection
After the the stage has been selected, the players will see a Player 1 Options menu where Player 1 can select their desired marble texture, trail, and hat. This is done similarly to stage selection and the changes can be seen on the marble orbiting the menu. Once Player 1 has confirmed their settings, Player 2 will be allowed to do the same. 

![](https://github.com/vrp56/Game2/blob/AR_dev/Screenshots/PlayerOptions.PNG)

### Player Turn
Once both players have finalized their settings, the competition begins. The player marble can be moved along the edge of the board by moving the device left or right and confirming location by tapping the check button. Each player will take turns launching their mable via a direction/power arrow that is controlled by the check button under the board. 

![](https://github.com/vrp56/Game2/blob/AR_dev/Screenshots/Play.PNG)

### Rules
- Each player gets 5 marbles
- Points are determined by distance from scoring marble
	- Hitting other marbles, including the scoring marble, is legal
	-	Concentric circles visualize score zones and can be viewed by moving camera toward the score marble
	-	The lowest score wins
- Player 1 goes first
- If the score marble is outside of the play bounds at the end of a player's turn, they lose the game

![](https://github.com/vrp56/Game2/blob/AR_dev/Screenshots/ScoreBounds.PNG)

### Links

The APK of the game can be downloaded [here](https://drive.google.com/file/d/1xLdMYatewBzxmCQs6Mgx8Svhbo9Xn12I/view?usp=sharing "here").

