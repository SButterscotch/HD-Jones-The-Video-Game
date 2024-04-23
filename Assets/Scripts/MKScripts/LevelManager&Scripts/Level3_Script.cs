/*
* Filename: Level3_Script.cs
* Developer: Matthew K
* Purpose: Individual script for Level 3 with dynamic binding
*/
using UnityEngine;


public class Level3 : BaseLevel
{
    protected void StartLevel() // removed override here
    {
        Debug.Log("Initializing Level 3");
        
    }
    private void EndLevel() {
        Debug.Log("Ending Level 3");
    }
}