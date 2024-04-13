/*
* Filename: FinalBossBullet.cs
* 
* Developer: K Atkinson
* 
* Purpose: Script used for dynamic binding with Bullet parent class, used in Level4 to defeat FinalBoss 
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
public class FinalBossBullet : Bullet
{
    private static int hitsToKillFB = 3;
    private static int currentHits = 0;  
    /**************DYNAMIC BINDING****************************
    * Summary: Override method to shoot a bullet that bounces off things a set number of times 
    * 
    * Parameters: "other" game object with 2D collider     
    * 
    * Returns:  None  
    */
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("FinalBoss"))
        {
            currentHits++; 
            if (currentHits >= hitsToKillFB) { 
                // Apply damage to FinalBoss            
                Destroy(otherGameObject);

                //Reset hit count for next instance 
                currentHits = 0; 
            } 
    
            // Proceed with destroying the bullet as usual
            base.OnTriggerEnter2D(other);
        }
        else
        {
            // If the bullet exceeds the maximum number of bounces, destroy it
            base.OnTriggerEnter2D(other);
        }
    }
}
