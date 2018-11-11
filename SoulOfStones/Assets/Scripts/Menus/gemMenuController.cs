using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemMenuController : MonoBehaviour {
    public GameObject gameMenu;
	// Use this for initialization
	void Start () {
        HideMenu();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown (KeyCode.Escape)) {
            if (gameMenu.activeSelf){
                HideMenu();
            }
            else{
                ShowMenu();
            }
        }
	}
    public void ShowMenu(){
        gameMenu.SetActive(true);
    }
    public void HideMenu() {
        gameMenu.SetActive(false);
    }
}
