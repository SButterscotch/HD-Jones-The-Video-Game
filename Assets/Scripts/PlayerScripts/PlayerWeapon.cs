/*
* Filename: PlayerWeapon.cs
* Developer: K Atkinson
* Purpose: Script used to instantiate and manipulate the player's weapon, aiming with mouse and shooting with mouse click. 
* Private Class Data Pattern used - see comments 
* Singleton Pattern used - see comments   
*/
using System.Collections;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerWeapon : MonoBehaviour
{
    //Used for the singleton pattern instance 
    public static PlayerWeapon Instance { get; private set; }
    [SerializeField] private GameObject bullet; 
    [SerializeField] private Transform bulletDirection;
    [SerializeField] private float cooldownTime = 0.5f;

    private PrivateData privateData; //Used for PCD pattern 

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
    }

    private void Update()
    {
        privateData.HandleAiming();
        if (Input.GetMouseButtonDown(0))
        {
            privateData.PlayerShoot(bullet, bulletDirection, cooldownTime);
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
    private class PrivateData
    {
        private Transform aimTransform;
        private Camera main;
        private bool canShoot = true;

        public PrivateData(Transform parentTransform)
        {
            aimTransform = parentTransform.Find("Aim");
            main = Camera.main;
        }

        public void HandleAiming()
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            aimTransform.eulerAngles = new Vector3(0, 0, angle);

            Vector3 aimLocalScale = Vector3.one; 
            if (angle > 90 || angle < -90) { 
                aimLocalScale.y = -1f; 
            } else { 
                aimLocalScale.y = +1f; 
            }
            aimTransform.localScale = aimLocalScale;
        }

        public void PlayerShoot(GameObject bulletPrefab, Transform bulletDirection, float cooldownTime)
        {
            if (!canShoot)
            {
                return;
            }

            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;

            GameObject g = Instantiate(bulletPrefab, bulletDirection.position, Quaternion.Euler(0, 0, angle));
            g.SetActive(true);

            // Start cooldown coroutine
            MonoBehaviour coroutineOwner = bulletPrefab.GetComponent<MonoBehaviour>();
            coroutineOwner.StartCoroutine(CanShootCoroutine(cooldownTime));
        }

        private IEnumerator CanShootCoroutine(float cooldownTime)
        {
            canShoot = false;
            yield return new WaitForSeconds(cooldownTime);
            canShoot = true;
        }
    }
}

//   using System.Collections;
// using UnityEngine;
// using CodeMonkey.Utils;
// using UnityEngine.Diagnostics;

// public class PlayerWeapon : MonoBehaviour
// {
//     [SerializeField] private GameObject bullet; 
//     [SerializeField] 
//     private Transform BulletDirection;
//     private Transform aimTransform;
//     private Camera main;
//     private bool canShoot = true;
//     [SerializeField] private float coolDownTime = 0.5f;

//     private void Awake()
//     {
//         aimTransform = transform.Find("Aim");
//         main = Camera.main;
//     }

//     private void Update()
//     {
//         HandleAiming();
//         if (Input.GetMouseButtonDown(0))
//         {
//             PlayerShoot();
//         }
//     }

//     private void HandleAiming()
//     {
//         Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
//         Vector3 aimDirection = (mousePosition - transform.position).normalized;
//         float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
//         //float adjustedAngle = angle - 90f; 
//         aimTransform.eulerAngles = new Vector3(0, 0, angle);

//                 //To keep weapon from going upside down when facing left 
//         Vector3 aimLocalScale = Vector3.one; 
//         if (angle > 90 || angle < -90) { 
//             aimLocalScale.y = -1f; 
//         } else { 
//             aimLocalScale.y = +1f; 
//         }
//         aimTransform.localScale = aimLocalScale; 
//     }

//     private void PlayerShoot()
//     {
//         if (!canShoot)
//         {
//             return;
//         } 
//         Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
//         Vector3 aimDirection = (mousePosition - transform.position).normalized;
//         float angle = (Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg) - 90f;
//         GameObject g = Instantiate(bullet, BulletDirection.position, Quaternion.Euler(0, 0, angle)); //aimTransform second, BulletDirection.positon 
//         g.SetActive(true);
//         StartCoroutine(CanShoot());
//     }

//     private IEnumerator CanShoot()
//     {
//         canShoot = false;
//         yield return new WaitForSeconds(coolDownTime);
//         canShoot = true;
//     }
// }

