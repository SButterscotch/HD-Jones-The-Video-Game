using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopText : MonoBehaviour
{
    public TextMesh floatingText;
    public float shakeDuration = 0.2f;
    public float shakeAmount = 0.7f;
    public float fadeDuration = 1f;

    private Vector3 originalPosition;
    private Color originalColor;

    private void Start()
    {
        originalPosition = floatingText.transform.position;
        originalColor = floatingText.color;
        StartCoroutine(ShakeText());
    }

    private IEnumerator ShakeText()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            Vector3 newPos = originalPosition + Random.insideUnitSphere * shakeAmount;
            floatingText.transform.position = newPos;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        floatingText.transform.position = originalPosition;

        yield return new WaitForSeconds(0.5f);

        elapsedTime = 0f;
        Color transparentColor = originalColor;
        transparentColor.a = 0f;
        while (elapsedTime < fadeDuration)
        {
            floatingText.color = Color.Lerp(originalColor, transparentColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        floatingText.color = transparentColor;

        Destroy(gameObject);
    }
}

