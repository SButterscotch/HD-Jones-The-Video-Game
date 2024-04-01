using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLevel : MonoBehaviour
{
    protected virtual void StartLevel() {
        Debug.Log("Starting Level");
    }
    protected virtual void EndLevel() {
        Debug.Log("Ending Level");
    }
}
