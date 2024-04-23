/*
* Filename: SubHealthManager.cs
* Developer: Rebecca Smith
* Purpose: Implements dynamic binding for HealthBarManager.cs
*/

using UnityEngine;

/*
* Summary: This is the subclass to HealthBarManager that I created to demonstrate dynamic binding with IsPlayerDead
* Member Methods: IsPlayerDead
*/
public class SubHealthManager : HealthBarManager
{
    /*
    * Summary: Checks if the player is dead or not, implements dynamic binding and changes the color of the healthbar
    * Parameters: currentHealth - current health of the player
    * Returns: true or false 
    */
    public override bool IsPlayerDead(int currentHealth)
    {
        // Changes the color of the health bar to blue when overriding HealthBarManager's IsPlayerDead
        //healthBar.fill.color = Color.blue;
        if (currentHealth == 0)
        {
            return true;
        } 
        else
        {
            return false;
        } 
    }
}
