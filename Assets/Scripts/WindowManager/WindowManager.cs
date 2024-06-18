using Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public static WindowManager Instance;

    public Window[] windows;
    public Window currentWindow;
    public WindowTransition transition;

    [SerializeField] private WindowTransition windowTransition;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (currentWindow == null) currentWindow = windows[0];
        DeactivateWindows();
    }
    public void ChangeWindowButton()
    {
        ChangeWindow(false);
    }
    /// <summary>
    /// Change windows with or without transition, choose which window or default next window of current window
    /// </summary>
    /// <param name="doTransition"></param>
    /// <param name="window"></param>
    public void ChangeWindow(bool doTransition, Window window = null)
    {
        // no transition
        if (!doTransition)
        {
            LoadWindow(window);
            return;
        }

        // transition
        transition = Instantiate(windowTransition);
        transition.StartTransition(window);
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
