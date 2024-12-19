// ObjectsToCollect.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsToCollect : MonoBehaviour
{
    public static int objects = 0;

    void Awake()
    {
        objects++;
    }

    void OnTriggerEnter(Collider plyr)
    {
        if (plyr.CompareTag("Player")) // Updated for better tag comparison
        {
            objects--;
            gameObject.SetActive(false);
        }
    }
}

// PickUp.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Animator anim;
    private Inventory invScript;

    public bool money;
    public int moneyAmount;
    private Currency moneyScript;

    public bool item;
    public GameObject itemIcon;

    private bool pickedUp = false;

    void Start()
    {
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            anim = player.GetComponent<Animator>();
        }

        var gameController = GameObject.FindWithTag("GameController");
        if (gameController != null)
        {
            moneyScript = gameController.GetComponent<Currency>();
            invScript = gameController.GetComponent<Inventory>();
        }
    }

    void OnTriggerStay(Collider player)
    {
        if (player.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !pickedUp)
        {
            pickedUp = true;
            StartCoroutine(PlayAnim());
        }
    }

    private IEnumerator PlayAnim()
    {
        if (anim != null)
        {
            anim.SetTrigger("pickup");
        }

        yield return new WaitForSeconds(1);

        if (money && moneyScript != null)
        {
            moneyScript.gold += moneyAmount;
            Destroy(gameObject);
        }
        else if (item && invScript != null)
        {
            GameObject i = Instantiate(itemIcon);
            i.transform.SetParent(invScript.invTab.transform, false);
            Destroy(gameObject);
        }
    }
}

// ThrowObject.cs
using UnityEngine;
using System.Collections;

public class ThrowObject : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public float throwForce = 10f;
    private bool hasPlayer = false;
    private bool beingCarried = false;
    public AudioClip[] soundToPlay;
    private AudioSource audioSource;
    public int dmg;
    private bool touched = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        hasPlayer = dist <= 2.5f;

        if (hasPlayer && Input.GetButtonDown("Use"))
        {
            PickupObject();
        }

        if (beingCarried)
        {
            if (touched)
            {
                DropObject();
                touched = false;
            }

            if (Input.GetMouseButtonDown(0)) // Throw
            {
                ThrowObjectAction();
            }
            else if (Input.GetMouseButtonDown(1)) // Drop
            {
                DropObject();
            }
        }
    }

    private void PickupObject()
    {
        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        transform.SetParent(playerCam);
        beingCarried = true;
    }

    private void DropObject()
    {
        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
        transform.SetParent(null);
        beingCarried = false;
    }

    private void ThrowObjectAction()
    {
        DropObject();
        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(playerCam.forward * throwForce, ForceMode.VelocityChange);
        }
        PlayRandomAudio();
    }

    private void PlayRandomAudio()
    {
        if (audioSource.isPlaying) return;

        audioSource.clip = soundToPlay[Random.Range(0, soundToPlay.Length)];
        audioSource.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (beingCarried)
        {
            touched = true;
        }
    }
}

/*
### Tutorial: How to Implement and Use These Scripts in Unity ###

#### Step 1: Set Up Your Unity Project
1. Open Unity and create a new 3D project.
2. Import your assets (e.g., 3D models for objects, player, and UI icons).

#### Step 2: Add Scripts to GameObjects
- **ObjectsToCollect.cs**: Attach this script to any collectible object in your scene.
- **PickUp.cs**: Attach this script to objects the player can pick up, such as coins or items.
- **ThrowObject.cs**: Attach this script to objects the player can throw.
- **Inventory.cs**: Attach this script to a GameObject managing the inventory system.
- **UseInventory.cs**: Attach this script to consumable or usable item prefabs.

#### Step 3: Configure GameObjects
1. **Objects to Collect**:
   - Add a Collider (e.g., Sphere Collider) and set it as a Trigger.
   - Tag the object with a suitable tag (e.g., "Collectible").

2. **Player Setup**:
   - Ensure your player GameObject has the "Player" tag.
   - Add an Animator component and set up animations for picking up objects.
   - Attach the `ThirdPersonCharacter` script to your player GameObject and configure health, hunger, and thirst.

3. **GameController**:
   - Create an empty GameObject and tag it as "GameController".
   - Add scripts for managing inventory (`Inventory.cs`) and consumables (`UseInventory.cs`).
   - Set up the `invTab` property in the `Inventory.cs` script to reference your UI inventory panel.

4. **Pickable Objects**:
   - Attach the `PickUp` script to objects.
   - Configure the `money`, `moneyAmount`, `item`, and `itemIcon` properties in the Inspector.

5. **Throwable Objects**:
   - Attach the `ThrowObject` script to the objects you want throwable.
   - Assign the `player` and `playerCam` Transforms to the player and camera objects, respectively.
   - Add an AudioSource component and populate the `soundToPlay` array.

6. **UI Setup**:
   - Create sliders for health, hunger, and thirst, and link them to the respective properties in the `ThirdPersonCharacter` script.
   - Add buttons or interactive elements to use items and call `UseItem` in the `UseInventory` script.

#### Step 4: Input Configuration
- Ensure that input mappings for "Use", "Tab", and mouse buttons are set in Unity's Input Manager.
  - "Use": Map it to a key like "E".
  - "Tab": Map it for toggling inventory.

#### Step 5: Test Your Game
1. Play your scene.
2. Move the player near objects and test collecting, picking up, and throwing objects.
3. Toggle the inventory using the `Tab` key and interact with items.
4. Observe sliders for health, hunger, and thirst values, and verify their changes during gameplay.

#### Notes:
- Adjust the `throwForce` value in the `ThrowObject` script to control the throwing distance.
- Customize animations and UI elements to fit your game's theme.
- Ensure the player's stats (health, hunger, thirst) are balanced with the game mechanics.
*/
