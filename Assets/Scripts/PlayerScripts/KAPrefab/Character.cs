using UnityEngine;

public class Character : MonoBehaviour
{
    // Private class data
    [SerializeField] private GameObject _footballPrefab;
    [SerializeField] private float _footballSpeed = 5f;

    // Method to shoot football
    public void ShootFootball(Vector2 direction)
    {
        // Instantiate football prefab and shoot it in the specified direction
        GameObject football = Instantiate(_footballPrefab, transform.position, Quaternion.identity);
        football.GetComponent<Rigidbody2D>().velocity = direction * _footballSpeed;
    }
}

public class FinalBoss : Character
{
    // Singleton instance
    private static FinalBoss _instance;
    public static FinalBoss Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FinalBoss>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(FinalBoss).Name);
                    _instance = singletonObject.AddComponent<FinalBoss>();
                }
            }
            return _instance;
        }
    }

    // Private class data
    [SerializeField] private float _rotationSpeed = 30f; // degrees per second
    [SerializeField] private float _shootingInterval = 1f; // seconds
    private float _lastShootTime;

    // Start is called before the first frame update
    void Start()
    {
        // Set initial rotation direction
        RotateRight();
    }

// Update is called once per frame
void Update()
{
    // Rotate the final boss
    Rotate();

    // Check if it's time to shoot
    if (Time.time - _lastShootTime >= _shootingInterval)
    {
        // Shoot football
        Debug.Log("Shooting football");
        ShootFootball(transform.right);  // Accessing method directly from the base class

        // Update last shoot time
        _lastShootTime = Time.time;
    }
}

    // Method to rotate the final boss
    private void Rotate()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }

    // Method to rotate the final boss to the right
    private void RotateRight()
    {
        _rotationSpeed = Mathf.Abs(_rotationSpeed); // Ensure rotation speed is positive
        Debug.Log("Rotating: " + transform.rotation.eulerAngles.z);
    }

    // Method to rotate the final boss to the left
    private void RotateLeft()
    {
        _rotationSpeed = -Mathf.Abs(_rotationSpeed); // Ensure rotation speed is negative
    }
}
