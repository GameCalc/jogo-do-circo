using UnityEngine;

public class Player : MonoBehaviour {
    public static Player instance = null;
    [SerializeField]
    private GameObject ballInstance;
    [SerializeField]
    float speed = 1;
    Animator animator;
    Vector2 direction;
    Vector2 lookingAt = Vector2.zero;
    Flashlight flashlight;
    [HideInInspector]
    public bool canMove = true;
    public bool carryingObject = false;
    public bool objectNearby = false;
    private bool pressedThrow = false;
    private GameObject ball;
    private GameObject bolinha;
    private bool bolinhaBoss = false;
    private Sprite spriteBall;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
        flashlight = this.GetComponentInChildren<Flashlight>();
    }

    // Update is called once per frame
    void Update () {
        if (canMove)
        {
            Move();
            GetInput();
        }
	}

    public void Move() {
        transform.Translate(direction * speed * Time.deltaTime);

        if (direction.x != 0 || direction.y != 0)
            AnimateMovement(direction);
        else
            animator.SetLayerWeight(1, 0);
    }

    void GetInput () {
        direction = Vector2.zero;

        if (Input.GetAxis("Vertical") == 1) {
            direction = Vector2.up;
            lookingAt = Vector2.up;
            flashlight.RotateLight(direction);
        }
        else if (Input.GetAxis("Horizontal") == -1) {
            direction = Vector2.left;
            lookingAt = Vector2.left;
            flashlight.RotateLight(direction);
        }
        else if (Input.GetAxis("Vertical") == -1) {
            direction = Vector2.down;
            lookingAt = Vector2.down;
            flashlight.RotateLight(direction);
        }
        else if (Input.GetAxis("Horizontal") == 1) {
            direction = Vector2.right;
            lookingAt = Vector2.right;
            flashlight.RotateLight(direction);
        }
        else if (Input.GetAxis("Interact") == 1 && objectNearby && !carryingObject && !pressedThrow) {
            spriteBall = ball.GetComponent<SpriteRenderer>().sprite;
            if(ball.transform.name.Contains("Ball"))
                bolinhaBoss = true;
            else
                bolinhaBoss = false;
            Destroy(ball);
            ball = null;
            carryingObject = true;
            pressedThrow = true;
        }
        else if(carryingObject && Input.GetAxis("Interact") == 1 && !pressedThrow) {
            bolinha = Instantiate(ballInstance);
            bolinha.transform.position = new Vector2(transform.position.x + lookingAt.x, transform.position.y + lookingAt.y);
            bolinha.GetComponent<SpriteRenderer>().sprite = spriteBall;
            bolinha.GetComponent<Rigidbody2D>().AddForce(lookingAt * 10f, ForceMode2D.Impulse);
            carryingObject = false;
            pressedThrow = true;

        }
        else if(Input.GetAxis("Interact") == 0)
            pressedThrow = false;
    }

    void DestruirBolinha(){
        if(bolinha != null)
            Destroy(bolinha);
    }
    public void AnimateMovement(Vector2 direction) {
        animator.SetLayerWeight(1, 1);

        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }

    private void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("EnemyObject")){
            ball = col.gameObject;
            objectNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.CompareTag("EnemyObject")){
            ball = null;
            objectNearby = false;
        }
    }
}
