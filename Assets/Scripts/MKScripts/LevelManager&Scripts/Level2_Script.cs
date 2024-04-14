using UnityEngine;


public class Level2 : BaseLevel
{
    protected void StartLevel() // removed override here
    {
        Debug.Log("Initializing Level 2");
        
    }
    private void EndLevel() {
        Debug.Log("Ending Level 2");
    }
}