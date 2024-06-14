using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject settings;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
    }
    public void Pause()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        menu.SetActive(!menu.activeInHierarchy);
        settings.SetActive(false);
    }
    public void Continue()
    {
        Pause();
    }
    public void Settings()
    {
        settings.SetActive(!settings.activeInHierarchy);
        menu.SetActive(!menu.activeInHierarchy);
    }
}
