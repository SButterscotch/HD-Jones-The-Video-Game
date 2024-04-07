using UnityEngine;

public class HapticController : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to control the player's movement speed
    public float tiltSensitivity = 0.1f; // Adjust this to control the sensitivity of tilt controls
    public float obstacleCollisionDuration = 0.2f; // Duration of haptic feedback when colliding with an obstacle

    private Rigidbody2D rb;
    private float xAxis, yAxis;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Check if the current platform is Android or iOS
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            // Enable tilt controls only for Android and iOS
            enabled = true;
        }
        else
        {
            // Disable tilt controls for other platforms
            enabled = false;
        }
    }

    void Update()
    {
        // Read accelerometer data to control player movement
        xAxis = Input.acceleration.x;
        yAxis = Input.acceleration.y;

        // Apply movement based on accelerometer data
        Vector2 movement = new Vector2(xAxis, yAxis) * moveSpeed * tiltSensitivity * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collided with an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Trigger haptic feedback (vibration)
            Vibration.Vibrate(obstacleCollisionDuration);

            // Optionally, add other collision-related logic here (e.g., game over, score deduction, etc.)
        }
    }*/
}