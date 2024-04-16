README - Prefab

Prefab: KP

Purpose: The prefab is used by the object spawner used to spawn power-ups. The spawner uses the designed prefab as an element in its list that is spawned.

Design: Simple square shape, modified to resembled that of a ketchup packet. Red, planning to make the prefab have a shadow and bouncing effect, possibly audio as well.

Components: Power-Up script. This script is used to detect when the player has collided with the power-ups. It is connected to the box collider 2D component, which triggers the script to apply the power up effect. 

Effect: When the power up is collided with, the player will gain a special power and the power up will be destroyed. 

Implementation: Using the spawn script, the prefab is placed in the spawnPoint gameObject, that is used by the spawnlocations to place individual power ups in specific locations. 

Further Use: Placed throughout the levels in specially picked locations, will be merged with other team members code to have effect on the player. 






