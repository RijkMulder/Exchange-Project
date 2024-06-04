using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] private Window nextWidow;

    GameObject windowBase;
    private void Awake() { windowBase = transform.GetChild(0).gameObject; }
    public void Activate() { windowBase.SetActive(true); }
    public void DeActivate() { windowBase.SetActive(false); }
    public Window ChangeWindow(Window window = null)
    {
        if (window == null) window = nextWidow;
        return window;
    }
}