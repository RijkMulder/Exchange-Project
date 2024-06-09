using Gambling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crankScript : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.addChips(1000);
    }
    private void OnMouseDown()
    {
        SlotMachineScript.instance.Spin();
    }
}
