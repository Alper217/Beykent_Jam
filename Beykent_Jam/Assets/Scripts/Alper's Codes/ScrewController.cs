using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrewController : MonoBehaviour
{
    public GameObject screwUI; // Tek bir UI paneli, týklanan objeye göre açýlacak
    public List<Image> allPoints; // 9 nokta (UI Image olarak atanacak)
    private List<Image> criticalPoints = new List<Image>(); // Seçilen objeye göre deðiþecek
    private List<Image> selectedPoints = new List<Image>();
    private int maxSelections = 4;
    private float earthquakeEffect = 1.0f; // %100 etkilenecek
    private EarthquakeManager earthquakeManager;
    private GameObject currentObject; // Hangi obje için UI açýldý
    private Dictionary<GameObject, List<int>> objectCriticalPoints = new Dictionary<GameObject, List<int>>(); // Her obje için kritik noktalar

    void Start()
    {
        earthquakeManager = FindObjectOfType<EarthquakeManager>();
        if (earthquakeManager == null)
        {
            Debug.LogError("EarthquakeManager sahnede bulunamadý!");
        }
        screwUI.SetActive(false); // Baþlangýçta UI kapalý olacak
    }

    public void RegisterCriticalPoints(GameObject obj, List<int> criticalIndexes)
    {
        objectCriticalPoints[obj] = criticalIndexes;
    }

    public void OpenScrewUI(GameObject obj)
    {
        currentObject = obj;
        screwUI.SetActive(true);
        selectedPoints.Clear();
        earthquakeEffect = 1.0f;
        ResetPointColors();
        UpdateCriticalPoints();
    }

    void ResetPointColors()
    {
        foreach (Image point in allPoints)
        {
            point.color = Color.white;
        }
    }

    void UpdateCriticalPoints()
    {
        criticalPoints.Clear();
        if (objectCriticalPoints.ContainsKey(currentObject))
        {
            foreach (int index in objectCriticalPoints[currentObject])
            {
                if (index >= 0 && index < allPoints.Count)
                {
                    criticalPoints.Add(allPoints[index]);
                }
            }
        }
    }

    public void SelectPoint(Image point)
    {
        if (selectedPoints.Count >= maxSelections || selectedPoints.Contains(point)) return;
        selectedPoints.Add(point);
        point.color = Color.green;
        earthquakeEffect -= criticalPoints.Contains(point) ? 0.25f : 0.1f; // Kritik noktalar daha fazla avantaj saðlar
    }

    public void ConfirmSelection()
    {
        Debug.Log("Deprem etkisi: " + (earthquakeEffect * 100) + "%");
        if (earthquakeManager != null)
        {
            earthquakeManager.force *= earthquakeEffect; // Deprem etkisini azalt
            earthquakeManager.StartEarthquake();
        }
        screwUI.SetActive(false); // UI kapat
    }
}
