using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHider : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject[] walls; // 4 duvar� buraya atayaca��z
    private float hideThreshold = 0.5f; // Kamera a��s�na g�re gizleme e�i�i

    void Update()
    {
        foreach (GameObject wall in walls)
        {
            Vector3 toWall = (wall.transform.position - cameraTransform.position).normalized;
            float dotProduct = Vector3.Dot(-cameraTransform.forward, toWall);

            // E�er duvar kameraya bak�yorsa gizle, de�ilse g�ster
            wall.SetActive(dotProduct < hideThreshold);
        }
    }
}

