using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDPlayerController : MonoBehaviour
{
    //SerializeField for customization 
    [SerializeField] private GameObject bullet; 
    [SerializeField] 
    private Transform BulletDirection; //no math involved! 
    // [SerializeField] 
    // private Transform BulletCollector; 

    //Referencing our action map 
    private TDActions controls; 

    private void Awake(){ 
        controls = new TDActions(); 
    }

    private void OnEnable(){ 
        controls.Enable(); 
    }

    private void OnDisable() { 
        controls.Disable(); 
    }
    void Start()
    {
        controls.Player.Shoot.performed += _ => PlayerShoot(); //underscore used bc we're not passing in a value
    }

    private void PlayerShoot(){ 
        Vector2 mousePosition = controls.Player.MousePosition.ReadValue<Vector2>(); 
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition); 
        //Instantiate bullet game object 
        GameObject g = Instantiate(bullet, BulletDirection.position, BulletDirection.rotation); //ransform.BulletCollector); 
        g.SetActive(true); 

    }
    void Update()
    {
        
    }
}
