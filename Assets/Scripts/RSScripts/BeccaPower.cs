/*
* Filename: BeccaPower.cs
* Developer: Rebecca Smith
* Purpose: This file handles power up collsions with player 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
* Summary: Handles collisions with the player and power up objects
* Member Variables: N/A
*/
public class BeccaPower : MonoBehaviour
{
    public PowerBar powerBar = new PowerBar(); // needed for PowerBarManager to work, needs a definition to assign the powerup bar to each powerup
    private HealthBarManager healthBarManager;
    public HotdogAnimatorController player; 
  
    //[SerializeField] private AudioSource audioSource;
    //[SerializeField] private AudioClip audioClip;

    /* 
    * Summary: Handles collisions with the player and power up objects
    * Parameters: other - collider object
    * Returns: N/A
    */
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //audioSource.PlayOneShot(audioClip);

            // Destroy the powerup object
            Destroy(gameObject);
            healthBarManager.AddHealth();

            powerBar = other.GetComponent<PowerBar>();
            if (powerBar != null)
            {
                powerBar.CallCountdownFromOutside();
            }
        } 
    }
}