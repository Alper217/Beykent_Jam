using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EarthquakeManager : MonoBehaviour
{
    [Header("Fiziksel Objeler için Ayarlar")]
    public float force = 5f; // Kuvvetin büyüklüğü
    public float radius = 10f; // Depremin etki alanı
    public float duration = 3f; // Depremin süresi

    [Header("Kamera Sallama Ayarları")]
    public Transform cameraTransform; // Kameranın transform'u
    public float shakeMagnitude = 0.1f; // Sallanma şiddeti
    public float shakeSpeed = 10f; // Sallanma hızı

    private Vector3 originalCameraPosition;
    private bool isEarthquakeActive = false;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        originalCameraPosition = cameraTransform.localPosition;
    }

    public void StartEarthquake()
    {
        if (!isEarthquakeActive)
        {
            isEarthquakeActive = true;
            StartCoroutine(EarthquakeSequence());
        }
    }

    IEnumerator EarthquakeSequence()
    {
        float elapsedTime = 0f;
        InvokeRepeating("ShakeObjects", 0f, 0.2f);
        StartCoroutine(ShakeCamera());

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        CancelInvoke("ShakeObjects");
        StopCoroutine(ShakeCamera());
        cameraTransform.localPosition = originalCameraPosition;
        isEarthquakeActive = false;
    }

    void ShakeObjects()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = Random.insideUnitSphere;
                rb.AddForce(randomDirection * force, ForceMode.Impulse);
            }
        }
    }

    IEnumerator ShakeCamera()
    {
        float timer = 0f;
        while (timer < duration)
        {
            float x = Mathf.PerlinNoise(Time.time * shakeSpeed, 0f) * 2 - 1;
            float y = Mathf.PerlinNoise(0f, Time.time * shakeSpeed) * 2 - 1;
            cameraTransform.localPosition = originalCameraPosition + new Vector3(x, y, 0) * shakeMagnitude;

            timer += Time.deltaTime;
            yield return null;
        }
        cameraTransform.localPosition = originalCameraPosition;
    }
}
