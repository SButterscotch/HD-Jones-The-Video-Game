using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseLevel : MonoBehaviour
{

    private static BaseLevel instance;

    // Public property to access the singleton instance
    public static BaseLevel Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        /*instance = this;

        DontDestroyOnLoad(gameObject); */
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // This method will be called whenever a scene is loaded
        // You can perform any necessary initialization here
        StartLevel();
    }

    protected virtual void StartLevel() //virtual goes here
    {
        Debug.Log("BASE LEVEL Starting Level");

        // Activate the GameObject
        gameObject.SetActive(true);
    }

    protected virtual void EndLevel()
    {
        Debug.Log("Ending Level");

        // Deactivate the GameObject
        gameObject.SetActive(false);
    }
}