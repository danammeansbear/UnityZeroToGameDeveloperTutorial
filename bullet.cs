// Updated Bullet Script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Time after which the bullet will automatically be destroyed
    [SerializeField] private float lifeTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy the bullet after its lifetime expires
    }

    // OnCollisionEnter is called when the bullet collides with another object
    private void OnCollisionEnter(Collision collision)
    {
        // Optional: Add effects here (e.g., sparks, sounds, etc.)

        Destroy(gameObject); // Destroy the bullet upon collision
    }
}

/* ==================== TUTORIAL FOR IMPLEMENTATION ====================
This guide will help you implement the Bullet script with the Gun script provided earlier.

1. **Set Up the Bullet Prefab:**
   - Create a GameObject in your Unity scene that will represent the bullet (e.g., a small sphere).
   - Add a `Rigidbody` component to the bullet GameObject to enable physics interactions.
   - Attach the `Bullet` script to the bullet GameObject.
   - Save the bullet GameObject as a prefab by dragging it into the `Assets` folder.

2. **Set Up the Gun:**
   - Create a GameObject that will act as the gun (e.g., a cube or a 3D model of a weapon).
   - Attach the `Gun` script to the gun GameObject.

3. **Link the Bullet Prefab to the Gun Script:**
   - Select the gun GameObject in the Unity Editor.
   - In the Inspector, locate the `Gun` script.
   - Drag the bullet prefab from the `Assets` folder into the `Bullet` field of the `Gun` script.

4. **Configure the Bullet Spawn Point:**
   - Create an empty GameObject as a child of the gun.
   - Position this empty GameObject where you want the bullets to spawn (e.g., at the gun barrel).
   - Assign this empty GameObject to the `Bullet Spawn` field in the `Gun` script.

5. **Adjust Bullet Speed:**
   - In the `Gun` script component in the Inspector, set an appropriate value for `Bullet Speed`. This determines how fast the bullets travel.

6. **Test the Setup:**
   - Press the Play button in Unity.
   - Press the left mouse button (or the key mapped to `Fire1`) to fire bullets.
   - Bullets should spawn at the `Bullet Spawn` position, move forward, and be destroyed either after a set lifetime or upon collision.

7. **Enhancements:**
   - Add particle effects or sounds when the bullet is fired or collides with an object.
   - Consider implementing additional logic, such as dealing damage to objects hit by the bullet.

==================================================================== */
