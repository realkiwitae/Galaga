# Galaga
Galaga arcade implementation

# Engine choice 
I picked Unity over ue4 as I have not managed to get support for cpp project last year when I tried on my ubuntu 18.04.

# Art
I am getting the art assets from online courses



# TASKS
2. Rules:
    1. Win: (you may choose one) achieve N score or kill  M enemies. WIP
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

# 15th/12
1. Added a scoring system scoring points when our laser kills an enemy
2. UImanager in the scene as well screen scalable
3. linked score and lives to UImanager

# 16th/12
1. Main Menu for starting game and scoreboard at the end of game
2. Menu stuff is working atm gonna start checking how to spawn enemies and formations

# 17th/12
1. no time ~ I was doing AOC day 16th https://adventofcode.com/2022 got 32*  
2. I added a game mode for diff win conditions, ill add a toggle in the menu at some point

# 18th/12
1. won't be there tomorrow ~

# 19th/12 AFK

# 20th/12
1. I made a roadmap for making engaging enemies, I want them to arrive in waves and attack the player.
2. Added a wavemanager as a bitmap grid which ensure we have enemies rows spwaning as each wave gets cleared.
3. the wave moves slowly on X axis just to make it bit alive.
4. Step 1 make enemies spawn on the wave spot
5. Step 2 - TODO spawn them on top of the screen, and make they travel to their grid target.
6. Step 3 - TODO randomly select part of the wave to go down and attack the player
7. Added score bonuses for clearing rows of the wave the highest row on screen gets more points.
8. NB: too much convolution in my code, too many references, its not clean, so I went on and made a singleton EventManager to deal with game events, as such the code is now robust and easier to read.
3events so far AddScore, PlayerHurt , EnemyDeath
9. Enemy state machine : SPAWN move toward player then fly back towards wave pos. READY IN WAVE
10. DIVE behavior done, its random groups of range 1 to 4, centered on random row/column with cd
Enemies diving increase chance of shooting, and first row goes deep for collision