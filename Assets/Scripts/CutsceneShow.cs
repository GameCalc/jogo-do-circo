using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneShow : MonoBehaviour {
    int currentHQ = 0;
    public List<Sprite> scenes = new List<Sprite>();

    public void NextScene ()
    {
        if (currentHQ == scenes.Count - 1)
            GameManager.Instance.LoadTutorial();
        else
            currentHQ++;

        GetComponent<Image>().sprite = scenes[currentHQ];
    }
}
