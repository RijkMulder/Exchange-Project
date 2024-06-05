using Events;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager Instance;

    [SerializeField] private Window[] windows;
    private Window currentWindow;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (currentWindow == null) currentWindow = windows[0];
        DeactivateWindows();
    }
    public void ChangeWindow()
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
