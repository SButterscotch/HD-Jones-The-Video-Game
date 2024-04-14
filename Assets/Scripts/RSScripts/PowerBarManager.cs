/*
* Filename: PowerBarManager.cs
* Developer: Rebecca Smith
* Purpose: This file initializes the power up bar
*/

using UnityEngine;

/*
* Summary: Manages the power up bar by assigning the powerbar instance to each powerup gameObject
* Member Variables: N/A
*/
public class PowerBarManager : MonoBehaviour
{
    private PowerBar powerBar;
    
    /*
    * Summary: Manages relationship between the power up bar and the power up objects 
    * Parameters: N/A
    * Returns: N/A
    */
    public void Start()
    {
        // Find and assign the PowerBar object based on its tag "PowerBar"
        GameObject powerBarObject = GameObject.FindWithTag("PowerBar");
        
        if (powerBarObject != null)
        {
            powerBar = powerBarObject.GetComponent<PowerBar>();
        }
        else
        {
            Debug.LogError("Powerbar not found. Make sure to tag your Powerbar GameObject with 'PowerBar'.");
        }

        // Assign the powerbar instance to each power up script attached to powerup GameObjects
        BeccaPower[] powerups = FindObjectsOfType<BeccaPower>();
        foreach (BeccaPower powerup in powerups)
        {
            powerup.powerBar = powerBar;
        }
    }
}
