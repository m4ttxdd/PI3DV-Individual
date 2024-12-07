<a name="_mqn2ieo1327s"></a>Mini project

Name: Mattias Frej Lindstrøm Ehlert

Student Number: 20234643

Link to Project: [github.com/m4ttxdd/PI3DV-Individual](http://github.com/m4ttxdd/PI3DV-Individual) 

## <a name="_6jey5cb9np0j"></a>Game idea and breakdown:
The idea was to make a medieval sword fighting game like Mordhau, Chivalry, For Honor etc. Since I didn't want to focus too much on the combat system the combat is broken down into either attacks or blocks, which when performed at the right time stagger the opponent. The player moves using a CharacterController using WASD with attacks and blocks being mouse 1 and 2, and the enemies navigate a NavMesh. The game takes place on a set level and the goal is to reach the end portal by defeating or dodging the enemies.

Main parts of the game:

- Player movement, moved with CharacterController and uses a Cinemachine follow for the view, which is rotated from code, the scripts are from the [Unity FPS Controller](#_3c17jplzf8f). The camera target is a child of the actual player model so it feels more immersive. The hands are always in view. The way I did this was using Animation Rigging, and making the pelvis and up look at the forward of the camera using Multi-Aim Constraint.
- Enemy behaviour, enemy will stand still guarding their area until a player enters their vision cone where they will then chase using the NavMesh until the player is out of view for a couple of seconds. If the player is in range will attack.
- Character animations, both enemy and player use the same animation tree-and script. The movement is a blend between idle, walk and run depending on speed. The other animations play depending on triggers for respectively, attack, block and gotBlocked.
- Character combat, when attacking a character will enable the collider on their sword enabling it to deal damage, the player can also block which works the same way, except the shield uses a massive collider for consistency of blocks.
- Physics, all characters when running out of health will die and ragdoll, and get a new PhysicsMaterial which is better suited for ragdolls. Barrels in the environment have rigidbodies and need to be hit out of the way to progress, the barrels are also NavMeshObstacles and carve the navmesh allowing the enemies to path around them.
- Level, the level is a simple courtyard leading into a hallway made with ProBuilder, with enemies and environment to pass along the way. The game is very short but the level would be classified as an alley type.
- Character UI, enemies have a health bar hovering above them, and the player can see their own health bar at the top of their screen.

Visual parts of the game:

- All materials, models and animations were imported, the characters and animations from mixamo, and the environmental models and textures from Quixel.
- The end portal shader was made as part of the shader lectures but worse exercise, and uses noise twirl and rotate for the main shape, as well as sine to make it reverse back and forth.
- Particle systems, I made two particle systems one for the rain and one for the torches fire/smoke. The rain works by taking the shape and scaling it to be long and narrow, adding negative velocity over lifetime on the y axis, combined with some noise. The torch particles use a rounded material so the edges are smooth, as well as an upwards cone shape. Then color and size are added over lifetime, so the color goes from yellow to red to black as well as size increases to simulate a fire creating smoke and growing, lastly some noise was added as well. 
- The material used for the ground outdoors in the rain, was made to look very reflective as if the rain had drenched the ground whereas the indoor ground has almost no reflectiveness and looks dry.
- The game is made in HDRP and uses a global volume for many visual effects. The effects which are used are listed below:
  - Visual environment (Sky & sun)
  - Fog
  - Exposure
  - Bloom
  - Film grain
  - Chromatic aberration
  - Motion blur
  - Vignette
  - Panini projection
  - Screen space ambient occlusion
  - Volumetric clouds

\- By using the visual effects it is possible to make the game have a greater atmosphere, although the performance can decrease depending on which. I tried to use the effects to make a kind of grimy and gloomy atmosphere.
## <a name="_j9ysozq5vun2"></a>
## <a name="_cwwmzdi3gxbr"></a>Time management:

|**Task**|**Time (hrs)**|
| :- | :- |
|Unity setup & github|.33|
|Importing FPS controller|.33|
|Importing Mixamo model and animations|.5|
|Creating scripts (CharacterAnimations, CharacterAttacks, PlayerCharacter, Sword, Shield)|2|
|Creating enemy (EnemyController script, navmesh)|1\.5|
|Creating ragdoll and ragdoll script|.5|
|Creating obstacles|.33|
|Creating rain shader|.5|
|Creating level (ProBuilder)|1|
|Importing materials and models from Quixel|.66|
|Creating portal shader|.66 (made as part of lecture)|
|Adding lighting and torch shader|.5|
|Created healthbar UI|.5|
|Adding visual effects|1\.33|
|Baking lighting|2 (pc & hdrp diff)|
|Code comments|.33|
|Making readme|.5|
|**Total**|**13.5 hours**|


## <a name="_3c17jplzf8f"></a>Used resources:
Mixamo: [www.mixamo.com](http://www.mixamo.com) 

Quixel: [www.fab.com/sellers/Quixel](https://www.fab.com/sellers/Quixel) 

Unity FPS Controller: [assetstore.unity.com/packages/essentials/starter-assets-firstperson-updates-in-new-charactercontroller-pa-196525](http://assetstore.unity.com/packages/essentials/starter-assets-firstperson-updates-in-new-charactercontroller-pa-196525) 

