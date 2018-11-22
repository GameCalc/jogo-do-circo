using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private KeyCode botaoDeAcao;
    [SerializeField]
    private KeyCode botaoDeAcenderLuz;
    [SerializeField]
    private int maxCharges = 5;
    [SerializeField]
    private int tempoDeCarga = 5;

    private static GameManager instance = null;
    private bool noPicadeiro = false;
    private int baterias = 0;

    // Pega o jogador no inspector
    public GameObject player;

    public static GameManager Instance
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

    private void Start() {
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void OnDestroy() {
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    void OnSceneChange(Scene actual, Scene next) {
        if(next.name == "Picadeiro"){
            noPicadeiro = true;
        } else {
            noPicadeiro = false;
        }
    }

    public bool EstaNoPicadeiro() {
        return noPicadeiro;
    }

    public bool CollectBattery() {
        if (baterias < maxCharges) {
            baterias++;

            return true;
        }

        return false;
    }

    public void IrParaTutorial() {
        SceneManager.LoadScene("Tutorial");
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
   
    public KeyCode PegarTeclaAcao() {
        return botaoDeAcao;
    }
    
    public KeyCode PegarBotaoLuz() {
        return botaoDeAcenderLuz;
    }
    
    public void SairDoTutorial() {
        SceneManager.LoadScene("Picadeiro");
    }

    // Este metodo fará a escolha da cena (ou level) que o player irá jogar
    void escolhelevel()
    {
        // Alum meno vai passar o numero da fase que o jogador quer jogar e esta função fará o jogo mudar de cena
    }
}
