/*
* Filename: Level4_Script.cs
* Developer: Matthew K
* Purpose: Individual script for Level 4 with dynamic binding
*/
using UnityEngine;


public class Level4 : BaseLevel
{
    protected void StartLevel() // removed override here
    {
        Debug.Log("Initializing Level 4");
        
    }
    protected override void EndLevel() {
        Debug.Log("Ending Level 4");
    }
}