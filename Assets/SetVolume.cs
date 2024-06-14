using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string channel;
    public float value;

    public void setValue(float sliderValue)
    {
        audioMixer.SetFloat(channel, Mathf.Log10(sliderValue) * 20);
    }
}
