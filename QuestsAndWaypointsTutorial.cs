// Quest.cs
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private bool showObjective = false;
    [SerializeField]
    private Texture objective;
    [SerializeField]
    private int collision;

    void Start()
    {
        showObjective = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !showObjective && collision == 0)
        {
            showObjective = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            showObjective = false;
            collision = 1;
        }
    }

    void OnGUI()
    {
        if (showObjective)
        {
            GUI.DrawTexture(new Rect(Screen.width / 1.5f, Screen.height / 1.4f, 178, 178), objective);
        }
    }

    void Update()
    {
        if (Input.GetButton("ShowObj") && collision == 1)
        {
            showObjective = true;
        }
        if (Input.GetButtonUp("ShowObj") && collision == 1)
        {
            showObjective = false;
        }
    }
}

// QuestMarker.cs
using UnityEngine;

public class QuestMarker : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = Camera.main.gameObject;
    }

    void Update()
    {
        Vector3 playerPoint = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position;
        Quaternion objRot = Quaternion.LookRotation(-playerPoint, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, objRot, Time.deltaTime * 5);
    }
}

// ShowQuest.cs
using UnityEngine;

public class ShowQuest : MonoBehaviour
{
    public GameObject questUI;
    private int collision = 0;
    public AudioClip sound;
    private AudioSource audio;
    private bool alreadyPlayed = false;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        questUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !questUI.activeSelf && collision == 0 && !alreadyPlayed)
        {
            questUI.SetActive(true);
            audio.PlayOneShot(sound, 5);
            alreadyPlayed = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            questUI.SetActive(false);
            collision = 1;
        }
    }

    void Update()
    {
        if (Input.GetButton("Q") && collision == 1)
        {
            questUI.SetActive(true);
            if (Input.GetButtonDown("Q"))
            {
                audio.PlayOneShot(sound, 5);
            }
        }
        if (Input.GetButtonUp("Q") && collision == 1)
        {
            questUI.SetActive(false);
        }
    }
}

// ShowQuest2.cs
using UnityEngine;

public class ShowQuest2 : MonoBehaviour
{
    public GameObject questUI;
    public GameObject showQuest;

    private bool entered = false;
    private bool alreadyPlayed = false;
    public AudioClip sound;
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        showQuest.SetActive(false);
        questUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !alreadyPlayed && !entered)
        {
            questUI.SetActive(true);
            showQuest.SetActive(true);
            audio.PlayOneShot(sound, 5);
            alreadyPlayed = true;
            entered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(showQuest);
        }
    }
}

// Waypoints.cs
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject player;
    private int current = 0;
    public float speed;
    private float WPradius = 1;

    void Update()
    {
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            current = Random.Range(0, waypoints.Length);
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider n)
    {
        if (n.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider n)
    {
        if (n.gameObject == player)
        {
            player.transform.parent = null;
        }
    }
}

/*
### Detailed Tutorial: Step-by-Step Instructions for Beginners ###

#### Step 1: Setting Up the Unity Scene
1. **Create a New Unity Project**:
   - Open Unity Hub and create a new 3D project.
   - Name your project and click "Create".

2. **Set Up Your Scene**:
   - Add a plane as the ground (`GameObject > 3D Object > Plane`).
   - Create your player character (`GameObject > 3D Object > Capsule`).
     - Rename the Capsule to "Player".
     - Add a Rigidbody component (`Inspector > Add Component > Rigidbody`).
     - Tag this object as "Player".

3. **Add Quest Objects**:
   - Create a cube (`GameObject > 3D Object > Cube`).
   - Attach the `Quest.cs` script to the cube.
   - Drag a texture for the quest objective into the `objective` field in the Inspector.

4. **Set Up Quest Markers**:
   - Create another GameObject (e.g., a sphere) to act as the quest marker.
   - Attach the `QuestMarker.cs` script to the sphere.
   - Place it near your quest object.

5. **Create UI for Quests**:
   - In the Hierarchy, right-click and select `UI > Canvas`.
   - Add a `Text` element (`Right-click on Canvas > UI > Text`) for displaying quest information.
   - Attach the `ShowQuest.cs` or `ShowQuest2.cs` script to the quest object.
   - Assign the text GameObject to the `questUI` field in the Inspector.

6. **Set Up Waypoints**:
   - Create multiple empty GameObjects to serve as waypoints (`GameObject > Create Empty`).
   - Position them in the scene where you want the object to move.
   - Group them under an empty parent object (optional, for organization).
   - Attach the `Waypoints.cs` script to a moving object.
   - Assign the waypoints and the player in the Inspector.

#### Step 2: Configuring Input Settings
1. Open `Edit > Project Settings > Input Manager`.
2. Add new input axes:
   - `ShowObj`: Assign a key (e.g., "O").
   - `Q`: Assign a key (e.g., "Q").

#### Step 3: Testing the Scene
1. Click the "Play" button to start the game.
2. Move your player using the default controls.
3. Interact with quest objects and observe the following:
   - The quest objective appears when the player enters the trigger.
   - The quest UI updates when prompted.
   - Markers rotate to face the player.
   - Objects move between waypoints.

#### Step 4: Adjusting Parameters
- Use the Inspector to modify speed, UI positions, quest text, and other settings.
- Test repeatedly to ensure smooth interactions.

#### Additional Tips
- Use `Gizmos` to visualize triggers and waypoints in the Scene view.
- Save your scene frequently to avoid losing progress.
- Customize UI colors and fonts to match your game's theme.
*/
