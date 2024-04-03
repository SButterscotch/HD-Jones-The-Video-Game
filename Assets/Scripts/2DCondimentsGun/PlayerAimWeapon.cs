using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.Diagnostics;
using CodeMonkey.Utils;
using System;//free utility, using their find mouse and follow mouse methods  


public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransform;
    private Transform firePoint; 
    public event EventHandler<OnShootEventArgs> OnShoot;  

    public class OnShootEventArgs : EventArgs { 
        public Vector3 firePoint; 
        public Vector3 shootPosition; 
    }

    private void Awake() { 
        aimTransform = transform.Find("Aim"); 
    }

    private void Update(){ 
        HandleAiming(); 
        HandleShooting(); 
    }

    private void HandleAiming(){ 
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition(); 
        Vector3 aimDirection = (mousePosition - transform.position).normalized; 
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg; 
        aimTransform.eulerAngles = new Vector3(0, 0, angle); 
        Debug.Log(angle); 
    }

    private void HandleShooting(){ 
        if (Input.GetMouseButtonDown(0)) { 
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition(); 
            
            OnShoot?.Invoke(this, new OnShootEventArgs { 
                firePoint = aimTransform.position,
                shootPosition = mousePosition, 
            }); 
        }
    }

    

}
