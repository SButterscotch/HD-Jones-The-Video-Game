using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string nextScene;

    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Level1")
        {
            nextScene = "Level2";
        }
        else if (currentScene == "Level2")
        {
            nextScene = "Level3";
        }
        else if (currentScene == "Level3")
        {
            nextScene = "Level4";
        }
        else if (currentScene == "Level4")
        {
            nextScene = "Level1"; // waiting for final menu/credits/whatever
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger Activated!!");
            SceneManager.LoadScene(nextScene);
        }
    }
}
