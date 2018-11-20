using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudcoracao : MonoBehaviour {


    public int nmedo;
    Image coracao;
    public Sprite coracaocheio;
    public Sprite co1;
    public Sprite co2;
    public Sprite co3;
    public Sprite medo;

    // Use this for initialization
    void Start () {

        //Fetch the Image from the GameObject
        coracao = GetComponent<Image>();
        
        // Esse numero vai decidir o nivel do medo e qual imagem usar
        nmedo = 0;

    }
	
	// Update is called once per frame
	void Update () {

        aumentamedo(nmedo);

	}

    void aumentamedo(int nmedo)
    {
        if(nmedo == 0)
        {
            coracao.sprite = coracaocheio;
        }
        if (nmedo == 1)
        {
            coracao.sprite = co1;
        }
        if(nmedo == 2)
        {
            coracao.sprite = co2;
        }
        if(nmedo == 3)
        {
            coracao.sprite = co3;
        }
        if(nmedo == 4)
        {
            coracao.sprite = medo;
        }


    }
}
