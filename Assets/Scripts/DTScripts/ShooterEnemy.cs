/*
* Filename: ShooterEnemy.cs
* Developer: David Tral
* Purpose: Controls behavior of shooter enemy, including movement, targeting, and shooting at intervals.
*/

using System.Collections;
using UnityEngine;

/*
 * Summary: Controls the behavior of the shooter enemy, including movement, targeting, and shooting at intervals.
 *          Inherits from the Enemy class and overrides certain methods for customized behavior.
 */
public class ShooterEnemy : Enemy
{
    // Serialized fields for adjusting enemy behavior in the Unity Editor
    [SerializeField] public float DistanceToStop = 5f;      // Distance at which the enemy stops approaching the target
    [SerializeField] public float RetreatDistance = 3f;     // Distance at which the enemy starts retreating from the target
    public bool OverrideUpdate = true;                      // Flag to override the Update method
    [SerializeField] public float TimeBetweenShots = 1f;   // Time between shots
    [SerializeField] public float StartTimeBetweenShots;    // Initial time between shots

    public GameObject EnemyBullet;                          // Prefab for enemy bullet

    private Coroutine shootingCoroutine;                    // Coroutine for shooting at intervals

    /*
    * Summary: Initializes the shooter enemy by setting the time between shots and starting the shooting coroutine.
    */    
    public override void Start()
    {
        TimeBetweenShots = StartTimeBetweenShots;           // Initialize time between shots
        StartShooting();                                    // Start shooting coroutine
    }
    
    /*
     * Summary: Updates the behavior of the shooter enemy, including movement, targeting, and shooting at intervals.
     *          Overrides the base Update method if OverrideUpdate is true, otherwise calls the base Update method.
     */
    public override void Update()
    {
        if (OverrideUpdate)
        {
            // Move towards or away from the target based on distance
            transform.position = Vector2.MoveTowards(transform.position, target.position, (-speed) * Time.deltaTime);

            if (target == null)
            {
                GetTarget();                                // Get target if it's null
            }

            Distance = Vector2.Distance(transform.position, target.position);   // Calculate distance to target
            Vector2 Direction = target.position - transform.position;           // Calculate direction to target

            if (Distance > DistanceToStop)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else if (Distance < RetreatDistance)
            {
                // Retreat from the player
                transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
            }
        }
        else
        {
            base.Update();                                    // Call base Update if OverrideUpdate is false
        }
    }
    
    /*
    * Summary: Coroutine to handle shooting at intervals
    */
    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Attack();                                          // Perform attack
            yield return new WaitForSeconds(TimeBetweenShots); // Wait for specified time between shots
        }
    }

    /*
    * Summary: Method to start shooting coroutine
    */
    public void StartShooting()
    {
        if (shootingCoroutine == null)
        {
            shootingCoroutine = StartCoroutine(ShootCoroutine()); // Start shooting coroutine if not already started
        }
    }

    /*
    * Summary: Method to stop shooting coroutine
    */
    public void StopShooting()
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);         // Stop shooting coroutine if it's running
            shootingCoroutine = null;
        }
        
    }

    /*
    * **************DYNAMIC BINDING*****************************
    * Summary: Override of the Attack method to instantiate enemy bullets
    */
    protected override void Attack()
    {
        Instantiate(EnemyBullet, transform.position, Quaternion.identity); // Instantiate enemy bullet at enemy's position
    }
}
