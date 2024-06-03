using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public enum soundChannel
{
    Music,
    SFX
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;
    public soundChannel channel;
    public bool loop;
    [Range(0f, 1f)] public float volume;
    [Range(0.1f, 3f)] public float pitch;
    [HideInInspector] public AudioSource source;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioMixer audioMixer;

    public AudioMixerGroup SFXGroup;
    public AudioMixerGroup musicGroup;


    public Sound[] soundsList;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Sound s in soundsList)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            if (s.channel == soundChannel.Music) s.source.outputAudioMixerGroup = musicGroup;
            else if (s.channel == soundChannel.SFX) s.source.outputAudioMixerGroup = SFXGroup;
        }
    }

    public void Play (string name)
    {
        Sound s = GetSound(name);
        if (s != null)
        {
            s.source.Play();
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
        }
    }

    public void Stop(string name)
    {
        Sound s = GetSound(name);
        s.source.Stop();
    }

    private Sound GetSound(string name)
    {
        Sound s = Array.Find(soundsList, sound => sound.name == name);
        return s;
    }

    public void SetVolume(string name, float volume)
    {
        Sound s = GetSound(name);
        if (s != null)
        {
            s.source.volume = volume;
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
        }
    }

    public void FadeIn(string name, float duration = -1f)
    {
        Sound s = GetSound(name);
        if (s != null)
        {
            s.source.Play();
            s.source.volume = 0;
            StartCoroutine(StartFade(name, 0f, 1f, duration, false));
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
        }
    }

    public void FadeOut(string name, float duration = -1f)
    {
        Sound s = GetSound(name);
        if (s != null)
        {
            StartCoroutine(StartFade(name, 1f, 0f, duration, true));
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
        }
    }

    private IEnumerator StartFade(string name, float startVolume, float endVolume, float duration, bool stop)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float currentVolume = Mathf.Lerp(startVolume, endVolume, elapsedTime / duration);
            SetVolume(name, currentVolume);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (stop) Stop(name);
        else SetVolume(name, endVolume);
    }
}

