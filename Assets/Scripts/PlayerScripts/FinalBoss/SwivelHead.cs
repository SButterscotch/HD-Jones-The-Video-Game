using UnityEngine;

public class SwivelBetweenAngles : MonoBehaviour
{
    [SerializeField] private float minAngle = 100f; // Minimum angle
    [SerializeField] private float maxAngle = 240f; // Maximum angle
    [SerializeField] private float swivelSpeed = 30f; // Swivel speed in degrees per second

    private bool swivelingClockwise = true; // Flag to track swiveling direction

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
