using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugglerBoss : MonoBehaviour {
	public float speed, spawnTime, throwTime, yBallOffset = 1;
	public GameObject ballPrefab;
	public Transform[] spawnPoints;

    Animator animator;
	SpriteRenderer spriteRenderer;
	Vector2 moveVector;
	int moveDirection = 3;
	int ballAmount = 5;
	float timeCounter = 0;
	float spawnCounter = 0;
	float throwCounter = 0;
	bool spawnScheduled = false;

    void Start () {
        animator = this.GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer> ();

		foreach (Transform t in spawnPoints) {
			GameObject newBall = Instantiate (ballPrefab, t.position, t.rotation);
			newBall.transform.parent = t;
		}			
    }
	
	// Update is called once per frame
	void Update () {
		MoveBoss ();
		MoveBalls();

		throwCounter += Time.deltaTime;

		if (throwCounter >= throwTime && ballAmount > 0 && spriteRenderer.isVisible)
			ThrowBall();
		
        SpawnBall();
    }

	void MoveBoss () {
		// Tem 10% de chance de trocar de direção;
		ChangeDirection(1);
		transform.Translate(moveVector * Time.deltaTime * speed);
	}

	void ChangeDirection (int percentage) {		
		if (Random.Range (1, 100) <= percentage) {
			moveDirection += Random.Range (1, 3);
			moveDirection -= moveDirection > 4 ? 4 : 0;

			switch (moveDirection) {
				case 1:
					moveVector = Vector2.up;
					break;

				case 2:
					moveVector = Vector2.right;
					break;

				case 3:
					moveVector = Vector2.down;
					break;

				case 4:
					moveVector = Vector2.left;
					break;
			}
		}

		animator.SetInteger ("Direction", moveDirection);
	}

	// Faz o movimento circular das bolas ao redor do chefão
    void MoveBalls()
    {
		timeCounter += Time.deltaTime * 2;

		for (int i = 0; i < 5; i++)
			spawnPoints[i].localPosition = new Vector3 (Mathf.Cos (timeCounter - 1.25f * i), Mathf.Sin (timeCounter - 1.25f * i) / 2 + yBallOffset, 0);
    }

    void SpawnBall()
    {
		if (ballAmount < 5) {
			if (spawnCounter < spawnTime) {
				spawnCounter += Time.deltaTime;
			} else if (!spawnScheduled) {
				spawnScheduled = true;
			}
		}

		if (spawnScheduled) {
			foreach (Transform t in spawnPoints) {
				if (Mathf.Abs(t.localPosition.x) < 0.2 && t.localPosition.y > yBallOffset && t.childCount == 0) {
					GameObject newBall = Instantiate (ballPrefab, t.position, t.rotation);
					newBall.transform.parent = t;
					ballAmount++;
					spawnScheduled = false;
					spawnCounter = 0;
					break;
				}
			}
		}
    }

	Transform nearestBall;
	float shortestDistance = 0;

	void ThrowBall () {
		foreach (Transform t in spawnPoints) {
			if (t.childCount != 0 && (nearestBall == null || Vector2.Distance(t.position, Player.instance.transform.position) < shortestDistance)){
				nearestBall = t;
				shortestDistance = Vector2.Distance (nearestBall.position, Player.instance.transform.position);
			}
		}
			
		Vector3 throwDirection = Player.instance.transform.position - transform.position;
		throwDirection /= throwDirection.magnitude;
		nearestBall.GetComponentInChildren<BallProjectile> ().throwDirection = throwDirection;

		nearestBall.GetComponentInChildren<BallProjectile> ().thrown = true;
		nearestBall.GetChild(0).transform.parent = null;
		nearestBall = null;
		ballAmount--;
		throwCounter = 0;
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (!collision.transform.name.Contains ("Ball")) {

			Vector2 direction = collision.contacts [0].point - (Vector2)transform.position;

			if (Mathf.Abs (direction.x) >= Mathf.Abs (direction.y)) {
				direction.x = Mathf.Round (direction.x);
				direction.y = 0;
			} else {
				direction.y = Mathf.Round (direction.y);
				direction.x = 0;
			}

			moveVector = -0.5f * direction;

			if (moveVector == Vector2.up)
				moveDirection = 1;
			else if (moveVector == Vector2.right)
				moveDirection = 2;
			else if (moveVector == Vector2.down)
				moveDirection = 3;
			else if (moveVector == Vector2.left)
				moveDirection = 4;

			animator.SetInteger ("Direction", moveDirection);
		}
	}
}
