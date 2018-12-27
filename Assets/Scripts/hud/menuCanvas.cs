using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuCanvas : MonoBehaviour {

	public void StartGame(){
		GameManager.instance.StartGame();
	}

	public void ExitGame(){
		Application.Quit();
	}	
}
