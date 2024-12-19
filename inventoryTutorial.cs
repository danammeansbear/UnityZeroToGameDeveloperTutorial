//inventory script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject invTab;

    void Start()
    {
        if (invTab != null)
        {
            invTab.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && invTab != null)
        {
            invTab.SetActive(!invTab.activeSelf);
        }
    }
}


//use inventory script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class UseInventory : MonoBehaviour
{
    public bool stats;
    public bool item;

    public int thirstValue;
    public int hungerValue;

    private ThirdPersonCharacter playerCharacter;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerCharacter = player.GetComponent<ThirdPersonCharacter>();
        }
    }

    public void UseItem()
    {
        if (playerCharacter == null) return;

        if (stats)
        {
            playerCharacter.thirst += thirstValue;
            playerCharacter.hunger += hungerValue;
            Destroy(gameObject);
        }

        if (item)
        {
            // Add specific item usage logic here
        }
    }
}



//add this or modify the Thirdperson Character script
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class ThirdPersonCharacter : MonoBehaviour
    {
        // All existing code remains unchanged.

        // Health, Hunger, and Thirst caps to prevent exceeding limits
        private const float MaxValue = 100f;

        void Update()
        {
            healthBar.value = Mathf.Clamp(health, 0, MaxValue);
            Hbar.value = Mathf.Clamp(hunger, 0, MaxValue);
            Tbar.value = Mathf.Clamp(thirst, 0, MaxValue);

            hunger -= hungerRate * Time.deltaTime;
            thirst -= thirstRate * Time.deltaTime;

            if (health <= 0)
            {
                health = 0;
                m_Animator.SetTrigger("dead");
                GetComponent<ThirdPersonUserControl>().enabled = false;
            }
            if (hunger <= 0 || thirst <= 0)
            {
                health -= deathRate * Time.deltaTime;
            }
            if (hunger <= 0) hunger = 0;
            if (thirst <= 0) thirst = 0;
        }
    }
}
Tutorial: How to Use the Scripts
1. Setting Up the Scene
Create a Player GameObject:

Add a Rigidbody, CapsuleCollider, and an Animator component.
Attach the ThirdPersonCharacter script to the GameObject.
Tag the GameObject as "Player".
Create an Inventory GameObject:

Add the Inventory script.
Create a UI panel for the inventory tab and assign it to the invTab property of the Inventory script.
Set Up UI:

Create sliders for health, hunger, and thirst, and link them to the respective properties in the ThirdPersonCharacter script.
2. Configuring Items
Create a prefab for inventory items (e.g., food or drink).
Add the UseInventory script to the prefab.
Configure the stats, item, thirstValue, and hungerValue properties based on the item's effect.
3. Player Interaction with Items
Place item prefabs in the scene or instantiate them dynamically during gameplay.
When the player interacts with an item, call the UseItem method of the UseInventory script.
4. Testing
Run the scene and press the Tab key to toggle the inventory.
Check the sliders for health, hunger, and thirst values as they decrease over time.
Interact with items to restore hunger or thirst and observe changes in the UI.
