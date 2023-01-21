# Psychic Shooter Man

## Finished

### Player GameObject
#### Summary

- Sprite
- Collider2D
- Movement 
- Movement Speed

------------------------------------------------------------------------
#### In-depth
COMPONENTS:

- Sprite Renderer component
- Box Collider 2d
- Player.cs script
- PlayerHealth.cs script
- Boundaries.cs script


The Player.cs script controls movement, as well as having a fully functional dodge/dash mechanic.
PlayerHealth.cs script manages player health states, as well as invulnerability while dodging/after taking damage
Many of the variables for these scripts can be tweaked in teh Unity Inspector


### Beam

Made BeamWeapon prefab with it's children,
AimingLaser and BeamBlast. More comments in the files themselves.
The most information will be found in BeamWeapon.cs. Values can be
adjusted to change difficulty. BeamBlast.cs is what controls the
collision detection and destruction of the player object.

Collisions are handled via Collider components, with the isTrigger option enabled.

BeamWeapon (Parent class)
AimingLaser (Child of BeamWeapon) - telegraphs where BeamBlast will occur.
Does not have collision and cannot kill player.
BeamBlast (Child of BeamWeapon) - damaging blast that kills player.

Properties of Beam Weapon

AimingLaserTimer - how long the the aiming laser will appear for
BeamBlastTimer - how long the beam blast will appear for
Interval - interval between beam weapon firings

## To Be Added

### Laser
### Asteroid/Debris
### Shockwave
### Bomb
### Defense Platforms
### Generator

## Bonus

### Homing Missile/Bomb
### Area Bomb

Art:

Spaceship Sprites: By Gisha, https://gisha.itch.io/spaceships-asset-pack
Backgrounds: By Screaming Brain Studios, https://gisha.itch.io/spaceships-asset-pack
