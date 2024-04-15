/*
* Filename: PowerBar.cs
* Developer: Rebecca Smith
* Purpose: This file initializes the power up bar
*/

using UnityEngine.UI;
using UnityEngine;

/*
* Summary: Controls all functionality of the power up bar 
* Member Variables:
* slider - slider value inside of Unity inspector
* fill - color of the bar inside of game
*/
public class PowerBar : MonoBehaviour
{ 
    // Instance of private implementation class
    private PrivatePowerData privatePowerData; 
    [SerializeField] private Slider slider; 
    [SerializeField] private Image fill;
    private float pubCurrentPower;
    public HotdogAnimatorController player; 
    [SerializeField] private GameObject powerUpBar;
    

    /*
    * Summary: Calls functions at start to initalize health bar
    * Parameters: N/A
    * Returns: N/A
    */
    public void Start() 
    { 
        privatePowerData = new PrivatePowerData(slider, fill, powerUpBar); // Pass references to slider and fill
        privatePowerData.SetUpPowerBar();
    }


    /*
    * Summary: Public method to access a method from PrivatePowerData
    * Parameters: N/A
    * Returns: N/A
    */
    public void CallCountdownFromOutside()
    {
        privatePowerData.CountdownPowerUp();
    }


    /*
    * Summary: Public method to access currentPower from PrivatePowerData
    * Parameters: N/A
    * Returns: privatePowerData.GetCurrentPowerFromInside(); 
    */
    public float GetCurrentPowerFromOutside()
    {
        return privatePowerData.GetCurrentPowerFromInside();
    }

    /* Private Class Data Pattern:
    Why use this pattern?
        - This pattern allows for great encapsulation of methods and attributes so they cannot be directly accessed or changed. All methods must be accessed through a public
        itteration of the private class. This added layer of security ensure the integrity of the state of the power bar. It also reduces the coupling between classes because 
        changes made on the inside of the private class will not effect the external interface.
    Would something else have worked better? 
        - Yes, I think the Observer pattern would have worked better for this scenario. It has been challenging to update other classes because of data I need to broadcast is 
        hidden. It has also been annoying to create tests for anything in this class.
    When would be a bad time to use this pattern?
        - This would be a bad example to use this pattern. There are multiple instances that will end up depending on this class, which will increase the amount of public
        instances I have to make for this class. This just adds complexity and maintance that could be avoided with other patterns.
    */


    /*
    * Summary: Initializes max and min power bar values, allows the player to view the power bar, and starts the countdown
    * Member Variables:
    * maxPower - max power value
    * decreasePower - the rate at which the bar should decrease
    * currentPower - current power of the bar
    */
    private class PrivatePowerData
    {
        [SerializeField] private Slider slider; 
        [SerializeField] private Image fill;
        private float maxPower = 100f;
        private float currentPower = 100f;
        private float decreasePower = 1f;
        private GameObject powerUpBar;
        private Gradient gradient = new Gradient(); // Initialize gradient


        /*
        * Summary: Public class to initialize the slider and fill objects
        * Member methods: N/A
        */
        public PrivatePowerData(Slider slider, Image fill, GameObject powerUpBar)
        {
            this.slider = slider;
            this.fill = fill;
            this.powerUpBar = powerUpBar;
        }


        /*
        * Summary: Initializes the power bar 
        * Parameters: N/A
        * Returns: N/A
        */
        public void SetUpPowerBar()
        {
            // Check for null references before accessing
            if (slider != null && fill != null)
            {
                slider.maxValue = maxPower;
                slider.value = currentPower; 
                fill.color = gradient.Evaluate(1f); 
                // Initially disable the power-up bar so it's not viewable
                powerUpBar.SetActive(false);
            }
        }


        /*
        * Summary: Starts countdown with powerup bar once the player collides with a power up object
        * Parameters: N/A
        * Returns: N/A
        */
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


        /*
        * Summary: Method to toggle the visibility of the power-up bar
        * Parameters: value - true or false to determine if the bar will be shown
        * Returns: N/A
        */
        public void TogglePowerUpBar(bool value)
        {
            powerUpBar.SetActive(value);
        }


        /*
        * Summary: Method to return the value for currentPower
        * Parameters: N/A
        * Returns: N/A
        */
        public float GetCurrentPowerFromInside()
        {
            return currentPower;
        }

    }
}
