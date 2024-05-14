using Gambling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoGambling : MonoBehaviour
{
    public void Go()
    {
        GamblingManager.Instance.StartGamblingDay();
    }
}
