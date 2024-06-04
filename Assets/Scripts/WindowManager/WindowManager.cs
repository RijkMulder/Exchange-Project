using Events;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    [SerializeField] private Window[] windows;
    private Window currentWindow;
    private void Start()
    {
        if (currentWindow == null) currentWindow = windows[0];
        DeactivateWindows();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) ChangeWindow();
    }
    private void ChangeWindow()
    {
        currentWindow = currentWindow.ChangeWindow();
        currentWindow.Activate();
        DeactivateWindows();
    }
    private void DeactivateWindows()
    {
        foreach (Window window in windows)
        {
            if (window != currentWindow) window.DeActivate();
        }
    }
}
