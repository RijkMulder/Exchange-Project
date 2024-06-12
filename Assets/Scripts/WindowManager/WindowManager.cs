using Events;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager Instance;

    public Window[] windows;
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
    }
    public void ChangeWindow(bool doTransition, Window window = null)
    {
        // no transition
        if (!doTransition)
        {
            LoadWindow(window);
            return;
        }

        // transition
        WindowTransition transition = Instantiate(windowTransition);
        transition.StartTransition();
    }
    private void DeactivateWindows()
    {
        foreach (Window window in windows)
        {
            if (window != currentWindow) window.DeActivate();
        }
    }
    private void LoadWindow(Window window = null)
    {
        currentWindow = currentWindow.ChangeWindow(window);
        currentWindow.Activate();
        DeactivateWindows();
    }
}
