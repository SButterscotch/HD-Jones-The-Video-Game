using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,
        Moving
    }

    private Rigidbody2D rb;
    private Animator animator;
    private EnemyState currentState = EnemyState.Idle;

    private Transform playerTransform; // Reference to the player's transform
    public float detectionRange = 0.01f; // Distance within which the enemy detects the player

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Assuming you have a reference to the player's transform, set it here
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                UpdateIdleState();
                break;
            case EnemyState.Moving:
                UpdateMovingState();
                break;
        }
    }

    // Update method for the Idle state
    private void UpdateIdleState()
    {
        // Check if player is within detection range and transition to Moving state if true
        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) < detectionRange)
        {
            currentState = EnemyState.Moving;
        }

        // Set animator parameters for Idle state
        animator.SetBool("MovingUp", false);
        animator.SetBool("MovingDown", false);
        animator.SetBool("MovingLeft", false);
        animator.SetBool("MovingRight", false);
        animator.SetBool("Idle", true);
    }

    // Update method for the Moving state
    private void UpdateMovingState()
    {
        // Check if player is no longer within detection range and transition to Idle state if true
        if (playerTransform == null || Vector3.Distance(transform.position, playerTransform.position) >= detectionRange)
        {
            currentState = EnemyState.Idle;
            return;
        }

        // Update movement direction
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // Set animator parameters based on movement direction
        animator.SetBool("MovingUp", direction.y > 0);
        animator.SetBool("MovingDown", direction.y < 0);
        animator.SetBool("MovingLeft", direction.x < 0);
        animator.SetBool("MovingRight", direction.x > 0);
        animator.SetBool("Idle", false);
    }
}