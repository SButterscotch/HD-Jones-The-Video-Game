using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticMovementScript : MonoBehaviour
{
    Rigidbody2D rb;
    float dirX;
    float dirY;
    float moveSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.acceleration.x * moveSpeed;
        dirY = Input.acceleration.y * moveSpeed;
        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2 (dirX, dirY);
    }
}