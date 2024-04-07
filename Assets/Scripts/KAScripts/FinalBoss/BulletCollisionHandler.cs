
using UnityEngine; 
public class BulletCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("obstacle"))
        {
            Destroy(gameObject);
        }
    }
}

