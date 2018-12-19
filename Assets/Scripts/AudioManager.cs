using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance = null;

    [Range(0f, 1f)]
    public float musicVolume = 1;
    [Range(0f, 1f)]
    public float soundVolume = 1;

    public Sound[] musics;
    public Sound[] sounds;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        foreach (Sound m in musics)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.volume = m.volume * musicVolume;
            m.source.pitch = m.pitch;
            m.source.loop = m.loop;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * soundVolume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        PlayMusic("Main Theme");
    }

    public void PlayMusic(string name)
    {
        Sound m = Array.Find(musics, music => music.name == name);

        if (m == null)
            Debug.LogWarning("Music: " + name + " not found!");
        else
            m.source.Play();
    }

    public void PlaySound (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            Debug.LogWarning("Sound: " + name + " not found!");
        else
            s.source.Play();
    }
}
