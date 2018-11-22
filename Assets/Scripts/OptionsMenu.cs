using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {
    [SerializeField]
    private Slider volumeSlider;

    private void Start() {
        volumeSlider.value = SoundManager.Instance.GetVolume();
        volumeSlider.onValueChanged.AddListener(delegate { OnValueChanged(); });
    }

    public void OnValueChanged() {
        SoundManager.Instance.ChangeVolume(volumeSlider.value);
    }

    public void ChangeVolume(float volume) {
        SoundManager.Instance.ChangeVolume(volume);
    }

}
