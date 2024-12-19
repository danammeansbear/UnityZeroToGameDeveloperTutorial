using UnityEngine;

public class Radiation : MonoBehaviour
{
    public GameObject radiationEffect; // Visual effect for radiation
    public float damagePerSecond = 10f; // Damage dealt per second in radiation zone
    public GameObject explosionEffect; // Explosion effect prefab
    public string throwableTag = "Throwable"; // Tag for throwable objects

    private bool isPlayerInZone = false;
    private ThirdPersonCharacter player;

    void Update()
    {
        // Apply damage if the player is in the radiation zone
        if (isPlayerInZone && player != null)
        {
            player.health -= damagePerSecond * Time.deltaTime;
            player.health = Mathf.Max(player.health, 0); // Clamp health to non-negative values

            if (player.health == 0)
            {
                player.GetComponent<Animator>().SetTrigger("dead");
                player.GetComponent<ThirdPersonUserControl>().enabled = false; // Disable player controls
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
            player = other.GetComponent<ThirdPersonCharacter>();
            radiationEffect?.SetActive(true); // Activate the radiation visual effect
        }
        else if (other.CompareTag(throwableTag))
        {
            TriggerExplosion(other.transform.position);
            Destroy(other.gameObject); // Destroy the throwable object
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            player = null;
            radiationEffect?.SetActive(false); // Deactivate the radiation visual effect
        }
    }

    private void TriggerExplosion(Vector3 position)
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, position, Quaternion.identity); // Create an explosion effect
        }

        // Optional: Add logic to disable the radiation effect temporarily or permanently
    }

    /*
    ==================== INTEGRATION TUTORIAL ====================
    This script handles radiation damage and throwable object interaction.

    1. Player Setup:
       - Ensure the player GameObject has the following:
         - Components:
           - ThirdPersonCharacter
           - ThirdPersonUserControl
           - Animator
           - Rigidbody
           - CapsuleCollider
         - Tag: "Player"
       - Make sure the health, hunger, and thirst sliders are configured in the Unity Editor.

    2. Radiation Zone Setup:
       - Create a GameObject for the radiation zone (e.g., "RadiationZone").
       - Add a BoxCollider (or any Collider) component to the GameObject and set it to "IsTrigger".
       - Attach this Radiation script to the GameObject.
       - Assign the following fields in the Unity Editor:
         - Radiation Effect: Drag a particle system or visual effect prefab.
         - Explosion Effect: Drag an explosion prefab.
       - Optional: Adjust the `damagePerSecond` field to control the rate of damage.

    3. Throwable Object Setup:
       - Create a prefab for the throwable object (e.g., "ThrowableObject").
       - Add the following components:
         - Rigidbody
         - Collider (set to non-trigger)
       - Tag: "Throwable"
       - Optional: Add a script to handle throwing mechanics.

    4. Animator:
       - Ensure the Animator Controller for the player has a "dead" trigger and a death animation.

    5. Functionality:
       - Radiation Effect:
         - When the player enters the radiation zone, their health decreases over time.
         - If health reaches zero, the death animation is triggered, and player controls are disabled.
       - Throwable Interaction:
         - When a throwable object enters the radiation zone, an explosion effect is instantiated.
         - The throwable object is destroyed upon contact.

    6. Testing:
       - Play the game and ensure the player takes damage in radiation zones.
       - Test throwing objects into the radiation zone to trigger explosions.
    =============================================================
    */
}
