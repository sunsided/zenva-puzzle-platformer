# Industrial Jones

![](.readme/editor-1.png)

## Lessons Learned

- When working with sprites, have a `Sprites` asset folder with a
  `Tiles` subdirectory. The sprite images (PNG, etc.) are going to be placed
  in the `Sprites` directory, while the tilemaps and tiles generated from it
  are going to be placed withing `Sprites/Tiles`.
- The Sprite Editor allows the placement of a custom "Physics Shape" that defines
  the shape of the collision polygon. To update the collision shape on tiles that
  are already placed, the collider needs to be disabled and re-enabled.
  - Instead of changing the physics shape to a rectangle in the sprite editor,
    it might make sense to set the sprite's collider type to `Grid` instead (from the
    default of `Sprite`).
  - The ladder sprite had its shape auto-generated and contained holes,
    which was fixed manually by removing some vertices from the shape polygon
    in the Sprite Editor.
- In the Editor, an Icon gizmo can be assigned to any game object
  In 2019.3.0b2 this is done by clicking on the icon next to the active/enable
  checkbox in the Inspector. In the screenshot above, the waypoints for the
  sawblades are using label icons as gizmos.
- Using different tilemaps for different purposes works pretty well:
  Background tilemaps don't use a collider, while "Ground" tilemaps can;
  tilemaps can be assigned to different _layers_ for collision checks.
  - The "Hazard" tilemap used in this game is used for hazard objects such
    as acid/poison and spikes; it is on the "Hazard" layer and uses (composite) colliders.
  - The "Climbable" tilemap (on the "Climbable" layer) is used for ladders;
    tiles here have (non-composite) tilemap colliders that are configured
    to be triggers.
- Collisions with the hazard layer are checked by comparing to
  `other.gameObject.layer` in the `OnCollisionEnter2D()` method.
- Using a Rigidbody 2D with a body type of `Static` results in discrete
  collisions that can result in objects moving "into" the collider when
  they are too fast. Due to this, the player character can get stuck in
  the ground when falling. Switching the body type to `Kinematic` and
  setting the collision detection to `Continuous` (from the default of
  `Discrete`) fixes this issue.

## Things to learn

- Right now, the invisible ladder top platform needs to be placed manually.
  This is an issue, because the tile is placed on the grid, while the platform
  is added manually afterwards. Apparently custom brushes can be used to automate
  this behavior, such that the platform would be added, whenever a ladder top
  is being painted.
