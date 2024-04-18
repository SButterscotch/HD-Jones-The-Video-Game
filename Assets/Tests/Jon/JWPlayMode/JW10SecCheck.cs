using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class JW10SecCheck
{
    [SetUp]
    public void Setup()
    {
        // Load the test scene
        SceneManager.LoadScene("TestingScene");

    }

    [UnityTest]
    public IEnumerator JW10SecCheckWithEnumeratorPasses()
    {
        // Wait for 10 seconds
        yield return new WaitForSeconds(10);

        // Access all power-ups in the scene
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        // Check if any power-ups are missing
        if (powerUps.Length == 0)
        {
            Assert.Pass("No power-ups are missing after 10 seconds.");
        }
        else
        {
            string missingPowerUps = "";
            foreach (GameObject powerUp in powerUps)
            {
                missingPowerUps += powerUp.name + ", ";
            }
            Assert.Fail($"The following power-ups are missing after 10 seconds: {missingPowerUps}");
        }
    }
}
