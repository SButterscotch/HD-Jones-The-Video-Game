/*
* Filename: HealthBarManager.cs
* Developer: Rebecca Smith
* Purpose: Maintains the state of the healthbar, subject for Observer (HealthBar.cs) and observer for Subject (Enemy.cs)
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
* Summary: Initializes max and min health values, checks if player is dead, and plays the death animations
* Member Variables:
* maxHealth - max health value
* minHealth - min health value
* currentHealth - current health of player
* playerDead - true if player is dead, false if player is alive
*/
public class HealthBarManager : MonoBehaviour
{
    /* Observer Pattern: 
    Why use this pattern?
        - This pattern defines a one-to-many dependency between objects to allow for multiple different observers to get updated on changes that occur within the subject.
        When one object state changes, all dependent objects get notified automatically. There is no need to create a new object in every class that needs to access the 
        Subject's data because there can be multiple subscribers to the same event. This greatly decreases the amount of dependencies between communicating classes 
        and for them to stay loosely coupled. For a health bar, this is important because there are lots of objects that depend on its data. 
    Would something else have worked better? 
        - For my part of the project, no. I really enjoy how simple the Observer pattern is to implement and how low the coupling is between classes. It also allows me to integrate 
        code into other people's classes without worrying if it will break their code. 
    When would be a bad time to use this pattern?
        - This pattern does not scale well once the amount of Observers and Subjects increase. Managing subscriptions, notifications, and updates between classes can cause 
        unneeded complexity. Therefore this pattern would not be good to use on large scale projects or classes.
    */
    public static event Action<float> OnHealthChanged; // delegate signiture, all subscribers must follow same format
    public int maxHealth = 100;
    public int minHealth = 0;
    public float currentHealth = 100f;
    public bool playerDead;
    public HealthBar healthBar;
    public HotdogAnimatorController player;
    public GameOver GameOverScr;
    public float tempHealth = 0;
    public bool touchPowerUp = false;

    //public GameObject floatingText; 

    /*
    * Summary: Initalizes health bar at the start of the game
    * Parameters: N/A
    * Returns: N/A
    */
    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth, currentHealth);

        // Observes the subject (Enemy.cs) and subscribes to the EnemyEffectHealth event using TakeDamage() as the handler for this event
        Enemy.EnemyEffectHealth += TakeDamage;
        EnemyBullet.EnemyEffectHealth += TakeDamage;
    }


    /*
    * Summary: Regularly calls isPlayerDead() and performs an action based on T/F
    * Parameters: N/A
    * Returns: N/A
    */
    public void Update()
    {
        playerDead = IsPlayerDead(currentHealth);
        
        if (playerDead == true)
        {
            DeathAnimation();
            GameOverScr.gameOver();
        }

        if (touchPowerUp == true)
        {
            currentHealth = 100f;
            OnHealthChanged?.Invoke(currentHealth);
            touchPowerUp = false;
        }
    }


    /*
    * Summary: Checks if the player is dead or not
    * Parameters: currentHealth - current health of the player
    * Returns: true or false
    */
    public bool IsPlayerDead(float currentHealth)
    {
        if (currentHealth == 0)
        {
            return true;
        } 
        else
        {
            return false;
        } 
    }


    /*
    * Summary: Decreases the players health when they contact an enemy
    * Parameters: damage - how much the health bar will decrease
    * Returns: N/A
    */
    public void TakeDamage(int damage)
    {
        // Call function to see if player is dead
        playerDead = IsPlayerDead(currentHealth);
        
        if (playerDead == false)
        {
            currentHealth -= damage;
            // Notify observers(those who subscribed to OnHealthChanged event) that the health has changed
            // Passing currentHealth to observers so they can pass it to their methods
            // The ?. operator is a null-conditional operator, meaning the event will only be invoked if != null
            OnHealthChanged?.Invoke(currentHealth);
        } 
        else 
        {
            Debug.LogError("Player is dead.");
        }
        
    }


    /*
    * Summary: Increases the players health when they contact an power up
    * Parameters: addHealth - how much the health bar will increase
    * Returns: N/A
    */
    public void AddHealth()
    {
        tempHealth = maxHealth - currentHealth;
        currentHealth += tempHealth;
        OnHealthChanged?.Invoke(currentHealth);

    }

    //Added by K for power-ups 

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PowerUp")
        {
            
            AddHealth(); 
            //Instantiate(floatingText, transform.position, Quaternion.identity);
            touchPowerUp = true;
            Debug.Log($"Current health is {currentHealth}"); 
        }
    }
    /*
    * Summary: Starts the death animation when the player dies
    * Parameters: N/A
    * Returns: N/A
    */
    public void DeathAnimation()
    {
        // Access the Entity script to change the state to deadState
        HotdogAnimatorController player = GetComponent<HotdogAnimatorController>();
        if (player != null)
        {
            //player.currentState = player.deadState;
        }

        // Additional logic if needed, e.g., play death animation, disable controls, etc.
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            //animator.runtimeAnimatorController = player.MushrioDead as RuntimeAnimatorController;
        }

        // Wait for 5 seconds before loading the game over scene
        //StartCoroutine(LoadGameOverSceneAfterDelay(2.5f));
        LeaveTheGame();
    }


    /*
    * Summary: Loads game over scene when player dies
    * Parameters: delay - float that contains the amount of seconds the game over screen will be delayed before it loads
    * Returns: IEnumerator (coroutine)
    */
    private IEnumerator LoadGameOverSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        // Load the game over scene
        //SceneManager.LoadScene("GameOver"); 
    }


    //using this instead of game over scene for now
    private void LeaveTheGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    /*
    * Summary: Unsubscribes from EnemyEffectHealth event 
    * Parameters: N/A
    * Returns: N/A
    */
    public void OnDestroy()
    {
        Enemy.EnemyEffectHealth -= TakeDamage;
    }
}
