using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteButtonScript : MonoBehaviour
{
    public bool setActive;
    public bool setInActive;
    public bool openUpgradeShop;
    public GameObject toSetActive;

    private void OnMouseDown()
    {
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
            UpgradeShop.UpgradeShopScript.Instance.SetActive();
        }
    }
}
