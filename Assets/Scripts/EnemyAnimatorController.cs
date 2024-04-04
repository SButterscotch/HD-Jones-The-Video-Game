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

    private EnemyState currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Initialize the state machine with the Idle state
        currentState = new IdleState(this);
    }

    void FixedUpdate()
    {
        // Update the current state
        currentState.UpdateState();
    }

    // Method to transition to a new state
    public void TransitionToState(EnemyState newState)
    {
        currentState = newState;
        currentState.EnterState();
    }

    // Check the movement direction and transition to the corresponding state
    public void CheckMovementDirection()
    {
        Vector2 velocity = rb.velocity.normalized;

        // Set bool parameters based on movement direction
        MovingUp = velocity.y > 0 && Mathf.Abs(velocity.y) > Mathf.Abs(velocity.x);
        MovingDown = velocity.y < 0 && Mathf.Abs(velocity.y) > Mathf.Abs(velocity.x);
        MovingLeft = velocity.x < 0 && Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y);
        MovingRight = velocity.x > 0 && Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y);

        // Transition to the corresponding state based on movement direction
        if (MovingUp)
            TransitionToState(new MovingUpState(this));
        else if (MovingDown)
            TransitionToState(new MovingDownState(this));
        else if (MovingLeft)
            TransitionToState(new MovingLeftState(this));
        else if (MovingRight)
            TransitionToState(new MovingRightState(this));
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
            // Set bool parameters for idle animation
            controller.MovingUp = false;
            controller.MovingDown = false;
            controller.MovingLeft = false;
            controller.MovingRight = false;

            // Update Animator parameters
            controller.UpdateAnimatorParams();
        }

        public override void UpdateState()
        {
            // Check for movement direction and transition to the corresponding state
            controller.CheckMovementDirection();
        }
    }

    // Define other state classes similarly for MovingUp, MovingDown, MovingLeft, and MovingRight
    public class MovingUpState : EnemyState
    {
        public MovingUpState(EnemyAnimatorController controller) : base(controller) { }

        public override void EnterState()
        {
            // Set bool parameter for moving up animation
            controller.MovingUp = true;
            controller.MovingDown = false;
            controller.MovingLeft = false;
            controller.MovingRight = false;

            // Update Animator parameters
            controller.UpdateAnimatorParams();
        }

        public override void UpdateState()
        {
            // Check for movement direction and transition to the corresponding state
            controller.CheckMovementDirection();
        }
    }

    public class MovingDownState : EnemyState
    {
        public MovingDownState(EnemyAnimatorController controller) : base(controller) { }

        public override void EnterState()
        {
            // Set bool parameter for moving down animation
            controller.MovingUp = false;
            controller.MovingDown = true;
            controller.MovingLeft = false;
            controller.MovingRight = false;

            // Update Animator parameters
            controller.UpdateAnimatorParams();
        }

        public override void UpdateState()
        {
            // Check for movement direction and transition to the corresponding state
            controller.CheckMovementDirection();
        }
    }

    public class MovingLeftState : EnemyState
    {
        public MovingLeftState(EnemyAnimatorController controller) : base(controller) { }

        public override void EnterState()
        {
            // Set bool parameter for moving left animation
            controller.MovingUp = false;
            controller.MovingDown = false;
            controller.MovingLeft = true;
            controller.MovingRight = false;

            // Update Animator parameters
            controller.UpdateAnimatorParams();
        }

        public override void UpdateState()
        {
            // Check for movement direction and transition to the corresponding state
            controller.CheckMovementDirection();
        }
    }

    public class MovingRightState : EnemyState
    {
        public MovingRightState(EnemyAnimatorController controller) : base(controller) { }

        public override void EnterState()
        {
            // Set bool parameter for moving right animation
            controller.MovingUp = false;
            controller.MovingDown = false;
            controller.MovingLeft = false;
            controller.MovingRight = true;

            // Update Animator parameters
            controller.UpdateAnimatorParams();
        }

        public override void UpdateState()
        {
            // Check for movement direction and transition to the corresponding state
            controller.CheckMovementDirection();
        }
    }
}
