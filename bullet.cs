// Updated Bullet Script
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private float damage = 10f; // Amount of damage the bullet causes

    private void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy the bullet after its lifetime expires
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has a Health component
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage); // Apply damage to the object
        }

        // Destroy the bullet upon collision
        Destroy(gameObject);
    }
}

/* ==================== TUTORIAL FOR IMPLEMENTATION ====================
This guide will help you implement the Bullet script with the Gun script provided earlier, 
including functionality for causing damage to other objects.

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

6. **Enable Damage Application:**
   - Ensure that objects in your scene that can take damage have a `Health` component attached.
   - The `Health` script should include methods for reducing health and destroying the object when health reaches zero.
   - The `Bullet` script will automatically detect collisions with objects that have the `Health` component and apply damage.

7. **Test the Setup:**
   - Press the Play button in Unity.
   - Press the left mouse button (or the key mapped to `Fire1`) to fire bullets.
   - Bullets should spawn at the `Bullet Spawn` position, move forward, and apply damage to objects with a `Health` component.
   - Watch the target's health decrease in the Console log (or add UI for visual feedback).

8. **Enhancements:**
   - Add particle effects or sounds when the bullet is fired or collides with an object.
   - Customize the `damage` value in the `Bullet` script to vary damage for different bullet types.
   - Implement a visual health bar on target objects for easier testing and gameplay immersion.

9. **Advanced Features (Optional):**
   - Introduce additional mechanics such as armor or shields by modifying the `Health` script.
   - Integrate networking for multiplayer damage synchronization.

==================================================================== */

