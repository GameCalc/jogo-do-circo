using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    float speed = 1;
    Animator animator;
    Vector2 direction;

    // Use this for initialization
    void Start () {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        Move();
        GetInput();
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

        if (Input.GetKey(KeyCode.W)) {
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A)) {
            direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S)) {
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D)) {
            direction += Vector2.right;
        }
    }

    public void AnimateMovement(Vector2 direction) {
        animator.SetLayerWeight(1, 1);

        animator.SetFloat("x", direction.x);
        animator.SetFloat("y", direction.y);
    }
}
