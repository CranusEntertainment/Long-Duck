using UnityEngine;
using System.Collections;
public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.1f; // Sallanma süresi
    public float shakeMagnitude = 0.2f; // Sallanma þiddeti
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void TriggerShake()
    {
        StopAllCoroutines(); // Mevcut sallanma iþlemlerini durdur
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = originalPosition + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition; // Orijinal pozisyona dön
    }
}
