/*
* Filename: PlayerMovement.cs
* 
* Developer: K Atkinson
* 
* Purpose: Script used to move player with the arrow keys or WASD keys. Speed may be modified in inspector. 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
* Summary: Class to control movement of the player        
* 
* Member variables: 
* moveSpeed, rb, movement, 
* 
*/ 
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f; 

    public Rigidbody2D rb; 

    public GameObject floatingText; 
    Vector2 movement; 

    //Good for registering input like the WASD keys 
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical");  

        movement.x += Input.acceleration.x; // Andrew: Added this components for haptic movement
        movement.y += Input.acceleration.y; // Andrew: Added this components for haptic movement

    }

    //Works like Update, called a bunch of times per second but fixed on timer and good for physics, more reliable 
    void FixedUpdate() { 
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); //constant movement speed 
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PowerUp" || other.tag == "PowerUpKetchup")
        {
            Instantiate(floatingText, transform.position, Quaternion.identity);
            Debug.Log($"Dynamic Text should pop up"); 
        }
    }
}