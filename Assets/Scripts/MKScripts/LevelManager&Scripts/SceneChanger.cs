/* Implementation here is Singleton.
* The reason why we need it here is because we don't want multiple instances of a SceneManager.
* That can cause chaos with changing scenes, so we want to make sure there's only one active at a time.
*
* Implemented private class for changing scenes.
*/

using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public static SceneChanger Instance { get; private set; }

    public string nextScene;

    public static int killedCount; //Retrieves Enemy.cs enemiesKilledCount variable 

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
            return; //Exit early to rpevent further execution 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger Activated!!");
            /****************Added by K for enemy kill count check*****************/ 
            killedCount = Enemy.enemiesKilledCount;
            Debug.Log($"Enemies killed = {killedCount}"); 
            /****************End K's code******************************************/ 

            NextSceneCalculator calculator = new NextSceneCalculator();
            string nextScene = calculator.CalculateNextScene(killedCount);
            if (!string.IsNullOrEmpty(nextScene))
            {
                Debug.Log("Loading next scene: " + nextScene);
                SceneManager.LoadScene(nextScene);
            }
            else
            {
                Debug.LogWarning("No next scene found or condition not met.");
            }
        }
    } 

    private class NextSceneCalculator {
        public string CalculateNextScene(int killedCount) // Kill Count Added by K
        {
            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "Level1" && killedCount > 0)
            {
                return "Level2";
            }
            else if (currentScene == "Level2" && killedCount > 4)
            {
                return "Level3";
            }
            else if (currentScene == "Level3" && killedCount > 7)
            {
                return "Level4";
            }
            else if (currentScene == "Level4")
            {
                return "MainMenu";
            }
            else
            {
                return ""; // No next scene found or condition not met
            }
        }
    }
}