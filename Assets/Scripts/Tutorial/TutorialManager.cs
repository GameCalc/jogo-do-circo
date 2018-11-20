using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {
    [SerializeField]
    private GameObject text1;
    [SerializeField]
    private GameObject text2;
    [SerializeField]
    private GameObject textoLanterna;
    [SerializeField]
    private GameObject primeiroInimigo;
    [SerializeField]
    private GameObject faca;
    [SerializeField]
    private GameObject lanterna;

    private bool pegouLanterna = false;

    void Start () {
        text1.SetActive(true);
        primeiroInimigo.SetActive(false);
        faca.SetActive(false);
        lanterna.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool PegouLanterna() {
        return pegouLanterna;
    }

    public void AtivarTextoPortao() {
        text2.SetActive(true);
        lanterna.SetActive(true);
    }

    public void ChegouPertoLanterna() {
        textoLanterna.SetActive(true);
    }

    public void PegarLanterna() {
        pegouLanterna = true;
        primeiroInimigo.SetActive(true);
        lanterna.SetActive(false);
        faca.SetActive(true);
    }
}
