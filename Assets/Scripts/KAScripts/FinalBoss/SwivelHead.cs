/* This codebase serves to create a customizable side to side automatic shooter player for a top-down 2D game.*/ 
/* 
* Filename: SwivelHead.cs 
* Developer: Kay Atkinson
* Purpose: Script to swivel the weapon between certain angles   
*/ 
using UnityEngine;

/* 
* Summary: Class used to swivel a game object (weapon) between two angle coordinates      
* 
* Member variables: 
* minAngle, maxAngle, swivelSpeed, swivelingClockwise 
* 
*/ 
public class SwivelBetweenAngles : MonoBehaviour
{
    [SerializeField] public float minAngle = 100f; // Minimum angle
    [SerializeField] public float maxAngle = 240f; // Maximum angle
    [SerializeField] public float swivelSpeed = 30f; // Swivel speed in degrees per second

    private bool swivelingClockwise = true; // Flag to track swiveling direction


    //Update function called normally to perform the swivel action every frame 
    void Update()
    {
        // Calculate the target angle based on the current swivel direction
        float targetAngle = -(swivelingClockwise ? maxAngle : minAngle);

        // Calculate the new rotation angle using LerpAngle for smooth transition
        float newAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, swivelSpeed * Time.deltaTime);

        // Apply the new rotation
        transform.eulerAngles = new Vector3(0f, 0f, newAngle);

        // Check if the object reaches the target angle, then change the swivel direction
        if (Mathf.Abs(newAngle - targetAngle) < 0.1f)
        {
            swivelingClockwise = !swivelingClockwise;
        }
    }
}
