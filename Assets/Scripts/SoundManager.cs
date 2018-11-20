using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;

    private static SoundManager instance = null;

    public SoundManager Instance {
        get {
            return instance;
        }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    
    public void PlaySFX(AudioClip sfx) {
        sfxSource.Stop();
        sfxSource.clip = sfx;
        sfxSource.Play();
    }

    public void PlayMusic(AudioClip music) {
        musicSource.Stop();
        musicSource.clip = music;
        musicSource.Play();
    }
}
