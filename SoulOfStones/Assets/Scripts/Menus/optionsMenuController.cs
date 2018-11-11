using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class optionsMenuController : MonoBehaviour {
    public Toggle Sound;
    public Toggle Music;
    public Slider VolumeSound;
    public Slider VolumeMusic;
    // Use this for initialization
    void Start () {

        if (applicationController.IsFirstTime()){
            applicationController.DefaultConfig();
        }

        Sound.isOn = applicationController.IsMuteSound();
        Music.isOn = applicationController.IsMuteMusic();
        VolumeSound.value = applicationController.GetVolumeSound();
        VolumeMusic.value = applicationController.GetVolumeMusic();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSound(){
        if(Sound.isOn){
            applicationController.EnableSound();
        }
        else{
            applicationController.DisableSound();
        }
    }
    public void SetMusic(){
        if (Music.isOn){
            applicationController.EnableMusic();
        }
        else{
            applicationController.DisableMusic();
        }
    }
    public void SetVolumeSound(){
        applicationController.SetVolumeSound(VolumeSound.value);
    }
    public void SetVolumeMusic(){
        applicationController.SetVolumeMusic(VolumeMusic.value);
    }  
}
