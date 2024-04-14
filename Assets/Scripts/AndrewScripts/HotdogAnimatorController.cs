/*
* HotdogAnimatorController.cs
* Andrew Bonilla
* This script controls player animations depending on movement
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotdogAnimatorController : MonoBehaviour
{
    // Public variable for movement speed
    public float moveSpeed = 5f; // Speed of movement

    // Private variables for Rigidbody, Animator, and current state
    private Rigidbody2D rb;
    private Animator animator;
    private HotdogState currentState;

    // Bool parameters in the Animator Controller
    private bool RunningUp;
    private bool RunningDown;
    private bool RunningLeft;
    private bool RunningRight;

    // Called when the script is initialized
    void Start()
    {
        // Getting references to Rigidbody and Animator components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // Setting the initial state to Idle
        currentState = new IdleState(this);
    }

    // Called every frame
    void Update()
    {
        // Handling input for the current state
        currentState.HandleInput();
    }

    // Called at a fixed interval for physics calculations
    void FixedUpdate()
    {
        // Updating the current state and animator parameters
        currentState.Update();
        UpdateAnimatorParams();
    }

    // Method to set the current state
    public void SetState(HotdogState state)
    {
        currentState = state;
        currentState.EnterState();
    }

    // Getter method for Rigidbody
    public Rigidbody2D GetRigidbody()
    {
        return rb;
    }

    // Getter method for Animator
    public Animator GetAnimator()
    {
        return animator;
    }

    // Getter method for movement speed
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    // Update bool parameters in the Animator Controller
    private void UpdateAnimatorParams()
    {
        animator.SetBool("RunningUp", RunningUp);
        animator.SetBool("RunningDown", RunningDown);
        animator.SetBool("RunningLeft", RunningLeft);
        animator.SetBool("RunningRight", RunningRight);
    }

    // Methods to set bool parameters
    public void SetRunningUp(bool value)
    {
        RunningUp = value;
    }

    public void SetRunningDown(bool value)
    {
        RunningDown = value;
    }

    public void SetRunningLeft(bool value)
    {
        RunningLeft = value;
    }

    public void SetRunningRight(bool value)
    {
        RunningRight = value;
    }
}

// Abstract class representing different states of the hotdog
// Example for dynamic binding 
//----------------------------------------------------------//
/* Static binding example
HotdogState staticState = new IdleState(); // Static type is HotdogState
staticState.EnterState(); // Calls EnterState() method of HotdogState

   Dynamic binding example
HotdogState dynamicState = new MovingRightState(); // Dynamic type is MovingRightState
dynamicState.EnterState(); // Calls EnterState() method of MovingRightState
*/
public abstract class HotdogState
{
    protected HotdogAnimatorController controller;

    // Constructor
    public HotdogState(HotdogAnimatorController controller)
    {
        this.controller = controller;
    }

    // Methods to be overridden by subclasses
    // Virtual methods - a function that can be overridden by a derived class. 
    // - If a function is declard as a virtual base class, it allows a subclass to provide a specific implementation of that function. 
    public virtual void EnterState() { }
    public virtual void HandleInput() { }
    public virtual void Update() { }
}

// Subclass representing the idle state of the hotdog
public class IdleState : HotdogState
{
    // Constructor
    public IdleState(HotdogAnimatorController controller) : base(controller) { }

    // Method called when entering the state
    public override void EnterState()
    {
        // Set bool parameter for idle animation
        controller.SetRunningUp(false);
        controller.SetRunningDown(false);
        controller.SetRunningLeft(false);
        controller.SetRunningRight(false);
    }

    // Method to handle input
    public override void HandleInput()
    {
        // Check for input to transition to other states
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float hapticHorizontalInput = Input.acceleration.x;
        float hapticVerticalInput = Input.acceleration.y;

        if ((Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f) ||
            (Mathf.Abs(hapticHorizontalInput) > 0.1f || Mathf.Abs(hapticVerticalInput) > 0.1f))
        {
            if (horizontalInput > 0 || hapticHorizontalInput > 0)
                controller.SetState(new MovingRightState(controller));
            else if (horizontalInput < 0 || hapticHorizontalInput < 0)
                controller.SetState(new MovingLeftState(controller));
            else if (verticalInput > 0 || hapticVerticalInput > 0)
                controller.SetState(new MovingUpState(controller));
            else if (verticalInput < 0 || hapticVerticalInput < 0)
                controller.SetState(new MovingDownState(controller));
        }
    }
}

// Subclass representing the state of moving right
public class MovingRightState : HotdogState
{
    // Constructor
    public MovingRightState(HotdogAnimatorController controller) : base(controller) { }

    // Method called when entering the state
    public override void EnterState()
    {
        // Set bool parameter for running right animation
        controller.SetRunningRight(true);
        controller.SetRunningUp(false);
        controller.SetRunningDown(false);
        controller.SetRunningLeft(false);
    }

    // Method to handle input
    public override void HandleInput()
    {
        // Check for input to transition to idle state
        float horizontalInput = Input.GetAxis("Horizontal");
        float hapticHorizontalInput = Input.acceleration.x;

        if (Mathf.Abs(horizontalInput) < 0.1f && Mathf.Abs(hapticHorizontalInput) < 0.1f)
            controller.SetState(new IdleState(controller));
    }

    // Method to update movement
    public override void Update()
    {
        controller.GetRigidbody().velocity = Vector2.right * controller.GetMoveSpeed();
    }
}


public class MovingLeftState : HotdogState
{
    // Constructor
    public MovingLeftState(HotdogAnimatorController controller) : base(controller) { }

    // Method called when entering the state
    public override void EnterState()
    {
        // Set bool parameter for running left animation
        controller.SetRunningRight(false);
        controller.SetRunningUp(false);
        controller.SetRunningDown(false);
        controller.SetRunningLeft(true);
    }

    // Method to handle input
    public override void HandleInput()
    {
        // Check for input to transition to idle state
        float horizontalInput = Input.GetAxis("Horizontal");
        float hapticHorizontalInput = Input.acceleration.x;

        if (Mathf.Abs(horizontalInput) < 0.1f && Mathf.Abs(hapticHorizontalInput) < 0.1f)
            controller.SetState(new IdleState(controller));
    }

    // Method to update movement
    public override void Update()
    {
        controller.GetRigidbody().velocity = Vector2.left * controller.GetMoveSpeed();
    }
}

public class MovingUpState : HotdogState
{
    // Constructor
    public MovingUpState(HotdogAnimatorController controller) : base(controller) { }

    // Method called when entering the state
    public override void EnterState()
    {
        // Set bool parameter for running up animation
        controller.SetRunningRight(false);
        controller.SetRunningUp(true);
        controller.SetRunningDown(false);
        controller.SetRunningLeft(false);
    }

    // Method to handle input
    public override void HandleInput()
    {
        // Check for input to transition to idle state
        float verticalInput = Input.GetAxis("Vertical");
        float hapticVerticalInput = Input.acceleration.y;

        if (Mathf.Abs(verticalInput) < 0.1f && Mathf.Abs(hapticVerticalInput) < 0.1f)
            controller.SetState(new IdleState(controller));
    }

    // Method to update movement
    public override void Update()
    {
        controller.GetRigidbody().velocity = Vector2.up * controller.GetMoveSpeed();
    }
}

public class MovingDownState : HotdogState
{
    // Constructor
    public MovingDownState(HotdogAnimatorController controller) : base(controller) { }

    // Method called when entering the state
    public override void EnterState()
    {
        // Set bool parameter for running down animation
        controller.SetRunningRight(false);
        controller.SetRunningUp(false);
        controller.SetRunningDown(true);
        controller.SetRunningLeft(false);
    }

    // Method to handle input
    public override void HandleInput()
    {
        // Check for input to transition to idle state
        float verticalInput = Input.GetAxis("Vertical");
        float hapticVerticalInput = Input.acceleration.y;

        if (Mathf.Abs(verticalInput) < 0.1f && Mathf.Abs(hapticVerticalInput) < 0.1f)
            controller.SetState(new IdleState(controller));
    }

    // Method to update movement
    public override void Update()
    {
        controller.GetRigidbody().velocity = Vector2.down * controller.GetMoveSpeed();
    }
}