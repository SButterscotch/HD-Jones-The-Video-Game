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
    /* Observer Pattern:  !!!!!!!!!!!!!!!!!!
    
    */
    public static event Action<int> OnHealthChanged; 
    public int maxHealth = 100;
    public int minHealth = 0;
    public int currentHealth = 100;
    public bool playerDead;
    public HealthBar healthBar;
    public HotdogAnimatorController player; 

    /*
    * Summary: Initalizes health bar at the start of the game
    * Parameters: N/A
    * Returns: N/A
    */
    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth, currentHealth);
        // Observs the subject (Enemy.cs) and subscribes to TakeDamage() so it will call it everytime there is an event
        Enemy.EnemyEffectHealth += TakeDamage;
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
        }
    }


    /*
    * Summary: Checks if the player is dead or not
    * Parameters: currentHealth - current health of the player
    * Returns: true or false
    */
    public virtual bool IsPlayerDead(int currentHealth)
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
            // Notify observers that the health has changed
            OnHealthChanged?.Invoke(currentHealth);
        } 
        else 
        {
            Debug.LogError("Player is dead.");
        }
        
    }


    /*
    * Summary: Increases the players health when they contact an enemy
    * Parameters: addHealth - how much the health bar will increase
    * Returns: N/A
    */
    public void AddHealth(int addHealth)
    {
        if (currentHealth != 100)
        {
            currentHealth += addHealth;
            OnHealthChanged?.Invoke(currentHealth);
        }
    }

    /*
    * Summary: Starts the death animation when the player dies
    * Parameters: N/A
    * Returns: N/A
    */
    private void DeathAnimation()
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
}
