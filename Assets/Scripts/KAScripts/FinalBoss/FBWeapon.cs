using UnityEngine;

public class FBWeapon : MonoBehaviour
{
    // Private class and other data
    [SerializeField] protected float _fireSpeed = 0.5f; // Fire speed in seconds
    [SerializeField] private float _roundsPerMinute = 120f; // Rounds per minute
    public bool _canFire = true; // Indicates whether the weapon can fire
    [SerializeField] protected float _bulletVelocity = 10f; // Velocity of the bullet
    [SerializeField] private string[] _obstacleTags = { "Obstacle", "obstacle" }; // Tags of obstacles
    [SerializeField] protected float _bulletLifespan = 4f; // Lifespan of the bullet

    // Serialized fields for public access
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePoint;

    // Public properties for accessing private class data
    public float FireSpeed => _fireSpeed;
    public float RoundsPerMinute => _roundsPerMinute;
    public float BulletVelocity => _bulletVelocity;
    public string[] ObstacleTags => _obstacleTags;
    public float BulletLifespan => _bulletLifespan;

    protected void Start()
    {
        InvokeRepeating(nameof(AutoFire), 0f, _fireSpeed);
    }

    public virtual void Fire()
    {
        if (_canFire)
        {
            GameObject bullet = Instantiate(bulletPrefab, (-firePoint.position), firePoint.rotation);
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

    protected void AutoFire()
    {
        if (_canFire)
        {
            Fire();
        }
    }

    protected void ResetFire()
    {
        _canFire = true;
    }
}
