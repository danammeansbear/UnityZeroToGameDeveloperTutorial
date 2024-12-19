// Updated LoadLevelAfterWait.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadLevelAfterWait : MonoBehaviour
{
    [Tooltip("The name of the scene to load after the wait period.")]
    public string sceneToLoad;

    [Tooltip("The wait time in seconds before the scene is loaded.")]
    public float waitTime = 210f; // Default: 3.5 minutes

    void Start()
    {
        // Start the coroutine that handles scene loading after a delay.
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        // Log the start of the coroutine for debugging purposes.
        Debug.Log($"Started waiting at timestamp: {Time.time}");

        // Wait for the specified duration.
        yield return new WaitForSeconds(waitTime);

        // Log the scene loading action and load the specified scene.
        Debug.Log($"Loading scene: {sceneToLoad} at timestamp: {Time.time}");
        SceneManager.LoadScene(sceneToLoad);
    }
}

/*
**How to Use LoadLevelAfterWait.cs**
1. Attach this script to any GameObject in your scene.
2. Set the `sceneToLoad` property to the name of the scene you want to load after the wait.
   - Make sure the scene is added to the Build Settings.
3. Set the `waitTime` property to the desired wait duration in seconds.
4. Run the game, and the script will load the specified scene after the defined wait time.
*/

// Updated OnTriggerLoadLevel.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTriggerLoadLevel : MonoBehaviour
{
    [Tooltip("The UI text that prompts the player to interact.")]
    public GameObject enterText;

    [Tooltip("The name of the scene to load when triggered.")]
    public string sceneToLoad;

    void Start()
    {
        // Ensure the interaction text is hidden when the game starts.
        if (enterText != null)
        {
            enterText.SetActive(false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Check if the object colliding with the trigger is the player.
        if (other.CompareTag("Player"))
        {
            if (enterText != null)
            {
                enterText.SetActive(true);
            }

            // Check for player input to load the scene.
            if (Input.GetButtonDown("Use"))
            {
                Debug.Log($"Loading scene: {sceneToLoad}");
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Hide the interaction text when the player leaves the trigger area.
        if (other.CompareTag("Player") && enterText != null)
        {
            enterText.SetActive(false);
        }
    }
}

/*
**How to Use LoadLevelAfterWait.cs**
1. Attach this script to any GameObject in your scene.
2. Set the `sceneToLoad` property to the name of the scene you want to load after the wait.
   - Make sure the scene is added to the Build Settings.
3. Set the `waitTime` property to the desired wait duration in seconds.
4. Run the game, and the script will load the specified scene after the defined wait time.

**How to Use OnTriggerLoadLevel.cs**
1. Attach this script to a GameObject with a Collider set to "Is Trigger".
2. Assign the `enterText` property to a UI Text GameObject that displays interaction prompts.
   - Ensure this text is hidden by default.
3. Set the `sceneToLoad` property to the name of the scene you want to load.
   - Make sure the scene is added to the Build Settings.
4. Tag the player GameObject with "Player" and ensure it has a Collider.
5. Add an input action named "Use" in Unity's Input Manager (Edit > Project Settings > Input Manager).
   - Default: Map it to a key, e.g., "E".
6. When the player enters the trigger area, the interaction prompt will appear.
   - Press the "Use" key to load the specified scene.
*/
