/*
* Filename: PowerBar.cs
* Developer: Rebecca Smith
* Purpose: This file initializes the power up bar
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer.Explorer.Operations;

public class PowerBar : MonoBehaviour
{ 
    private PrivatePowerData privatePowerData; // Instance of private implementation class
     [SerializeField] private Slider slider; 
    [SerializeField] private Image fill;
    
    //Setting Max value for health so we don't have change it inside Unity Editor 
    public void Start() 
    { 
        privatePowerData = new PrivatePowerData(slider, fill); // Pass references to slider and fill
        privatePowerData.SetUpPowerBar();
    }

    // Public method to access a method from PrivatePowerData
    public void CallPrivateMethodFromOutside()
    {
        privatePowerData.CountdownPowerUp();
    }


    private class PrivatePowerData
    {
        [SerializeField] private Slider slider; 
        [SerializeField] private Image fill;
        private float maxPower = 100f;
        private float currentPower = 100f;
        private float decreasePower = 1f;
        private GameObject powerUpBar;
        private Gradient gradient;

        public PrivatePowerData(Slider slider, Image fill)
        {
            this.slider = slider;
            this.fill = fill;
        }

        public void SetUpPowerBar()
        {
            // Check for null references before accessing
            if (slider != null && fill != null)
            {
                slider.maxValue = maxPower;
                slider.value = currentPower; 
                // Assuming gradient is set elsewhere
                fill.color = gradient.Evaluate(1f); 
            }
            // Initially disable the power-up bar
            powerUpBar.SetActive(false);
        }
        //Starts countdown with powerup bar once the player collides with a power up       
        public void CountdownPowerUp()
        {
            TogglePowerUpBar(true); //make bar visible
            if (currentPower > 0)
            {
                currentPower -= decreasePower * Time.deltaTime;
                slider.value = currentPower;
            }
            else
            {
                // Power has reached 0, change player state back to normal too!!
                TogglePowerUpBar(false);
            }
        }

        // Method to toggle the visibility of the power-up bar
        public void TogglePowerUpBar(bool value)
        {
            powerUpBar.SetActive(value);
        }
    }
}
