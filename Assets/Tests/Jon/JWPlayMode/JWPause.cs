using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;


public class JWPauseTest
{
    [SetUp]
    public void Setup()
    {
        // Load the test scene
        SceneManager.LoadScene("Level 1");

    }

    [UnityTest]
    public IEnumerator JWPauseWithEnumeratorPasses()
    {

        // Wait until the escape key is pressed to pause the game
        while (!Input.GetKeyDown(KeyCode.Escape))
        {
            yield return null;
        }
        // Pause the game
        Time.timeScale = 0;

        // Check if time is 0 when paused
        Assert.AreEqual(0, Time.timeScale);

        // Resume the game
        Time.timeScale = 1;

        // Wait for one frame to allow time to resume
        yield return null;

        // Check if time is 1 when resumed
        Assert.AreEqual(1, Time.timeScale);
    }
}