using UnityEngine;
using System.Collections;

public class FinalBossMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Movement speed
    [SerializeField] private Transform topPoint; // Top point on the screen
    [SerializeField] private Transform bottomPoint; // Bottom point on the screen
    [SerializeField] private float bottomPointY = -5f; // Y-coordinate of the bottom point

    private Rigidbody2D rb;
    private Vector3 pointB;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Set point B initially
        pointB = bottomPoint.position;

        StartCoroutine(MoveBetweenPoints());
    }

    private IEnumerator MoveBetweenPoints()
    {
        Vector3 pointA = transform.position;

        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, moveSpeed));
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, moveSpeed));
        }
    }

    private IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float speed)
    {
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        while (true)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;

            thisTransform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);

            if (thisTransform.position == endPos)
                yield break;

            yield return null;
        }
    }
}
