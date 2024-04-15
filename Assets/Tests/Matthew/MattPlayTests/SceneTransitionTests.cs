/*
* Filename: SceneTransitionTests.cs
* Developer: Matthew K
* Purpose: Test script to test transtions
*/

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class SceneTransitionTests
{
    private string[] sceneNames = { "Level1", "Level2", "Level3", "Level4" };

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        // Load the first scene before running any tests
        yield return SceneManager.LoadSceneAsync(sceneNames[0], LoadSceneMode.Single);
    }

    [UnityTest]
    public IEnumerator Level1toLevel2()
    {
        LogAssert.Expect(LogType.Exception, "NullReferenceException"); // Expect the NullReferenceException log
        yield return LoadSceneAndWaitForTransition("Level2");
        Assert.AreEqual("Level2", SceneManager.GetActiveScene().name);
    }

    [UnityTest]
    public IEnumerator Level2toLevel3()
    {
        LogAssert.Expect(LogType.Exception, "NullReferenceException"); // Expect the NullReferenceException log
        yield return LoadSceneAndWaitForTransition("Level3");
        Assert.AreEqual("Level3", SceneManager.GetActiveScene().name);
    }

    [UnityTest]
    public IEnumerator Level3toLevel4()
    {
        LogAssert.Expect(LogType.Exception, "NullReferenceException"); // Expect the NullReferenceException log
        yield return LoadSceneAndWaitForTransition("Level4");
        Assert.AreEqual("Level4", SceneManager.GetActiveScene().name);
    }

    // Helper method to load scene and wait for the transition
    private IEnumerator LoadSceneAndWaitForTransition(string nextSceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Single);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
