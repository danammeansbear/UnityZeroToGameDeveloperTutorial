// DestroyAfterTime.cs
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [Tooltip("Optional effect to instantiate before destruction.")]
    public GameObject effect;

    [Tooltip("Time (in seconds) before this GameObject is destroyed.")]
    public float timeToDestroy = 5f;

    // Start is called before the first frame update.
    void Start()
    {
        // Schedule the destruction of this GameObject after the specified time.
        Invoke(nameof(Explode), timeToDestroy);
    }

    // This method handles instantiation of the effect and destruction of the GameObject.
    void Explode()
    {
        if (effect != null)
        {
            Instantiate(effect, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}

-------------------------------------------------------------------

// DestroyOnTrigger.cs
using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    [Tooltip("The GameObject to destroy.")]
    public GameObject objToDestroy;

    [Tooltip("Optional effect to instantiate upon destruction.")]
    public GameObject effect;

    // This method is called when another collider enters this GameObject's trigger collider.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is tagged as "Player".
        if (other.CompareTag("Player") && objToDestroy != null)
        {
            // Instantiate the effect if it is assigned.
            if (effect != null)
            {
                Instantiate(effect, objToDestroy.transform.position, objToDestroy.transform.rotation);
            }
            // Destroy the specified GameObject.
            Destroy(objToDestroy);
        }
    }
}

-------------------------------------------------------------------

// Jumpscare.cs
using System.Collections;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    [Tooltip("The GameObject representing the jumpscare.")]
    public GameObject jumpscareObject;

    // Start is called before the first frame update.
    void Start()
    {
        // Ensure the jumpscare object is initially inactive.
        if (jumpscareObject != null)
        {
            jumpscareObject.SetActive(false);
        }
    }

    // This method is triggered when another collider enters this GameObject's trigger collider.
    private void OnTriggerEnter(Collider player)
    {
        // Check if the colliding object is tagged as "Player".
        if (player.CompareTag("Player") && jumpscareObject != null)
        {
            // Activate the jumpscare object.
            jumpscareObject.SetActive(true);

            // Start a coroutine to destroy the jumpscare object after a delay.
            StartCoroutine(DestroyObjectAfterDelay());
        }
    }

    // Coroutine to handle delayed destruction of the jumpscare object.
    private IEnumerator DestroyObjectAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        // Destroy the jumpscare object and the GameObject this script is attached to.
        if (jumpscareObject != null)
        {
            Destroy(jumpscareObject);
        }
        Destroy(gameObject);
    }
}

-------------------------------------------------------------------

// Tutorial
/*
Tutorial: Implementing the Scripts in Unity

1. **DestroyAfterTime Script**:
    - Attach `DestroyAfterTime` to any GameObject you want to destroy after a specific time.
    - In the Inspector, set the `effect` (optional) to a prefab (e.g., an explosion particle effect).
    - Set `timeToDestroy` to the desired delay in seconds.

2. **DestroyOnTrigger Script**:
    - Attach `DestroyOnTrigger` to any GameObject with a trigger collider.
    - Assign `objToDestroy` to the GameObject you want to destroy when the trigger is activated.
    - Set the `effect` (optional) to a prefab for visual feedback.
    - Ensure the colliding object (e.g., the Player) has the tag "Player".

3. **Jumpscare Script**:
    - Attach `Jumpscare` to a GameObject with a trigger collider.
    - Assign `jumpscareObject` to the GameObject that will appear during the jumpscare.
    - Ensure the `jumpscareObject` is initially inactive.

4. **Setup Trigger Colliders**:
    - For `DestroyOnTrigger` and `Jumpscare`, add a Collider component to the GameObject.
    - Check "Is Trigger" in the Collider settings.

5. **Testing**:
    - Run the scene and interact with the triggers or wait for the specified time to see the effects.
    - Verify that the `Player` GameObject has the tag "Player" in the Inspector.

6. **Optional Enhancements**:
    - Add sound effects or animations to enhance the effects.
    - Use Unity's particle system for more realistic visual feedback.

By following this tutorial, you can quickly integrate these scripts into your Unity project for timed destruction, trigger-based destruction, or jumpscare effects.
*/
