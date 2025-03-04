using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixableObject : MonoBehaviour
{
    [SerializeField] private GameObject fixingUI;
    [SerializeField] private List<Button> fixingButtons = new List<Button>(); // 9 buton
    [SerializeField] private List<Button> importantFixingPoints = new List<Button>(); // %25 etki veren butonlar
    private Dictionary<Button, float> buttonEffectiveness = new Dictionary<Button, float>();

    private int fixedPoints = 0;
    private bool isFullyFixed = false;
    private const int requiredFixes = 4; // 4 çivi tamamlanýnca obje sabitlenmiþ olacak
    private float earthquakeResistance = 0f; // Deprem dayanýklýlýðý

    private void Start()
    {
        if (fixingUI != null)
        {
            fixingUI.SetActive(true); // UI açýlýyor
        }

        AssignFixingPoints();

        foreach (Button btn in fixingButtons)
        {
            btn.onClick.AddListener(() => AddFixingPoint(btn));
        }
    }

    private void AssignFixingPoints()
    {
        if (fixingButtons.Count != 9)
        {
            Debug.LogError($"Fixing UI içinde 9 buton olmasý gerekir ama {fixingButtons.Count} bulundu!");
            return;
        }

        foreach (Button btn in fixingButtons)
        {
            if (importantFixingPoints.Contains(btn))
            {
                buttonEffectiveness[btn] = 25f; // %25 etki saðlayan noktalar (Yeþil)
            }
            else
            {
                buttonEffectiveness[btn] = 10f; // %10 etki saðlayan noktalar (Sarý)
            }
        }
    }

    public void AddFixingPoint(Button button)
    {
        if (isFullyFixed || !buttonEffectiveness.ContainsKey(button)) return; // Geçersiz buton seçme

        button.interactable = false; // Buton devre dýþý kalýyor
        float effect = buttonEffectiveness[button]; // Etki deðerini al

        // Renk deðiþtir
        if (effect == 25f)
        {
            button.GetComponent<Image>().color = Color.green; // %25 etkili butonlar yeþil
        }
        else
        {
            button.GetComponent<Image>().color = Color.yellow; // %10 etkili butonlar sarý
        }

        earthquakeResistance += effect; // Objenin deprem direncini artýr
        fixedPoints++;

        // Eðer 4 nokta seçildiyse tamamlandý
        if (fixedPoints >= requiredFixes)
        {
            isFullyFixed = true;
            fixingUI.SetActive(false); // UI kapanýyor
            Debug.Log($"Obje tamamen sabitlendi! Depreme karþý dayanýklýlýk: %{earthquakeResistance}");
        }
    }

    public bool IsFullyFixed()
    {
        return isFullyFixed;
    }
}
