/*
* Filename: WeaponPrefab.cs
* Developer: K Atkinson
* Purpose: Script used for the 2DCondimentsWeapon prefab. Bullets and customization included.   
*/
using System.Collections;
using UnityEngine;
//using Playerspace;
public class WeaponPrefab : MonoBehaviour
{
    // Reference to the player controller how? 
    //private PlayerController playerController;
    public Transform firePoint;
    public bool isFiring = false; 

    //will need to customize these with SERIALIZEDFIELD
    //HOW DO I GET THIS SCRIPT TO FIRE ON IT'S OWN WITHOUT DOING IT INSIDE PLAYERCONTROLLER? 
    [SerializeField] public GameObject bulletPrefab; //may change to color? 
    [SerializeField] public float fireForce = 20f; 
    [SerializeField] public float  clusterNumber = 20f; //how to change how many are fired at once?  
    [SerializeField] public float bulletSize = 20f; 
    [SerializeField] public float bulletLifetime = 3f; 

    // Initialize the weapon with the player controller reference
    // public void Initialize(PlayerController controller)
    // {
    //     playerController = controller;
    // }

    public void StartFiring()
    {
        isFiring = true;
        //playerController.StartCoroutine(FireCoroutine());
    }

    public void StopFiring()
    {
        isFiring = false;
    }

    public void Fire()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);

        // Change to: Destroy bullet when it hits boundaries 
        GameObject.Destroy(bullet, bulletLifetime);
    }

    private IEnumerator FireCoroutine()
    {
        while (isFiring)
        {
            Fire();
            yield return new WaitForSeconds(0.1f); // Adjust the delay between shots
        }
    }
}
