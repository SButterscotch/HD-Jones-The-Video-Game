/* This codebase serves to create a customizable side to side automatic shooter player for a top-down 2D game.*/ 
/* 
* Filename: BulletCollisionHandler.cs 
* Developer: Kay Atkinson
* Purpose: Specific script for any bullet prefab that will handle bullet destruction when it hits something
*/ 
using UnityEngine; 

/* 
* Summary: Class that handles bullet destruction/collision behavior  
* 
* Member variables: 
* Game object with tags "Obstacle" or "obstacle" <- this is buildable  
*
*/ 
public class BulletCollisionHandler : MonoBehaviour
{
    /* 
    * Summary: Unity built-in function for objects with 2D colliders  
    * 
    * Parameters: Game object labelled "other" for the object hit  
    * 
    * Returns: None  
    */ 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("obstacle"))
        {
            Destroy(gameObject);
        }
    }
}

