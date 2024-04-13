/*
* Filename: BeccaPower.cs
* Developer: Rebecca Smith
* Purpose: This file handles power up collsions with player so I can create the power up bar before the oral exam
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeccaPower : MonoBehaviour
{
    public PowerBar powerBar;
  
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //audioSource.PlayOneShot(audioClip);
            Destroy(gameObject); // Destroy the Coin object

            //powerBar.UpdateHealth(10);
        } 
    }
}