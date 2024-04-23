/*
* Filename: Bullet.cs
*
* Developer: K Atkinson
*
* Purpose: Script used to control bullet speed, size, sprite, and lifetime.  
* Attached to what in the inspector? Bullet prefab
* Note: Uses protected variables and functions so that methods and vars stay isolated to the class and subclasses - works with virtual for later overrides!   
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Summary: Meghan Nulf sound initializing for bullets when they shoot and contact enemies
 * 
 * Member Variables:
 * bullet sound, monster death

/* 
* Summary: Parent class for customizing bullets and handling their collisions       
* 
* Member variables: 
* speed, bulletTimetoLive
* 
*/ 
public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed = 4f; //How fast the bullet can move 
    [SerializeField] protected float bulletTimetoLive = 3f; //how long the bullet lives before it's destroyed

    //Meghan Initialization
    AudioManager audioManager;

    //Meghan Function
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    protected virtual void Start()
    {
        StartCoroutine(DestroyBulletAfterTime()); //Uses bulletTimetoLive
    }

    protected virtual void Update()
    {
        Move();
    }

    /* 
    * Summary: Protected virtual function for moving the bullet across the screen  
    * 
    * Parameters: None    
    * 
    * Returns: None, just translates using vectors, speed and constant time 
    */
    protected virtual void Move()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime); 
    }

    /* 
    * Summary: Protected virtual function for handling bullet collisions in the game  
    * 
    * Parameters: "other", a game object with a 2D Collider component     
    * 
    * Returns: None, destroys the game object based on certain conditions  
    */
    protected virtual void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.HitByBullet();
            //Meghan code line
            audioManager.PlaySFX(audioManager.Monster);
            if (enemy.IsDestroyed()) 
            {
                Destroy(other.gameObject); 
            }
            Destroy(gameObject); 
        }  
        if (other.transform.tag == "obstacle") 
        { 
            Destroy(gameObject); 
        }       
    }

    /* 
    * Summary: IEnum function used to wait for a certain amount of time before destroying bullet   
    * 
    * Parameters: None     
    * 
    * Returns: None, destroys the game object based on certain conditions  
    */
    protected virtual IEnumerator DestroyBulletAfterTime()
    { 
        yield return new WaitForSeconds(bulletTimetoLive); 
        Destroy(gameObject); 
    }
}

//This code works too, keep in case something breaks:
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Bullet : MonoBehaviour
// {

//     [SerializeField] 
//     private float speed = 4f; 
//     [SerializeField]
//     private float bulletTimetoLive = 3f; 

//     void Start()
//     {
//         StartCoroutine(DestroyBulletAfterTime()); 
//     }
//     void Update()
//     {
//         transform.Translate(Vector3.up * speed *Time.deltaTime); 
//     }

//     //This function uses the 2D Colliders to destroy each other if contacted 
//     protected virtual void OnTriggerEnter2D(Collider2D other) { 
//         //This code will work independentally of David's Enemy.cs script 
//         // if (other.gameObject.GetComponent<Enemy>() != null) { 
//         //     Destroy(other.gameObject); //to destroy enemy, @David or Andrew to animate  
//         //     Destroy(gameObject); //Destroy bullet 
//         // }
//         if (other.gameObject.GetComponent<Enemy>() != null)
//         {
//             Enemy enemy = other.gameObject.GetComponent<Enemy>();
//             enemy.HitByBullet(); // Call method to register bullet hit from Enemy.cs 
//             if (enemy.IsDestroyed()) //Call method to tell if bullet is destroyed already, found in Enemy.cs 
//             {
//                 Destroy(other.gameObject); // Destroy enemy if hit twice
//             }
//             Destroy(gameObject); // Destroy bullet regardless
//         }  
//         if (other.transform.tag == "obstacle") { 
//             Destroy(gameObject); //Destroy bullet if it hits an obstacle 
//         }       
//     }
//     IEnumerator DestroyBulletAfterTime(){ 
//         yield return new WaitForSeconds(bulletTimetoLive); 
//         Destroy(gameObject); 
//     }
// }
