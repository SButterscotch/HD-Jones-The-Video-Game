/*
* Filename: RelishBullet.cs
* 
* Developer: K Atkinson
* 
* Purpose: Script used for dynamic binding of the OnTriggerEnter2D function bound in Bullet parent class 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
* Summary: Subclass of Bullet used to augment the OnTriggerEnter2D function with override with bouncing bullet        
* 
* Member variables: 
* maxBounces, currentBounces 
*
*/ 
public class RelishBullet : Bullet
{
    [SerializeField]
    private int maxBounces = 3; // Maximum number of times the bullet can bounce
    private int currentBounces = 0; // Current number of bounces


    /* 
    * Summary: Override method to shoot a bullet that bounces off things a set number of times 
    * 
    * Parameters: "other" game object with 2D collider     
    * 
    * Returns:  None  
    */
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