// using System.Collections;
// using System.Collections.Generic;
// //using System.Numerics;
// using UnityEngine;
// using UnityEngine.Diagnostics;
// using CodeMonkey.Utils;
// using System;//free utility, using their find mouse and follow mouse methods  
using System.Collections;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bullet; 
    [SerializeField] 
    private Transform BulletDirection;
    private Transform aimTransform;
    private Camera main;
    private bool canShoot = true;
    [SerializeField] private float coolDownTime = 0.5f;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        main = Camera.main;
    }

    private void Update()
    {
        HandleAiming();
        if (Input.GetMouseButtonDown(0))
        {
            PlayerShoot();
        }
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void PlayerShoot()
    {
        if (!canShoot)
        {
            return;
        }
        GameObject g = Instantiate(bullet, BulletDirection.position, aimTransform.rotation);
        g.SetActive(true);
        StartCoroutine(CanShoot());
    }

    private IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(coolDownTime);
        canShoot = true;
    }
}


// public class PlayerAimWeapon : MonoBehaviour
// {
//     private Transform aimTransform;
//     private Transform aimDirection; 
//     // public event EventHandler<OnShootEventArgs> OnShoot;  

//     // public class OnShootEventArgs : EventArgs { 
//     //     public Vector3 firePoint; 
//     //     public Vector3 shootPosition; 
//     //}

//     private void Awake() { 
//         aimTransform = transform.Find("Aim"); 
//     }

//     private void Update(){ 
//         HandleAiming(); 
//        // HandleShooting(); 
//     }

//     public void HandleAiming(){ 
//         Vector3 mousePosition = UtilsClass.GetMouseWorldPosition(); 
//         Vector3 aimDirection = (mousePosition - transform.position).normalized; 
//         float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg; 
//         aimTransform.eulerAngles = new Vector3(0, 0, angle); 
//         Debug.Log(angle); 
//     }

//     // private void HandleShooting(){ 
//     //     if (Input.GetMouseButtonDown(0)) { 
//     //         Vector3 mousePosition = UtilsClass.GetMouseWorldPosition(); 

//     //         OnShoot?.Invoke(this, new OnShootEventArgs { 
//     //             firePoint = aimTransform.position,
//     //             shootPosition = mousePosition, 
//     //         }); 
//     //     }
//     }

    
