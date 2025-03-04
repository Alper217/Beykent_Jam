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
    private const int requiredFixes = 4; // 4 �ivi tamamlan�nca obje sabitlenmi� olacak
    private float earthquakeResistance = 0f; // Deprem dayan�kl�l���

    private void Start()
    {
        if (fixingUI != null)
        {
            fixingUI.SetActive(true); // UI a��l�yor
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
            Debug.LogError($"Fixing UI i�inde 9 buton olmas� gerekir ama {fixingButtons.Count} bulundu!");
            return;
        }

        foreach (Button btn in fixingButtons)
        {
            if (importantFixingPoints.Contains(btn))
            {
                buttonEffectiveness[btn] = 25f; // %25 etki sa�layan noktalar (Ye�il)
            }
            else
            {
                buttonEffectiveness[btn] = 10f; // %10 etki sa�layan noktalar (Sar�)
            }
        }
    }

    public void AddFixingPoint(Button button)
    {
        if (isFullyFixed || !buttonEffectiveness.ContainsKey(button)) return; // Ge�ersiz buton se�me

        button.interactable = false; // Buton devre d��� kal�yor
        float effect = buttonEffectiveness[button]; // Etki de�erini al

        // Renk de�i�tir
        if (effect == 25f)
        {
            button.GetComponent<Image>().color = Color.green; // %25 etkili butonlar ye�il
        }
        else
        {
            button.GetComponent<Image>().color = Color.yellow; // %10 etkili butonlar sar�
        }

        earthquakeResistance += effect; // Objenin deprem direncini art�r
        fixedPoints++;

        // E�er 4 nokta se�ildiyse tamamland�
        if (fixedPoints >= requiredFixes)
        {
            isFullyFixed = true;
            fixingUI.SetActive(false); // UI kapan�yor
            Debug.Log($"Obje tamamen sabitlendi! Depreme kar�� dayan�kl�l�k: %{earthquakeResistance}");
        }
    }

    public bool IsFullyFixed()
    {
        return isFullyFixed;
    }
}
