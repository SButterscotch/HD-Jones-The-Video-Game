using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    // Bool parameters in the Animator Controller
    private bool MovingUp;
    private bool MovingDown;
    private bool MovingLeft;
    private bool MovingRight;

    private Transform playerTransform; // Reference to the player's transform
    public float detectionRange = 10f; // Distance within which the enemy detects the player

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Assuming you have a reference to the player's transform, set it here
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        // Update the movement direction based on player's position
        UpdateMovementDirection();
        // Update the bool parameters in the Animator Controller
        UpdateAnimatorParams();
    }

    // Method to update the movement direction based on player's position
    private void UpdateMovementDirection()
    {
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;

            // Check if the player is within detection range
            if (Vector3.Distance(transform.position, playerTransform.position) < detectionRange)
            {
                // Set bool parameters based on movement direction
                MovingUp = direction.y > 0;
                MovingDown = direction.y < 0;
                MovingLeft = direction.x < 0;
                MovingRight = direction.x > 0;
            }
            else
            {
                // Player is not within detection range, set all movement bools to false (idle)
                MovingUp = false;
                MovingDown = false;
                MovingLeft = false;
                MovingRight = false;
            }
        }
    }

    // Update bool parameters in the Animator Controller
    private void UpdateAnimatorParams()
    {
        animator.SetBool("MovingUp", MovingUp);
        animator.SetBool("MovingDown", MovingDown);
        animator.SetBool("MovingLeft", MovingLeft);
        animator.SetBool("MovingRight", MovingRight);
    }
}