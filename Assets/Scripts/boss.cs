using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour {
    [SerializeField]
    private GameObject[] checkPoints;

    public float velocidade;
    private Animator animator;
    private Vector2 direction;
    // Use this for initialization
    public GameObject bola1;
    public GameObject bola2;
    public GameObject bola3;
    public GameObject bola4;
    public GameObject bola5;
    private bool playerInRadius = false;
    private int nextCheckpoint = 0;

    public GameObject sBola;
    GameObject[] bolinha = new GameObject[5];
    public Transform[] spawnPoints;

    private bool podeJogar, existeBolaC;

    public float spawnTime, jogaBolaTime;
    private float timeCopia, bolacopia;
    int nBolas;

    float timeCounter=0, x, x2, x3, x4, x5, y, y2, y3, y4, y5, z;

    Transform posicaoPlayer;

    void Start () {
        animator = this.GetComponent<Animator>();
        nBolas = 0;
        timeCopia = spawnTime;
        bolacopia = jogaBolaTime;

        podeJogar = false;
        existeBolaC = true;


    }
	
	// Update is called once per frame
	void Update () {
        Movimentacao();

        MoveBolas(transform.position.x, transform.position.y);

        SpawnBolas();

        tempoParaArremeso(arremesaBola(podeJogar));

    }

    void Movimentacao()
    {
        switch (nextCheckpoint) {
            case 0:
                transform.Translate(-Vector2.up * velocidade * Time.deltaTime);
                animator.SetInteger("direcao", 4);
                MoveBolas(transform.position.x, transform.position.y);
                break;
            case 1:
                transform.Translate(Vector2.right * velocidade * Time.deltaTime);
                animator.SetInteger("direcao", 1);
                MoveBolas(transform.position.x, transform.position.y);
                break;
            case 2:
                transform.Translate(Vector2.up * velocidade * Time.deltaTime);
                animator.SetInteger("direcao", 3);
                MoveBolas(transform.position.x, transform.position.y);
                break;
            case 3:
                transform.Translate(-Vector2.right * velocidade * Time.deltaTime);
                animator.SetInteger("direcao", 2);
                MoveBolas(transform.position.x, transform.position.y);
                break;
            default:
                nextCheckpoint %= checkPoints.Length;
                break;
        }
        if (Mathf.Abs(transform.position.x - checkPoints[nextCheckpoint].transform.position.x) <= 0.5f && Mathf.Abs(transform.position.y - checkPoints[nextCheckpoint].transform.position.y) <= 0.5f) {
            nextCheckpoint++;
        }
    }
    void MoveBolas(float xt, float yt)
    {
        timeCounter += Time.deltaTime * 2;
        x = Mathf.Cos(timeCounter);
        y = Mathf.Sin(timeCounter)/2;

        x2 = Mathf.Cos(timeCounter-1.25f);
        y2 = Mathf.Sin(timeCounter-1.25f)/2;

        x3 = Mathf.Cos(timeCounter - 2.5f);
        y3 = Mathf.Sin(timeCounter - 2.5f)/2;

        x4 = Mathf.Cos(timeCounter - 3.75f);
        y4 = Mathf.Sin(timeCounter - 3.75f)/2;

        x5 = Mathf.Cos(timeCounter - 5);
        y5 = Mathf.Sin(timeCounter - 5)/2;
        

        z = -2;
        bola1.transform.position = new Vector3(x, y+3, z);
        bola1.transform.Translate(new Vector2(xt, yt-2));

        bola2.transform.position = new Vector3(x2, y2 + 3, z);
        bola2.transform.Translate(new Vector2(xt, yt - 2));

        bola3.transform.position = new Vector3(x3, y3 + 3, z);
        bola3.transform.Translate(new Vector2(xt, yt - 2));

        bola4.transform.position = new Vector3(x4, y4 + 3, z);
        bola4.transform.Translate(new Vector2(xt, yt - 2));

        bola5.transform.position = new Vector3(x5, y5 + 3, z);
        bola5.transform.Translate(new Vector2(xt, yt - 2));

    }
    
    void SpawnBolas()
    {
        if (nBolas == 0)
        {
            for (int i = 0; i < 5; i++)
            {
               bolinha[i]=Instantiate(sBola, spawnPoints[i].position, spawnPoints[i].rotation);
               bolinha[i].transform.parent = spawnPoints[i].transform;
               nBolas = 5;
            }
        }

        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0)
        {
            if (nBolas == 4)
            {
                if (y5>=-0.465f && y5<=0)
                {
                    bolinha[4] = Instantiate(sBola, spawnPoints[4].position, spawnPoints[4].rotation);
                    bolinha[4].transform.parent = spawnPoints[4].transform;
                    nBolas = 5;
                    existeBolaC = true;
                    
                }

                
            }
            spawnTime = timeCopia;
        }

    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.gameObject.CompareTag("Player"))
        {
            podeJogar = true;
            playerInRadius = true;
            
            posicaoPlayer = outro.transform;
        }
    }
    void OnTriggerExit2D(Collider2D outro)
    {
        if (outro.gameObject.CompareTag("Player"))
        {
            playerInRadius = false;

            podeJogar = false;
        }
    }

    bool arremesaBola(bool pode)
    {
        if (pode == true && existeBolaC == true && jogaBolaTime==bolacopia)
        {
            //bolinha[4].transform.parent = null;
            //bolinha[4].transform.position = Vector2.MoveTowards(bolinha[4].transform.position, posicaoPlayer.position, 3*Time.deltaTime);

            GameObject.Destroy(bolinha[4], 0);

            GameObject bolaArremessada = Instantiate(sBola, spawnPoints[4].position, spawnPoints[4].rotation);
            bolaArremessada.GetComponent<movebolateplayer>().playerPosition = new Vector3(posicaoPlayer.position.x, posicaoPlayer.position.y, posicaoPlayer.position.z);
            

            existeBolaC = false;

            nBolas = nBolas - 1;
            
        }
        return playerInRadius;
    }

    void tempoParaArremeso(bool jogou)
    {
        jogaBolaTime -= Time.deltaTime;
        if (jogou == true)
        {
            if (jogaBolaTime <= 0)
            {
                podeJogar = true;
                jogaBolaTime = bolacopia;
            }
        }
        else if (jogaBolaTime <= 0)
        {
            jogaBolaTime = bolacopia;
        }
    }


}
