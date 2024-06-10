using Events;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager Instance;

    [SerializeField] private Window[] windows;
    [SerializeField] private WindowTransition windowTransition;
    private Window currentWindow;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (currentWindow == null) currentWindow = windows[0];
        DeactivateWindows();

        ChangeWindow(true);
    }
    public void ChangeWindow(bool doTransition)
    {
        // no transition
        if (!doTransition)
        {
            LoadWindow();
            return;
        }

        // transition
        Instantiate(windowTransition);
    }
    private void DeactivateWindows()
    {
        foreach (Window window in windows)
        {
            if (window != currentWindow) window.DeActivate();
        }
    }
    private void LoadWindow()
    {
        currentWindow = currentWindow.ChangeWindow();
        currentWindow.Activate();
        DeactivateWindows();
    }
}
