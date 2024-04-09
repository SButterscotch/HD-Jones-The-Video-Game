/*
* Filename: HealthBaar.cs
* Developer: Rebecca Smith
* Purpose: This file initializes the health bar
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
* Summary: Initializes max health value, slider value, and color of the bar
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
    * Summary: Sets max health for player
    * Parameters: 
    * health - value for max health
    * Returns: N/A
    */
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        // Sets health bar to green at start
        fill.color = gradient.Evaluate(1f);
    }


    /*
    * Summary: Updates health for player
    * Parameters: 
    * health - value for health for player
    * Returns: N/A
    */
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue); //normalized changes slider value so it's on the same scale 0-1
    }


}
