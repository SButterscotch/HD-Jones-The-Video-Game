// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using CodeMonkey.Utils;
// using UnityEngine.Diagnostics;

// public class TestingAim : MonoBehaviour
// {
//     [SerializeField] private PlayerAimWeapon playerAimWeapon; 

//     private void Start(){ 
//         playerAimWeapon.OnShoot += PlayerAimWeapon_OnShoot; 
//     }
//     private void PlayerAimWeapon_OnShoot(object sender, PlayerAimWeapon.OnShootEventArgs e){
//         UtilsClass.ShakeCamera(1f, .2f); 
//         WeaponTracer.Create(e.firePoint, e.shootPosition);
//         Shoot_Flash.AddFlash(e.firePoint);  
//     }
// }
