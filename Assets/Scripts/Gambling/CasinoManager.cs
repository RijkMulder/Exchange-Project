using Gambling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoManager : MonoBehaviour
{
    [SerializeField] GameObject logBook;
    [SerializeField] GameObject slotMachine;
    [SerializeField] GameObject upgradeShop;

    public void ToggleLogBook()
    {
        if (!logBook.active) logBook.active = true;
        else logBook.active = false;
    }
    public void ToggleSlotMachine()
    {
        if (!slotMachine.active) slotMachine.active = true;
        else slotMachine.active = false;
    }
    public void ToggleUpgradeShop()
    {
        if (!upgradeShop.active) upgradeShop.active = true;
        else upgradeShop.active = false;
    }
    public void EndNight()
    {
        GamblingManager.Instance.QuitGambling();
    }
}
