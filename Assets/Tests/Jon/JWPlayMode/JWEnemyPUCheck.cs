using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;


public class JWEnemyPUCheck
{
    private GameObject enemy;

    [SetUp]
    public void Setup()
    {
        // Load the test scene
        SceneManager.LoadScene("TestingScene");


        // Find the enemy object
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    [UnityTest]
    public IEnumerator JWEnemyPUCheckWithEnumeratorPasses()
    {
        // Access all power-ups in the scene
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        foreach (GameObject powerUp in powerUps)
        {
            // Move enemy to collide with the power-up
            enemy.transform.position = powerUp.transform.position;

            // Wait for physics to process collision
            yield return new WaitForFixedUpdate();

            // Check if the power-up is destroyed
            Assert.IsTrue(powerUp == null || !powerUp.activeSelf, "Power-up was not destroyed upon collision with enemy.");
        }
    }
}

