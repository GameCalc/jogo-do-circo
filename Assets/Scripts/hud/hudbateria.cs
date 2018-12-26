using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudbateria : MonoBehaviour {

    Image nvlbateria;
    public Sprite cheia;
    public Sprite c3;
    public Sprite c2;
    public Sprite c1;
    public Sprite vazia;

    // Se tem bateria no iventario do personagem e quantas, vai ser adquirirdo através do script do Jhonny
    int baterias;
    // esse int vai decidir o nivel da bateria e qual imagem usar
    int nivelb;

    // Verdadeiro se lanterna estiver ligada
    public bool lanternaligada;

    // tempo necessário para gastar uma bateria se a lanterna estiver ligada
    public float segundos = 10;
    float oldTimer;

    // Use this for initialization
    void Start () {
        
        //Fetch the Image from the GameObject
        nvlbateria = GetComponent<Image>();
        nivelb = 5;

        // dando baterias para testes
        baterias = 2;

        oldTimer = segundos;
    }
	
	// Update is called once per frame
	void Update () {

        
		
        if (nivelb == 5)
        {
            nvlbateria.sprite = cheia;
        }
        recargalanterna();
        gastalanterna();

	}

    void recargalanterna()
    {
        if (baterias > 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                nvlbateria.sprite = cheia;
                baterias = baterias - 1;
                nivelb = 5;
            }
        }
    }


    void gastalanterna()
    {

        if (lanternaligada)
        {
            segundos -= Time.deltaTime;

            //GetComponent<Text>().text = "Tempo: " + Mathf.RoundToInt(timer).ToString() + " s";

            if (segundos <= 0)
            {
                

                if(nivelb == 2)
                {
                    nivelb = nivelb - 1;
                    nvlbateria.sprite = vazia;
                    //deve desligar lanterna
                    lanternaligada = false;

                }
                if (nivelb == 3)
                {
                    nivelb = nivelb - 1;
                    nvlbateria.sprite = c1;
                }
                if (nivelb == 4)
                {
                    nivelb = nivelb - 1;
                    nvlbateria.sprite = c2;
                }
                if (nivelb == 5)
                {
                    nivelb = nivelb - 1;
                    nvlbateria.sprite = c3;
                }

                segundos = 15;
            }
        }




    }



}
