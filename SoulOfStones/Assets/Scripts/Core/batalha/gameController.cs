using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour {

    public int totalMana;

    public playerController playerOne;
    public int CurrentTurn = 1;

    public static gameController instance;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ExitGame(){
        Application.LoadLevel("Menu");
    }
}
