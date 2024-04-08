/* This codebase serves to create a customizable side to side automatic shooter player for a top-down 2D game.*/ 
/* 
* Filename: AutomaticWeapon.cs 
* Developer: Kay Atkinson
* Purpose: A subclass of FBWeapon.cs used to automatically fire bullet objects, implements the singleton design pattern
*/ 

using UnityEngine;

/* 
* Summary: Subclass of FBWeapon to fire bullet objects automatically 
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
    private void Awake()
    {
        // Singleton implementation
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

    /* 
    * Summary:  Override Fire method to destroy bullets on collision with obstacles 
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
