/*
* Filename: SubClassBullet.cs
* Developer: K Atkinson
* Purpose: Script used to .  
* Attached to what in the inspector? 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RelishBullet : Bullet
{
    [SerializeField]
    private int maxBounces = 3; // Maximum number of times the bullet can bounce
    private int currentBounces = 0; // Current number of bounces

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            // Apply damage to the enemy
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            Destroy(other.gameObject);

            // Proceed with destroying the bullet as usual
            base.OnTriggerEnter2D(other);
        }
        else if (currentBounces < maxBounces)
        {
            // Bounce the bullet off in a random direction
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Vector3 newDirection = new Vector3(randomDirection.x, randomDirection.y, 0f);
            transform.up = newDirection;
            currentBounces++;

            // Play bounce sound effect or particle effect here if desired

            // Do not destroy the bullet
        }
        else
        {
            // If the bullet exceeds the maximum number of bounces, destroy it
            base.OnTriggerEnter2D(other);
        }
    }
}