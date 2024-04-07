using UnityEngine;

public class AutomaticWeapon : FBWeapon
{
    // Singleton instance
    public static AutomaticWeapon Instance { get; private set; }

    private void Awake()
    {
        // Singleton implementation
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of AutomaticWeapon found. Destroying the extra instance.");
            Destroy(gameObject);
        }
    }

    // Override Fire method to destroy bullets on collision with obstacles

    public override void Fire()
    {
        if (_canFire)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = _bulletVelocity * (-transform.right);
            }
            Destroy(bullet, _bulletLifespan); // Destroy bullet after lifespan

            _canFire = false;
            Invoke(nameof(ResetFire), _fireSpeed);
        }
    }
}
