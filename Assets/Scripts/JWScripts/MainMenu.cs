using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SwitchScene(string sceneName){
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void LeaveTheGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void MenuActivateDRBCMode(bool value){
        if (value == false)
        {
            int boolValue = 0; //setDrBCMode ? 0 : 1;
            PlayerPrefs.SetInt("setDrBCMode", boolValue);
        }
 
    }
    /*public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }*/
}
