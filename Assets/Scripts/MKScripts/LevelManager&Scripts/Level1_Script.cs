using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Level1 : BaseLevel
{
    public Text popupText;
    protected override void StartLevel() {
        
        if (popupText != null) {
            popupText.gameObject.SetActive(true);
        }
        Debug.Log("Initializing Level 1");
    }

    private void EndLevel() {
        Debug.Log("Ending Level 1");
    }
}