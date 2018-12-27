using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallProjectile : MonoBehaviour {
	bool inRange;

	[HideInInspector]
	public Vector2 throwDirection;
	[HideInInspector]
	public bool thrown = false;
	bool interactable = false;
	public float speed = 5;
	public Sprite[] objectSprites = new Sprite[6];

	// Update is called once per frame
	void Update () {
		if (thrown) 
			transform.Translate (throwDirection * Time.deltaTime * speed);
	}

	void OnTriggerEnter2D (Collider2D other)
    {
		if (thrown) {
			if (other.tag == "Player") {
				Debug.Log ("ACERTOU!");
				Destroy (gameObject);
			} else {
				GetComponent<SpriteRenderer>().sprite = objectSprites [Random.Range (0, 5)];
				GetComponent<CircleCollider2D> ().radius = 1;
				thrown = false;
				interactable = true;
			}	
		}			
    }

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
			inRange = false;
	}
}
