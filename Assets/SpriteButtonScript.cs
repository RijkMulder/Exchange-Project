using Logbook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpgradeShop;

public class SpriteButtonScript : MonoBehaviour
{
    public bool setActive;
    public bool setInActive;
    public bool openUpgradeShop;
    public GameObject toSetActive;

    private void OnMouseDown()
    {
        if (UpgradeShopScript.Instance.shop.activeInHierarchy) return;
        if (setActive)
        {
            toSetActive.SetActive(true);
        }
        if (setInActive)
        {
            toSetActive.SetActive(false);
        }
        if (openUpgradeShop)
        {
            if (LogBook.instance.gameObject.activeInHierarchy) return;
            UpgradeShopScript.Instance.SetActive();
        }
    }
}
