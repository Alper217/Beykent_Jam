using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrewEffect : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector3 originalScale;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale; // Ba�lang�� �l�e�ini sakla

        // Butona t�klama event'ini ekle
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => StartCoroutine(RotateAndResize()));
        }
    }

    private IEnumerator RotateAndResize()
    {
        if (rectTransform == null) yield break;

        float duration = 0.5f; // Animasyon s�resi
        float elapsedTime = 0f;

        Quaternion startRotation = rectTransform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 360); // 360 derece d�nd�r

        Vector3 startScale = rectTransform.localScale;
        Vector3 endScale = new Vector3(startScale.x - 0.02f, startScale.y - 0.11f, startScale.z);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            rectTransform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            rectTransform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        // Son de�erleri at�yoruz
        rectTransform.rotation = endRotation;
        rectTransform.localScale = endScale;
    }
}
