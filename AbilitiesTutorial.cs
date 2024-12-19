// Updated DishonoredSpell.cs
using UnityEngine;

public class DishonoredSpell : MonoBehaviour
{
    private RaycastHit lastRaycastHit;
    public AudioClip audioClip;
    public float range = 1000f;

    private GameObject GetLookedAtObject()
    {
        Vector3 origin = transform.position;
        Vector3 direction = Camera.main.transform.forward;
        if (Physics.Raycast(origin, direction, out lastRaycastHit, range))
        {
            return lastRaycastHit.collider.gameObject;
        }
        return null;
    }

    private void TeleportToLookAt()
    {
        transform.position = lastRaycastHit.point + lastRaycastHit.normal * 1.5f;
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && GetLookedAtObject() != null)
        {
            TeleportToLookAt();
        }
    }

    /*
    Implementation Tutorial:
    1. Attach this script to the Player GameObject.
    2. Assign an AudioClip to the "audioClip" field in the Inspector (optional).
    3. Adjust the "range" field to set the spell's range.
    4. The teleportation mechanism works by using raycasting to determine what the player is looking at.
        - The "GetLookedAtObject" method casts a ray forward from the camera's viewpoint to identify a hit point.
        - The "TeleportToLookAt" method moves the player to the hit point plus an offset based on the normal of the hit surface.
    5. Press the "Q" key during gameplay to teleport to the looked-at location.
    */
}

// Updated Teleport.cs
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject ui;
    public GameObject objToTeleport;
    public Transform teleportLocation;

    private void Start()
    {
        if (ui != null) ui.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (ui != null) ui.SetActive(true);
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            objToTeleport.transform.position = teleportLocation.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ui != null) ui.SetActive(false);
    }

    /*
    Implementation Tutorial:
    1. Attach this script to a trigger collider in your scene.
    2. Assign a UI GameObject to the "ui" field for interaction prompts (optional).
    3. Assign the GameObject to teleport to the "objToTeleport" field.
    4. Assign the teleport destination Transform to the "teleportLocation" field.
    5. Ensure the Player GameObject has the "Player" tag.
    */
}

// Updated UnlockAbility.cs
using UnityEngine;

public class UnlockAbility : MonoBehaviour
{
    public bool enableDishonoredSpell;
    public bool enableSlowDownTime;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (enableDishonoredSpell)
            {
                col.GetComponent<DishonoredSpell>().enabled = true;
                Destroy(gameObject);
            }
            if (enableSlowDownTime)
            {
                col.GetComponent<SlowDownTime>().enabled = true;
                Destroy(gameObject);
            }
        }
    }

    /*
    Implementation Tutorial:
    1. Attach this script to an unlockable ability item in your scene.
    2. Check the "enableDishonoredSpell" or "enableSlowDownTime" fields to specify which ability to unlock.
    3. Ensure the Player GameObject has the corresponding ability script attached but disabled by default.
    */
}

// New UseAbility.cs
using UnityEngine;

public class UseAbility : MonoBehaviour
{
    public DishonoredSpell dishonoredSpell;
    public SlowDownTime slowDownTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && dishonoredSpell != null && dishonoredSpell.enabled)
        {
            dishonoredSpell.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && slowDownTime != null && slowDownTime.enabled)
        {
            slowDownTime.enabled = true;
        }
    }

    /*
    Implementation Tutorial:
    1. Attach this script to the Player GameObject.
    2. Drag and drop the DishonoredSpell and SlowDownTime components onto their respective fields in the Inspector.
    3. Ensure that abilities are unlocked through the UnlockAbility script.
    4. Use the "Q" and "E" keys to activate the respective abilities during gameplay.
    */
}

// Updated SlowDownTime.cs
using UnityEngine;

public class SlowDownTime : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Time.timeScale = 0.65f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    /*
    Implementation Tutorial:
    1. Attach this script to the Player GameObject.
    2. This script slows down time when the "Q" key is held and resets it when released.
    3. Adjust the "Time.timeScale" value inside the script to modify the slowdown effect.
    4. Ensure this ability is enabled through the UnlockAbility script during gameplay.
    */
}
