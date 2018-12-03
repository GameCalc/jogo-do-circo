using UnityEngine;

public class Enemy : MonoBehaviour {
    // Velocidade de movimento do inimigo;
    [SerializeField]
    private float speed = 1;
    // Tempo para que o inimigo volte a virar uma sombra depois que é transformado em objeto;
    [SerializeField]
    private float timeToShadowBack = 10;
    // Tempo que o inimigo tem que passar debaixo da lanterna para virar um objeto;
    [SerializeField]
    private float timeToFadeShadow = 3;
    [SerializeField]
    private Sprite shadowSprite;
    [SerializeField]
    private Sprite objectSprite;

    private SpriteRenderer sr;
    private GameObject player;
    private Flashlight flashlight;

    // Variável para saber se o inimigo está debaixo da luz
    private bool illuminated = false;

    // Distância mínima para se manter do jogador
    public float minDistance = 2;
    // Distância máxima ao qual ele segue o jogador
    public float maxDistance = 8;

    // O objeto está almadiçoado (pode seguir o jogador)?
    [HideInInspector]
    public bool cursed = true;

    float illuminationCounter = 0;
    float darkCounter = 0;

    // Aqui o sprite renderer é pegado para que o objeto mude o sprite quando passa x tempo debaixo da luz
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = shadowSprite;
        flashlight = GameObject.Find("Flashlight").GetComponent<Flashlight>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void LateUpdate () {
        float curDistance = Vector2.Distance(transform.position, player.transform.position);

        // Se o objeto estiver almadiçoado e estiver dentro do intervalo de distância definida, segue o jogador
        if (cursed && !illuminated && curDistance > minDistance && curDistance <= maxDistance)
        {
            // Espelha a sprite para que ela olhe para o jogador
            if (transform.position.x - player.transform.position.x < 0f)
                transform.rotation = Quaternion.Euler(transform.rotation.x, -180f, transform.rotation.z);
            else
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);

            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
        }  
    }

    void Update ()
    {
        bool lastStatus = illuminated;
        illuminated = flashlight.illuminatedEnemies.Contains(transform);

        if (cursed) {
            if (illuminated)
            {
                illuminationCounter += Time.deltaTime;

                if (!lastStatus)
                    sr.sprite = objectSprite;
            }
            else if (!illuminated && lastStatus)
            {
                sr.sprite = shadowSprite;
                illuminationCounter = 0;
            }

            if (illuminationCounter >= timeToFadeShadow)
            {
                cursed = false;
                illuminationCounter = 0;
            }
        } else {
            darkCounter += illuminated ? 0 : Time.deltaTime;

            if (darkCounter >= timeToShadowBack)
            {
                sr.sprite = shadowSprite;
                darkCounter = 0;
                cursed = true;
            }
        }
    }
}
