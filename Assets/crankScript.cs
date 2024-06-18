using Gambling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crankScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void OnMouseDown()
    {
        SlotMachineScript.instance.Spin();
        animator.SetTrigger("pull");
    }
}
