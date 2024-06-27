using Events;
using Fishing;
using Logbook;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupVisual : MonoBehaviour
{
    [SerializeField] private Image img;
    [SerializeField] private float fadeTime;
    [SerializeField] private float holdTime;
    [SerializeField] private TMP_Text popupText;
    [SerializeField] private GameObject discoveryObject;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        EventManager.FishCaught += Discover;
        EventManager.DoTutorial += Popup;
    }
    private void DoPopup()
    {
        animator.SetTrigger("Popup");
        AudioManager.Instance.Play("Achievement");
    }
    private void Discover(FishType type)
    {
        if (LogBook.instance.fishDictionary[type].Item2 > 1) return;
        ResetPopup();
        DoPopup();
        img.sprite = type.fishSprite;
    }
    private void Popup(PopupType popup)
    {
        popupText.gameObject.SetActive(true);
        discoveryObject.SetActive(false);
        popupText.text = popup.popup;
        animator.SetTrigger("Popup");
    }
    private void ResetPopup()
    {
        animator.ResetTrigger("Popup");
        popupText.gameObject.SetActive(false);
        discoveryObject.SetActive(true);
    }
}
