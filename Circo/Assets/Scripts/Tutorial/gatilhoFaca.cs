using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gatilhoFaca : MonoBehaviour {

    public bool libera;
    public GameObject faca;
	// Use this for initialization
	void Start () {

        libera = false;

	}
	
	// Update is called once per frame
	void Update () {
        if (libera == true)
        {
            faca.gameObject.transform.Translate(new Vector3(0, 6 * Time.deltaTime, 0));
        }
	}

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.gameObject.CompareTag("Player"))
        {
            libera = true;
        }
        
    }

    /*private void OnTriggerExit2D(Collider2D outro)
    {
        if (outro.gameObject.CompareTag("Player"))
        {
            libera = false;
        }

    }*/
}
