using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeTrap : MonoBehaviour {
	[SerializeField]
	private GameObject knife;
	[SerializeField]
	private float knifeForce = 1f;
	[SerializeField]
	private float timeKnifeDisappear = 1f;

	private Vector2 impulse;
	private bool triggered = false;
	private SpriteRenderer spriteRenderer;

	void Start(){
		switch ((int) transform.rotation.eulerAngles.z) {
		case 0:
			impulse = Vector2.down * knifeForce;
			break;

		case 90:
			impulse = Vector2.right * knifeForce;
			break;

		case 180:
			impulse = Vector2.up * knifeForce;
			break;

		case 270:
			impulse = Vector2.left * knifeForce;
			break;
		}
	}

	void ResetSpawn(){
		knife.SetActive(true);
		Rigidbody2D rb = knife.GetComponent<Rigidbody2D>();
		knife.transform.position = transform.position;
		rb.velocity = Vector2.zero;

		if(triggered){
			rb.AddForce(impulse, ForceMode2D.Impulse);
			Invoke("ResetSpawn", timeKnifeDisappear);
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.CompareTag("Player")){
			triggered = true;
			Rigidbody2D rb = knife.GetComponent<Rigidbody2D>();
			knife.SetActive(true);
			rb.velocity = Vector2.zero;
			rb.AddForce(impulse, ForceMode2D.Impulse);
			Invoke("ResetSpawn", timeKnifeDisappear);
		}
	}

	void OnTriggerExit2D(Collider2D collider){
		if(collider.CompareTag("Player")){
			triggered = false;
		}
	}

}
