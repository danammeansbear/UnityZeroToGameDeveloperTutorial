// Updated ShowGUI Script
using UnityEngine;
using System.Collections;

public class ShowGUI : MonoBehaviour
{
    private bool showGUI = false;
    [SerializeField] private Texture pressE;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            showGUI = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            showGUI = false;
        }
    }

    private void OnGUI()
    {
        if (showGUI && pressE != null)
        {
            GUI.DrawTexture(new Rect(Screen.width * 0.75f, Screen.height * 0.7f, 178, 178), pressE);
        }
    }
}

// Updated ShowUI Script
using System.Collections;
using UnityEngine;

public class ShowUI : MonoBehaviour
{
    [SerializeField] private GameObject uiObject;
    [SerializeField] private float displayTime = 5f;

    private void Start()
    {
        if (uiObject != null)
        {
            uiObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && uiObject != null)
        {
            uiObject.SetActive(true);
            StartCoroutine(HideAfterDelay());
        }
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        if (uiObject != null)
        {
            Destroy(uiObject);
        }
        Destroy(gameObject);
    }
}

/* ==================== TUTORIAL ====================
**How to Implement and Use ShowGUI and ShowUI Scripts:**

1. **ShowGUI Script Usage:**
   - Attach the `ShowGUI` script to a GameObject that represents a trigger area (e.g., an invisible cube with a Collider set to "Is Trigger").
   - Assign a texture (e.g., an image of the "Press E" prompt) to the `Press E` field in the Unity Editor.
   - When the player enters the trigger area, the texture will display on the screen. It will disappear when the player exits the trigger area.

2. **ShowUI Script Usage:**
   - Attach the `ShowUI` script to a GameObject that represents a trigger area.
   - Assign a UI GameObject (e.g., a Canvas or a UI Panel) to the `UI Object` field in the Unity Editor.
   - The UI element will become visible when the player enters the trigger area and will disappear after the `Display Time` (default 5 seconds).

3. **Creating a UI Canvas:**
   - Go to the Unity Editor and navigate to `GameObject > UI > Canvas` to create a new Canvas.
   - The Canvas will serve as the root for all UI elements.
   - Add a UI element such as a `Panel` (right-click the Canvas in the hierarchy and select `UI > Panel`).
   - Customize the Panel (e.g., set its size, color, or add child elements like Text or Buttons).
   - Ensure the `Canvas` Render Mode is set to `Screen Space - Overlay` for front-end rendering.

4. **Assigning the Canvas to the ShowUI Script:**
   - Drag the Canvas or a specific UI element (e.g., the Panel) into the `UI Object` field of the `ShowUI` script in the Inspector.

5. **Common Steps for Both Scripts:**
   - Ensure the trigger area GameObject has a Collider component with "Is Trigger" enabled.
   - Tag the player GameObject with the tag "Player".

6. **Integration with ThirdPersonCharacter Script:**
   - Add the `ShowGUI` and/or `ShowUI` trigger areas to your scene.
   - Place them strategically where you want specific interactions (e.g., near objects or locations).

7. **Testing the Setup:**
   - Run the game and move the character near the trigger areas.
   - Observe the behavior of the UI elements or GUI texture prompts as expected.

8. **Enhancements:**
   - Add additional logic to the `ShowGUI` or `ShowUI` scripts, such as enabling interaction when the "E" key is pressed.
   - Customize the display time or appearance of the UI elements to suit your game's needs.

==================================================== */
