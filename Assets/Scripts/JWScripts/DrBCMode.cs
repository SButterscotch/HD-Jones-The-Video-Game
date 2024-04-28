using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class DrBCMode : MonoBehaviour
{
    //public HealthBarManager healthBarManager;
    public bool setDrBCMode = false;
    /*public void ActivateDRBCMode()
    {
        //setDrBCMode = true;
        int boolValue = setDrBCMode ? 1 : 0;
        PlayerPrefs.SetInt("setDrBCMode", boolValue);
        
        //healthBarManager.drBC = true;
        //Debug.Log($"It is {healthBarManager.drBC}");

        //setDrBC = true;
        Debug.Log($"It is {setDrBCMode}");
    }*/

    public void ActivateDRBCMode(bool value){
        if (value)
        {
            setDrBCMode = true;
            int boolValue = setDrBCMode ? 1 : 0;
            PlayerPrefs.SetInt("setDrBCMode", boolValue);
        }
        else 
        {
            setDrBCMode = false;
            int boolValue = setDrBCMode ? 1 : 0;
            PlayerPrefs.SetInt("setDrBCMode", boolValue);
        }
    }
}
