using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private GameObject flashlightDown;
    [SerializeField]
    private GameObject flashlightLeft;
    [SerializeField]
    private GameObject flashlightRight;
    [SerializeField]
    private GameObject flashlightUp;
    [SerializeField]
    private float flashLightDistance = 100;

    // Define o botão que liga a luz
    const KeyCode TURNLIGHT = KeyCode.Space;

    //Para onde o jogador está se movendo
    //0 - baixo, 1 - direita, 2 - cima, 3 - esqueda
    private int moving = 0;
    private bool flashLightOn = false;
    // Armazena se o botão para ligar a luz foi pressionado
    private bool turnOnLightPressed = false;
    private Animator animator;
    private Vector2 direction;
    private GameObject enemy = null;

    // Use this for initialization
    private void Start () {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update () {
        // Toda vez a luz é apagada e reacendida, uma vez que são 4 luzes no ambiente e o jogo precisa saber qual delas acender
        if (flashLightOn) {
            TurnOffFlashlight();
            TurnOnFlashlight();
        } else {
            TurnOffFlashlight();
        }
        GetInput();
        // Verifica qual das luzes está ligada e traça um raio a partir dela, através da função VerifyHit
        if (flashlightDown.activeSelf) {
            VerifyHit(Vector2.down, transform);
        }else if (flashlightUp.activeSelf) {
            VerifyHit(Vector2.up, transform);
        }
        else if (flashlightLeft.activeSelf) {
            VerifyHit(Vector2.left, transform);
        }
        else if (flashlightRight.activeSelf) {
            VerifyHit(Vector2.right, transform);
        }
	}

    // Essa função é responsável por traçar um raio de um ponto de origem até uma direção, verificando se há um inimigo no caminho
    private void VerifyHit(Vector2 direction, Transform origin) {
        // Crie o 'raio' do ponto de origem em uma direção passada pelo script, com uma distância definida em flashLightDistance, colidindo somente na camada "Enemy", fazendo com que, se houver um inimigo no caminho, a retorne uma colisão
        RaycastHit2D hit = Physics2D.Raycast(origin.position, direction, flashLightDistance, LayerMask.GetMask("Enemy"));
        // Se existir a colisão então o inimigo está sob a luz da lanterna
        if (hit.collider != null) {
            enemy = hit.collider.gameObject;
            // Chama a função SetUnderLight que está dentro do script do inimigo em que houve a colisão
            enemy.GetComponentInParent<Enemy>().SetUnderLight();
        }
    }

    // Função para desligar as luzes
    private void TurnOffFlashlight() {
        flashlightDown.SetActive(false);
        flashlightUp.SetActive(false);
        flashlightLeft.SetActive(false);
        flashlightRight.SetActive(false);
        // Se existia um inimigo sob a luz, então chame a função dele que define que o inimigo não está mais sob a luz
        if (enemy != null)
            enemy.GetComponentInParent<Enemy>().NotUnderLight();
        enemy = null;
        flashLightOn = false;
    }

    // Função que liga uma das 4 luzes de acordo para onde o jogador está olhando
    private void TurnOnFlashlight() {
        switch (moving) {
            case 0:
                flashlightDown.SetActive(true);
                break;
            case 1:
                flashlightRight.SetActive(true);
                break;
            case 2:
                flashlightUp.SetActive(true);
                break;
            case 3:
                flashlightLeft.SetActive(true);
                break;
        };
        flashLightOn = true;
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        transform.Translate(direction * speed * Time.deltaTime);
        if (flashLightOn) {
            float x = transform.position.x;
            float y = transform.position.y;
            // As 4 lanternas se movimentam de acordo com a posição do personagem na tela
            flashlightDown.transform.position = new Vector3(x, y, flashlightDown.transform.position.z);
            flashlightUp.transform.position = new Vector3(x, y, flashlightUp.transform.position.z);
            flashlightLeft.transform.position = new Vector3(x, y, flashlightLeft.transform.position.z);
            flashlightRight.transform.position = new Vector3(x, y, flashlightRight.transform.position.z);
        }

        if (direction.x != 0 || direction.y != 0)
            AnimateMovement(direction);
        else
            animator.SetLayerWeight(1, 0);
    }

    private void GetInput () {
        direction = Vector2.zero;

        if (Input.GetKeyDown(TURNLIGHT) && !turnOnLightPressed) {
            turnOnLightPressed = true;
            if (flashLightOn)
                TurnOffFlashlight();
            else
                TurnOnFlashlight();
        }

        if (Input.GetKeyUp(TURNLIGHT)) {
            turnOnLightPressed = false;
        }

        if (Input.GetKey(KeyCode.W)) {
            direction += Vector2.up;
            moving = 2;
        }

        if (Input.GetKey(KeyCode.A)) {
            direction += Vector2.left;
            moving = 3;
        }

        if (Input.GetKey(KeyCode.S)) {
            direction += Vector2.down;
            moving = 0;
        }

        if (Input.GetKey(KeyCode.D)) {
            direction += Vector2.right;
            moving = 1;
        }
    }

    private void AnimateMovement(Vector2 direction) {
        animator.SetLayerWeight(1, 1);

        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }
}
