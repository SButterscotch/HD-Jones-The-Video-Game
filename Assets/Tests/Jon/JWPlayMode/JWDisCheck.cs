using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class JWDisCheck
{
    private GameObject player;

    [SetUp]
    public void Setup()
    {
        // Load the test scene
        SceneManager.LoadScene("TestingScene");


        // Find the player object
        player = GameObject.FindGameObjectWithTag("Player");
    }

    [UnityTest]
    public IEnumerator JWDisCheckWithEnumeratorPasses()
    {
        // Access all power-ups in the scene
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        // Check if any power-up is destroyed upon collision with the player
        foreach (GameObject powerUp in powerUps)
        {
            // Move player to collide with the power-up
            player.transform.position = powerUp.transform.position;

            // Wait for physics to process collision
            yield return new WaitForFixedUpdate();

            // Check if the power-up is destroyed
            Assert.IsTrue(powerUp == null || !powerUp.activeSelf, "Power-up was not destroyed upon collision with player.");
        }
    }
}
