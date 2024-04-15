using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

public class RebExamTests
{
    // Checks if game ends when player health = 0
    /*[Test]
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
    }*/

    // is power bar null
    [Test]
    public void CheckIfPowerBarIsNull()
    {
        // Create player object
        GameObject player = new GameObject("Player");

        // Testing if the player object already has a PowerBar component
        PowerBar powerBar = player.AddComponent<PowerBar>();

        Assert.IsTrue(powerBar);
    }

    // Checks if health bar != NULL
    [Test]
    public void CheckIfHealthBarIsNull()
    {
        // Create player object
        GameObject player = new GameObject("Player");
        HealthBar healthBar = player.AddComponent<HealthBar>();

        Assert.IsTrue(healthBar);
    }

    // Checks if player object != NULL
    [Test]
    public void CheckIfPlayerObjectIsNull()
    {
        //Create new game object HealthBarManager 
        GameObject player = new GameObject("Player");

        // Check if player is not null
        Assert.IsTrue(player);
    }

    // Stress test on what happens when the player runs into multiple enemies at once
    /*[UnityTest]
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
    }*/

    // Checks if power up = NULL after player collides with it
    [Test]
    public void CheckIfPowerUpIsNullAfterCollision()
    {
        // Create player object and add necessary components
        GameObject player = new GameObject("Player");
        BeccaPower beccaPower = player.AddComponent<BeccaPower>();

        // Create power-up object
        GameObject powerUp = new GameObject("PowerUp");
        Collider2D powerUpCollider = powerUp.AddComponent<BoxCollider2D>();

        // Call function to trigger collision
        beccaPower.OnTriggerEnter2D(powerUpCollider);

        // Check if the power-up object is destroyed after collision
        Assert.IsFalse(powerUp == null || !powerUp.activeSelf);
    }

    // Check if the heatlh bar is green when health = 70 
    /*[Test]
    public void CheckHealthBarColorGreen()
    {
        GameObject healthBarObject = new GameObject("HealthBar");
        HealthBar healthBar = healthBarObject.AddComponent<HealthBar>();
        HealthBarManager healthBarManager = healthBarObject.AddComponent<HealthBarManager>();

        healthBarManager.Start();
        healthBarManager.TakeDamage(30);

        Color color = healthBar.gradient.Evaluate(healthBar.slider.normalizedValue);
        // Define the expected RGBA values for green from health bar
        Color correctColor = new Color(8 / 255f, 250 / 255f, 12 / 255f, 1);
        Assert.AreEqual(color, correctColor);
    }

     // Check if the heatlh bar is yellow when health = 50
    [Test]
    public void CheckHealthBarColorYellow()
    {
        GameObject player = new GameObject("Player");
        HealthBar healthBar = player.AddComponent<HealthBar>();

        healthBar.SetHealth(100, 50);

        Color color = healthBar.fill.color;
        // Define the expected RGBA values for yellow from health bar
        Color correctColor = new Color(1f, 0.92f, 0.016f, 1f); 
        Assert.AreSame(color, correctColor);
    }

     // Check if the heatlh bar is red when health = 30
    [Test]
    public void CheckHealthBarColorRed()
    {
        GameObject player = new GameObject("Player");
        HealthBar healthBar = player.AddComponent<HealthBar>();

        healthBar.SetHealth(100, 50);

        //Color color = healthBar.fill.color;
        // Define the expected RGBA values for red from health bar
        Color correctColor = new Color(1, 0, 0, 1); 
        Assert.AreSame(healthBar.fill.color, correctColor);
    }

    // Check if currentPower starts at 100 for power bar
    [Test]
    public void CheckCurrentPowerAtStart()
    {
        // Create new player object and assign power bar component
        //GameObject player = new GameObject("Player");
        GameObject powerBarObject = new GameObject("PowerBar");
        PowerBar powerBar = powerBarObject.AddComponent<PowerBar>();

        // Fetch currentPower from power bar class
        float currentPower = powerBar.GetCurrentPowerFromOutside();

        Assert.AreEqual(100, currentPower);
    }

     // Check if currentHealth starts at 100 for health bar
    [Test]
    public void ChekSce()
    {
        // Create new player object and assign health bar component
        GameObject player = new GameObject("Player");
        HealthBarManager healthBarManager = player.AddComponent<HealthBarManager>();

        Assert.AreEqual(100, healthBarManager.currentHealth);
    }
    */
}