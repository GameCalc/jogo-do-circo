using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class applicationController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //funçoes de game
    public static void ExitGame() {
        Application.Quit(); 
    }
    public static bool IsFirstTime() {
        if (PlayerPrefs.GetString("FirstTime") != "soulOfstones"){
            return true;
        }
        else {
            return false;
        }
    }
    public static void DefaultConfig(){
        PlayerPrefs.SetString("FirstTime", "soulOfstones");
        EnableMusic();
        EnableSound();
        SetVolumeMusic(1);
        SetVolumeSound(1);
    }
    //configurações de sons
    public static void EnableSound() {
        PlayerPrefs.SetInt("Sound", 1);
    }
    public static void DisableSound() {
        PlayerPrefs.SetInt("Sound", 0);
    }
    public static bool IsMuteSound() {
        if (PlayerPrefs.GetInt("Sound") == 1){
            return true;
        }
        else{
            return false;
        }
    }
    public static void SetVolumeSound(float volume){
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }
    public static float GetVolumeSound(){
        return PlayerPrefs.GetFloat("SoundVolume");
    }
    public static void EnableMusic(){
        PlayerPrefs.SetInt("Music", 1);
    }
    public static void DisableMusic(){
        PlayerPrefs.SetInt("Music", 0);
    }
    public static bool IsMuteMusic(){
        if (PlayerPrefs.GetInt("Music") == 1){
            return true;
        }
        else{
            return false;
        }
    }
    public static void SetVolumeMusic(float volume){
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public static float GetVolumeMusic(){
        return PlayerPrefs.GetFloat("MusicVolume");
    }
}
