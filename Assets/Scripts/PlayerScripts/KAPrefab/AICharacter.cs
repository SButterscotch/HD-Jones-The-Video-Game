using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Rotation speed of the enemy
    public float rotationSpeed = 30f; // degrees per second

    // Direction of rotation (-1 for left, 1 for right)
    protected int rotationDirection = 1;

    // Update is called once per frame
    protected virtual void Update()
    {
        // Rotate the enemy
        Rotate();

        // Check if the enemy should change direction
        if (ShouldChangeDirection())
        {
            // Change rotation direction
            rotationDirection *= -1;
        }
    }

    // Method to rotate the enemy
    protected virtual void Rotate()
    {
        transform.Rotate(Vector3.forward, rotationDirection * rotationSpeed * Time.deltaTime);
    }

    // Method to check if the enemy should change direction
    protected virtual bool ShouldChangeDirection()
    {
        // Implement your logic here to determine when the enemy should change direction
        // For example, you could check if the enemy has reached the edge of a platform

        return false; // Default behavior: do not change direction
    }
}

public class AIFinalBoss : Character
{
    // Total rotation angle of the final boss
    private float totalRotationAngle = 180f; // degrees

    // Shooting interval
    public float shootingInterval = 1f; // seconds
    private float lastShootTime;

    // Method to check if the enemy should change direction
    protected override bool ShouldChangeDirection()
    {
        // Override the base method to prevent the final boss from changing direction
        return false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        // Check if the final boss has rotated the total required angle
        if (Mathf.Abs(transform.rotation.eulerAngles.z) >= totalRotationAngle)
        {
            // Stop rotating
            rotationSpeed = 0f;
        }

        // Call the parent class's update method
        base.Update();

        // Check if it's time to shoot
        if (Time.time - lastShootTime >= shootingInterval)
        {
            // Shoot football
            Shoot();

            // Update last shoot time
            lastShootTime = Time.time;
        }
    }

    // Method to shoot
    private void Shoot()
    {
        Debug.Log("Final Boss shoots");
        // Implement your shoot logic here
    }
}
