using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Meghan Initialization
    AudioManager audioManager;

    //Meghan Function
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void PlayGame()
    {
        //Meghan Code
        audioManager.PlaySFX(audioManager.Button);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
