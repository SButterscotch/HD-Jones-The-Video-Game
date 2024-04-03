using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    private float speed = 4f; 
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.Translate(Vector3.up * speed *Time.deltaTime); 
    }
}
