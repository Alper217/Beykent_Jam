using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrewRotate : MonoBehaviour
{
    private RectTransform rectTransform;
    public Button screwButton; // Butonu Inspector'dan atamak i�in

   

    public void StartRotation()
    {
        StopAllCoroutines(); // �nceki coroutine �al���yorsa durdur
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        if (rectTransform == null) yield break;

        float duration = 0.5f;
        float elapsedTime = 0f;

        Quaternion startRotation = rectTransform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 360);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            rectTransform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }

        rectTransform.rotation = endRotation;
    }
}
