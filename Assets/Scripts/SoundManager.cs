using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;

    private float volume = 1f;
    private static SoundManager instance = null;

    public static SoundManager Instance {
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

    private void Start() {
        musicSource.volume = volume;
        sfxSource.volume = volume;
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

    public void ContinueMusic() {
        musicSource.Play();
    }

    public void PauseMusic() {
        musicSource.Pause();
    }
    
    public void ChangeVolume(float NewVolume) {
        musicSource.volume = NewVolume;
        sfxSource.volume = NewVolume;
    }

    public float GetVolume() {
        return volume;
    }
}
