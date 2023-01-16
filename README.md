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

- Sprite Renderer component (triangle sprite, but can be replaced with
    other sprites)
- Polygon Collider 2D (shaped into triangle, but can be shaped to
    conform to other sprites)
- Player.cs script

So far, the Player.cs script just controls movement and movement speed.
Other functionality can be added in the future though.
The speed variable can be adjusted within the Unity UI as well under the
Player.cs script component.

May add RigidBody2D component in future.

Commented out code in Player.cs is a different implementation of the
same functionality. Will probably remove in the future.

### Beam

Made BeamWeapon prefab with it's children
AimingLaser and BeamBlast. More comments in the files themselves.
The most information will be found in BeamWeapon.cs. BeamWeapon
is working pretty well and follows the design set out. Values can be
adjusted to change difficulty. Something that can be added is some sort
of value that randomizes the firings of the different BeamWeapons so
they don't all fire at the same time. BeamBlast.cs is what controls the
collision detection and destruction of the player object.

Of note is that one of the objects when a collision happens must have
a dynamic Rigidbody (as opposed to a static or kinematic). I gave the player
object the dynamic rigidbody because if the BeamBlast has it then it gets
knocked way when it's collided with, destroying the illusion that it's a
laser.

reference:
https://needoneapp.medium.com/
unity-should-i-choose-kinematic-
static-or-dynamic-for-rigidbody-2d-body-type-191ce65fa35f

BeamWeapon (Parent class)
AimingLaser (Child of BeamWeapon) - telegraphs where BeamBlast will occur.
Does not have collision and cannot kill player.
BeamBlast (Child of BeamWeapon) - damaging blast that kills player.

Properties of Beam Weapon

AimingLaserTimer - how long the the aiming laser will appear for
BeamBlastTimer - how long the beam blast will appear for
Interval - interval between beam weapon firings

Also, added small script to give the player infinite lives, but it isn't
working yet. Needs to be added to a more global object besides the player.

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
