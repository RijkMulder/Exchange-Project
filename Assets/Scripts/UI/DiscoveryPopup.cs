using Events;
using Fishing;
using Logbook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscoveryPopup : MonoBehaviour
{
    CanvasGroup group;
    [SerializeField] private Image img;
    [SerializeField] private float fadeTime;
    [SerializeField] private float holdTime;
    private Animator animator;

    bool doPopup;

    private void Start()
    {
        group = GetComponent<CanvasGroup>();
        animator = GetComponent<Animator>();
        EventManager.FishCaught += Initialize;
    }
    private void OnEnable()
    {
        if (doPopup)
        {
            animator.SetTrigger("Popup");
            doPopup = false;
        }
    }
    private void Initialize(FishType type)
    {
        if (LogBook.instance.fishDictionary[type].Item2 > 1) return;
            animator.ResetTrigger("Popup");
        img.sprite = type.fishSprite;
        doPopup = true;
    }
}
