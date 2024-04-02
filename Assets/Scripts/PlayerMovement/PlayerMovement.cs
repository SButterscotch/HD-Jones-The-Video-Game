using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; 

    public Rigidbody2D rb; 

    Vector2 movement; 

    // Update is called once per frame
    void Update()
    {
        //Good for registering input!
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical");  
    }

    void FixedUpdate() { 
        //Works like Update, called a bunch of times per second but fixed on timer and good for physics, more reliable 
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); //constant movement speed 
         
    }
}
