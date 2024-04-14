/*
* Filename: BeccaPower.cs
* Developer: Rebecca Smith
* Purpose: This file handles power up collsions with player so I can create the power up bar before the oral exam
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeccaPower : PowerBar
{
    public PowerBar powerBar = new PowerBar(); // needed for PowerBarManager to work, needs a definition to assign the powerup bar to each powerup
    private HealthBarManager healthBarManager;
  
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //audioSource.PlayOneShot(audioClip);
            Destroy(gameObject); // Destroy the powerup object
            healthBarManager.AddHealth(5);

            powerBar = other.GetComponent<PowerBar>();
            if (powerBar != null)
            {
                powerBar.CallPrivateMethodFromOutside();
            }
        } 
    }
}