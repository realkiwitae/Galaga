# Galaga
Galaga arcade implementation

# Engine choice 
I picked Unity over ue4 as I have not managed to get support for cpp project last year when I tried on my ubuntu 18.04.

# Art
I am getting the art assets from online courses



# TASKS
1. General: 
    1. User can start the game and restart after a win/loss TODO
2. Rules:
    1. Win: (you may choose one) achieve N score or kill  M enemies. WIP
    2. Lose:  the player's lives are exhausted. WIP
3. UI:
    1. Current score, Remaining lives, Menu to start/restart game. WIP
4. Gameplay:
    3. Enemies fly in groups into a formation near the top of the screen, then begin flying down toward the player, firing bullets. TODO
5. Bonus:
    1. Enemies may drop item, Player's ship can equip it to enhance gun, such as multi-bullets in one shot, or faster shooting. TODO


# -------------------------------- #

# 13th/12:
This is gonna be fun, freshly downloaded unity and made a custom setup for my project.
I chose Galaga because I already I already played around with PACMAN and Tetris like games on Codingame.

# 14th/12
1. Screen ratio to 9/16 and orthographic camera for that 2D feel.
2. Quick player movement behaviour restrained to bottom of the screen. I probably overdid it ith the traveling function ~ but It feels more natural this way
3. Added the laser prefab as projectiles. Turned out I needed to delete them once they reach out of screen, or they woud stack up.
4. I wanted a way to share the info between all the classes about the screen positions ingame, and potential other features, so I made a singleton class. Hence the gamemanager.
5. Now the player is moving and firing weapons.
6. adding enemies gotta remember that needs a rigid body with trigger on for collisions.
7. Looked to cleanup gitignore as I keep getting untracked files 
