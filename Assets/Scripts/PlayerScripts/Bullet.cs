// /*
// * Filename: Bullet.cs
// * Developer: K Atkinson
// * Purpose: Script used to control bullet speed, size, sprite, and lifetime.  
// * Attached to what in the inspector? Bullet prefab 
// */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    protected float speed = 4f; 
    [SerializeField]
    protected float bulletTimetoLive = 3f; 

    protected virtual void Start()
    {
        StartCoroutine(DestroyBulletAfterTime()); 
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime); 
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.HitByBullet(); 
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

    protected virtual IEnumerator DestroyBulletAfterTime()
    { 
        yield return new WaitForSeconds(bulletTimetoLive); 
        Destroy(gameObject); 
    }
}

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
