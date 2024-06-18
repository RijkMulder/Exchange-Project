using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechBubbleScript : MonoBehaviour
{
    public string[] speechList;
    [SerializeField] TextMeshProUGUI speechText;

    void Start()
    {
        rollSpeech();
    }

    public void rollSpeech()
    {
        speechText.text = speechList[Random.Range(0, speechList.Length)];
    }
}
