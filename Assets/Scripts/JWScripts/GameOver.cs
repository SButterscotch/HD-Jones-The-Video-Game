using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverUI;

    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void goHome()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
