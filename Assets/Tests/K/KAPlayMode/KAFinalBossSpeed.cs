using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class FinalBossSpeed
{
    //Assign 
    private GameObject finalBoss;
    private Rigidbody2D finalBossRb;
    private GameObject obstacle;

    [SetUp]
    public void Setup()
    {
        // load dummy scene
        SceneManager.LoadScene("KTestingScene");
    }

    //Act 
    [UnityTest]
    public IEnumerator TestFinalBossSpeedOnCollision()
    {
        // Load or instantiate the FinalBoss prefab
        finalBoss = GameObject.FindGameObjectWithTag("FinalBoss");
        finalBossRb = finalBoss.GetComponent<Rigidbody2D>();

        // Load or instantiate the obstacle prefab
        obstacle = GameObject.FindGameObjectWithTag("Obstacle");

        // Set FinalBoss's velocity to a high speed (you can adjust this according to your needs)
        finalBossRb.velocity = Vector2.up * 100000f;

        // Adjust collider positions to ensure collision
        CircleCollider2D finalBossCollider = finalBoss.GetComponent<CircleCollider2D>();
        BoxCollider2D obstacleCollider = obstacle.GetComponent<BoxCollider2D>();
        Vector2 offset = new Vector2(0f, 10f); // Adjust offset according to collider size
        finalBossCollider.offset += offset;
        obstacleCollider.offset -= offset;

        // Wait for FinalBoss to collide with obstacle
        yield return new WaitUntil(() => finalBoss.GetComponent<Collider2D>().IsTouching(obstacle.GetComponent<Collider2D>()));

        // Check the FinalBoss's speed upon collision
        float finalBossSpeed = finalBossRb.velocity.magnitude;
        Debug.Log("FinalBoss Speed on Collision: " + finalBossSpeed);

        //Assert
        // The FinalBoss's speed is within an acceptable range
        Assert.LessOrEqual(finalBossSpeed, 100000f); // Adjust the maximum speed limit as needed
    }
}

