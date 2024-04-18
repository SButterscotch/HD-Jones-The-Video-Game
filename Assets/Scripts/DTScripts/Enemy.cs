/*
* Filename: Enemy.cs
* Developer: David Trail
* Purpose: Base class for enemy behavior, including movement, targeting, health management, and interactions with the player.
*/

using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Public variables
    public Transform target;            // Reference to the target (usually the player)
    public float Distance;              // Distance between the enemy and the target
    [SerializeField] public float DistanceBetween = 10f;   // Minimum distance to keep from the target
    [SerializeField] public float speed = 3f;              // Movement speed of the enemy
    public static Enemy _instance;                          // Singleton instance of the Enemy class
    public static Enemy Instance { get { return _instance; } } // Getter for the singleton instance
    public Rigidbody2D rb;                                  // Reference to the enemy's Rigidbody2D component
    public int EnemyHealth = 3;                             // Health of the enemy
    private Vector3 InitialScale;                           // Initial scale of the enemy GameObject

    // Meghan's code
    public AudioSource enemyNoise;                          // Audio source for enemy noise
    public AudioSource championSound;                       // Audio source for champion sound
    public static int totalEnemies;                         // Total number of enemies in the scene
    public static int enemiesKilledCount = 0;               // Total number of enemies destroyed

    // Rebecca's code for pattern
    public static event Action<int> EnemyEffectHealth;      // Event for notifying subscribers about the effect of enemy on player's health

    /*
    * Summary: Initializes the enemy by setting initial scale, getting Rigidbody2D component, finding the target (usually the player), and initializing audio sources.
    */
    public virtual void Start()
    {
        InitialScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        GetTarget(); // Find player upon entry
        DistanceBetween = 4f;

        // Initialize audio sources
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length >= 2)
        {
            enemyNoise = audioSources[0];
            championSound = audioSources[1];
        }
    }

    /*
    * Summary: Handles cleanup tasks when the enemy is destroyed, such as decrementing the totalEnemies count and playing the champion sound if all enemies are defeated.
    */
    public virtual void OnDestroy()
    {
        // Decrement totalEnemies when an enemy is destroyed
        totalEnemies--;

        // Increment enemiesKilledCount
        enemiesKilledCount++; 

        // Play the champion sound if all enemies are defeated
        if (totalEnemies == 0)
        {
            if (championSound != null)
            {
                championSound.Play();
            }
        }
    }

    /*
    * Summary: Updates the enemy's behavior, including movement towards the target and collision detection.
    */
    public virtual void Update()
    {
        if (target == null)
        {
            GetTarget(); // Find target if it's null
        }

        Distance = Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        Vector2 Direction = target.transform.position - transform.position;

        if (Distance < DistanceBetween)
        {
            // Move towards the player
            transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed = Time.deltaTime);
        }
    }
    
    /*
    * Summary: Finds the target (usually the player) and sets it as the enemy's target.
    */
    public void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    /*
    * Summary: Handles collision events with other objects, such as the player and bullets.
    * Parameters:
    *   other: The collision object.
    */
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Attack();       // Attack player
            target = null;  // Reset target
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            EnemyHealth--;          // Reduce enemy health
            enemyNoise.Play();      // Play enemy noise
            Destroy(other.gameObject); // Destroy bullet

            if (EnemyHealth == 0)
            {
                Destroy(gameObject); // Destroy enemy if health reaches zero
            } 
        }  
    }

    /*
    * Summary: Records that the enemy has been hit by a bullet.
    */
    private int bulletsHit = 0;
    [SerializeField]
    private int bulletsToDestroy = 2; // Number of bullets required to destroy enemy

    public void HitByBullet()
    {
        bulletsHit++;
    }

    /*
    * Summary: Checks if the enemy has been destroyed by bullets.
    * Returns:
    *   bool: True if the enemy has been destroyed, otherwise false.
    */
    public bool IsDestroyed()
    {
        return bulletsHit >= bulletsToDestroy;
    }

    /*
    * Summary: Performs the enemy's attack behavior.
    */
    protected virtual void Attack()
    {
        EnemyEffectHealth?.Invoke(10); // Invoke event for player's health reduction
    }
}
