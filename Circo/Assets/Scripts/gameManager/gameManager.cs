using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {

    

    private static gameManager instance = null;

    // Pega o jogador no inspector
    public GameObject player;

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
        DontDestroyOnLoad(gameObject);
    }

   //HUD//
   //Os designers definirão se serão varias imagens ou se será uma imagem dinamica, neste caso estudar HUD dinâmico
    
    // Ao personagem receber o medo do inimigo, este metodo deve retornar o nivel do medo
    void hudmudacoracao()
    {
        
        //codigo que vai fazer o personagem colidir com a sombra e aumentar medo

        
        // Na classe hudcoracao, será chamda essa função e nela acontecerá a mudança da sprite
    }

    // Esta função será chamada na classe hudbateria, onde mudará a imagem da bateria
    void hudmudabateria()
    {
        // codigo de gastar bateria
    }
   
    
    
    
    
    // Este metodo fará a escolha da cena (ou level) que o player irá jogar
    void escolhelevel()
    {
        // Alum meno vai passar o numero da fase que o jogador quer jogar e esta função fará o jogo mudar de cena
    }
}
