/* Implementation here is Singleton.
* The reason why we need it here is because we don't want multiple instances of a SceneManager.
* That can cause chaos with changing scenes, so we want to make sure there's only one active at a time.
*
*
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public static SceneChanger Instance { get; private set; }

    public string nextScene;

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Destroyed the object");
            Destroy(gameObject);
        }

        
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
            nextScene = "MainMenu"; 
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
