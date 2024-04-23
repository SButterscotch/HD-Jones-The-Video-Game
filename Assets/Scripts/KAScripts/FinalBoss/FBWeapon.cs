/* This codebase serves to create a customizable side to side automatic shooter player for a top-down 2D game.*/ 
/* 
* Filename: FBWeapon.cs 
* Developer: Kay Atkinson
* Purpose: A parent class of AutomaticWeapon using private and protected class data to build a firing weapon 
*/ 
using UnityEngine;

/* 
* Summary: Class that creates variables and functions used for modular design in subclasses   
* 
* Member variables: 
* _fireSpeed, _roundsPerMinute, _canFire, _bulletVelocity, _obstacleTags, firePoint, bulletPrefab 
* 
*/ 
public class FBWeapon : MonoBehaviour
{
    // Private class and other data
    /*********PCD Pattern*****************
    /* _firespeed, _roundsPerMinute, _canFire, _bulletVelocity, _obstacleTags, and _bulletLifespan 
    /* are declared as private but exposed to the Unity inspector so they can be configured in he Editor. 
    */  
    [SerializeField] protected float _fireSpeed = 0.5f; // Fire speed in seconds
    [SerializeField] private float _roundsPerMinute = 120f; // Rounds per minute
    public bool _canFire = true; // Indicates whether the weapon can fire
    [SerializeField] protected float _bulletVelocity = 10f; // Velocity of the bullet
    [SerializeField] private string[] _obstacleTags = { "Obstacle", "obstacle" }; // Tags of obstacles
    [SerializeField] protected float _bulletLifespan = 4f; // Lifespan of the bullet

    // Serialized fields for public access
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePoint;

    // Public properties for accessing private class data
    public float FireSpeed => _fireSpeed;
    public float RoundsPerMinute => _roundsPerMinute;
    public float BulletVelocity => _bulletVelocity;
    public string[] ObstacleTags => _obstacleTags;
    public float BulletLifespan => _bulletLifespan;

    protected void Start()
    {
        InvokeRepeating(nameof(AutoFire), 0f, _fireSpeed);
    }

    /*********************DYNAMIC BINDING****************************
    /* This method is an instance of a subclass and it's exeuction is made at runtime 
    /* the object's actual type is the sublass, the overridden version of Fire() in the AutomaticWeapon 
    /* subclass. This allows for polymorphic code, allowing for unique behavior in each Fire() function. 
    * Summary: Function used for dynamic binding in subclasses, fires bullets 
    * 
    * Parameters: None   
    * 
    * Returns: None, uses Invoke and Instantiate to perform its actions   
    */
    public virtual void Fire()
    {
        if (_canFire)
        {
            GameObject bullet = Instantiate(bulletPrefab, (-firePoint.position), firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = _bulletVelocity * (-transform.right);
            }
            // Destroy bullet after lifespan
            //Destroy(bullet, _bulletLifespan); 

            _canFire = false;
            Invoke(nameof(ResetFire), _fireSpeed);
        }
    }

    //Unused functions that may be used for additionaly functionality or dynamic binding 

    protected void AutoFire()
    {
        if (_canFire)
        {
            Fire();
        }
    }

    protected void ResetFire()
    {
        _canFire = true;
    }
}
