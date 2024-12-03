using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Transform bulletSpawn;
    public Rigidbody bullet;
    public float bulletSpeed;
    
    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bulletRigidbody;
            bulletRigidbody = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation) as Rigidbody;
            bulletRigidbody.AddForce(bulletSpawn.forward * bulletSpeed);
        }     
    }

    /*
    ==================== TUTORIAL FOR DEVELOPERS ====================
    This script is used to simulate a basic gun mechanism in Unity.
    It includes the following key features:

    1. Bullet Spawning:
       - The `bulletSpawn` is a `Transform` that determines where the bullet will be instantiated.
       - The `bullet` is a `Rigidbody` that represents the bullet prefab to be fired.
       - The `bulletSpeed` determines how fast the bullet will travel after being fired.

    2. Shooting Mechanism:
       - The `Update` function checks for input using `Input.GetButtonDown("Fire1")` to detect when the player presses the fire button (default mapped to the left mouse button).
       - When the fire button is pressed, a bullet is instantiated at the position and rotation of `bulletSpawn`.
       - The bullet's Rigidbody is used to apply a force (`AddForce`) in the forward direction, which propels the bullet forward.

    3. How to Use the Script:
       - Attach this script to a GameObject that will act as the gun (e.g., a player's weapon).
       - Assign the `bulletSpawn` Transform in the Unity Editor to the position where the bullets should be spawned.
       - Create a bullet prefab with a Rigidbody component and assign it to the `bullet` variable in the Unity Editor.
       - Set an appropriate value for `bulletSpeed` to control how fast the bullet travels.

    4. Notes:
       - Make sure the bullet prefab has a Rigidbody component attached for physics interactions.
       - You can adjust the `bulletSpeed` to make the bullets faster or slower, depending on your game mechanics.
       - Consider adding additional features such as sound effects or particle effects to make the shooting more immersive.
       - You could also add a limit to the number of bullets that can be fired or add a reload mechanism for more realistic gameplay.
    =================================================================
    */
}
