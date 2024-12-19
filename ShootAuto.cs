using UnityEngine;

public class ShootAuto : MonoBehaviour
{
    [Header("Ball Settings")]
    public Transform ballSpawnPoint; // The position and rotation where the ball will spawn
    public Rigidbody ballPrefab; // The prefab for the ball
    [SerializeField] private float spawnForce = 13000f; // Force applied to the ball when spawned

    [Header("Timing Settings")]
    [SerializeField] private float spawnTimeMin = 10f; // Minimum time before the first spawn
    [SerializeField] private float spawnTimeMax = 15f; // Maximum time before the first spawn
    [SerializeField] private float spawnIntervalMin = 3f; // Minimum interval between spawns
    [SerializeField] private float spawnIntervalMax = 7f; // Maximum interval between spawns

    [Header("Effects")]
    public GameObject spawnEffect; // Optional effect to instantiate when the ball is spawned

    private void Start()
    {
        ScheduleNextSpawn();
    }

    private void SpawnBall()
    {
        if (ballPrefab == null || ballSpawnPoint == null)
        {
            Debug.LogWarning("Ball or Spawn Point is not assigned. Please assign them in the Inspector.");
            return;
        }

        // Instantiate the ball and add force
        Rigidbody ballInstance = Instantiate(ballPrefab, ballSpawnPoint.position, ballSpawnPoint.rotation);
        if (ballInstance != null)
        {
            ballInstance.AddForce(ballSpawnPoint.forward * spawnForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("Failed to instantiate ball. Please check the prefab.");
        }

        // Instantiate the effect if provided
        if (spawnEffect != null)
        {
            Instantiate(spawnEffect, ballSpawnPoint.position, ballSpawnPoint.rotation);
        }

        // Schedule the next spawn
        ScheduleNextSpawn();
    }

    private void ScheduleNextSpawn()
    {
        float timeToNextSpawn = Random.Range(spawnTimeMin, spawnTimeMax);
        float interval = Random.Range(spawnIntervalMin, spawnIntervalMax);
        Invoke("SpawnBall", timeToNextSpawn + interval);
    }

    /*
    ==================== TUTORIAL FOR DEVELOPERS ====================
    This script automatically spawns a ball (or bullet) at regular intervals 
    and applies a force to it in the forward direction.

    1. **Purpose:**
       - Automatically spawns bullets/balls and applies a force for shooting functionality.

    2. **Setup:**
       - Attach this script to a GameObject (e.g., an empty GameObject in your scene).
       - Assign the following in the Unity Inspector:
         - `ballSpawnPoint`: A Transform that determines where and in what direction the ball is spawned.
         - `ballPrefab`: A Rigidbody prefab representing the ball to be spawned.
         - `spawnEffect` (optional): A GameObject prefab for an effect to show when the ball spawns.

    3. **Timing Settings:**
       - Adjust the following parameters in the Inspector:
         - `spawnTimeMin` and `spawnTimeMax`: Control the random range for the time before the first spawn.
         - `spawnIntervalMin` and `spawnIntervalMax`: Control the random range for the interval between subsequent spawns.

    4. **Force Application:**
       - The `spawnForce` determines how much force is applied to the ball when it spawns.
       - The force is applied in the forward direction of the `ballSpawnPoint`.

    5. **Effects (Optional):**
       - Assign an effect prefab to `spawnEffect` (e.g., particle system or visual indicator) to show when the ball is spawned.

    6. **Enhancements:**
       - Integrate with a `Health` script (example below) for damage handling.
       - Add sounds or effects for a more immersive experience.

    =================================================================
    */

    /* =================== Example Health Script ===================
    using UnityEngine;

    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        private float currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            Debug.Log($"{gameObject.name} took {damage} damage. Current health: {currentHealth}");

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log($"{gameObject.name} has been destroyed.");
            Destroy(gameObject);
        }
    }
    ================================================================ */
}
