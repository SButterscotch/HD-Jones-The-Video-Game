/*
* Filename: Level1_Script.cs
* Developer: Matthew K
* Purpose: Individual script for Level 1 with dynamic binding
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : BaseLevel
{
    public AudioClip popupSound;
    private AudioSource audioSource;

    protected void StartLevel() { // removed override here
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = popupSound;
        audioSource.Play();
        Debug.Log("Initializing Level 1");
    }

    private void EndLevel() {
        Debug.Log("Ending Level 1");
    }
}