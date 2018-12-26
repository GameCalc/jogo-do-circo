using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider){
		if(collider.CompareTag("Player")){
			this.gameObject.SetActive(false);
			// DO SOMETHING
		}
	}
}
