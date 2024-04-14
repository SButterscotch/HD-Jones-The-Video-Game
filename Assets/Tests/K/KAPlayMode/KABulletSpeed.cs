using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BulletSpeedTest
{
    [UnityTest]
    public IEnumerator TestBulletSpeed()
    {
        // Load or instantiate the FBWeapon prefab
        GameObject fbWeapon = GameObject.FindGameObjectWithTag("Weapon");
        FBWeapon fbWeaponScript = fbWeapon.GetComponent<FBWeapon>();

        // Start firing bullets automatically
        fbWeaponScript.Fire();

        // Wait for a few seconds to allow bullets to be fired
        yield return new WaitForSeconds(3f);

        // Check the speed of the last fired bullet
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        if (bullets.Length > 0)
        {
            GameObject lastBullet = bullets[bullets.Length - 1];
            Rigidbody2D bulletRb = lastBullet.GetComponent<Rigidbody2D>();
            float bulletSpeed = bulletRb.velocity.magnitude;
            Debug.Log("Last bullet speed: " + bulletSpeed);
        }
        else
        {
            Debug.Log("No bullets fired.");
        }

        // Stop firing bullets
        // There is no StopFiring method in FBWeapon
    }
}
