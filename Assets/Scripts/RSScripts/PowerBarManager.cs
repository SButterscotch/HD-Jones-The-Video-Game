/*
* Filename: PowerBarManager.cs
* Developer: Rebecca Smith
* Purpose: This file initializes the health bar
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBarManager : MonoBehaviour
{
    private PowerBar powerBar; // Reference to the PowerBar component
    
    public void Start()
    {
        // Find and assign the CoinBar object based on its tag "CoinBar"
        GameObject powerBarObject = GameObject.FindWithTag("PowerBar");
        
        if (powerBarObject != null)
        {
            powerBar = powerBarObject.GetComponent<PowerBar>();
        }
        else
        {
            Debug.LogError("Coinbar not found. Make sure to tag your Coinbar GameObject with 'CoinBar'.");
        }

        // Assign the Coinbar instance to each Coin script attached to Coin GameObjects
        BeccaPower[] powerups = FindObjectsOfType<BeccaPower>();
        foreach (BeccaPower powerup in powerups)
        {
            powerup.powerBar = powerBar;
        }
    }
}
