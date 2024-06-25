using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject settings;
    [SerializeField] private CanvasGroup group;
    public void Pause()
    {
        Time.timeScale = 0;
        menu.SetActive(true);
        settings.SetActive(false);

        group.blocksRaycasts = true;
    }
    public void Continue()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
        settings.SetActive(false);

        group.blocksRaycasts = false;
    }
    public void Settings()
    {
        settings.SetActive(!settings.activeInHierarchy);
        menu.SetActive(!menu.activeInHierarchy);
    }
}
