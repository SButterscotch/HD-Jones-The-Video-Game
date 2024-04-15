using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class JWBoundIn
{
    private Camera mainCamera;

    [SetUp]
    public void Setup()
    {
        // Load the test scene
        SceneManager.LoadScene("Level 1");
    }

    [UnityTest]
    public IEnumerator AllPowerUpsWithinBoundsTest()
    {
        // Access all power-ups in the scene
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");

        // Get the camera bounds
        mainCamera = Camera.main;
        Bounds cameraBounds = CameraBounds(mainCamera);

        // Check if all power-ups are within camera bounds
        foreach (GameObject powerUp in powerUps)
        {
            if (!cameraBounds.Contains(powerUp.transform.position))
            {
                Assert.Fail($"Power-up {powerUp.name} is not within camera bounds!");
            }
        }

        yield return null;
    }

    private Bounds CameraBounds(Camera camera)
    {
        float cameraHeight = 2f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;

        Vector3 cameraCenter = camera.transform.position;
        Bounds cameraBounds = new Bounds(cameraCenter, new Vector3(cameraWidth, cameraHeight, 0));

        return cameraBounds;
    }
}

