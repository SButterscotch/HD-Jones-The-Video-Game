/*
* Filename: HealthBar.cs
* Developer: Rebecca Smith
* Purpose: This file initializes the health bar
*/

using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.UI;

/*
* Summary: Observer for the subject (HealthBarManager), updates the health bar 
* Member Variables:
* slider - slider value inside of Unity inspector
* gradient - gradient value inside of Unity inspector
* fill - color of the bar inside of game
*/
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;


    /*
    * Summary: Invokes everytime an event takes place
    * Parameters: N/A
    * Returns: N/A
    */
    private void Start()
    {
        HealthBarManager.OnHealthChanged += SetHealth;
    }


    /*
    * Summary: Updates health for player
    * Parameters: health - value for health for player
    * Returns: N/A
    */
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue); //normalized changes slider value so it's on the same scale 0-1
    }


    /*
    * Summary: Sets max health for player
    * Parameters: health - value for max health
    * Returns: N/A
    */
    public void SetHealth(int maxHealth, int currentHealth)
    {
        Debug.LogError("Current health is: " + currentHealth);
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        // Sets health bar to green at start
        fill.color = gradient.Evaluate(1f);
    }
}
