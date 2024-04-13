/* Implementation here is Singleton.
* The reason why we need it here is because we don't want multiple instances of a SceneManager.
* That can cause chaos with changing scenes, so we want to make sure there's only one active at a time.
*
*
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

            string currentScene = SceneManager.GetActiveScene().name;

            if ((currentScene == "Level1") && (killedCount > 0)) //Added killedCount checks - K 
            { 
                nextScene = "Level2"; 
            }
            else if (currentScene == "Level2" && (killedCount > 4))
            {
                nextScene = "Level3";
            }
            else if (currentScene == "Level3" && (killedCount > 7))
            {
                nextScene = "Level4";
            }
            else if (currentScene == "Level4")
            {
                nextScene = "MainMenu"; 
            }
                SceneManager.LoadScene(nextScene);
            }
    } 
}
