using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class menuController : MonoBehaviour {
    public GameObject menuOptions;
    public GameObject menuStart;
    public GameObject battleScene;
	// Use this for initialization
	void Start () {
        ActiveMenu(menuStart);
    }
	// Update is called once per frame
	void Update () {
		
	}
    public void HideMenu(){
        menuStart.SetActive(false);
        menuOptions.SetActive(false);
        battleScene.SetActive(false);
    }
    public void ActiveMenu(GameObject menu) {
        HideMenu();
        menu.SetActive(true);
    }
    public void ExitGame() {
        applicationController.ExitGame();
    }
    public void Play(){
        Application.LoadLevel("batalha");
    }
}
