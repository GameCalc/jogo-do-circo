using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudlanterna : MonoBehaviour
{

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
        
    }

    // Update is called once per frame
    void Update()
    {
        baterias = GameManager.instance.GetBatteriesCount();
        mudaSprite(baterias);
    }

    // muda Sprite da Lanterna
    void mudaSprite(int nivel)
    {
        switch (nivel)
        {
            case 0:
                nvlbateria.sprite = l1;
                break;
            case 1:
                nvlbateria.sprite = l2;
                break;
            case 2:
                nvlbateria.sprite = l3;
                break;
            case 3:
                nvlbateria.sprite = l4;
                break;
            case 4:
                nvlbateria.sprite = l5;
                break;
            case 5:
                nvlbateria.sprite = l6;
                break;
            case 6:
                nvlbateria.sprite = l7;
                break;
        }
    }

    
    
   
}

