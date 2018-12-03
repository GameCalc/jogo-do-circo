using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    float speed = 1;
    Animator animator;
    Vector2 direction;
    Flashlight flashlight;
    [HideInInspector]
    public bool canMove = true;

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
            flashlight.RotateLight(direction);
        }
        else if (Input.GetAxis("Horizontal") == -1) {
            direction = Vector2.left;
            flashlight.RotateLight(direction);
        }
        else if (Input.GetAxis("Vertical") == -1) {
            direction = Vector2.down;
            flashlight.RotateLight(direction);
        }
        else if (Input.GetAxis("Horizontal") == 1) {
            direction = Vector2.right;
            flashlight.RotateLight(direction);
        }

    }

    public void AnimateMovement(Vector2 direction) {
        animator.SetLayerWeight(1, 1);

        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }
}
