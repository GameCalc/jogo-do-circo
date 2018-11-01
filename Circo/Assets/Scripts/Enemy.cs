using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private int timeToShadowBack = 10;
    [SerializeField]
    private int timeToFadeShadow = 3;

    private SpriteRenderer sr;
    private GameObject player;
    private bool canFollowPlayer;
    private bool underLight = false;
    private bool firstTimeLighting = false;
    private bool canMove = true;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	void FixedUpdate () {
        if(canFollowPlayer && !underLight && canMove)
          transform.position = Vector2.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void SetUnderLight() {
        underLight = true;
        if (!firstTimeLighting) {
            firstTimeLighting = true;
            Invoke("VerifyStatus", timeToFadeShadow);
        }
    }

    private void VerifyStatus() {
        if (underLight) {
            canMove = false;
            sr.color = Color.white;
            Invoke("TurnShadowOn", timeToShadowBack);
        }
        firstTimeLighting = false;
    }

    private void TurnShadowOn() {
        sr.color = Color.black;
        canMove = true;
    }

    public void NotUnderLight() {
        underLight = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            canFollowPlayer = true;
            player = collision.gameObject;
        }else if(collision.gameObject.tag == "Light") {
            canFollowPlayer = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            canFollowPlayer = false;
        } else if (collision.gameObject.tag == "Light") {
            canFollowPlayer = true;
        }
    }
}
