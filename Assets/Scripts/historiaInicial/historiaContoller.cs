using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class historiaContoller : MonoBehaviour {
    [SerializeField]
    private RawImage[] hqs;
    [SerializeField]
    private Text textoBotao;

    private int hqAtual = 0;

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < hqs.Length; i++) {
            if (i != hqAtual)
                hqs[i].gameObject.SetActive(false);
            else
                hqs[i].gameObject.SetActive(true);
        }
        if(hqAtual == hqs.Length - 1) {
            textoBotao.text = "Tutorial";
        } else {
            textoBotao.text = "Próximo";
        }
	}

    public void PassarHq() {
        hqAtual++;
        if (hqAtual == hqs.Length)
            GameManager.Instance.IrParaTutorial();
    }
}
