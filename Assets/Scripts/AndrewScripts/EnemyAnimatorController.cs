using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Transform player;

    // Bool parameters in the Animator Controller
    private bool MovingUp;
    private bool MovingDown;
    private bool MovingLeft;
    private bool MovingRight;

    private EnemyState currentState;

    public float detectionDistance = 0.01f; // Distance to detect the player

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming player has a "Player" tag

        // Initialize the state machine with the Idle state
        currentState = new IdleState(this);
    }

    void FixedUpdate()
    {
        // Update the current state
        currentState.UpdateState();

        // Check if player is within detection distance
        if (Vector2.Distance(transform.position, player.position) < detectionDistance)
        {
            // Get the direction towards the player
            Vector2 direction = (player.position - transform.position).normalized;

            // Set bool parameters based on direction towards the player
            MovingUp = direction.y > 0;
            MovingDown = direction.y < 0;
            MovingLeft = direction.x < 0;
            MovingRight = direction.x > 0;

            // Update Animator parameters
            UpdateAnimatorParams();
        }
    }

    // Method to transition to a new state
    public void TransitionToState(EnemyState newState)
    {
        currentState = newState;
        currentState.EnterState();
    }

    // Update bool parameters in the Animator Controller
    private void UpdateAnimatorParams()
    {
        animator.SetBool("MovingUp", MovingUp);
        animator.SetBool("MovingDown", MovingDown);
        animator.SetBool("MovingLeft", MovingLeft);
        animator.SetBool("MovingRight", MovingRight);
    }

    // Define the base class for all states
    public abstract class EnemyState
    {
        protected EnemyAnimatorController controller;

        public EnemyState(EnemyAnimatorController controller)
        {
            this.controller = controller;
        }

        // Method called when entering the state
        public abstract void EnterState();

        // Method called to update the state
        public abstract void UpdateState();
    }

    // Define state classes for each movement direction
    public class IdleState : EnemyState
    {
        public IdleState(EnemyAnimatorController controller) : base(controller) { }

        public override void EnterState()
        {
            // Reset all bool parameters for idle animation
            controller.MovingUp = false;
            controller.MovingDown = false;
            controller.MovingLeft = false;
            controller.MovingRight = false;

            // Update Animator parameters
            controller.UpdateAnimatorParams();
        }

        public override void UpdateState()
        {
            // No specific action needed for Idle state
        }
    }
}