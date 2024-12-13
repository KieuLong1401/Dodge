# PvZs Dodge Game
## Player (Zombie):
- Health bar (The game is over if it's value becomes 0).
- Jump.
- Rotate to face the move direction.
- Smooth rotate.
## Enemy (Plants):
- Peashooter
    - Rotate to look at Player.
    - Fixed y position of bullets.
    - Bullets will be destroyed if touch walls.
- Hipnoshroom
    - Randomly spawn inside map.
    - Only spawn if no obstacle in it's spawn radius.
    - Make Player's movement reverse when eaten.
    - Disappear after few seconds.
- PotatoMine
    - Inactive:
        - Randomly spawn inside map.
        - Only spawn if no obstacle in it's spawn radius.
        - Can be eaten by Player and disappear if Player stand inside eating radius.
        - Active after few Seconds.
    - Active:
        - Deal damage to Player and disappear if touched.
        - If wasn't touched stay there forever.
- Lawnmower
    - Randomly spawn near walls.
    - After spawned, delay for few seconds before move.
    - Move straight to the wall of other side.
    - Rotation and Movement related to map's rotation.
    - Deal damage to player if touched.
    - Destroyed if touch walls.
## Item:
- Brain
    - Randomly spawn inside map.
    - Only spawn if no obstacle in it's spawn radius.
    - Heal Player if eaten.
## Map
- Rotation
    - Rotate the map.
    - Reverse the rotate direction after random delay.
    - Random speed.