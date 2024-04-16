/*
* Filename: EnemyBullet.cs
* Developer: David trail
* Purpose: Controls behavior of enemy bullets, including movement, collision detection, and object pooling.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Summary: Controls behavior of enemy bullets, including movement, collision detection, and object pooling.
 */
public class EnemyBullet : MonoBehaviour
{
    // Serialized fields for adjusting bullet behavior in the Unity Editor
    [SerializeField] private float speed = 5f;       // Speed of the bullet
    [SerializeField] private float lifespan = 2f;    // Time until the bullet is automatically destroyed if not collided

    private Vector2 targetDirection;     // Direction towards the target (usually the player)
    private Rigidbody2D rb;             // Reference to the bullet's Rigidbody2D component
    private Transform playerTransform;  // Reference to the player's Transform
    private Vector2 targetPosition;     // Position of the target

    // Event for notifying subscribers about the effect of the bullet on the player's health
    public static event Action<int> EnemyEffectHealth;

    // Object Pooling Variables
    private bool isActivated = false;       // Flag indicating if the bullet is currently active
    private Coroutine deactivateCoroutine;  // Coroutine for deactivating the bullet after a certain time
    
    /* Justification for Object Pooling:
    Object Pooling is used to efficiently manage the creation and destruction of bullets.
    Instead of instantiating and destroying bullets every time they are needed or unused,
    a pool of bullets is created at the start, and they are reused when needed, reducing
    the overhead of frequent object creation and destruction.
    
    Object Pooling:
    This script utilizes Object Pooling to efficiently manage the creation, activation, and deactivation of bullet objects.
    When the game starts, a pool of bullet objects is instantiated and stored.
    Bullets are retrieved from the pool when needed, activated, and positioned for use.
    After traveling for a certain lifespan or colliding with objects, bullets are deactivated and returned to the pool for reuse.
    Object Pooling minimizes the overhead of instantiating and destroying objects, leading to better performance, especially in scenarios with frequent bullet firing.


    Alternatives:
    One alternative to object pooling could be instantiating and destroying bullets as needed.
    However, this approach can lead to performance issues, especially in scenarios with a high
    frequency of bullet firing, as frequent instantiation and destruction can cause performance
    overhead due to memory allocation and garbage collection.

     Bad times to use Object Pooling:
     Object pooling might not be suitable for scenarios where the number of objects to be pooled
     is very large or highly variable. Managing a large number of objects in the pool can consume
     significant memory, and if the number of objects needed exceeds the preallocated pool size,
    it can result in additional overhead or performance degradation.
    */
    
    /*
    * Summary: Initializes the bullet by obtaining necessary references and activating it.
    */
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        targetPosition = new Vector2(playerTransform.position.x, playerTransform.position.y);
        Activate(targetPosition);
    }

    /*
    *Summary: Activates the bullet and sets its direction towards the target.
    *Parameters: direction - The direction towards the target
    */
    public void Activate(Vector2 direction)
    {
        targetDirection = direction.normalized;
        isActivated = true;
        if (deactivateCoroutine != null)
            StopCoroutine(deactivateCoroutine);
        deactivateCoroutine = StartCoroutine(DeactivateAfterDelay());
    }

    /*
    *Summary: Coroutine to deactivate the bullet after its lifespan.
    */
    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(lifespan);
        Deactivate();
    }

    /*
    * Summary: Deactivates the bullet, returning it to the object pool.
    */
    private void Deactivate()
    {
        isActivated = false;
        gameObject.SetActive(false);
    }

    /*
    Summary Moves the bullet towards its target position.
    */
    private void MoveBullet()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    /*
    * Summary: Checks for collisions with other objects and handles them accordingly.
    * Parameter: other- the collider of the object collided with
    */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("HitPlayer");
            EnemyEffectHealth?.Invoke(10);
            Deactivate();
        }
        else if (other.transform.tag == "obstacle")
        {
            Deactivate();
        }
    }

    /*
    Summary: Updates the bullet's movement and deactivates it if it reaches its target position.
    */
    private void Update()
    {
        if (isActivated)
            MoveBullet();
        
        if (transform.position.x == targetPosition.x && transform.position.y == targetPosition.y)
        {
            Deactivate();
        }
    }
}
