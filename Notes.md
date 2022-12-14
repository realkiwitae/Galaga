# Galaga
Galaga arcade implementation

# Engine choice 
I picked Unity over ue4 as I have not managed to get support for cpp project last year when I tried on my ubuntu 18.04.

# Art
I am getting the art assets from online courses

# 13th/12:
This is gonna be fun, freshly downloaded unity and made a custom setup for my project.
I chose Galaga because I already I already played around with PACMAN and Tetris like games on Codingame.

# 14th/12
1. Screen ratio to 9/16 and orthographic camera for that 2D feel.
2. Quick player movement behaviour restrained to bottom of the screen. I probably overdid it ith the traveling function ~ but It feels more natural this way
3. Added the laser prefab as projectiles. Turned out I needed to delete them once they reach out of screen, or they woud stack up.
4. I wanted a way to share the info between all the classes about the screen positions ingame, and potential other features, so I made a singleton class. Hence the gamemanager.
5. Now the player is moving and firing weapons.
