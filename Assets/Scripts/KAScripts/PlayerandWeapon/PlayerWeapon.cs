/*
* Filename: PlayerWeapon.cs
* 
* Developer: K Atkinson
* 
* Purpose: Script used to aim and shoot different bullet prefabs using mouse 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.SceneManagement; 

/* 
* Summary: Class to aim a weapon at the mouse and fire a weapon upon mouse click        
* 
* Member variables: 
* Instance, normalBulletPrefab, relishBulletPrefab, bulletDirection, cooldownTime, privateData, currentBulletPrefab
* 
*/ 
public class PlayerWeapon : MonoBehaviour
{
    //Used for the singleton pattern instance 
    public static PlayerWeapon Instance { get; private set; }
    [SerializeField] private GameObject normalBulletPrefab;
    [SerializeField] private GameObject relishBulletPrefab;

    [SerializeField] private GameObject finalbossBulletPrefab;
    [SerializeField] private Transform bulletDirection;
    [SerializeField] private float cooldownTime = 0.5f;

    //Used for PCD pattern 
    private PrivateData privateData; 
    private GameObject currentBulletPrefab;

    private void Awake()
    {
        //Singleton pattern implementation 
        /* This is how the PCD pattern works in this private class:
        * The Singleton pattern is implemented through the static property Instance. 
        * Upon creation, the Awake() method checks whether an instance of PlayerWeapon already exists. 
        * If so, any additional instances are destroyed. 
        * If not, the current instance is assigned to the Instance property. 
        * This ensures that only one instance of PlayerWeapon exists throughout the lifetime of the game. 
        * The Instance property can then be accessed statically from other classes, a convenient and consistent way to interact with PlayerWeapon.
        */ 
        if (Instance != null && Instance != this) { 
            Destroy(gameObject); 
            return; 
        }
        Instance = this; 
        privateData = new PrivateData(transform);
        currentBulletPrefab = normalBulletPrefab; // Start with normal bullet by default
    }

    private void Update()
    {
        privateData.HandleAiming();
        //Hit mouse button to shoot bullets 
        if (Input.GetMouseButtonDown(0))
        {
            privateData.PlayerShoot(currentBulletPrefab, bulletDirection, cooldownTime);
        }

        // Toggle between bullet types (for example, press 'B' key)
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleBulletType();
        }
        if (SceneManager.GetActiveScene().name == "Level4")
        {
            currentBulletPrefab = finalbossBulletPrefab;
        }
    }

    /* 
    * Summary: Method to toggle between normal and relish bullet types
    * 
    * Parameters: None    
    * 
    * Returns: None, toggles the currentBulletPrefab from normal to relish  
    */
    private void ToggleBulletType()
    {
        if (currentBulletPrefab == normalBulletPrefab)
        {
            currentBulletPrefab = relishBulletPrefab;
        }
        else
        {
            currentBulletPrefab = normalBulletPrefab;
        }
    }

    /* This is how the PCD pattern works in this private class: 
    * This pattern is used to enforce information hiding and improve encapsulation by restricting access to the inner workings of a class. 
    * This encapsulates aiming behavior and shooting logic of the PlayerWeapon class within a private nested class called PrivateData. 
    * The nested class contains private member variables and methods: aimTransform, canShoot, main, HandleAiming and PlayerShoot. 
    * The outer class (PlayerWeapon) exposes a clean interface to interact with the weapon, 
    * abstracting away the implementation details of aiming and shooting. 
    * This improves the modularity and maintainability of the codebase bc we can easily change and read the codebase. 
    */ 

    /* 
    * Summary: Class to handle private class data 
    * 
    * Member variables:     
    * aimTransform, main, canShoot, 
    * 
    */
    private class PrivateData
    {
        private Transform aimTransform;
        private Camera main;
        private bool canShoot = true;

        /* 
        * Summary: Method to find the game object called "Aim" and assign aimTransform to it 
        * 
        * Parameters: None    
        * 
        * Returns: None   
        */
        public PrivateData(Transform parentTransform)
        {
            aimTransform = parentTransform.Find("Aim");
            main = Camera.main;
        }

        /* 
        * Summary: This function uses the mouse position for aiming the weapon 
        * 
        * Parameters: None    
        * 
        * Returns: None, adjusts aimTransform so that the gun orients correctly   
        */
        public void HandleAiming()
        {
            // This uses the mouse's position to aim the weapon 
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            aimTransform.eulerAngles = new Vector3(0, 0, angle);

            // This block of code turns the weapon on the y-axis so that it does not flip upside down 
            Vector3 aimLocalScale = Vector3.one; 
            if (angle > 90 || angle < -90) { 
                aimLocalScale.y = -1f; 
            } else { 
                aimLocalScale.y = +1f; 
            }
            aimTransform.localScale = aimLocalScale;
        }
        
       /* 
        * Summary: This function shoots the bullet upon mouse click  
        * 
        * Parameters: 
        * bulletPrefab, bulletDirection, cooldownTime    
        * 
        * Returns: None, used as break/check   
        */
        public void PlayerShoot(GameObject bulletPrefab, Transform bulletDirection, float cooldownTime)
        {
            if (!canShoot)
            {
                return;
            }

            // Added this code because the axis needed to be flipped by -90 to shoot straight  
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;

            // Create and fire bullet prefab 
            GameObject g = Instantiate(bulletPrefab, bulletDirection.position, Quaternion.Euler(0, 0, angle));
            g.SetActive(true);

            // Start cooldown coroutine
            MonoBehaviour coroutineOwner = bulletPrefab.GetComponent<MonoBehaviour>();
            coroutineOwner.StartCoroutine(CanShootCoroutine(cooldownTime));
        }

        /* 
        * Summary: IEnum function used to "cool down" the weapon  
        * 
        * Parameters: cooldownTime    
        * 
        * Returns:  IEnumerator   
        */
        private IEnumerator CanShootCoroutine(float cooldownTime)
        {
            canShoot = false;
            yield return new WaitForSeconds(cooldownTime);
            canShoot = true;
        }
    }
}
