using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class collection
{
    public string name;
    public GameObject fishOBJ;
    public TextMeshProUGUI sizeText;
    public float highSize;
    public bool unlocked;
}

public class LogBookScript : MonoBehaviour
{
    [SerializeField] collection[] collection;

    public void checkLog(string name, float size)
    {
        for (int i = 0; i < collection.Length; i++)
        {
            collection fish = collection[i];
            if (fish.name == name)
            {
                if (!fish.unlocked)
                {
                    fish.unlocked = true;
                    fish.fishOBJ.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                }
                CheckSize(fish, size);
            }
        }
    }

    void CheckSize(collection fish, float size)
    {
        if (size > fish.highSize)
        {
            fish.highSize = size;
            fish.sizeText.text = "PB: " + fish.highSize.ToString() + "kg";
        }
    }
}