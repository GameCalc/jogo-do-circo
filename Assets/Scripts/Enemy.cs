using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    // Velocidade de movimento do inimigo;
    [SerializeField]
    private float speed = 1;
    // Tempo para que o inimigo volte a virar uma sombra depois que é transformado em objeto;
    [SerializeField]
    private int timeToShadowBack = 10;
    // Tempo que o inimigo tem que passar debaixo da lanterna para virar um objeto;
    [SerializeField]
    private int timeToFadeShadow = 3;
    [SerializeField]
    private Sprite imagemSombra;
    [SerializeField]
    private Sprite imagemObjeto;

    private SpriteRenderer sr;
    private GameObject player;
    // Essa variável é usada para saber se o inimigo pode perseguir o player
    private bool canFollowPlayer;
    // Variável para saber se o inimigo está debaixo da luz
    private bool underLight = false;
    // Variável utilizada dentro da função SetUnderLight, para que uma parte da função só seja executada uma vez
    private bool firstTimeLighting = false;
    // Variável responsável por dizer se o inimigo pode se mover ou não, uma vez que quando ele está transformado em objeto ele não pode se mover
    private bool canMove = true;

    // Aqui o sprite renderer é pegado para que o objeto mude o sprite quando passa x tempo debaixo da luz
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = imagemSombra;
	}
	
	void FixedUpdate () {
        // Verifica se pode se mover, se sim, persiga o jogador
        if (canMove && player != null) {
            if (transform.position.x - player.transform.position.x < 0f) {
                transform.rotation = new Quaternion(transform.rotation.x, -180f, transform.rotation.z, transform.rotation.w);
            } else {
                transform.rotation = new Quaternion(transform.rotation.x, 0f, transform.rotation.z, transform.rotation.w);
            }
        }
        if (canFollowPlayer && canMove) {
            float movingSpeed = speed * Time.deltaTime;
            if (underLight)
                movingSpeed /= 2;
            transform.position = Vector2.Lerp(transform.position, player.transform.position, movingSpeed);
        }
    }

    // Essa função é chamada de dentro do script do player, uma vez que a luz da lanterna esteja sobre o inimigo.
    // Lembrando que ela é chamada várias vezes enquanto a lanterna iluminar o inimigo, por isso a necessidade de uma parte do código só ser executada uma vez.
    public void SetUnderLight() {
        underLight = true;
        // Essa parte função só pode ser executada uma vez, já que ela possui o método que invoca uma outra função em um determinado tempo x
        if (!firstTimeLighting) {
            firstTimeLighting = true;
            // Chama a função VerifyStatus após um tempo x em segundos, que é definido na variável timeToFadeShadow
            Invoke("VerifyStatus", timeToFadeShadow);
        }
    }

    // Essa função é chamada para verificar se o inimigo ainda está sob a luz da lanterna após x segundos.
    private void VerifyStatus() {
        if (underLight) {
    // Se estiver sob a luz da lanterna, então ele se transforma no objeto, parando de se mover até que a função TurnShadowOn seja chamado após y segundos, que é definido na variável timeToShadowBack
            canMove = false;
            sr.sprite = imagemObjeto;
            Invoke("TurnShadowOn", timeToShadowBack);
        }
        // Agora a variável que armazena se é a primeira vez que a luz bate no inimigo é resetada
        firstTimeLighting = false;
    }

    // Essa função só é responsável por definir que o inimigo virou uma sombra novamente, podendo perseguir o player quando for o caso
    private void TurnShadowOn() {
        sr.sprite = imagemSombra;
        canMove = true;
    }

    // Essa variável é chamada quando o player desliga a lanterna ou muda o direcionamento da mesma
    public void NotUnderLight() {
        underLight = false;
    }

    // Essa função só verifica se o player entrou no raio do inimigo, que é definido por um colisor em volta do mesmo.
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            canFollowPlayer = true;
            player = collision.gameObject;
        }else if(collision.gameObject.tag == "Light") {
            // Essa parte é importante pois, quando o inimigo entra em contato com o colisor que fica ao redor da luz própria do jogador, o inimigo deverá parar seu movimento.
            canFollowPlayer = false;
        }
    }
    // Essa opção diz respeito a quando o player sai do raio do inimigo.
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            canFollowPlayer = false;
        } else if (collision.gameObject.tag == "Light") {
            canFollowPlayer = true;
        }
    }
}
