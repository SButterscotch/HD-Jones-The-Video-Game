/*
* Filename: PowerBar.cs
* Developer: Rebecca Smith
* Purpose: This file initializes the power up bar
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PowerBar : MonoBehaviour
{
    public int powerHealth;
    public Slider slider; 
    public Gradient gradient; 
    public Image fill; 
   
    //Setting Max value for health so we don't have change it inside Unity Editor 
    ///*
    public void SetMaxHealth() { 
        slider.maxValue = 100;
        slider.value = 0; //start at 0 health 
        fill.color = gradient.Evaluate(1f); 

    }
    //*/

    //Change health from Slider inside HealthBar 
    public void SetHealth(int health) { 
        slider.value = health; 
        fill.color = gradient.Evaluate(slider.normalizedValue); 
    }
    public void UpdateHealth(int health)
    {
        powerHealth += health;
        SetHealth(powerHealth);
    }
}

public class PrivatePowerData
{

}