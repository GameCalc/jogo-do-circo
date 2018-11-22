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

    //Para onde o jogador está se movendo
    //0 - baixo, 1 - direita, 2 - cima, 3 - esqueda
    private int moving = 0;
    private bool flashLightOn = false;
    private bool turnOnLightPressed = false;
    private Animator animator;
    private Vector2 direction;
    private GameObject enemy = null;

    int nivelb;

    //boolean para o script da lanterna que ira gastar a bateria receber
    public static bool lantOn = false;



    // Use this for initialization
    private void Start () {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update () {
        if (flashLightOn) {
            TurnOffFlashlight();
            TurnOnFlashlight();
        } else {
            TurnOffFlashlight();
        }
        GetInput();
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

        // Desligar Lanterna se Acabar Bateria
        nivelb = hudlanterna.nivelb;
        if (nivelb <= 1)
        {
            TurnOffFlashlight();
        }

    }

    private void VerifyHit(Vector2 direction, Transform origin) {
        RaycastHit2D hit = Physics2D.Raycast(origin.position, direction, flashLightDistance, LayerMask.GetMask("Enemy"));
        if (hit.collider != null) {
            hit.collider.gameObject.GetComponentInParent<Enemy>().SetUnderLight();
            enemy = hit.collider.gameObject;
        }
    }

    private void TurnOffFlashlight() {
        flashlightDown.SetActive(false);
        flashlightUp.SetActive(false);
        flashlightLeft.SetActive(false);
        flashlightRight.SetActive(false);
        if(enemy != null)
            enemy.GetComponentInParent<Enemy>().NotUnderLight();
        enemy = null;
        flashLightOn = false;
    }

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

        if (Input.GetKeyDown(KeyCode.Space) && !turnOnLightPressed) {
            turnOnLightPressed = true;
            if (flashLightOn)
            {
                TurnOffFlashlight();
                lantOn = false;
            }
            else
            {
                TurnOnFlashlight();
                lantOn = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
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
