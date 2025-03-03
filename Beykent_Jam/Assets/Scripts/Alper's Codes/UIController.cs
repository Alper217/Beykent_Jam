using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject settingPage;
    [SerializeField] GameObject HowCanPlayPage;

    public void OpenSettingPage()
    {
        settingPage.SetActive(true);
    }
    public void ClosePage()
    {
        settingPage.SetActive(false);
        HowCanPlayPage.SetActive(false);
    }
    public void StartGame()
    {

    }
    public void HowCanPlay()
    {
        HowCanPlayPage.SetActive(true);
    }
}
