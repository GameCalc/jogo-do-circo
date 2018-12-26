using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {

    

    private static gameManager instance = null;

    // Pega o jogador no inspector
    public GameObject player;


    // // // // variaveis do HUD // // // // 

    // Verdadeiro se lanterna estiver ligada
    //public bool lanternaligada;

    // Variaveis da Lanterna

    int baterias = 2;

    // tempo necessário para gastar uma bateria se a lanterna estiver ligada
    public float segundos = 10;
    float oldTimer;
    int nivelLanterna;
    // // // // // // // // //

    public gameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        oldTimer = segundos;

        DontDestroyOnLoad(gameObject);
    }

   //HUD//
   
    
    // Ao personagem receber o medo do inimigo, este metodo deve retornar o nivel do medo
    void hudMudaCoracao()
    {
        
        //codigo que vai fazer o personagem colidir com a sombra e aumentar medo

        
        // Na classe hudcoracao, será chamda essa função e nela acontecerá a mudança da sprite
    }

    // Esta função será chamada na classe hudbateria, onde gastará a bateria
    public int GastaBateria(bool lanternaligada, int nivel)
    {
        // código que gasta bateria
        if (lanternaligada && nivel>1)
        {
            segundos -= Time.deltaTime;
            if (segundos <= 0)
            {
                nivel=nivel-1;
                segundos = 10;
                return nivel;
            }
        }
        // Recarrega a bateria da lanterna
        if (Input.GetKeyDown(KeyCode.R) && baterias >= 1)
        {
            nivel = 7;
            baterias = baterias - 1;
            return nivel;
        }

        return nivel;
    }
    
    
    
    
    
    // Este metodo fará a escolha da cena (ou level) que o player irá jogar
    void escolhelevel()
    {
        // Algum menu vai passar o numero da fase que o jogador quer jogar e esta função fará o jogo mudar de cena
    }
}
