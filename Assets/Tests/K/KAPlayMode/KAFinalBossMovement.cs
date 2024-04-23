using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement; 


/* Tests movement of final boss between two points*/ 
public class FinalBossMovementTest
{
    
    [SetUp]
    public void Setup()
    {
        // load dummy scene
        SceneManager.LoadScene("KTestingScene");
    }
    [UnityTest]
    public IEnumerator TestFinalBossMovement()
    {
        // Load or instantiate the FinalBoss prefab
        GameObject finalBoss = GameObject.FindGameObjectWithTag("FinalBoss");
        if (finalBoss == null)
        {
            Debug.LogError("FinalBoss not found.");
            yield break;
        }

        FinalBossMovement movementScript = finalBoss.GetComponent<FinalBossMovement>();

        // Set movement speed for testing
        movementScript.moveSpeed = 5f;

        // Set top and bottom points for testing
        Vector3 topPoint = new Vector3(0f, 5f, 0f); // Example top point
        Vector3 bottomPoint = new Vector3(0f, -5f, 0f); // Example bottom point

        // Set FinalBoss position to the top point to start the test
        finalBoss.transform.position = topPoint;

        // Wait for a short time to allow the FinalBoss to move
        yield return new WaitForSeconds(1f);

        // Verify if the FinalBoss has moved to the bottom point
        Assert.AreEqual(bottomPoint, finalBoss.transform.position, "FinalBoss did not move to the bottom point.");

        // Set FinalBoss position to the top point again
        finalBoss.transform.position = topPoint;

        // Wait for a short time to allow the FinalBoss to move
        yield return new WaitForSeconds(1f);

        // Verify if the FinalBoss has moved back to the top point
        Assert.AreEqual(topPoint, finalBoss.transform.position, "FinalBoss did not move back to the top point.");
    }
}
