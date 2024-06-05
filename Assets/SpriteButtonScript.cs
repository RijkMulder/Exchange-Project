using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteButtonScript : MonoBehaviour
{
    public GameObject toSetActive;

    private void OnMouseDown()
    {
        toSetActive.SetActive(true);
    }
}
