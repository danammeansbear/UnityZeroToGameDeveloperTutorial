using UnityEngine;

public class ShowWeapon : MonoBehaviour
{
    [SerializeField] private GameObject item1; // Weapon or item 1
    [SerializeField] private GameObject item2; // Weapon or item 2
    private bool showItem1 = false; // Tracks visibility of item1
    private bool showItem2 = false; // Tracks visibility of item2

    // Start is called before the first frame update
    private void Start()
    {
        UpdateVisibility(); // Ensure both items are hidden initially
    }

    // Update is called once per frame
    private void Update()
    {
        // Show item 1 and hide item 2 when pressing the "1" key
        if (Input.GetKeyDown(KeyCode.Alpha1) && !showItem1)
        {
            showItem1 = true;
            showItem2 = false;
            UpdateVisibility();
        }

        // Show item 2 and hide item 1 when pressing the "2" key
        if (Input.GetKeyDown(KeyCode.Alpha2) && !showItem2)
        {
            showItem2 = true;
            showItem1 = false;
            UpdateVisibility();
        }

        // Hide both items when pressing the "R" key
        if (Input.GetKeyDown(KeyCode.R))
        {
            showItem1 = false;
            showItem2 = false;
            UpdateVisibility();
        }
    }

    // Updates the visibility of item1 and item2 based on the flags
    private void UpdateVisibility()
    {
        if (item1 != null) item1.SetActive(showItem1);
        if (item2 != null) item2.SetActive(showItem2);
    }

    /*
    ==================== TUTORIAL FOR IMPLEMENTATION ====================
    This guide explains how to implement the ShowWeapon script and integrate it with 
    the ThirdPersonCharacter script for a smooth weapon-switching experience.

    1. **Purpose:**
       - Toggles the visibility of two weapons or items based on player input.

    2. **Setup:**
       - Attach this script to the character GameObject that already has the ThirdPersonCharacter script.
       - Assign `item1` and `item2` to the weapon or item GameObjects in the Unity Inspector.

    3. **Default Controls:**
       - Press `1` to show item 1 and hide item 2.
       - Press `2` to show item 2 and hide item 1.
       - Press `R` to hide both items.

    4. **Integration with ThirdPersonCharacter:**
       - Ensure that `item1` and `item2` are parented to appropriate bones of the character model (e.g., hand bones).
       - This will ensure the weapons follow the character's movement and animations.
       - If you want the `ThirdPersonCharacter` script to react to the equipped weapon, you can:
         - Add references to `item1` and `item2` in the ThirdPersonCharacter script.
         - Modify the script to check which weapon is currently active.

    5. **Testing:**
       - Assign two weapon models to `item1` and `item2` in the Inspector.
       - Ensure both weapons are properly positioned and aligned with the character's hands.
       - Play the scene and switch weapons using the assigned keys (1, 2, and R).

    6. **Customization:**
       - Modify the keys for weapon switching in the `Input.GetKeyDown` checks.
       - Add additional weapons by expanding the script to include more items.
       - Integrate animations for equipping and unequipping weapons.

    7. **Advanced Enhancements:**
       - Implement weapon-specific actions in conjunction with the ThirdPersonCharacter script (e.g., shooting, melee attacks).
       - Add UI elements to visually indicate the currently equipped weapon.
       - Include sounds or visual effects when switching weapons.

    ====================================================================
    */
}
