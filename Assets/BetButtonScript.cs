using Gambling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetButtonScript : MonoBehaviour
{
    public int direction;
    [SerializeField] private Animator animator;

    private void OnMouseDown()
    {
        SlotMachineScript.instance.changeBetAmount(direction);
        animator.SetTrigger("press");
    }
}
