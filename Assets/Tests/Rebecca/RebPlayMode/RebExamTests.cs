using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

public class RebExamTests
{
    // Checks if game ends when player health = 0
    [Test]
    public void CheckSceneWhenPlayerDies()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        // Create a game object with the player's health script attached
        GameObject player = new GameObject("Player");
        HealthBarManager healthBarManager = player.AddComponent<HealthBarManager>();

        // Set player's health to 0
        healthBarManager.currentHealth = 0;
        // Call function to check player health
        healthBarManager.Update();

        // If false, then the correct functions have been called and the game is over
        Assert.IsFalse(UnityEditor.EditorApplication.isPlaying);
    }

    // is power bar null
    [Test]
    public void Check()
    {
        
    }

    // Checks if health bar != NULL
    [Test]
    public void CheckIfHealthBarIsNull()
    {
        //Create new game object HealthBarManager 
        GameObject gameObject = new GameObject();
        HealthBarManager healthBarManager = gameObject.AddComponent<HealthBarManager>();

        Assert.IsTrue(healthBarManager.healthBar);

    }

    // Checks if player object != NULL
    [Test]
    public void CheckIfPlayerObjectIsNull()
    {
        //Create new game object HealthBarManager 
        GameObject player = new GameObject("Player");
        HealthBar healthBar = player.AddComponent<HealthBar>();

        // Check if health bar is not null
        Assert.IsTrue(healthBar);
    }

    // Stress test on what happens when the player runs into multiple enemies at once
    [Test]
    public IEnumerator TestIfCurrentHealthStaysAtZeroWithThousandEnemies()
    {
        // Create player object with health manager
        GameObject player = new GameObject("Player");
        HealthBarManager healthBarManager = player.AddComponent<HealthBarManager>();
        healthBarManager.maxHealth = 100;
        healthBarManager.currentHealth = 100;

        // Create healthbar object
        GameObject healthbarobject = new GameObject("HealthBar");
        HealthBar healthBar = healthbarobject.AddComponent<HealthBar>();
        healthBar.SetHealth(healthBarManager.currentHealth);

        // Spawn 1000 enemies around the player
        for (int i = 0; i < 1000; i++)
        {
            Vector3 spawnPosition = player.transform.position + Random.insideUnitSphere * 10f; // Random position around player
            GameObject enemy = new GameObject("Enemy");
            enemy.transform.position = spawnPosition; // Set enemy position
        }

        // Wait for a short time to allow any damage calculations to occur
        yield return new WaitForSeconds(1.0f);

        // Check if player's health is above zero
        Assert.Greater(healthBarManager.currentHealth, 0);
    }

    // Checks if power up = NULL after player collides with it
    [Test]
    public void CheckSceneWhenPlayers()
    {
        
    }

    // Check if the heatlh bar is green when health = 70 ish
    [Test]
    public void CheckSceneWs()
    {
        
    }

     // Check if the heatlh bar is yellow when health = 50 ish
    [Test]
    public void CheckScenes()
    {
        
    }

     // Check if the heatlh bar is red when health = 30 ish
    [Test]
    public void ChecScenes()
    {
        
    }

     // Check if the heatlh bar is yellow when health = 50 ish
    [Test]
    public void ChekScenes()
    {
        
    }
}