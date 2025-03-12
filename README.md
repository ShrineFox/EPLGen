# EPLGen
A tool used to quickly generate certain predefined EPL effects. Mostly used to help make "corruption" effects for the P5R Vinesauce mod.  
![](https://i.imgur.com/rXijngN.png)  
  
# Features
- Add/remove/replace ``.DDS`` files in sprites list
- Save/load presets for sprites list as ``.json``
- Set parameters for each sprite (name, position, size, particle properties...)
- Set parameters for overall particle effect (type, position, size)
- Copy parameters from a specific sprite and paste them over selected sprites
- Generate ``.EPT`` files from a directory of DDS files (assumes that the DDS has 4 frames of animation from left to right)
- Create UV animation data for a specific number of frames (4 by default, needs to be manually added via hex editing)
- Create ``.GMD`` files from a directory of DDS files (using selected GMD as a base)
- Create ``.EPL`` from sprites list (using selected GMD as a base for each particle)

# Usage
1. Place GMD files that you want to use as a base for particles in the ``Dependencies\GMD`` folder.  
2. By default, the sprite GMDs provided in there already have their UVs scaled for use with a texture that has 4 frames of animation.  
3. When you load the program, you can select a GMD from the dropdown in the toolbar. It will be used as the base for each particle.  
4. You can add DDS textures to apply to each particle in the effect.

# Effects
Currently, there are two effects you can create. "Cone" or "Floor".
- "Floor" effects cover the ground with flat sprite textures that appear, animate briefly, and then disappear.
- "Cone" effects are similar, but surround a specific point in the air rather than being flat on the ground.
