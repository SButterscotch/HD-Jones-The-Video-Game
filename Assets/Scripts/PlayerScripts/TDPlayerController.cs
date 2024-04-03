using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils; 

public class TDPlayerController : MonoBehaviour
{
    //SerializeField for customization 
    [SerializeField] private GameObject bullet; 
    [SerializeField] 
    private float movementVelocity = 3f; 
    [SerializeField] 
    private Transform BulletDirection; //no math involved! 
    // [SerializeField] 
    // private Transform BulletCollector; 

    //Referencing our action map 
    private TDActions controls;
    //private Transform aimTransform;
    private Camera main; 
    private bool canShoot = true; //for if the Player can shoot bullets  

    [SerializeField]
    private float coolDownTime = .5f; 

    // [SerializeField]
    // private Transform weaponPivot; 
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
        main = Camera.main; 
        controls.Player.Shoot.performed += _ => PlayerShoot(); //underscore used bc we're not passing in a value
    }

    private void PlayerShoot(){ 
        if (!canShoot){ 
            return; 
        }
        Vector2 mousePosition = controls.Player.MousePosition.ReadValue<Vector2>(); 
        //Vector2 mousePosition = Input.mousePosition; 
        mousePosition = main.ScreenToWorldPoint(mousePosition); 
        //Instantiate bullet game object 
        GameObject g = Instantiate(bullet, BulletDirection.position, BulletDirection.rotation); //ransform.BulletCollector); 
        g.SetActive(true); 
        StartCoroutine(CanShoot()); 

    }

    IEnumerator CanShoot(){ //effective to a cool down period 
        canShoot = false; 
        yield return new WaitForSeconds(coolDownTime); //wait for 3 seconds before we can shoot again 
        canShoot = true; 
    }
    void Update()
    {
        //PlayerMovement(); 
        
    }

    private void PlayerMovement(){ 
        //Movement 
        Vector3 movement = controls.Player.Movement.ReadValue<Vector2>() * movementVelocity; 
        transform.position += movement * Time.deltaTime; 
    }

}
