using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{

    // Define a method to handle collision with the player
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Perform actions when the player collides with the obstacle
            Debug.Log("Player collided with " + gameObject.name);
        }
    }
}

