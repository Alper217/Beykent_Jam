using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHider : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject[] walls; // 4 duvarý buraya atayacaðýz
    private float hideThreshold = 0.5f; // Kamera açýsýna göre gizleme eþiði

    void Update()
    {
        foreach (GameObject wall in walls)
        {
            Vector3 toWall = (wall.transform.position - cameraTransform.position).normalized;
            float dotProduct = Vector3.Dot(-cameraTransform.forward, toWall);

            // Eðer duvar kameraya bakýyorsa gizle, deðilse göster
            wall.SetActive(dotProduct < hideThreshold);
        }
    }
}

