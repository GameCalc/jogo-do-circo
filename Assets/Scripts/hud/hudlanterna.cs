using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudlanterna : MonoBehaviour
{

    gameManager objGame = new gameManager();

    Image nvlbateria;
   
    public Sprite l7;
    public Sprite l6;
    public Sprite l5;
    public Sprite l4;
    public Sprite l3;
    public Sprite l2;
    public Sprite l1;

    // Se tem bateria no iventario do personagem e quantas, vai ser adquirirdo através do script do Jhonny
    int baterias;
    // esse int vai decidir o nivel da bateria e qual imagem usar
    public static int nivelb;

    // Verdadeiro se lanterna estiver ligada
    public bool lanternaligada;

    
   

    // Use this for initialization
    void Start()
    {

        //Fetch the Image from the GameObject
        nvlbateria = GetComponent<Image>();
        nivelb = 5;

        // dando baterias para testes
        baterias = 2;

        
    }

    // Update is called once per frame
    void Update()
    {
        //lanternaligada = Player.lantOn;


        /* if (nivelb == 5)
         {
             nvlbateria.sprite = cheia;
         }
         recargalanterna();
         gastalanterna();
         */

        //Vai desligar a bateria quando o nivel chegar em 0
        nivelb = objGame.GastaBateria(lanternaligada, nivelb);
        mudaSprite(nivelb);
        
        // Se o nivel da bateria for menor ou igual a 1 desliga a lanterna
        if (nivelb <= 1)
        {
            lanternaligada = false;
        }

    }
    /*
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


                if (nivelb == 2)
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


    
    }*/

    // muda Sprite da Lanterna
    void mudaSprite(int nivel)
    {
        switch (nivel)
        {
            case 1:
                nvlbateria.sprite = l1;
                break;
            case 2:
                nvlbateria.sprite = l2;
                break;
            case 3:
                nvlbateria.sprite = l3;
                break;
            case 4:
                nvlbateria.sprite = l4;
                break;
            case 5:
                nvlbateria.sprite = l5;
                break;
            case 6:
                nvlbateria.sprite = l6;
                break;
            case 7:
                nvlbateria.sprite = l7;
                break;
        }
    }

    
    
   
}

