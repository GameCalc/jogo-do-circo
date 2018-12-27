using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botoes : MonoBehaviour {

	public GameObject porta;
	//Para a porta que tem dois botoes
	public GameObject otoBotao;
	public bool temDoisBotoes;
	public bool apertado;

	public Sprite pressionado;

	SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		apertado = this.apertado;
		apertado = false;

		sr = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {


	}

	void OnCollisionEnter2D (Collision2D other)
	{
		
		if (temDoisBotoes == true) {
			if (other.gameObject.tag == "EnemyObject") {
				apertado = true;
				if(apertado == true && otoBotao.GetComponent<botoes>().apertado == true){
					porta.GetComponent<IntrasceneDoor> ().locked = false;
					sr.sprite = pressionado;
					Destroy(other.gameObject);
				}
			}
		} else {
			if (other.gameObject.tag == "EnemyObject") {
				porta.GetComponent<IntrasceneDoor> ().locked = false;
				sr.sprite = pressionado;
				Destroy(other.gameObject);
			}
		}
	}


}

