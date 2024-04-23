/* This codebase serves to create a customizable side to side automatic shooter player for a top-down 2D game.*/ 
/* 
* Filename: AutomaticWeapon.cs 
* Developer: Kay Atkinson
* Purpose: A subclass of FBWeapon.cs used to automatically fire bullet objects, implements the singleton design pattern
*/ 

using UnityEngine;

/* 
* Summary: Subclass of FBWeapon to fire bullet objects automatically and destroy after lifetime  
* 
* Member variables: 
* Instance, _canFire boolean, _fireSpeed, bulletPrefab, firePoint, _bulletVelocity 
*
*/ 
public class AutomaticWeapon : FBWeapon
{
    // Singleton instance
    public static AutomaticWeapon Instance { get; private set; }

    //Called when an instance is needed, called before Start() or Update() 
    /****STATIC BINDING*************************
    /* Not overridden in any subclass and the decision about which Awake() method 
    /* to use is made by the compiler at compile time based on the eclared type of the reference variable, 
    /* which is Automatic Weapon. */ 
    private void Awake()
    {
        // Singleton implementation
        /*****SINGLETON PATTERN******************************* 
        /* Ensures that only one instance for the weapon occurs and provides a global point of access to that instance.
        /* static property to Instance in line 20 prevents direct instatniation from outside the class. 
        /* Awake() checks whether 'Instance' is null and if it is, assigns the instance to it, if not, destroys it. 
        */ 
        //This logic acts as a constructor for the instance of Automatic Weapon 
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of AutomaticWeapon found. Destroying the extra instance.");
            Destroy(gameObject);
        }
    }

    // /* 
    // * Summary:  Constructor for singleton pattern, invoked when AutomaticWeapon instance is requested and ensures only 
    // * one instance exists, destroying other instances  
    // * 
    // * Parameters: None 
    // * 
    // * Returns: None, calls Invoke and Instantiate to perform actions 
    // */ 
    // private AutomaticWeapon()
    // {
    //     // // Ensure that only one instance of AutomaticWeapon exists
    //     // if (Instance == null)
    //     // {
    //     //     Instance = this;
    //     // }
    //     // else
    //     // {
    //     //     Debug.LogWarning("Multiple instances of AutomaticWeapon found. Destroying the extra instance.");
    //     //     Destroy(gameObject);
    //     // }
    // }

    /* 
    * Summary:  Override Fire method to destroy bullets after a lifetime  
    * 
    * Parameters: None 
    * 
    * Returns: None, calls Invoke and Instantiate to perform actions 
    */ 
    public override void Fire()
    {
        if (_canFire)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = _bulletVelocity * (-transform.right);
            }
            Destroy(bullet, _bulletLifespan); // Destroy bullet after lifespan

            _canFire = false;
            Invoke(nameof(ResetFire), _fireSpeed);
        }
    }
}
